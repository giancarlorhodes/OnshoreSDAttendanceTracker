USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUser]    Script Date: 5/9/2019 8:47:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kerry Murphy
-- Create date: <05/07/2019>
-- Description:	update user and entries in associated tables
-- =============================================
ALTER PROCEDURE [dbo].[sp_UpdateUser]
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
		set Active=0
		where UserID_FK=@UserId and TeamID_FK=@OldTeamId
		set @rowCount=@rowCount+@@ROWCOUNT

		update dbo.[TeamManagement]
		set Active=@active,ModifiedDate=GetDate()
		where UserID_FK=@UserId and TeamID_FK=@NewTeamId
		set @rowCount=@rowCount+@@ROWCOUNT

		if @rowCount <@validRowCount
		BEGIN
		    set @msg='number of successful updates is '+@rowCount+' but should be '+@validRowCount
	 	   raiserror (@msg,15,-1)
	    END

    END TRY
	BEGIN CATCH
        ROLLBACK TRANSACTION	   
	END CATCH
	COMMIT TRANSACTION

END
