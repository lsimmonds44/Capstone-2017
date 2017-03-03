USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_user_password')
BEGIN
DROP PROCEDURE sp_update_user_password
Print '' print  ' *** dropping procedure sp_update_user_password'
End
GO

Print '' print  ' *** creating procedure sp_update_user_password'
GO
Create PROCEDURE sp_update_user_password
(
@USERNAME[NVARCHAR](50)
,@OLD_SALT[NVARCHAR](64)
,@OLD_HASH[NVARCHAR](64)
,@NEW_SALT[NVARCHAR](64)
,@NEW_HASH[NVARCHAR](64)
)
AS
    UPDATE APP_USER
    SET PASSWORD_SALT = @NEW_SALT
    ,PASSWORD_HASH = @NEW_HASH
    WHERE USER_NAME = @USERNAME
    AND PASSWORD_SALT = @OLD_SALT
    AND PASSWORD_HASH = @OLD_HASH