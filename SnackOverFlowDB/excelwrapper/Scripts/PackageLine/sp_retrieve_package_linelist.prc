USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_package_line_list')
BEGIN
Drop PROCEDURE sp_retrieve_package_line_list
Print '' print  ' *** dropping procedure sp_retrieve_package_line_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_package_line_list'
GO
Create PROCEDURE sp_retrieve_package_line_list
AS
BEGIN
SELECT PACKAGE_LINE_ID, PACKAGE_ID, PRODUCT_LOT_ID, QUANTITY, PRICE_PAID
FROM package_line
END
