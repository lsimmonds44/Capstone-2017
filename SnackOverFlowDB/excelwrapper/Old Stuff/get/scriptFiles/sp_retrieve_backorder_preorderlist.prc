USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_backorder_preorder_list')
BEGIN
Drop PROCEDURE sp_retrieve_backorder_preorder_list
Print '' print  ' *** dropping procedure sp_retrieve_backorder_preorder_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_backorder_preorder_list'
GO
Create PROCEDURE sp_retrieve_backorder_preorder_list
AS
BEGIN
SELECT BACKORDER_PREORDER_ID, ORDER_ID, CUSTOMER_ID, AMOUNT, DATE_PLACED, DATE_EXPECTED, HAS_ARRIVED, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP
FROM backorder_preorder
END
