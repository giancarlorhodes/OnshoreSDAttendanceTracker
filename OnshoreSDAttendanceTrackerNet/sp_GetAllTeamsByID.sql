CREATE PROCEDURE [dbo].[sp_GetAllTeamsByID]
    @UserId int
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    DECLARE @active BIT = 1
    DECLARE @msg VARCHAR(100)

    IF ISNULL(@UserId, 0) <= 0
        OR (
            SELECT Active
            FROM dbo.[User]
            WHERE UserID = @UserId
            ) <> @active
    BEGIN
        SET @msg = 'team id must be greater than 0 and active'
        RAISERROR ( @msg ,15 ,- 1 )
    END

    SELECT        t.[Name],
                'Start Date: ' + t.CreateDate + ' | Modified Date: ' + t.ModifiedDate AS [Comments],
                t.Active
    FROM Team t
    JOIN TeamManagment tm ON t.TeamID = tm.TeamID_FK
    JOIN [User] u ON tm.UserID_FK = u.UserID
    WHERE u.UserID = @userId
    AND u.RoleID_FK = 2
END