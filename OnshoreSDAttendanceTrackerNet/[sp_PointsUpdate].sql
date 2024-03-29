USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_PointsUpdate]    Script Date: 5/10/2019 10:37:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 8, 2019
-- Description:	Update Actual Absence By ID 
-- =============================================
ALTER PROCEDURE [dbo].[sp_PointsUpdate]
	-- Add the parameters for the stored procedure here
	@PointBankID INT
	,@AbsenceTypeID INT
	,@TeamManagementID INT
	,@UpdatedUserID INT
	,@Active INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @msg NVARCHAR(300)

	BEGIN TRANSACTION 

	BEGIN TRY

			UPDATE p
			SET AbsenceTypeID_FK=@AbsenceTypeID
			,TeamManagementID_FK=@TeamManagementID
			,Active=@Active
			,ModifiedDate= GETDATE()
			,ModifiedUser=@UpdatedUserID
			FROM PointBank p
			WHERE PointBankID=@PointBankID
			 

			IF @@ROWCOUNT = 0
				BEGIN
				   SET @msg='Attendance was not inactivated, please check this is correct record. '
				   RAISERROR (@msg,15,-1)
				END
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg NVARCHAR(MAX)
		SELECT @ErrorMsg='ErrorState='+convert(varchar,ERROR_STATE())
			+ 'ErrorProcedure='+ convert(varchar,ERROR_PROCEDURE())
			+ 'ErrorLine='+convert(varchar,ERROR_LINE())
			+ 'ErrorMessage='+convert(varchar,ERROR_MESSAGE())

		EXEC sp_logError @ErrorMsg, '[sp_PointsUpdate]' 
		
		SET @msg='Something went wrong, please review Logs for [sp_PointsUpdate]'
		RAISERROR (@msg,15,-1)

		ROLLBACK TRANSACTION	
	END CATCH
	
		COMMIT TRANSACTION

END
