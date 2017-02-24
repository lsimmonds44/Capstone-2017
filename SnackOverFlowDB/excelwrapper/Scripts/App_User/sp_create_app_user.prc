USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_app_user')
BEGIN
DROP PROCEDURE sp_create_app_user
Print '' print  ' *** dropping procedure sp_create_app_user'
End
GO

Print '' print  ' *** creating procedure sp_create_app_user'
GO
Create PROCEDURE sp_create_app_user
(
@FIRST_NAME[NVARCHAR](150),
@LAST_NAME[NVARCHAR](100)= NULL,
@PHONE[NVARCHAR](15),
@PREFERRED_ADDRESS_ID[INT]=NULL,
@E_MAIL_ADDRESS[NVARCHAR](50),
@E_MAIL_PREFERENCES[BIT],
@PASSWORD_HASH[NVARCHAR](64),
@PASSWORD_SALT[NVARCHAR](64),
@USER_NAME[NVARCHAR](50),
@ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO APP_USER (FIRST_NAME, LAST_NAME, PHONE, PREFERRED_ADDRESS_ID, E_MAIL_ADDRESS, E_MAIL_PREFERENCES, PASSWORD_HASH, PASSWORD_SALT, USER_NAME, ACTIVE)
VALUES
(@FIRST_NAME, @LAST_NAME, @PHONE, @PREFERRED_ADDRESS_ID, @E_MAIL_ADDRESS, @E_MAIL_PREFERENCES, @PASSWORD_HASH, @PASSWORD_SALT, @USER_NAME, @ACTIVE)
END
