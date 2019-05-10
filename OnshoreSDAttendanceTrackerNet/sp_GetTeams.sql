USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTeamByID]    Script Date: 5/9/2019 11:20:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_GetTeams]
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
	 

END
