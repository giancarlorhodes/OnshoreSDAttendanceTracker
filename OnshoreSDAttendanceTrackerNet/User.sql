USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 5/6/2019 1:36:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserID] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Active] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CreateUser] [int] NOT NULL,
	[ModifiedDate] [date] NOT NULL,
	[ModifiedUser] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO