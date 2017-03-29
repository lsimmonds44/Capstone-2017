USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_reset_user_password')
BEGIN
DROP PROCEDURE sp_reset_user_password
Print '' print  ' *** dropping procedure sp_reset_user_password'
End
GO

Print '' print  ' *** creating procedure sp_reset_user_password'
GO
Create PROCEDURE sp_reset_user_password
(
@USERNAME[NVARCHAR](50)
,@PASSWORD_SALT[NVARCHAR](64)
,@PASSWORD_HASH[NVARCHAR](64)
)
AS
    UPDATE APP_USER
    SET PASSWORD_SALT = @PASSWORD_SALT
    ,PASSWORD_HASH = @PASSWORD_HASH
    WHERE USER_NAME = @USERNAME