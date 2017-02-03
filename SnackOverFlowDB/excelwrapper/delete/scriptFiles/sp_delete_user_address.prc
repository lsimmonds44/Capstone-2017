USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_user_address')
BEGIN
DROP PROCEDURE sp_delete_user_address
Print '' print  ' *** dropping procedure sp_delete_user_address'
End
GO

Print '' print  ' *** creating procedure sp_delete_user_address'
GO
Create PROCEDURE sp_delete_user_address
(
@USER_ADDRESS_ID[INT]
)
AS
BEGIN
DELETE FROM user_address
WHERE USER_ADDRESS_ID = @USER_ADDRESS_ID
END
