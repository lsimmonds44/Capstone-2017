USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_package_line')
BEGIN
DROP PROCEDURE sp_retrieve_package_line
Print '' print  ' *** dropping procedure sp_retrieve_package_line'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_package_line'
GO
Create PROCEDURE sp_retrieve_package_line
(
@PACKAGE_LINE_ID[INT]
)
AS
BEGIN
SELECT PACKAGE_LINE_ID, PACKAGE_ID, PRODUCT_LOT_ID, QUANTITY, PRICE_PAID
FROM package_line
WHERE PACKAGE_LINE_ID = @PACKAGE_LINE_ID
END
