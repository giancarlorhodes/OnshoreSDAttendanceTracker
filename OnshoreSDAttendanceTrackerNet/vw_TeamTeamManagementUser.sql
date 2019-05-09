USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  View [dbo].[vw_TeamTeamManagementUser]    Script Date: 5/9/2019 10:23:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vw_TeamTeamManagementUser]
AS
SELECT   
    tm.TeamManagementID,
	tm.Active AS TeamManagementActive,    
	t.TeamID, 
	t.[Name] As TeamName, 
	u.UserID, 
	u.FirstName, 
	u.LastName,
	r.RoleID,
	r.RoleNameShort,
	r.RoleNameLong
	
FROM 
	Team t INNER JOIN TeamManagement tm ON t.TeamID = tm.TeamID_FK 
	INNER JOIN [User] u ON tm.UserID_FK = u.UserID
	INNER JOIN [Role] r on r.RoleID = u.RoleID_FK

GO