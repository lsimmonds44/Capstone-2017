USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_orderline_list_by_order_id')
BEGIN
DROP PROCEDURE sp_retrieve_product_orderline_list_by_order_id
Print '' print  ' *** dropping procedure sp_retrieve_product_orderline_list_by_order_id'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_orderline_list_by_order_id'
GO
Create PROCEDURE sp_retrieve_product_orderline_list_by_order_id
(
@PRODUCT_ORDER_ID[INT]
)
AS
BEGIN
SELECT ORDER_LINE_ID, PRODUCT_ORDER_ID, PRODUCT_ID, QUANTITY, GRADE_ID, PRICE, UNIT_DISCOUNT
FROM ORDER_LINE
WHERE PRODUCT_ORDER_ID = @PRODUCT_ORDER_ID
END