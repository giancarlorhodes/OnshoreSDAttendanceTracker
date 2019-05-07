USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[TeamManagement]    Script Date: 5/7/2019 3:36:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TeamManagement](
	[TeamManagementID] [int] IDENTITY(1,1) NOT NULL,
	[UserID_FK] [int] NOT NULL,
	[TeamID_FK] [int] NOT NULL,
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

ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO

ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

ALTER TABLE [dbo].[TeamManagement] ADD  CONSTRAINT [DF_TeamManagement_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser]
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


