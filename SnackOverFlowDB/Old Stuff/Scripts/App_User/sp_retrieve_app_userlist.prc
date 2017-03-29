USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_app_user_list')
BEGIN
Drop PROCEDURE sp_retrieve_app_user_list
Print '' print  ' *** dropping procedure sp_retrieve_app_user_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_app_user_list'
GO
Create PROCEDURE sp_retrieve_app_user_list
AS
BEGIN
SELECT USER_ID, FIRST_NAME, LAST_NAME, PHONE, PREFERRED_ADDRESS_ID, E_MAIL_ADDRESS, E_MAIL_PREFERENCES, PASSWORD_HASH, PASSWORD_SALT, USER_NAME, ACTIVE
FROM app_user
END
