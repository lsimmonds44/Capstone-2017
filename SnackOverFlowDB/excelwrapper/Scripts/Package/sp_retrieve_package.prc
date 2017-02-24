USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_package')
BEGIN
DROP PROCEDURE sp_retrieve_package
Print '' print  ' *** dropping procedure sp_retrieve_package'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_package'
GO
Create PROCEDURE sp_retrieve_package
(
@PACKAGE_ID[INT]
)
AS
BEGIN
SELECT PACKAGE_ID, DELIVERY_ID, ORDER_ID
FROM package
WHERE PACKAGE_ID = @PACKAGE_ID
END
