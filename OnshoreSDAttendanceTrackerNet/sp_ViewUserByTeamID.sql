-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ViewUsersByTeamID]
	@TeamId int
AS
BEGIN
	SELECT	u.FirstName,
			u.LastName,
			u.Email,
			IIF(u.Active = 1, 'Yes', 'No') AS [Active]
	FROM Team t
	JOIN TeamManagement tm ON t.TeamID = tm.TeamID_FK
	JOIN [User] u ON tm.UserID_FK = u.UserID
	WHERE t.TeamID = @TeamId;
END
GO
