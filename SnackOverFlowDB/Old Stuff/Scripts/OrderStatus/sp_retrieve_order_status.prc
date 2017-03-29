USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_order_status')
BEGIN
DROP PROCEDURE sp_retrieve_order_status
Print '' print  ' *** dropping procedure sp_retrieve_order_status'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_order_status'
GO
Create PROCEDURE sp_retrieve_order_status
(
@ORDER_STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
SELECT ORDER_STATUS_ID
FROM order_status
WHERE ORDER_STATUS_ID = @ORDER_STATUS_ID
END
