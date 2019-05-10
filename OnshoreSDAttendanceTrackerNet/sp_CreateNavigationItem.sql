USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateNavigationItem]    Script Date: 5/9/2019 3:36:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
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
