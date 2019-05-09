USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPointsByID]    Script Date: 5/8/2019 9:05:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetPointsByID]
	-- Add the parameters for the stored procedure here
	@PointBankID INT
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	 

	SELECT AbsenceTypeID_FK 
	,TeamManagementID_FK 
	,p.Active 
	,createUser.FirstName +' ' + createUser.LastName CreateUserFull,
	createUser.UserID,
	updateUser.FirstName +' ' + updateUser.LastName UpdateUserFull,
	updateUser.UserID
	FROM PointBank p
	LEFT JOIN [User] createUser on createUser.UserID=P.CreateUser
	LEFT JOIN [User] updateUser on updateUser.UserID=P.CreateUser
	WHERE PointBankID=@PointBankID

	  
END
