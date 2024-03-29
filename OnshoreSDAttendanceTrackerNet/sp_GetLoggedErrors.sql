USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoggedErrors]    Script Date: 5/7/2019 9:27:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
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
