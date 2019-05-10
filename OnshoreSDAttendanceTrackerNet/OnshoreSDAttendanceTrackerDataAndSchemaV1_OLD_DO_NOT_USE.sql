USE [master]
GO
/****** Object:  Database [OnshoreSDAttendanceTracker]    Script Date: 5/6/2019 2:25:18 PM ******/
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
ALTER DATABASE [OnshoreSDAttendanceTracker] SET AUTO_CLOSE OFF 
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
/****** Object:  Table [dbo].[Role]    Script Date: 5/6/2019 2:25:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleNameShort] [varchar](100) NOT NULL,
	[RoleNameLong] [varchar](100) NOT NULL,
	[Comment] [varchar](max) NULL,
	[CreateDate] [date] NULL,
	[CreateUser_FK] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser_FK] [int] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/6/2019 2:25:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
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
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (1, N'ADMIN', N'Administrator', N'Administrtor get everything from other roles plus their specific functionality', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (2, N'SM', N'Service Manager', N'Can see and get multiple teams', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (3, N'TL', N'Team Lead', N'only get functionality to their team', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
INSERT [dbo].[Role] ([RoleID], [RoleNameShort], [RoleNameLong], [Comment], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (4, N'SDE', N'Service Desk Employee', N'this in the role that data in collected against', CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [RoleID_FK], [Email], [Active], [CreateDate], [CreateUser_FK], [ModifiedDate], [ModifiedUser_FK]) VALUES (0, N'system', N'system', 1, N'giancarlo.rhodes@onshoreoutsourcing.com', 1, CAST(N'2019-05-06' AS Date), 0, CAST(N'2019-05-06' AS Date), 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
GO
USE [master]
GO
ALTER DATABASE [OnshoreSDAttendanceTracker] SET  READ_WRITE 
GO
