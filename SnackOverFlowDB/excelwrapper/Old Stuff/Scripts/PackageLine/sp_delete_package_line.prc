USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_package_line')
BEGIN
DROP PROCEDURE sp_delete_package_line
Print '' print  ' *** dropping procedure sp_delete_package_line'
End
GO

Print '' print  ' *** creating procedure sp_delete_package_line'
GO
Create PROCEDURE sp_delete_package_line
(
@PACKAGE_LINE_ID[INT]
)
AS
BEGIN
DELETE FROM package_line
WHERE PACKAGE_LINE_ID = @PACKAGE_LINE_ID
END
