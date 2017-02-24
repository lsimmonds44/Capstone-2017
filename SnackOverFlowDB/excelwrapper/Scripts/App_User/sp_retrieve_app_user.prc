USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_app_user')
BEGIN
DROP PROCEDURE sp_retrieve_app_user
Print '' print  ' *** dropping procedure sp_retrieve_app_user'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_app_user'
GO
Create PROCEDURE sp_retrieve_app_user
(
@USER_ID[INT]
)
AS
BEGIN
SELECT USER_ID, FIRST_NAME, LAST_NAME, PHONE, PREFERRED_ADDRESS_ID, E_MAIL_ADDRESS, E_MAIL_PREFERENCES, PASSWORD_HASH, PASSWORD_SALT, USER_NAME, ACTIVE
FROM app_user
WHERE USER_ID = @USER_ID
END
