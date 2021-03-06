USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUsers]    Script Date: 5/8/2019 4:40:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kerry Murphy
-- Create date: <05/10/2019>
-- Description:	Gets the teams for a team id
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetAttendanceTypeByTeamId]
	@TeamId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @active bit=1
	Declare @msg varchar(100)

    if ISNULL(@TeamId,0) <= 0 or (Select Active from dbo.[AbsenceType] where TeamID_FK=@TeamId) <> @active
	BEGIN
	   set @msg='team id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

	SELECT at.AbsenceTypeID,at.Name, at.Point from dbo.[AbsenceType] at
	where at.Active = @active and at.TeamID_FK=@TeamId
END
