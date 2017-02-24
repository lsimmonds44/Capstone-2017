USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_package_lines_in_package_list')
BEGIN
Drop PROCEDURE sp_retrieve_package_lines_in_package_list
Print '' print  ' *** dropping procedure sp_retrieve_package_lines_in_package_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_package_lines_in_package_list'
GO
Create PROCEDURE sp_retrieve_package_lines_in_package_list
(
	@PACKAGE_ID	int
)
AS
BEGIN
SELECT PACKAGE_LINE_ID, package_line.PACKAGE_ID, PRODUCT_LOT_ID, QUANTITY, PRICE_PAID
FROM package_line
WHERE package_line.PACKAGE_ID = @PACKAGE_ID
END
