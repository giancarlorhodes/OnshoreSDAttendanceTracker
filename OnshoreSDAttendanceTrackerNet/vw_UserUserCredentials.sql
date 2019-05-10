USE [OnshoreSDAttendanceTracker]
GO

/****** Object:  View [dbo].[vw_UserUserCredentials]    Script Date: 5/10/2019 11:30:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vw_UserUserCredentials]
AS
SELECT	u.UserID,
		u.FirstName,
		u.LastName,
		u.Email,
		uc.UserPassword,
		uc.Salt 
FROM [dbo].[User] u 
	LEFT JOIN [dbo].[UserCredentials] uc 
	ON uc.UserID_FK = u.UserID


GO

