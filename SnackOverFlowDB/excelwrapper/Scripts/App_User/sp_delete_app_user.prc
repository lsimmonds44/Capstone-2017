USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_app_user')
BEGIN
DROP PROCEDURE sp_delete_app_user
Print '' print  ' *** dropping procedure sp_delete_app_user'
End
GO

Print '' print  ' *** creating procedure sp_delete_app_user'
GO
Create PROCEDURE sp_delete_app_user
(
@USER_ID[INT]
)
AS
BEGIN
DELETE FROM app_user
WHERE USER_ID = @USER_ID
END
