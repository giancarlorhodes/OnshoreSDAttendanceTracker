USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 5/7/2019 3:33:38 PM ******/
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

ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO

ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
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


