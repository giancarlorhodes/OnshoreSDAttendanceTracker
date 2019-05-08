USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_AttendanceUpdate]    Script Date: 5/7/2019 9:27:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_AttendanceUpdate]
	-- Add the parameters for the stored procedure here
	@PointBankID INT
	 ,@PointValue INT
	,@AbsenceTypeID INT
	,@TeamManagementID INT
	,@UpdatedUserID INT
	,@Active INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	BEGIN TRY

			UPDATE p
			SET PointValue=@PointValue
			,AbsenceTypeID_FK=@AbsenceTypeID
			,TeamManagementID_FK=@TeamManagementID
			,Active=@Active
			,ModifiedDate= GETDATE()
			,ModifiedUser=@UpdatedUserID
			FROM PointBank p
			WHERE PointBankID=@PointBankID

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg NVARCHAR(MAX)
		SELECT @ErrorMsg='ErrorState='+convert(varchar,ERROR_STATE())
			+ 'ErrorProcedure='+ convert(varchar,ERROR_PROCEDURE())
			+ 'ErrorLine='+convert(varchar,ERROR_LINE())
			+ 'ErrorMessage='+convert(varchar,ERROR_MESSAGE())

		EXEC sp_logError @ErrorMsg, '[sp_TeamUpdate]' 
	 
	END CATCH
	

END
