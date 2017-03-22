USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_lot_list')
BEGIN
Drop PROCEDURE sp_retrieve_product_lot_list
Print '' print  ' *** dropping procedure sp_retrieve_product_lot_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_lot_list'
GO
Create PROCEDURE sp_retrieve_product_lot_list
AS
BEGIN
SELECT PRODUCT_LOT_ID, WAREHOUSE_ID, SUPPLIER_ID, LOCATION_ID, PRODUCT_ID, SUPPLY_MANAGER_ID, QUANTITY, AVAILABLE_QUANTITY, DATE_RECEIVED, EXPIRATION_DATE
FROM product_lot
END
