USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[ExceptionLogging]    Script Date: 5/9/2019 10:05:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExceptionLogging](
	[Logid] [bigint] IDENTITY(1,1) NOT NULL,
	[ExceptionMsg] [nvarchar](max) NULL,
	[ExceptionType] [nvarchar](100) NULL,
	[ExceptionSource] [nvarchar](max) NULL,
	[ExceptionURL] [nvarchar](500) NULL,
	[Logdate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_ExceptionLoggingToDataBase] PRIMARY KEY CLUSTERED 
(
	[Logid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
