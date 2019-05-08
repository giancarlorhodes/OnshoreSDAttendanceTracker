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
-- Create date: <05/08/2019>
-- Description:	Remove user by id
-- =============================================
ALTER PROCEDURE sp_RemoveUserById
	-- Add the parameters for the stored procedure here
	@UserId int,
	@ModifiedByUserId int
AS
BEGIN
    Declare @non_active bit=0
	Declare @msg varchar(100)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @ModifiedByUserId <= 0 or (Select Active from dbo.[User] where UserID=@ModifiedByUserId) <> 1
	BEGIN
	   set @msg='modified by user id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END
    update dbo.[User]
	set Active=0,ModifiedDate=GetDate(),ModifiedUser_FK=@ModifiedByUserId
	where UserID=@UserId

	if @@ROWCOUNT = 0
	BEGIN
	   set @msg='User '+@UserId+' was not inactivated'
	   raiserror (@msg,15,-1)
    END

END
GO
