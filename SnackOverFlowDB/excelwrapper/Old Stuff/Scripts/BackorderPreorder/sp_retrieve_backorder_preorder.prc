USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_backorder_preorder')
BEGIN
DROP PROCEDURE sp_retrieve_backorder_preorder
Print '' print  ' *** dropping procedure sp_retrieve_backorder_preorder'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_backorder_preorder'
GO
Create PROCEDURE sp_retrieve_backorder_preorder
(
@BACKORDER_PREORDER_ID[INT]
)
AS
BEGIN
SELECT BACKORDER_PREORDER_ID, ORDER_ID, CUSTOMER_ID, AMOUNT, DATE_PLACED, DATE_EXPECTED, HAS_ARRIVED, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP
FROM backorder_preorder
WHERE BACKORDER_PREORDER_ID = @BACKORDER_PREORDER_ID
END
