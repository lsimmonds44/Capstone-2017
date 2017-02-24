USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_order_type')
BEGIN
DROP PROCEDURE sp_delete_order_type
Print '' print  ' *** dropping procedure sp_delete_order_type'
End
GO

Print '' print  ' *** creating procedure sp_delete_order_type'
GO
Create PROCEDURE sp_delete_order_type
(
@ORDER_TYPE_ID[NVARCHAR](250)
)
AS
BEGIN
DELETE FROM order_type
WHERE ORDER_TYPE_ID = @ORDER_TYPE_ID
END
