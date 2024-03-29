USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTeams]    Script Date: 5/10/2019 10:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 8,2019
-- Description:	Get All Teams
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetTeams]
	-- Add the parameters for the stored procedure here
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
	Where t.Active=1

END
