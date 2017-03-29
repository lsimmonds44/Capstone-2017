USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_order_list')
BEGIN
Drop PROCEDURE sp_retrieve_product_order_list
Print '' print  ' *** dropping procedure sp_retrieve_product_order_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_order_list'
GO
Create PROCEDURE sp_retrieve_product_order_list
AS
BEGIN
SELECT ORDER_ID, CUSTOMER_ID, ORDER_TYPE_ID, ADDRESS_TYPE, DELIVERY_TYPE_ID, AMOUNT, ORDER_DATE, DATE_EXPECTED, DISCOUNT, ORDER_STATUS_ID, USER_ADDRESS_ID, HAS_ARRIVED
FROM product_order
END
