USE [master]
GO
/****** Object:  Database [OnshoreSDAttendanceTracker]    Script Date: 5/9/2019 11:26:17 AM ******/
CREATE DATABASE [OnshoreSDAttendanceTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnshoreSDAttendanceTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\OnshoreSDAttendanceTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnshoreSDAttendanceTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\OnshoreSDAttendanceTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnshoreSDAttendanceTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET  MULTI_USER 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET QUERY_STORE = OFF
GO
USE [OnshoreSDAttendanceTracker]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[RoleID_FK] [int] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Active] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser_FK] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser_FK] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleNameShort] [varchar](100) NOT NULL,
	[RoleNameLong] [varchar](100) NOT NULL,
	[Comment] [varchar](max) NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser_FK] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser_FK] [int] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Team]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[TeamID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Comment] [varchar](max) NULL,
	[Active] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser_FK] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser_FK] [int] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeamManagement]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamManagement](
	[TeamManagementID] [int] IDENTITY(1,1) NOT NULL,
	[UserID_FK] [int] NOT NULL,
	[TeamID_FK] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser] [int] NOT NULL,
 CONSTRAINT [PK_TeamManagement] PRIMARY KEY CLUSTERED 
(
	[TeamManagementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_TeamTeamManagementUser]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_TeamTeamManagementUser]
AS
SELECT   
    tm.TeamManagementID,
	tm.Active AS TeamManagementActive,    
	t.TeamID, 
	t.[Name] As TeamName, 
	u.UserID, 
	u.FirstName, 
	u.LastName,
	r.RoleID,
	r.RoleNameShort,
	r.RoleNameLong
	
FROM 
	Team t INNER JOIN TeamManagement tm ON t.TeamID = tm.TeamID_FK 
	INNER JOIN [User] u ON tm.UserID_FK = u.UserID
	INNER JOIN [Role] r on r.RoleID = u.RoleID_FK

GO
/****** Object:  Table [dbo].[AbsenceType]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AbsenceType](
	[AbsenceTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Point] [decimal](18, 2) NOT NULL,
	[Active] [int] NOT NULL,
	[TeamID_FK] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser_FK] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser_FK] [int] NOT NULL,
 CONSTRAINT [PK_AbsenceType] PRIMARY KEY CLUSTERED 
(
	[AbsenceTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PointBank]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointBank](
	[PointBankID] [bigint] IDENTITY(1,1) NOT NULL,
	[AbsenceTypeID_FK] [int] NOT NULL,
	[TeamManagementID_FK] [int] NOT NULL,
	[Active] [int] NOT NULL,
	[Comment] [varchar](max) NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser] [int] NOT NULL,
 CONSTRAINT [PK_PointBank] PRIMARY KEY CLUSTERED 
(
	[PointBankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_EmployeePoints]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vw_EmployeePoints]
AS
SELECT	vttmu.TeamName, 
		vttmu.FirstName, 
		vttmu.LastName, 
		abt.[Name] AS AbsenceType, 
		abt.Point, pb.CreateDate  
FROM [dbo].[vw_TeamTeamManagementUser] vttmu
	INNER JOIN [dbo].[PointBank] pb ON pb.TeamManagementID_FK = vttmu.TeamManagementID
	INNER JOIN [dbo].[AbsenceType]  abt ON abt.AbsenceTypeID = pb.AbsenceTypeID_FK


GO
/****** Object:  Table [dbo].[ExceptionLogging]    Script Date: 5/9/2019 11:26:17 AM ******/
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
/****** Object:  Table [dbo].[NavigationMenu]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavigationMenu](
	[NavigationMenuID] [int] NOT NULL,
	[MenuItem] [varchar](100) NOT NULL,
	[Url] [varchar](100) NOT NULL,
	[RoleID_FK] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser] [int] NOT NULL,
 CONSTRAINT [PK_NavigationMenu] PRIMARY KEY CLUSTERED 
(
	[NavigationMenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserCredentials]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCredentials](
	[UserCredentialsID] [int] NOT NULL,
	[UserPassword] [varchar](100) NOT NULL,
	[UserID_FK] [int] NOT NULL,
	[Salt] [varchar](100) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser] [int] NOT NULL,
 CONSTRAINT [PK_UserCredentials] PRIMARY KEY CLUSTERED 
(
	[UserCredentialsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[AbsenceType] ON 

INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (2, N'Call In', CAST(1.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (3, N'Late', CAST(0.25 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (4, N'Partial Unplanned Absence', CAST(0.50 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (5, N'Earned Back .25', CAST(-0.25 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (6, N'Earned Back .5', CAST(-0.50 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (7, N'Working from Home', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (8, N'Medical Appointment', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (9, N'Partial Planned Absence', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (10, N'Bereavement', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (11, N'No Call', CAST(7.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (12, N'Weather', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (13, N'Jury Cuty', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (14, N'FMLA', CAST(0.00 AS Decimal(18, 2)), 1, 5, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (16, N'Call In', CAST(1.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (17, N'Late', CAST(0.25 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (18, N'Partial Unplanned Absence', CAST(0.50 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (19, N'Earned Back .25', CAST(-0.25 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (20, N'Earned Back .5', CAST(-0.50 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (21, N'Working from Home', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (22, N'Medical Appointment', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (23, N'Partial Planned Absence', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (24, N'Bereavement', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (25, N'No Call', CAST(7.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (26, N'Weather', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (27, N'Jury Cuty', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (28, N'FMLA', CAST(0.00 AS Decimal(18, 2)), 1, 6, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (29, N'Call In', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (30, N'Late', CAST(0.25 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (31, N'Partial Unplanned Absence', CAST(0.50 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (32, N'Earned Back .25', CAST(-0.25 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (33, N'Earned Back .5', CAST(-0.50 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (34, N'Working from Home', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (35, N'Medical Appointment', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (36, N'Partial Planned Absence', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (37, N'Bereavement', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (38, N'No Call', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (39, N'Weather', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (40, N'Jury Cuty', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
INSERT [dbo].[AbsenceType] ([AbsenceTypeID], [Name], [Point], [Active], [TeamID_FK], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (41, N'FMLA', CAST(0.00 AS Decimal(18, 2)), 1, 7, CAST(N'2019-05-09' AS Date), 0, CAST(N'2019-05-09' AS Date), 0)
SET IDENTITY_INSERT [dbo].[AbsenceType] OFF
SET IDENTITY_INSERT [dbo].[PointBank] ON 

INSERT [dbo].[PointBank] ([PointBankID], [AbsenceTypeID_FK], [TeamManagementID_FK], [Active], [Comment], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (1, 2, 7, 1, N'Anessa 
Lowery with Schneider', CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[PointBank] ([PointBankID], [AbsenceTypeID_FK], [TeamManagementID_FK], [Active], [Comment], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (2, 2, 7, 1, N'call in second time this week', CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[PointBank] ([PointBankID], [AbsenceTypeID_FK], [TeamManagementID_FK], [Active], [Comment], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (3, 3, 7, 1, NULL, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
SET IDENTITY_INSERT [dbo].[PointBank] OFF
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (1, N'ADMIN', N'Administrator', N'Administrtor get everything from other roles plus their specific functionality', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (2, N'SM', N'Service Manager', N'Can see and get multiple teams', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (3, N'TL', N'Team Lead', N'only get functionality to their team', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (4, N'SDE', N'Service Desk Employee', N'this in the role that data in collected against', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Team] ON 

INSERT [dbo].[Team] ([TeamID], [Name], [Comment], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (5, N'Schneider', NULL, 1, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Team] ([TeamID], [Name], [Comment], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (6, N'City of Hope', NULL, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[Team] ([TeamID], [Name], [Comment], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (7, N'Mallinckrodt', NULL, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Team] OFF
SET IDENTITY_INSERT [dbo].[TeamManagement] ON 

INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (1, 0, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (2, 0, 6, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (3, 0, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (4, 1, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (5, 1, 6, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (6, 1, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (7, 4, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (8, 5, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (9, 6, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (11, 7, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (12, 7, 6, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (13, 7, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (14, 8, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (15, 8, 6, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (16, 8, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (17, 9, 6, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (18, 10, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (19, 10, 6, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (20, 11, 5, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (21, 11, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (22, 13, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[TeamManagement] ([TeamManagementID], [UserID_FK], [TeamID_FK], [Active], [CreateDate], [CreateUser], [ModifiedDate], [ModifiedUser]) VALUES (23, 14, 7, 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
SET IDENTITY_INSERT [dbo].[TeamManagement] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (0, N'system', N'system', 1, N'giancarlo.rhodes@onshoreoutsourcing.com', 1, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (1, N'Admin', N'Admin', 1, N'administrator.test@onshoreoutsourcing.com', 1, CAST(N'2019-05-07' AS Date), 0, CAST(N'2019-05-07' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (4, N'Anessa 
', N'Lowery', 4, N'Anessa.Lowery@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (5, N'Cassandra', N'Lester
', 4, N'cassandra.lester
@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (6, N'Giancarlo', N'Rhodes', 3, N'giancarlo.rhodes@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (7, N'Josh', N'James', 1, N'josh.james@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (8, N'Aleris', N'Roman', 1, N'aleris.roman@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (9, N'Brittani', N'Johnson', 3, N'brittani.johnson@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (10, N'Jeremiah', N'Carpenter', 2, N'jeremiah.carpenter@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (11, N'Kerry', N'Murphy', 2, N'kerry.murphy@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (13, N'Josiah', N'Kaser', 4, N'josiah.kaser
@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
INSERT [dbo].[User] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (14, N'Tara', N'Ulrick', 4, N'tara.ulrick@onshoreoutsourcing.com', 1, CAST(N'2019-05-08' AS Date), 0, CAST(N'2019-05-08' AS Date), 0)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO
ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_ModfiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
GO
ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO
ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser]
GO
ALTER TABLE [dbo].[PointBank] ADD  CONSTRAINT [DF_PointBank_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[PointBank] ADD  CONSTRAINT [DF_PointBank_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[PointBank] ADD  CONSTRAINT [DF_PointBank_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO
ALTER TABLE [dbo].[PointBank] ADD  CONSTRAINT [DF_PointBank_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[PointBank] ADD  CONSTRAINT [DF_PointBank_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
GO
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_CreateUser_FK]  DEFAULT ((0)) FOR [CreateUser_FK]
GO
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_ModifiedUser_FK]  DEFAULT ((0)) FOR [ModifiedUser_FK]
GO
ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO
ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_Users_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
GO
ALTER TABLE [dbo].[AbsenceType]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceType_CreateUser] FOREIGN KEY([CreateUser_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[AbsenceType] CHECK CONSTRAINT [FK_AbsenceType_CreateUser]
GO
ALTER TABLE [dbo].[AbsenceType]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceType_ModifiedUser] FOREIGN KEY([ModifiedUser_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[AbsenceType] CHECK CONSTRAINT [FK_AbsenceType_ModifiedUser]
GO
ALTER TABLE [dbo].[AbsenceType]  WITH CHECK ADD  CONSTRAINT [FK_AbsenceType_Team] FOREIGN KEY([TeamID_FK])
REFERENCES [dbo].[Team] ([TeamID])
GO
ALTER TABLE [dbo].[AbsenceType] CHECK CONSTRAINT [FK_AbsenceType_Team]
GO
ALTER TABLE [dbo].[NavigationMenu]  WITH CHECK ADD  CONSTRAINT [FK_NavigationMenu_CreateUser] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[NavigationMenu] CHECK CONSTRAINT [FK_NavigationMenu_CreateUser]
GO
ALTER TABLE [dbo].[NavigationMenu]  WITH CHECK ADD  CONSTRAINT [FK_NavigationMenu_ModifiedUser] FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[NavigationMenu] CHECK CONSTRAINT [FK_NavigationMenu_ModifiedUser]
GO
ALTER TABLE [dbo].[NavigationMenu]  WITH CHECK ADD  CONSTRAINT [FK_NavigationMenu_Role] FOREIGN KEY([RoleID_FK])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[NavigationMenu] CHECK CONSTRAINT [FK_NavigationMenu_Role]
GO
ALTER TABLE [dbo].[PointBank]  WITH CHECK ADD  CONSTRAINT [FK_PointBank_AbsenceType] FOREIGN KEY([AbsenceTypeID_FK])
REFERENCES [dbo].[AbsenceType] ([AbsenceTypeID])
GO
ALTER TABLE [dbo].[PointBank] CHECK CONSTRAINT [FK_PointBank_AbsenceType]
GO
ALTER TABLE [dbo].[PointBank]  WITH CHECK ADD  CONSTRAINT [FK_PointBank_CreateUser] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PointBank] CHECK CONSTRAINT [FK_PointBank_CreateUser]
GO
ALTER TABLE [dbo].[PointBank]  WITH CHECK ADD  CONSTRAINT [FK_PointBank_ModifiedUser] FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PointBank] CHECK CONSTRAINT [FK_PointBank_ModifiedUser]
GO
ALTER TABLE [dbo].[PointBank]  WITH CHECK ADD  CONSTRAINT [FK_PointBank_TeamManagement] FOREIGN KEY([TeamManagementID_FK])
REFERENCES [dbo].[TeamManagement] ([TeamManagementID])
GO
ALTER TABLE [dbo].[PointBank] CHECK CONSTRAINT [FK_PointBank_TeamManagement]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_CreateUser] FOREIGN KEY([CreateUser_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_CreateUser]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_ModifiedUser] FOREIGN KEY([ModifiedUser_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_ModifiedUser]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_CreateUser] FOREIGN KEY([CreateUser_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_CreateUser]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_ModifiedUser] FOREIGN KEY([ModifiedUser_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_ModifiedUser]
GO
ALTER TABLE [dbo].[TeamManagement]  WITH CHECK ADD  CONSTRAINT [FK_TeamManagement_CreateUser] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[TeamManagement] CHECK CONSTRAINT [FK_TeamManagement_CreateUser]
GO
ALTER TABLE [dbo].[TeamManagement]  WITH CHECK ADD  CONSTRAINT [FK_TeamManagement_ModifierUser] FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[TeamManagement] CHECK CONSTRAINT [FK_TeamManagement_ModifierUser]
GO
ALTER TABLE [dbo].[TeamManagement]  WITH CHECK ADD  CONSTRAINT [FK_TeamManagement_Team] FOREIGN KEY([TeamID_FK])
REFERENCES [dbo].[Team] ([TeamID])
GO
ALTER TABLE [dbo].[TeamManagement] CHECK CONSTRAINT [FK_TeamManagement_Team]
GO
ALTER TABLE [dbo].[TeamManagement]  WITH CHECK ADD  CONSTRAINT [FK_TeamManagement_User] FOREIGN KEY([UserID_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[TeamManagement] CHECK CONSTRAINT [FK_TeamManagement_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID_FK])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[UserCredentials]  WITH CHECK ADD  CONSTRAINT [FK_UserCredentials_CreateUser] FOREIGN KEY([CreateUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserCredentials] CHECK CONSTRAINT [FK_UserCredentials_CreateUser]
GO
ALTER TABLE [dbo].[UserCredentials]  WITH CHECK ADD  CONSTRAINT [FK_UserCredentials_ModifiedUser] FOREIGN KEY([ModifiedUser])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserCredentials] CHECK CONSTRAINT [FK_UserCredentials_ModifiedUser]
GO
ALTER TABLE [dbo].[UserCredentials]  WITH CHECK ADD  CONSTRAINT [FK_UserCredentials_User] FOREIGN KEY([UserID_FK])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserCredentials] CHECK CONSTRAINT [FK_UserCredentials_User]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoggedErrors]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetLoggedErrors]
	-- Add the parameters for the stored procedure here
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ExceptionMsg,ExceptionType,ExceptionSource,ExceptionURL,Logdate
	FROM ExceptionLogging
	WHERE Logdate>DATEADD(MONTH,-6,GETDATE()) --Grab last 6 months of errors

END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetPointsByID]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetPointsByID]
	-- Add the parameters for the stored procedure here
	@PointBankID INT
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	 

	SELECT 
	AbsenceTypeID_FK 
	,TeamManagementID_FK 
	,p.Active 
	,createUser.FirstName +' ' + createUser.LastName CreateUserFull,
	createUser.UserID,
	updateUser.FirstName +' ' + updateUser.LastName UpdateUserFull,
	updateUser.UserID
	FROM PointBank p
	LEFT JOIN [User] createUser on createUser.UserID=P.CreateUser
	LEFT JOIN [User] updateUser on updateUser.UserID=P.CreateUser
	WHERE PointBankID=@PointBankID

	  
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetTeamByID]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_GetTeamByID]
	-- Add the parameters for the stored procedure here
	@TeamID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT Name,
	Comment,
	t.Active,
	t.CreateDate,
	createUser.FirstName +' ' + createUser.LastName CreateUserFull,
	createUser.UserID,
	updateUser.FirstName +' ' + updateUser.LastName UpdateUserFull,
	updateUser.UserID
	FROM Team t
	LEFT JOIN [User] createUser on createUser.UserID=t.CreateUser_FK
	LEFT JOIN [User] updateUser on updateUser.UserID=t.CreateUser_FK
	WHERE t.TeamID=@TeamID

END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetTeams]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_GetTeams]
	-- Add the parameters for the stored procedure here
	@TeamID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT Name,
	Comment,
	t.Active,
	t.CreateDate,
	createUser.FirstName +' ' + createUser.LastName CreateUserFull,
	createUser.UserID,
	updateUser.FirstName +' ' + updateUser.LastName UpdateUserFull,
	updateUser.UserID
	FROM Team t
	LEFT JOIN [User] createUser on createUser.UserID=t.CreateUser_FK
	LEFT JOIN [User] updateUser on updateUser.UserID=t.CreateUser_FK
	 

END

GO
/****** Object:  StoredProcedure [dbo].[sp_LogError]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_LogError] 
	-- Add the parameters for the stored procedure here
	@ErrorMsg NVARCHAR(MAX)
	,@SPName NVARCHAR(MAX)
	,@ExceptionType NVARCHAR(100)=NULL
	,@ExceptionSource NVARCHAR(100)=NULL
	,@ExceptionURL NVARCHAR(100)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO ExceptionLogging(ExceptionMsg,ExceptionType,ExceptionSource,ExceptionURL,Logdate)
		VALUES(@ErrorMsg,ISNULL(@ExceptionType,'SQL'),@SPName,@ExceptionURL,GETDATE())
END

GO
/****** Object:  StoredProcedure [dbo].[sp_PointsAddNew]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_PointsAddNew]
	-- Add the parameters for the stored procedure here
	@AbsenceTypeID INT
	,@TeamManagementID INT
	,@CreateUserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO PointBank (AbsenceTypeID_FK,TeamManagementID_FK,Active,CreateDate,CreateUser,ModifiedDate, ModifiedUser)
		VALUES(@AbsenceTypeID,@TeamManagementID,1,GETDATE(),@CreateUserID,GETDATE(),@CreateUserID)


END

GO
/****** Object:  StoredProcedure [dbo].[sp_PointsUpdate]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_PointsUpdate]
	-- Add the parameters for the stored procedure here
	@PointBankID INT
	,@AbsenceTypeID INT
	,@TeamManagementID INT
	,@UpdatedUserID INT
	,@Active INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @msg NVARCHAR(300)

	BEGIN TRANSACTION 

	BEGIN TRY

			UPDATE p
			SET 
			AbsenceTypeID_FK=@AbsenceTypeID
			,TeamManagementID_FK=@TeamManagementID
			,Active=@Active
			,ModifiedDate= GETDATE()
			,ModifiedUser=@UpdatedUserID
			FROM PointBank p
			WHERE PointBankID=@PointBankID
			 

			IF @@ROWCOUNT = 0
				BEGIN
				   SET @msg='Attendance was not inactivated, please check this is correct record. '
				   RAISERROR (@msg,15,-1)
				END
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg NVARCHAR(MAX)
		SELECT @ErrorMsg='ErrorState='+convert(varchar,ERROR_STATE())
			+ 'ErrorProcedure='+ convert(varchar,ERROR_PROCEDURE())
			+ 'ErrorLine='+convert(varchar,ERROR_LINE())
			+ 'ErrorMessage='+convert(varchar,ERROR_MESSAGE())

		EXEC sp_logError @ErrorMsg, '[sp_PointsUpdate]' 
		
		SET @msg='Something went wrong, please review Logs for [sp_PointsUpdate]'
		RAISERROR (@msg,15,-1)

		ROLLBACK TRANSACTION	
	END CATCH
	
		COMMIT TRANSACTION

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TeamAddNew]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_TeamAddNew]
	-- Add the parameters for the stored procedure here
	@TeamName VARCHAR(100)
	,@Comment VARCHAR(MAX)
	,@CreatedUser INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO Team (Name,Comment,Active,CreateDate,CreateUser_FK,ModifiedDate, ModifiedUser_FK)
		VALUES(@TeamName,@Comment,1,GETDATE(),@CreatedUser,GETDATE(),@CreatedUser)


END

GO
/****** Object:  StoredProcedure [dbo].[sp_TeamDeleteByID]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_TeamDeleteByID]
	-- Add the parameters for the stored procedure here
	@TeamID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	UPDATE t
	set Active=0
	FROM Team t
	WHERE t.TeamID=@TeamID

END


GO
/****** Object:  StoredProcedure [dbo].[sp_TeamUpdate]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_TeamUpdate]
	-- Add the parameters for the stored procedure here
	@TeamID INT
	,@TeamName VARCHAR(100)
	,@Comment VARCHAR(MAX)
	,@Active INT
	,@UpdatedUser INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @msg NVARCHAR(300) 

	BEGIN TRANSACTION 

	BEGIN TRY

			UPDATE t
			SET Name=@TeamName
				,Comment=@Comment
				,Active=@Active
				,ModifiedDate= GETDATE()
				,ModifiedUser_FK=@UpdatedUser
			FROM Team t
			WHERE TeamID=@TeamID

			IF @@ROWCOUNT = 0
				BEGIN
				   SET @msg='Team was not inactivated, please check this is correct record. '
				   RAISERROR (@msg,15,-1)
				END

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg NVARCHAR(MAX)
		SELECT @ErrorMsg='ErrorState='+convert(varchar,ERROR_STATE())
			+ 'ErrorProcedure='+ convert(varchar,ERROR_PROCEDURE())
			+ 'ErrorLine='+convert(varchar,ERROR_LINE())
			+ 'ErrorMessage='+convert(varchar,ERROR_MESSAGE())

		EXEC sp_logError @ErrorMsg, '[sp_TeamUpdate]' 
	 
	 	SET @msg='Something went wrong, please review Logs for [sp_TeamUpdate]'
		RAISERROR (@msg,15,-1)

		ROLLBACK TRANSACTION	
	END CATCH
	
		COMMIT TRANSACTION
END

GO
/****** Object:  StoredProcedure [dbo].[sp_UserCredentialsAddOrEdit]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Giancarlo Rhodes
-- Create date: 5/8/2019
-- Description:	Add or edit credentials in the UserCredential table
-- =============================================
CREATE PROCEDURE [dbo].[sp_UserCredentialsAddOrEdit]
	-- Add the parameters for the stored procedure here
	@parmUserPassword varchar(100), 
	@parmUserID int, 
	@parmSalt varchar(100),
	@parmCreateUser int,
	@parmModifiedUser int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	IF (@parmUserID = 0)
	BEGIN
			-- Insert statements for procedure here
			INSERT INTO [dbo].[UserCredentials]
				   (
						[UserPassword]
					   ,[UserID_FK]
					   ,[Salt]           
					   ,[CreateUser]
					   ,[ModifiedUser]
				   )
			 VALUES
				   (
						@parmUserPassword
					   ,@parmUserID
					   ,@parmSalt
					   ,@parmCreateUser
					   ,@parmModifiedUser 
				   )
			END;
	ELSE
	BEGIN
		
			UPDATE [dbo].[UserCredentials]
			SET [UserPassword] = @parmUserPassword				
				,[Salt] = @parmSalt
				,[ModifiedDate] = GETDATE()
				,[ModifiedUser] = @parmModifiedUser
			WHERE [UserID_FK] = @parmUserID;

	END

END

GO
/****** Object:  StoredProcedure [dbo].[sp_UserCredentialsDelete]    Script Date: 5/9/2019 11:26:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Giancarlo Rhodes
-- Create date: 5/8/2019
-- Description:	Delete credentials in the UserCredential table
-- =============================================
CREATE PROCEDURE [dbo].[sp_UserCredentialsDelete] 
	-- Add the parameters for the stored procedure here
	@parmUserID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[UserCredentials] 
	WHERE [UserID_FK] = @parmUserID;
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Team"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 215
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TeamManagement"
            Begin Extent = 
               Top = 132
               Left = 271
               Bottom = 262
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 16
               Left = 575
               Bottom = 146
               Right = 753
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_TeamTeamManagementUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_TeamTeamManagementUser'
GO
USE [master]
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET  READ_WRITE 
GO
