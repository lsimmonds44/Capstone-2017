USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_lot_list_w_grade_price')
BEGIN
Drop PROCEDURE sp_retrieve_product_lot_list_w_grade_price
Print '' print  ' *** dropping procedure sp_retrieve_product_lot_list_w_grade_price'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_lot_list_w_grade_price'
GO
Create PROCEDURE sp_retrieve_product_lot_list_w_grade_price
AS
BEGIN
SELECT product_lot.PRODUCT_LOT_ID, WAREHOUSE_ID, SUPPLIER_ID, LOCATION_ID, PRODUCT_ID, SUPPLY_MANAGER_ID, QUANTITY,
 AVAILABLE_QUANTITY, DATE_RECEIVED, product_lot.EXPIRATION_DATE,INSPECTION.GRADE_ID
FROM product_lot JOIN inspection ON inspection.PRODUCT_LOT_ID = product_lot.PRODUCT_LOT_ID
WHERE inspection.PRODUCT_LOT_ID = product_lot.PRODUCT_LOT_ID
ORDER BY inspection.PRODUCT_LOT_ID
END
