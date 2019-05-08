USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_AttendanceAddNew]    Script Date: 5/7/2019 9:26:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_AttendanceAddNew]
	-- Add the parameters for the stored procedure here
	 @PointValue INT
	,@AbsenceTypeID INT
	,@TeamManagementID INT
	,@CreateUserID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO PointBank (PointValue,AbsenceTypeID_FK,TeamManagementID_FK,Active,CreateDate,CreateUser,ModifiedDate, ModifiedUser)
		VALUES(@PointValue,@AbsenceTypeID,@TeamManagementID,1,GETDATE(),@CreateUserID,GETDATE(),@CreateUserID)


END
