USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_order_status')
BEGIN
DROP PROCEDURE sp_delete_order_status
Print '' print  ' *** dropping procedure sp_delete_order_status'
End
GO

Print '' print  ' *** creating procedure sp_delete_order_status'
GO
Create PROCEDURE sp_delete_order_status
(
@ORDER_STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
DELETE FROM order_status
WHERE ORDER_STATUS_ID = @ORDER_STATUS_ID
END
