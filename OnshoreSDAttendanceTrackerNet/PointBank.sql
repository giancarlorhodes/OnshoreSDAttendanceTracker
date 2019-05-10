USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  Table [dbo].[PointBank]    Script Date: 5/9/2019 11:08:50 AM ******/
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


