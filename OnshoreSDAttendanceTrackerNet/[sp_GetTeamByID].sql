USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTeamByID]    Script Date: 5/10/2019 10:28:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 8, 2019
-- Description:	Get Team Info by TeamID
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetTeamByID]
	-- Add the parameters for the stored procedure here
	@TeamID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT Name,
	Comment,
	t.Active,
	t.CreateDate,
	createUser.FirstName +' ' + createUser.LastName CreateUserFull,
	createUser.UserID,
	updateUser.FirstName +' ' + updateUser.LastName UpdateUserFull,
	updateUser.UserID
	FROM Team t
	LEFT JOIN [User] createUser on createUser.UserID=t.CreateUser_FK
	LEFT JOIN [User] updateUser on updateUser.UserID=t.CreateUser_FK
	WHERE t.TeamID=@TeamID

END
