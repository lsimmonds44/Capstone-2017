
USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_log_in_user')
BEGIN
  DROP PROCEDURE sp_log_in_user
  Print '' print  ' *** dropping procedure sp_log_in_user'
End
GO

Print '' print  ' *** creating procedure sp_log_in_user'
GO
CREATE PROCEDURE sp_log_in_user
(
@USER_NAME [NVARCHAR](20),
@PASSWORD_HASH [NVARCHAR](200)
)
AS
BEGIN
    SELECT USER_NAME, FIRST_NAME, LAST_NAME, PHONE, PREFERRED_ADDRESS_ID, E_MAIL_ADDRESS, E_MAIL_PREFERENCES, ACTIVE
    FROM APP_USER
    WHERE USER_NAME = @USER_NAME
    AND PASSWORD_HASH = @PASSWORD_HASH
END