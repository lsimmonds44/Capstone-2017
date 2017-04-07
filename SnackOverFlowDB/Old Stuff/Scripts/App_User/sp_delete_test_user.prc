USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_test_user')
BEGIN
DROP PROCEDURE sp_delete_test_user
Print '' print  ' *** dropping procedure sp_delete_test_user'
End
GO

Print '' print  ' *** creating procedure sp_delete_test_user'
GO
Create PROCEDURE sp_delete_test_user
AS
	BEGIN
		DELETE FROM APP_USER 
		WHERE USER_NAME = "Test"
	END
GO