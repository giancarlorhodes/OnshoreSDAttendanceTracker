use OnshoreSDAttendanceTracker;
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kerry Murphy
-- Create date: <05/09/2019>
-- Description:	Get user by id
-- =============================================
ALTER PROCEDURE sp_GetUserById
	@UserId int
AS
BEGIN
    Declare @active bit=1
	Declare @msg varchar(100)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if ISNULL(@UserId,0) <= 0 or (Select Active from dbo.[User] where UserID=@UserId) <> @active
	BEGIN
	   set @msg='user id must be greater than 0, non-null and active'
	   raiserror (@msg,15,-1)
    END
    	SELECT UserID,FirstName,LastName,RoleID_FK as RoleID,tm.TeamID_FK as TeamID from dbo.[User] u
	inner join dbo.[TeamManagement] tm on tm.UserID_FK= u.UserID where u.Active = @active and tm.Active=@active and UserID=@UserId
END
GO
