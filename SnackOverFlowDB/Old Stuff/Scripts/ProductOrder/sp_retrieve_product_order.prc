USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_order')
BEGIN
DROP PROCEDURE sp_retrieve_product_order
Print '' print  ' *** dropping procedure sp_retrieve_product_order'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_order'
GO
Create PROCEDURE sp_retrieve_product_order
(
@ORDER_ID[INT]
)
AS
BEGIN
SELECT ORDER_ID, CUSTOMER_ID, ORDER_TYPE_ID, ADDRESS_TYPE, DELIVERY_TYPE_ID, AMOUNT, ORDER_DATE, DATE_EXPECTED, DISCOUNT, ORDER_STATUS_ID, USER_ADDRESS_ID, HAS_ARRIVED
FROM product_order
WHERE ORDER_ID = @ORDER_ID
END
