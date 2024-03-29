USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUsers]    Script Date: 5/8/2019 4:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kerry Murphy
-- Create date: <05/09/2019>
-- Description:	Gets All the teams for a user
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetTeamsByUserId]
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @active BIT = 1
DECLARE @msg VARCHAR(100)

IF ISNULL(@UserId, 0) <= 0
	OR (
		SELECT Active
		FROM dbo.[User]
		WHERE UserID = @UserId
		) <> @active
BEGIN
	SET @msg = 'team id must be greater than 0 and active'

	RAISERROR ( @msg ,15 ,- 1 )
END

SELECT tm.TeamID_FK AS TeamID
	,t.Name
	,t.Comment
FROM dbo.[TeamManagement] tm
INNER JOIN dbo.[Team] t ON tm.TeamID_FK = t.TeamID
WHERE t.Active = @active
	AND tm.Active = @active
	AND tm.UserID_FK = @UserId

END
