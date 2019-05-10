USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[Team]    Script Date: 5/9/2019 10:13:55 AM ******/
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

ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_CreateUser_FK]  DEFAULT ((0)) FOR [CreateUser_FK]
GO

ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

ALTER TABLE [dbo].[Team] ADD  CONSTRAINT [DF_Team_ModifiedUser_FK]  DEFAULT ((0)) FOR [ModifiedUser_FK]
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




