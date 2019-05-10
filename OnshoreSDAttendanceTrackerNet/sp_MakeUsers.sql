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
-- Description:	Create user and entries in associated tables
-- =============================================
ALTER PROCEDURE sp_MakeUser

    @CreatedByUserId int,
    @TeamId int,
	@RoleId int,
	@Email varchar(100),  
	@FName varchar(100),
	@LName varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    Declare @active int=1
	Declare @userId int
	Declare @msg varchar(100)

	if ISNULL(@CreatedByUserId,0) <= 0 or (Select Active from dbo.[User] where UserID=@CreatedByUserId) <> @active
	BEGIN
	   set @msg='created by user id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

    if ISNULL(@TeamId,0) <= 0 or (Select Active from dbo.[Team] where TeamID=@TeamId) <> @active
	BEGIN
	   set @msg='team id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

	 if ISNULL(@RoleId,0) <= 0 
	BEGIN
	   set @msg='role id must be greater than 0 and non-null'
	   raiserror (@msg,15,-1)
    END

	BEGIN TRANSACTION 
	BEGIN TRY
	    insert into dbo.[User] (FirstName,LastName,RoleID_FK,Email,Active,CreateDate,CreateUser_FK,ModifiedDate,ModifiedUser_FK)
	    values (@FName,@LName,@RoleId,@Email,@active,GetDate(),@CreatedByUserId,GetDate(),@CreatedByUserId)
	    set @userId=SCOPE_IDENTITY()

		
		insert into dbo.[TeamManagement] (UserID_FK,TeamID_FK,Active,CreateDate,CreateUser,ModifiedDate,ModifiedUser)
		values (@userId,@TeamId,@active,GetDate(),@CreatedByUserId,GetDate(),@CreatedByUserId)
    END TRY
	BEGIN CATCH
		set @msg=ERROR_MESSAGE()
	    raiserror (@msg,15,-1)
        ROLLBACK TRANSACTION
	END CATCH
	COMMIT TRANSACTION

END
GO