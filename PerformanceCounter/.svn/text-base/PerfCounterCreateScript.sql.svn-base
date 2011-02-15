/****** Object:  Role [PerfCouunter_Logging]    Script Date: 02/13/2011 14:44:22 ******/
CREATE ROLE [PerfCouunter_Logging] AUTHORIZATION [dbo]
GO
/****** Object:  View [dbo].[PerfCounterDw_vw]    Script Date: 02/13/2011 14:44:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[PerfCounterDw_vw] as
select	pc.PerfCounterId , ld.LogDate , sn.ServerName , cnt.CategoryName , ctr.CounterName , ins.InstanceName , pc.Value
from	PerfCounterFact							pc
		inner join PerfCounterDimServerName		sn	on	sn.ServerNameId		=	pc.ServerNameId
		inner join PerfCounterDimCategoryName	cnt	on	cnt.CategoryNameId	=	pc.CategoryNameId
		inner join PerfCounterDimCounterName	ctr	on	ctr.CounterNameId	=	pc.CounterNameId
		inner join PerfCounterDimInstanceName	ins	on	ins.InstanceNameId	=	pc.InstanceNameId
		inner join PerfCounterDimLogDate		ld	on	ld.LogDateId		=	pc.LogDateId
GO
/****** Object:  Table [dbo].[PerfCounterStage]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PerfCounterStage](
	[PerfCounterId] [int] NOT NULL,
	[LogDate] [datetime] NULL,
	[ServerName] [varchar](100) NULL,
	[CategoryName] [varchar](100) NULL,
	[CounterName] [varchar](100) NULL,
	[InstanceName] [varchar](500) NULL,
	[Value] [float] NULL,
 CONSTRAINT [PK_perfcounterstage] PRIMARY KEY CLUSTERED 
(
	[PerfCounterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PerfCounterDimServerName]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PerfCounterDimServerName](
	[ServerNameId] [smallint] IDENTITY(1,1) NOT NULL,
	[ServerName] [varchar](100) NULL,
 CONSTRAINT [PK_CounterDimServerName] PRIMARY KEY CLUSTERED 
(
	[ServerNameId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [PerfCounterDimServerName_ServerName] ON [dbo].[PerfCounterDimServerName] 
(
	[ServerName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfCounterDimCategoryName]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PerfCounterDimCategoryName](
	[CategoryNameId] [smallint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NULL,
 CONSTRAINT [PK_PerfCounterDimCategoryName] PRIMARY KEY CLUSTERED 
(
	[CategoryNameId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [PerfCounterDimCategoryName_CategoryName] ON [dbo].[PerfCounterDimCategoryName] 
(
	[CategoryName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfCounterDimCounterName]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PerfCounterDimCounterName](
	[CounterNameId] [smallint] IDENTITY(1,1) NOT NULL,
	[CounterName] [varchar](100) NULL,
 CONSTRAINT [PK_CounterDimCounterName] PRIMARY KEY CLUSTERED 
(
	[CounterNameId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [PerfCounterDimCounterName_CounterName] ON [dbo].[PerfCounterDimCounterName] 
(
	[CounterName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfCounterDimInstanceName]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PerfCounterDimInstanceName](
	[InstanceNameId] [smallint] IDENTITY(1,1) NOT NULL,
	[InstanceName] [varchar](500) NULL,
 CONSTRAINT [PK_CounterDimInstanceName] PRIMARY KEY CLUSTERED 
(
	[InstanceNameId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [PerfCounterDimInstanceName_InstanceName] ON [dbo].[PerfCounterDimInstanceName] 
(
	[InstanceName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfCounterDimLogDate]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerfCounterDimLogDate](
	[LogDateId] [int] IDENTITY(-2147483648,1) NOT NULL,
	[LogDate] [datetime] NULL,
 CONSTRAINT [PK_PerfCounterDimLogDate] PRIMARY KEY CLUSTERED 
(
	[LogDateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [PerfCounterDimLogDate_LogDate] ON [dbo].[PerfCounterDimLogDate] 
(
	[LogDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfCounterFact]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerfCounterFact](
	[PerfCounterId] [int] NOT NULL,
	[LogDateId] [int] NULL,
	[ServerNameId] [smallint] NULL,
	[CategoryNameId] [smallint] NULL,
	[CounterNameId] [smallint] NULL,
	[InstanceNameId] [smallint] NULL,
	[Value] [float] NULL,
 CONSTRAINT [PK_PerfCounterFact] PRIMARY KEY CLUSTERED 
(
	[PerfCounterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[PerfCounterInsert_sp]    Script Date: 02/13/2011 14:44:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[PerfCounterInsert_sp]
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
GO
/****** Object:  Table [dbo].[PerfCounter]    Script Date: 02/13/2011 14:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PerfCounter](
	[PerfCounterId] [int] IDENTITY(-2147483648,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[ServerName] [varchar](100) NULL,
	[CategoryName] [varchar](100) NULL,
	[CounterName] [varchar](100) NULL,
	[InstanceName] [varchar](500) NULL,
	[Value] [float] NULL,
 CONSTRAINT [PK_PerfCounter] PRIMARY KEY CLUSTERED 
(
	[PerfCounterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[PerfCounterDwImport_sp]    Script Date: 02/13/2011 14:44:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[PerfCounterDwImport_sp]
as
--declare @ImportBookmark datetime
--select	@ImportBookmark = dateadd( mi , -1 , getdate() )

truncate table PerfCounterStage

insert	PerfCounterStage(
		PerfCounterId , LogDate , ServerName , CategoryName , CounterName , InstanceName , Value
)
select	PerfCounterId , LogDate , ServerName , CategoryName , CounterName , InstanceName , Value
from	PerfCounter
--where	LogDate	<	@ImportBookmark

delete	PerfCounter
--where	LogDate	<	@ImportBookmark

insert	PerfCounterDimServerName(
		ServerName
)
select	distinct 
		ServerName
from	PerfCounterStage
where	ServerName not in ( select ServerName from PerfCounterDimServerName )

insert	PerfCounterDimCategoryName(
		CategoryName
)
select	distinct 
		CategoryName
from	PerfCounterStage
where	CategoryName not in ( select CategoryName from PerfCounterDimCategoryName )

insert	PerfCounterDimCounterName(
		CounterName
)
select	distinct 
		CounterName
from	PerfCounterStage
where	CounterName not in ( select CounterName from PerfCounterDimCounterName )

insert	PerfCounterDimInstanceName(
		InstanceName
)
select	distinct 
		InstanceName
from	PerfCounterStage
where	InstanceName not in ( select InstanceName from PerfCounterDimInstanceName )

insert	PerfCounterDimLogDate(
		LogDate
)
select	distinct 
		LogDate
from	PerfCounterStage
where	LogDate not in ( select LogDate from PerfCounterDimLogDate )

insert	PerfCounterFact(
		PerfCounterId , LogDateId , ServerNameId , CategoryNameId , CounterNameId , InstanceNameId , Value
)
select	pc.PerfCounterId , ld.LogDateId , sn.ServerNameId , cnt.CategoryNameId , ctr.CounterNameId , ins.InstanceNameId , pc.Value
from	PerfCounterStage							pc
		inner join PerfCounterDimServerName		sn	on	sn.ServerName		=	pc.ServerName
		inner join PerfCounterDimCategoryName	cnt	on	cnt.CategoryName	=	pc.CategoryName
		inner join PerfCounterDimCounterName	ctr	on	ctr.CounterName		=	pc.CounterName
		inner join PerfCounterDimInstanceName	ins	on	ins.InstanceName	=	pc.InstanceName
		inner join PerfCounterDimLogDate		ld	on	ld.LogDate			=	pc.LogDate
GO
/****** Object:  View [dbo].[PerfCounter_24h_vw]    Script Date: 02/13/2011 14:44:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[PerfCounter_24h_vw]
as
select	*
from	perfcounter (nolock)
where	logdate	>	dateadd( dd , -1 , getdate() )
GO
