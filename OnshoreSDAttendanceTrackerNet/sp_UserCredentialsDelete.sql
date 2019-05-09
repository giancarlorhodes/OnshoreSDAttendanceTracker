
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
-- Author:		Giancarlo Rhodes
-- Create date: 5/8/2019
-- Description:	Delete credentials in the UserCredential table
-- =============================================
CREATE PROCEDURE sp_UserCredentialsDelete 
	-- Add the parameters for the stored procedure here
	@parmUserID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[UserCredentials] 
	WHERE [UserID_FK] = @parmUserID;
END
GO


