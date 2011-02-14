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

        EventLog.WriteEntry("Application", "PerformanceCounter started.")
        Console.WriteLine("PerformanceCounter started.")
        While 1 = 1
            For Each setting In ConfigurationManager.AppSettings.AllKeys
                If setting.ToString().Contains("PerfMon") Then
                    Try
                        LogCounters(appSettings.Item(setting.ToString()).Split(";")(0), appSettings.Item(setting.ToString()).Split(";")(1), appSettings.Item(setting.ToString()).Split(";")(2), appSettings.Item(setting.ToString()).Split(";")(3), appSettings.Item(setting.ToString()).Split(";")(4), appSettings.Item(setting.ToString()).Split(";")(5))
                        Thread.Sleep(appSettings.Item("SleepTimeMillisecions"))
                    Catch ex As Exception
                        EventLog.WriteEntry("Application", "Outer call to LogCounters failed.  " & ex.ToString())
                        Console.WriteLine("Outer call to LogCounters failed.  " & ex.ToString())
                    End Try
                End If
            Next
        End While
    End Sub

    Sub LogCounters(ByVal DbConnStr As String, ByVal DbProc As String, ByVal ServerName As String, ByVal CategoryName As String, ByVal CounterName As String, Optional ByVal InstanceFilter As String = "")
        Dim PcCat As New PerformanceCounterCategory(CategoryName, ServerName)
        Dim FilterHit As Boolean
        Dim DbComm As New SqlCommand
        Try
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
                        DbComm.Parameters.Add("@Value", SqlDbType.Int).Value = Pc.NextValue()
                        DbComm.Connection.Open()
                        DbComm.ExecuteNonQuery()
                        DbComm.Connection.Close()
                    End Using
                End If
            Next
        Catch ex As Exception
            EventLog.WriteEntry("Application", "Inner call to LogCounters failed.  " & ex.ToString())
            Console.WriteLine("Inner call to LogCounters failed.  " & ex.ToString())
            Diagnostics.PerformanceCounter.CloseSharedResources() ' This can clear buffer overrun issues
        End Try
    End Sub
End Module
