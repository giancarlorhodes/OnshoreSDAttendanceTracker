USE [OnshoreSDAttendanceTracker]
GO

INSERT INTO [dbo].[Role]
           ([RoleID]
           ,[RoleNameShort]
           ,[RoleNameLong]
           ,[Comment]
           ,[CreateDate]
           ,[CreateUser_FK]
           ,[ModifiedDate]
           ,[ModifiedUser_FK])
     VALUES
			(1,	
			'ADMIN',	
			'Administrator',	
			'Administrtor get everything from other roles plus their specific functionality',	
			'2019-05-06',
			0,
			'2019-05-06',
			0)
GO


INSERT INTO [dbo].[Role]
           ([RoleID]
           ,[RoleNameShort]
           ,[RoleNameLong]
           ,[Comment]
           ,[CreateDate]
           ,[CreateUser_FK]
           ,[ModifiedDate]
           ,[ModifiedUser_FK])
     VALUES
			(2,	
			'SM',	
			'Service Manager',	
			'Can see and get multiple teams',	
			'2019-05-06',
			0,
			'2019-05-06',
			0)
GO


INSERT INTO [dbo].[Role]
           ([RoleID]
           ,[RoleNameShort]
           ,[RoleNameLong]
           ,[Comment]
           ,[CreateDate]
           ,[CreateUser_FK]
           ,[ModifiedDate]
           ,[ModifiedUser_FK])
     VALUES
			(3,	
			'TL',	
			'Team Lead',	
			'only get functionality to their team',	
			'2019-05-06',
			0,
			'2019-05-06',
			0)
GO


INSERT INTO [dbo].[Role]
           ([RoleID]
           ,[RoleNameShort]
           ,[RoleNameLong]
           ,[Comment]
           ,[CreateDate]
           ,[CreateUser_FK]
           ,[ModifiedDate]
           ,[ModifiedUser_FK])
     VALUES
			(4,	
			'SDE',	
			'Service Desk Employee',	
			'this in the role that data in collected against',	
			'2019-05-06',
			0,
			'2019-05-06',
			0)
GO


