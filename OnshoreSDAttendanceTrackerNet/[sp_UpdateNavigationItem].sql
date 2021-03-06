USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateNavigationItem]    Script Date: 5/10/2019 10:35:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 9, 2019
-- Description: Update Menu Items
-- =============================================
ALTER PROCEDURE [dbo].[sp_UpdateNavigationItem]
	-- Add the parameters for the stored procedure here
	@NavigationMenuID INT
	  ,@MenuItem VARCHAR(100)
	 ,@URL Varchar(100)
	 ,@RoleID INT
	 ,@UserEmail VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @UserID INT

	SELECT @UserID=UserID
	FROM [User]
	WHERE Email=@UserEmail

     DECLARE @msg NVARCHAR(300) 

	BEGIN TRANSACTION 

	BEGIN TRY

			UPDATE t
			SET MenuItem=@MenuItem
				,[Url]=@URL
				,RoleID_FK=@RoleID
				,ModifiedDate= GETDATE()
				,ModifiedUser =@UserID
			FROM NavigationMenu t
			WHERE NavigationMenuID=@NavigationMenuID


			IF @@ROWCOUNT = 0
				BEGIN
				   SET @msg='Menu Item was not inactivated, please check this is correct record. '
				   RAISERROR (@msg,15,-1)
				END

	END TRY
	BEGIN CATCH
		DECLARE @ErrorMsg NVARCHAR(MAX)
		SELECT @ErrorMsg='ErrorState='+convert(varchar,ERROR_STATE())
			+ 'ErrorProcedure='+ convert(varchar,ERROR_PROCEDURE())
			+ 'ErrorLine='+convert(varchar,ERROR_LINE())
			+ 'ErrorMessage='+convert(varchar,ERROR_MESSAGE())

		EXEC sp_logError @ErrorMsg, '[sp_UpdateNavigationItem]' 
	 
	 	SET @msg='Something went wrong, please review Logs for [sp_UpdateNavigationItem]'
		RAISERROR (@msg,15,-1)

		ROLLBACK TRANSACTION	
	END CATCH
	
		COMMIT TRANSACTION
END
