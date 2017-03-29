USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_app_user_by_username')
BEGIN
DROP PROCEDURE sp_retrieve_app_user_by_username
Print '' print  ' *** dropping procedure sp_retrieve_app_user_by_username'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_app_user_by_username'
GO
Create PROCEDURE sp_retrieve_app_user_by_username
(
@USERNAME [NVARCHAR](50)
)
AS
BEGIN
SELECT USER_ID, FIRST_NAME, LAST_NAME, PHONE, PREFERRED_ADDRESS_ID, E_MAIL_ADDRESS, E_MAIL_PREFERENCES, USER_NAME, ACTIVE
FROM app_user
WHERE USER_NAME = @USERNAME
END