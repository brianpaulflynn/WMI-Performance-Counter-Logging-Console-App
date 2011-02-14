Imports System
Imports System.Diagnostics
Imports System.Threading
Imports System.Collections
Imports System.IO
Imports System.Management
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.Data.SqlClient

Module Module1
    Sub Main()
        Dim appSettings As NameValueCollection = ConfigurationManager.AppSettings
        Dim StartTime As DateTime
        Dim ThreadSleep As Integer
        Dim DbComm As New SqlCommand

        EventLog.WriteEntry("Application", "PerformanceCounter started.")
        Console.WriteLine("PerformanceCounter started.")
        'Grab the time to sleep between polling passes from the app.config.
        If IsNumeric(appSettings.Item("SleepTimeMillisecions")) Then
            ThreadSleep = appSettings.Item("SleepTimeMillisecions")
        Else
            ThreadSleep = 60000
        End If
        While 1 = 1
            StartTime = Now 'Log the start time of this pass for use at the end of the pass to determine how long to wait for the next pass.
            'Loop through counters in app.config and call LogCounters for each one.
            For Each setting In ConfigurationManager.AppSettings.AllKeys
                If setting.ToString().Contains("PerfMon") Then
                    Try
                        LogCounters(appSettings.Item(setting.ToString()).Split(";")(0), appSettings.Item(setting.ToString()).Split(";")(1), appSettings.Item(setting.ToString()).Split(";")(2), appSettings.Item(setting.ToString()).Split(";")(3), appSettings.Item(setting.ToString()).Split(";")(4), appSettings.Item(setting.ToString()).Split(";")(5))
                    Catch ex As Exception
                        EventLog.WriteEntry("Application", "Outer call to LogCounters failed.  " & ex.ToString())
                        Console.WriteLine("Outer call to LogCounters failed.  " & ex.ToString())
                    End Try
                End If
            Next
            'Compress Data into Dw Model
            Try
                Using Dbconn As New SqlConnection(ConfigurationManager.ConnectionStrings("DbRepository").ToString())
                    DbComm.Connection = Dbconn
                    DbComm.CommandType = CommandType.StoredProcedure
                    DbComm.CommandText = ConfigurationManager.AppSettings.Item("DwImportProc")
                    DbComm.Parameters.Clear()
                    DbComm.Connection.Open()
                    DbComm.ExecuteNonQuery()
                    DbComm.Connection.Close()
                End Using
            Catch ex As Exception
                EventLog.WriteEntry("Application", "DwImportProc failed.  " & ex.ToString())
                Console.WriteLine("DwImportProc failed.  " & ex.ToString())
            End Try
            If (ThreadSleep - Now.Subtract(StartTime).TotalMilliseconds) > 0 Then
                Thread.Sleep(ThreadSleep - Now.Subtract(StartTime).TotalMilliseconds)
            End If
        End While
    End Sub

    Sub LogCounters(ByVal DbConnStr As String, ByVal DbProc As String, ByVal ServerName As String, ByVal CategoryName As String, ByVal CounterName As String, Optional ByVal InstanceFilter As String = "")
        Dim PcCat As New PerformanceCounterCategory(CategoryName, ServerName)
        Dim FilterHit As Boolean
        Dim DbComm As New SqlCommand
        Try
            If PcCat.GetInstanceNames.Count = 0 Then 'Call to counter that does not contain instances
                Dim Pc As New Diagnostics.PerformanceCounter(CategoryName, CounterName)
                Pc.MachineName = ServerName
                Using Dbconn As New SqlConnection(ConfigurationManager.ConnectionStrings(DbConnStr).ToString())
                    DbComm.Connection = Dbconn
                    DbComm.CommandType = CommandType.StoredProcedure
                    DbComm.CommandText = DbProc
                    DbComm.Parameters.Clear()
                    DbComm.Parameters.Add("@ServerName", SqlDbType.VarChar, 100).Value = ServerName.ToString()
                    DbComm.Parameters.Add("@CategoryName", SqlDbType.VarChar, 100).Value = CategoryName.ToString()
                    DbComm.Parameters.Add("@InstanceName", SqlDbType.VarChar, 500).Value = ""
                    DbComm.Parameters.Add("@CounterName", SqlDbType.VarChar, 100).Value = CounterName.ToString()
                    DbComm.Parameters.Add("@Value", SqlDbType.Float).Value = Pc.NextValue()
                    DbComm.Connection.Open()
                    DbComm.ExecuteNonQuery()
                    DbComm.Connection.Close()
                End Using
            Else 'Call to counter that contains instances
                For Each Instance In PcCat.GetInstanceNames()
                    FilterHit = False
                    Dim Pc As New Diagnostics.PerformanceCounter(CategoryName, CounterName, Instance, ServerName)
                    If InstanceFilter.Length = 0 Then
                        FilterHit = True
                    ElseIf Instance.ToString().Contains(InstanceFilter) Then
                        FilterHit = True
                    End If
                    If FilterHit Then
                        Using Dbconn As New SqlConnection(ConfigurationManager.ConnectionStrings(DbConnStr).ToString())
                            DbComm.Connection = Dbconn
                            DbComm.CommandType = CommandType.StoredProcedure
                            DbComm.CommandText = DbProc
                            DbComm.Parameters.Clear()
                            DbComm.Parameters.Add("@ServerName", SqlDbType.VarChar, 100).Value = ServerName.ToString()
                            DbComm.Parameters.Add("@CategoryName", SqlDbType.VarChar, 100).Value = CategoryName.ToString()
                            DbComm.Parameters.Add("@InstanceName", SqlDbType.VarChar, 500).Value = Instance.ToString()
                            DbComm.Parameters.Add("@CounterName", SqlDbType.VarChar, 100).Value = CounterName.ToString()
                            DbComm.Parameters.Add("@Value", SqlDbType.Float).Value = Pc.NextValue()
                            DbComm.Connection.Open()
                            DbComm.ExecuteNonQuery()
                            DbComm.Connection.Close()
                        End Using
                    End If
                Next
            End If
        Catch ex As Exception
            EventLog.WriteEntry("Application", "Inner call to LogCounters failed.  " & ex.ToString())
            Console.WriteLine("Inner call to LogCounters failed.  " & ex.ToString())
            Diagnostics.PerformanceCounter.CloseSharedResources() ' This can clear buffer overrun issues
        End Try
    End Sub
End Module
