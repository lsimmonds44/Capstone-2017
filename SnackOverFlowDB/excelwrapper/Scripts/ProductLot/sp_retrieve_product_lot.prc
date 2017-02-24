USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_lot')
BEGIN
DROP PROCEDURE sp_retrieve_product_lot
Print '' print  ' *** dropping procedure sp_retrieve_product_lot'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_lot'
GO
Create PROCEDURE sp_retrieve_product_lot
(
@PRODUCT_LOT_ID[INT]
)
AS
BEGIN
SELECT PRODUCT_LOT_ID, WAREHOUSE_ID, SUPPLIER_ID, LOCATION_ID, PRODUCT_ID, SUPPLY_MANAGER_ID, QUANTITY, AVAILABLE_QUANTITY, DATE_RECEIVED, EXPIRATION_DATE
FROM product_lot
WHERE PRODUCT_LOT_ID = @PRODUCT_LOT_ID
END
