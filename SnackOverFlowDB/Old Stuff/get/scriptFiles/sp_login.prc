USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_login')
BEGIN
DROP PROCEDURE sp_login
Print '' print  ' *** dropping procedure sp_login'
End
GO

Print '' print  ' *** creating procedure sp_login'
GO
Create PROCEDURE sp_login (
    @Username[NVARCHAR](64),
    @Password_Hash[NVARCHAR](64)
)
AS
BEGIN
SELECT USER_ID, FIRST_NAME, LAST_NAME, PHONE, PREFERRED_ADDRESS_ID, E_MAIL_ADDRESS, E_MAIL_PREFERENCES, PASSWORD_HASH, PASSWORD_SALT, USER_NAME, ACTIVE
FROM app_user
WHERE USER_NAME = @Username
AND PASSWORD_HASH = @Password_Hash
END
GO