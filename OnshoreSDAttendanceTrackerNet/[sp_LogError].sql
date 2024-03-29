USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_LogError]    Script Date: 5/10/2019 10:31:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		ALeris Roman
-- Create date: May 9,2019
-- Description:	Create New Error
-- =============================================
ALTER PROCEDURE [dbo].[sp_LogError] 
	-- Add the parameters for the stored procedure here
	@ErrorMsg NVARCHAR(MAX)
	,@SPName NVARCHAR(MAX)
	,@ExceptionType NVARCHAR(100)=NULL
	,@ExceptionSource NVARCHAR(100)=NULL
	,@ExceptionURL NVARCHAR(100)=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO ExceptionLogging(ExceptionMsg,ExceptionType,ExceptionSource,ExceptionURL,Logdate)
		VALUES(@ErrorMsg,ISNULL(@ExceptionType,'SQL'),@SPName,@ExceptionURL,GETDATE())
END
