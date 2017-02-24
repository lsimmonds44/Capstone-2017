USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_user_salt')
BEGIN
DROP PROCEDURE sp_retrieve_user_salt
Print '' print  ' *** dropping procedure sp_retrieve_user_salt'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_user_salt'
GO
Create PROCEDURE sp_retrieve_user_salt (
    @Username[NVARCHAR](64)
)
AS
BEGIN
    SELECT PASSWORD_SALT
    FROM APP_USER
    WHERE USER_NAME = @Username
END
GO