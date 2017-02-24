USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_product_order_status')
BEGIN
DROP PROCEDURE sp_update_product_order_status
Print '' print  ' *** dropping procedure sp_update_product_order_status'
End
GO

Print '' print  ' *** creating procedure sp_update_product_order_status'
GO
Create PROCEDURE sp_update_product_order_status
(
@ORDER_ID[INT],
@NEW_ORDER_STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
UPDATE product_order
SET ORDER_STATUS_ID = @NEW_ORDER_STATUS_ID
WHERE (ORDER_ID = @ORDER_ID)
RETURN @@ROWCOUNT
END
