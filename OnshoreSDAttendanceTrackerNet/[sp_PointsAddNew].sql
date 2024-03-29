USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_PointsAddNew]    Script Date: 5/10/2019 10:37:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 8, 2019
-- Description:	Create New Actual Absence
-- =============================================
ALTER PROCEDURE [dbo].[sp_PointsAddNew]
	-- Add the parameters for the stored procedure here
	@AbsenceTypeID INT
	,@TeamManagementID INT
	,@CreateUserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO PointBank (AbsenceTypeID_FK,TeamManagementID_FK,Active,CreateDate,CreateUser,ModifiedDate, ModifiedUser)
		VALUES(@AbsenceTypeID,@TeamManagementID,1,GETDATE(),@CreateUserID,GETDATE(),@CreateUserID)


END