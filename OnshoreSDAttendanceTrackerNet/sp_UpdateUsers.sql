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
-- Create date: <05/07/2019>
-- Description:	update user and entries in associated tables
-- =============================================
ALTER PROCEDURE sp_UpdateUser
    @UserId int,
	@ModifiedByUserId int,
    @RoleId int=NULL,
	@OldTeamId int=NULL,
	@NewTeamId int=NULL,
	@Email varchar(100)=NULL,
	@FName varchar(100)=NULL,
	@LName varchar(100)=NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @msg nvarchar(300)
	if coalesce(@Email, @FName,@LName,@NewTeamId,@RoleId,@OldTeamId,0) = 0
	BEGIN
	   set @msg='At least one input parameter must be non-null'
	   raiserror (@msg,15,-1)
    END

	if @userId <= 0 or (Select Active from dbo.[User] where UserID=@UserId) <> 1
	BEGIN
	   set @msg='user id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

	if 	@ModifiedByUserId <= 0 or (Select Active from dbo.[User] where UserID=@ModifiedByUserId) <> 1
	BEGIN
	   set @msg='user id doing the modifying must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END
	/*Declare @cuFK int=0
	Declare @muFK int=0*/
    Declare @active bit=1
	Declare @validRowCount tinyint=6
	Declare @rowCount tinyint

	BEGIN TRANSACTION 
	BEGIN TRY
	    update dbo.[User] 
		set Email=@Email
		where @Email <> NULL and UserId=@UserId
		set @rowCount=@@ROWCOUNT

		update dbo.[User]
		set FirstName=@FName
		where @FName <> NULL and UserId=@UserId
		set @rowCount=@rowCount+@@ROWCOUNT

		update dbo.[User]
		set LastName=@LName
		where @LName <> NULL and UserId=@UserId
		set @rowCount=@rowCount+@@ROWCOUNT

		update dbo.[User]
		set RoleID_FK=@RoleId
		where UserID=@UserId
		set @rowCount=@rowCount+@@ROWCOUNT

		update dbo.[User]
		set ModifiedDate=GetDate(),ModifiedUser_FK=@ModifiedByUserId
		where UserID=@userId
		set @rowCount=@rowCount+@@ROWCOUNT

		update dbo.[TeamManagement]
		set TeamID_FK=@NewTeamId,ModifiedDate=GetDate()
		where UserID_FK=@UserId and TeamID_FK=@OldTeamId
		set @rowCount=@rowCount+@@ROWCOUNT

		if @rowCount <@validRowCount
		BEGIN
		    set @msg='number of successful updates is '+@rowCount+' but should be '+@validRowCount
	 	   raiserror (@msg,15,-1)
	    END

    END TRY
	BEGIN CATCH
	    set @msg=ERROR_MESSAGE()
	    raiserror (@msg,15,-1)		  
        ROLLBACK TRANSACTION	   
	END CATCH
	COMMIT TRANSACTION

END
GO
