USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateNavigationItem]    Script Date: 5/10/2019 10:26:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ALeris Roman
-- Create date: May 9, 2019
-- Description:	Insert New Menu Item by UserEmail
-- =============================================
ALTER PROCEDURE [dbo].[sp_CreateNavigationItem]
	-- Add the parameters for the stored procedure here
	  @MenuItem VARCHAR(100)
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

     INSERT INTO NavigationMenu(MenuItem,Url,RoleID_FK,CreateDate,CreateUser,ModifiedDate,ModifiedUser)
		VALUES(@MenuItem,@URL,@RoleID,GETDATE(),@UserID,GETDATE(),@UserID)
END
