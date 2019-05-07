USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[UserCredentials]    Script Date: 5/7/2019 4:04:05 PM ******/
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


