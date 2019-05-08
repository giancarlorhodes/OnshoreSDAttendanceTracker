USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_TeamAddNew]    Script Date: 5/7/2019 9:28:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
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
