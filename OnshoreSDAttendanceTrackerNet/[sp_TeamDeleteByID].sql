USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_TeamDeleteByID]    Script Date: 5/10/2019 10:34:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aleris Roman
-- Create date: May 9, 2019
-- Description:	Delete Team By ID
-- =============================================
ALTER PROCEDURE [dbo].[sp_TeamDeleteByID]
	-- Add the parameters for the stored procedure here
	@TeamID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	UPDATE t
	set Active=0
	FROM Team t
	WHERE t.TeamID=@TeamID

END

