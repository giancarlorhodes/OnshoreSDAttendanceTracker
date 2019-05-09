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
	Declare @active bit=1
	Declare @msg varchar(100)

    if ISNULL(@UserId,0) <= 0 or (Select Active from dbo.[User] where UserID=@UserId) <> @active
	BEGIN
	   set @msg='team id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

	SELECT tm.TeamID_FK as TeamID,t.Name, t.Comment from dbo.[TeamManagement] tm
	inner join dbo.[Team] t on tm.TeamID_FK=t.TeamID  where t.Active = @active and tm.Active=@active and  tm.UserID_FK=@UserId
END
