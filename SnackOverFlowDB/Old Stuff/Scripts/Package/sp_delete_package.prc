USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_package')
BEGIN
DROP PROCEDURE sp_delete_package
Print '' print  ' *** dropping procedure sp_delete_package'
End
GO

Print '' print  ' *** creating procedure sp_delete_package'
GO
Create PROCEDURE sp_delete_package
(
@PACKAGE_ID[INT]
)
AS
BEGIN
DELETE FROM package
WHERE PACKAGE_ID = @PACKAGE_ID
END
