USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_lot_by_supplier_id')
BEGIN
DROP PROCEDURE sp_retrieve_product_lot_by_supplier_id
Print '' print  ' *** dropping procedure sp_retrieve_product_lot_by_supplier_id'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_lot_by_supplier_id'
GO
Create PROCEDURE sp_retrieve_product_lot_by_supplier_id
(
@SUPPLIER_ID[INT]
)
AS
BEGIN
SELECT PRODUCT_LOT_ID, WAREHOUSE_ID, SUPPLIER_ID, LOCATION_ID, PRODUCT_ID, SUPPLY_MANAGER_ID, QUANTITY, AVAILABLE_QUANTITY, DATE_RECEIVED, EXPIRATION_DATE
FROM product_lot
WHERE SUPPLIER_ID = @SUPPLIER_ID
ORDER BY PRODUCT_LOT_ID DESC
END
