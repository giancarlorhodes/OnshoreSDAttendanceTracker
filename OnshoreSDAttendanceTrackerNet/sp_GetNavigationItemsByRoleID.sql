USE [OnshoreSDAttendanceTracker]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetNavigationItemsByRoleID]    Script Date: 5/9/2019 3:10:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetNavigationItemsByRoleID]
	-- Add the parameters for the stored procedure here
	 @RoleID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     SELECT
	 NavigationMenuID,
	 MenuItem,
	 [Url],
	 RoleID_FK as RoleID
	 FROM NavigationMenu
	 WHERE RoleID_FK=@RoleID
END
