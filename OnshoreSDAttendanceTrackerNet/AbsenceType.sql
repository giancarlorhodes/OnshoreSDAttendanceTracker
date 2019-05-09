USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[AbsenceType]    Script Date: 5/9/2019 10:05:20 AM ******/
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

ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_CreateUser]  DEFAULT ((0)) FOR [CreateUser_FK]
GO

ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_ModfiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

ALTER TABLE [dbo].[AbsenceType] ADD  CONSTRAINT [DF_AbsenceType_ModifiedUser]  DEFAULT ((0)) FOR [ModifiedUser_FK]
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


