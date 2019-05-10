USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[NavigationMenu]    Script Date: 5/9/2019 10:09:28 AM ******/
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

ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_CreateUser]  DEFAULT ((0)) FOR [CreateUser]
GO

ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

ALTER TABLE [dbo].[NavigationMenu] ADD  CONSTRAINT [DF_NavigationMenu_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser]
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


