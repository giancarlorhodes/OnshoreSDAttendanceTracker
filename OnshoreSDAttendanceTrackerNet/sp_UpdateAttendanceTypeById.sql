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
-- Description:	update attendance type and entries in associated tables
-- =============================================
ALTER PROCEDURE sp_UpdateAttendanceTypeById
    @AttendanceTypeId int,
	@ModifiedByUserId int,
	@Name varchar(100)=NULL,
	@Point decimal=NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @msg nvarchar(100)
	declare @active bit=1

	if coalesce(@Name,@Point,0) = 0
	BEGIN
	   set @msg='At least one input parameter must be non-null'
	   raiserror (@msg,15,-1)
    END

	if ISNULL(@AttendanceTypeId,0) <= 0
	BEGIN
	   set @msg='attendance type id must be greater than 0'
	   raiserror (@msg,15,-1)
    END

	if 	ISNULL(@ModifiedByUserId,0) <= 0 or (Select Active from dbo.[User] where UserID=@ModifiedByUserId) <> @active
	BEGIN
	   set @msg='user id doing the modifying must be greater than 0 and active'
	   raiserror (@msg,15,-1)
    END

	Declare @validRowCount tinyint=2
	Declare @rowCount tinyint

	BEGIN TRANSACTION 
	BEGIN TRY
	    update dbo.[AbsenceType] 
		set Name=@Name
		where @Name is NOT NULL and AbsenceTypeId=@AttendanceTypeId
		set @rowCount=@@ROWCOUNT

		update dbo.[AbsenceType]
		set Point=@Point
		where @Point is NOT NULL and AbsenceTypeId=@AttendanceTypeId
		set @rowCount=@rowCount+@@ROWCOUNT

		update dbo.[AbsenceType]
		set ModifiedDate=GetDate(),
		ModifiedUser_FK=@ModifiedByUserId
		if @rowCount <@validRowCount
		BEGIN
		    set @msg='number of successful updates is '+@rowCount+' but should be '+@validRowCount
	 	    raiserror (@msg,15,-1)
	    END

    END TRY
	BEGIN CATCH
        ROLLBACK TRANSACTION
	    set @msg=ERROR_MESSAGE()
	    raiserror (@msg,15,-1)		  
	END CATCH
	COMMIT TRANSACTION

END
GO
