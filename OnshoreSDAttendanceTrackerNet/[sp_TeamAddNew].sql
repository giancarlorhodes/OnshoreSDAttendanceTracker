USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_TeamAddNew]    Script Date: 5/10/2019 10:37:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 8,2019
-- Description:	Create New Team
-- =============================================
ALTER PROCEDURE [dbo].[sp_TeamAddNew]
	-- Add the parameters for the stored procedure here
	@TeamName VARCHAR(100)
	,@Comment VARCHAR(MAX)
	,@CreatedUser INT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO Team (Name,Comment,Active,CreateDate,CreateUser_FK,ModifiedDate, ModifiedUser_FK)
		VALUES(@TeamName,@Comment,1,GETDATE(),@CreatedUser,GETDATE(),@CreatedUser)


END
