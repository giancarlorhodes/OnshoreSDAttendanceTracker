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
-- Description:	Create attendance types
-- =============================================
ALTER PROCEDURE sp_MakeAttendanceTypeByUserId   
	@TeamMgtId int output,
    @Name varchar(100),
	@Point decimal,
	@CreatedByUserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    Declare @active bit=1
	Declare @msg varchar(100)
    Declare @teamId int=-1

	if ISNULL(@CreatedByUserId,0) <= 0 or (Select Active from dbo.[User] where UserID=@CreatedByUserId) <> @active
	BEGIN
	   set @msg='created by user id must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

	if COALESCE(@Name,@Point,0) = 0
	BEGIN
	   set @msg ='input parameters cannot be null'
	   raiserror (@msg,15,-1)
    END

	select @teamId=TeamID_FK from dbo.[TeamManagement] where TeamManagementID=CAST(SCOPE_IDENTITY() AS INT) and Active=@active

	if ISNULL(TeamManagementID,0) <= 0
		BEGIN
		   set @msg='team management id must be greater than 0'
		   raiserror (@msg,15,-1)
		END

	BEGIN TRANSACTION 
	BEGIN TRY
	    insert into dbo.[AbsenceType] (Name,Point,Active,TeamID_FK,CreateDate,CreateUser_FK,ModifiedDate,ModifiedUser_FK)
	    values (@Name,@Point,@active,@teamId,GetDate(),@CreatedByUserId,GetDate(),@CreatedByUserId)
		SET @TeamMgtId = SCOPE_IDENTITY()
    END TRY
	BEGIN CATCH
        ROLLBACK TRANSACTION
		set @msg=ERROR_MESSAGE()
	    raiserror (@msg,15,-1)		   
	END CATCH
	COMMIT TRANSACTION

END
GO
