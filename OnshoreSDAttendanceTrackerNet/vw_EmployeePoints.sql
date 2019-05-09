USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  View [dbo].[vw_EmployeePoints]    Script Date: 5/9/2019 10:21:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vw_EmployeePoints]
AS
SELECT	vttmu.TeamName, 
		vttmu.FirstName, 
		vttmu.LastName, 
		abt.[Name] AS AbsenceType, 
		abt.Point, pb.CreateDate  
FROM [dbo].[vw_TeamTeamManagementUser] vttmu
	INNER JOIN [dbo].[PointBank] pb ON pb.TeamManagementID_FK = vttmu.TeamManagementID
	INNER JOIN [dbo].[AbsenceType]  abt ON abt.AbsenceTypeID = pb.AbsenceTypeID_FK


GO