USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUsers]    Script Date: 5/9/2019 1:24:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kerry Murphy
-- Create date: <05/07/2019>
-- Description:	Gets All the Users that are active
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetUsers]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @active bit=1

    -- Insert statements for procedure here
	SELECT UserID,FirstName,LastName,RoleID_FK as RoleID,tm.TeamID_FK as TeamID from dbo.[User] u
	inner join dbo.[TeamManagement] tm on tm.UserID_FK= u.UserID where tm.Active = @active and u.Active=@active
END
