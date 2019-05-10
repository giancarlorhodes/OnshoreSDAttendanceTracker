USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  View [dbo].[vw_EmployeePointsSummary]    Script Date: 5/10/2019 11:29:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[vw_EmployeePointsSummary]
AS
SELECT	
			vttmu.TeamName, 
			vttmu.FirstName, 
			vttmu.LastName,
			SUM(abt.point) AS PointSum 
FROM [dbo].[vw_TeamTeamManagementUser] vttmu
	INNER JOIN [dbo].[PointBank] pb ON pb.TeamManagementID_FK = vttmu.TeamManagementID
	INNER JOIN [dbo].[AbsenceType]  abt ON abt.AbsenceTypeID = pb.AbsenceTypeID_FK
	WHERE pb.AbsenceDate >= dateadd(month, -6, getdate())
GROUP BY	vttmu.TeamName, 
			vttmu.FirstName, 
			vttmu.LastName


GO

