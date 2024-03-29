USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoggedErrors]    Script Date: 5/10/2019 10:27:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 8,2019
-- Description:	Get All current Logged Errors
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetLoggedErrors]
	-- Add the parameters for the stored procedure here
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ExceptionMsg,ExceptionType,ExceptionSource,ExceptionURL,Logdate
	FROM ExceptionLogging
	WHERE Logdate>DATEADD(MONTH,-6,GETDATE()) --Grab last 6 months of errors

END
