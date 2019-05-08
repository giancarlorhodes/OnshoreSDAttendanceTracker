USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_UserCredentialsAddOrEdit]    Script Date: 5/8/2019 3:07:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Giancarlo Rhodes
-- Create date: 5/8/2019
-- Description:	Add or edit credentials in the UserCredential table
-- =============================================
ALTER PROCEDURE [dbo].[sp_UserCredentialsAddOrEdit]
	-- Add the parameters for the stored procedure here
	@parmUserPassword varchar(100), 
	@parmUserID int, 
	@parmSalt varchar(100),
	@parmCreateUser int,
	@parmModifiedUser int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	IF (@parmUserID = 0)
	BEGIN
			-- Insert statements for procedure here
			INSERT INTO [dbo].[UserCredentials]
				   (
						[UserPassword]
					   ,[UserID_FK]
					   ,[Salt]           
					   ,[CreateUser]
					   ,[ModifiedUser]
				   )
			 VALUES
				   (
						@parmUserPassword
					   ,@parmUserID
					   ,@parmSalt
					   ,@parmCreateUser
					   ,@parmModifiedUser 
				   )
			END;
	ELSE
	BEGIN
		
			UPDATE [dbo].[UserCredentials]
			SET [UserPassword] = @parmUserPassword				
				,[Salt] = @parmSalt
				,[ModifiedDate] = GETDATE()
				,[ModifiedUser] = @parmModifiedUser
			WHERE [UserID_FK] = @parmUserID;

	END

END
