USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_TeamUpdate]    Script Date: 5/8/2019 10:02:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_TeamUpdate]
	-- Add the parameters for the stored procedure here
	@TeamID INT
	,@TeamName VARCHAR(100)
	,@Comment VARCHAR(MAX)
	,@Active INT
	,@UpdatedUser INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @msg NVARCHAR(300) 

	BEGIN TRANSACTION 

	BEGIN TRY

			UPDATE t
			SET Name=@TeamName
				,Comment=@Comment
				,Active=@Active
				,ModifiedDate= GETDATE()
				,ModifiedUser_FK=@UpdatedUser
			FROM Team t
			WHERE TeamID=@TeamID

			IF @@ROWCOUNT = 0
				BEGIN
				   SET @msg='Team was not inactivated, please check this is correct record. '
				   RAISERROR (@msg,15,-1)
				END

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg NVARCHAR(MAX)
		SELECT @ErrorMsg='ErrorState='+convert(varchar,ERROR_STATE())
			+ 'ErrorProcedure='+ convert(varchar,ERROR_PROCEDURE())
			+ 'ErrorLine='+convert(varchar,ERROR_LINE())
			+ 'ErrorMessage='+convert(varchar,ERROR_MESSAGE())

		EXEC sp_logError @ErrorMsg, '[sp_TeamUpdate]' 
	 
	 	SET @msg='Something went wrong, please review Logs for [sp_TeamUpdate]'
		RAISERROR (@msg,15,-1)

		ROLLBACK TRANSACTION	
	END CATCH
	
		COMMIT TRANSACTION
END
