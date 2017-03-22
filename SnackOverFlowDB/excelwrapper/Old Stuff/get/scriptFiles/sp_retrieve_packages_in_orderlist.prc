USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_packages_in_order_list')
BEGIN
Drop PROCEDURE sp_retrieve_packages_in_order_list
Print '' print  ' *** dropping procedure sp_retrieve_packages_in_order_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_packages_in_order_list'
GO
Create PROCEDURE sp_retrieve_packages_in_order_list
(
	@ORDER_ID	int
	)
AS
BEGIN
SELECT PACKAGE_ID, DELIVERY_ID, ORDER_ID
FROM package
WHERE ORDER_ID = @ORDER_ID
END
