USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_order_type')
BEGIN
DROP PROCEDURE sp_retrieve_order_type
Print '' print  ' *** dropping procedure sp_retrieve_order_type'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_order_type'
GO
Create PROCEDURE sp_retrieve_order_type
(
@ORDER_TYPE_ID[NVARCHAR](250)
)
AS
BEGIN
SELECT ORDER_TYPE_ID
FROM order_type
WHERE ORDER_TYPE_ID = @ORDER_TYPE_ID
END
