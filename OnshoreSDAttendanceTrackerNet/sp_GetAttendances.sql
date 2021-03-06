USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUsers]    Script Date: 5/8/2019 10:03:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Kerry Murphy
-- Create date: <05/08/2019>
-- Description:	Gets All the attendances that are active
-- =============================================
ALTER PROCEDURE [dbo].[sp_GetAttendances]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AbsenceTypeID,Name, Point, Active, TeamID_FK as TeamID from dbo.[AbsenceType]
END
