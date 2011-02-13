-- drop table PerfCounter
-- drop proc PerfCounterInsert_sp
create table PerfCounter( PerfCounterId int identity(-2147483648,1), LogDate datetime , ServerName varchar(100) , CategoryName varchar(100) , CounterName varchar(100) , InstanceName varchar(500) , Value float )
go
GO
ALTER TABLE dbo.PerfCounter ADD CONSTRAINT
	PK_PerfCounter PRIMARY KEY CLUSTERED 
	(
	PerfCounterId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
-- create index PerfCounter01 on PerfCounter( ServerName , CategoryName , CounterName )
go
create proc PerfCounterInsert_sp
	@ServerName		varchar(100)
,	@CategoryName	varchar(100)
,	@CounterName	varchar(100)
,	@InstanceName	varchar(500)
,	@Value			float
as
insert	PerfCounter( 
		LogDate , ServerName , CategoryName , CounterName , InstanceName , Value 
)
values	(
		getdate() , @ServerName , @CategoryName , @CounterName , @InstanceName , @Value 
)
go
grant execute on PerfCounterInsert_sp to public