USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_pickup_line_list')
BEGIN
Drop PROCEDURE sp_retrieve_pickup_line_list
Print '' print  ' *** dropping procedure sp_retrieve_pickup_line_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_pickup_line_list'
GO
Create PROCEDURE sp_retrieve_pickup_line_list
AS
BEGIN
SELECT PICKUP_LINE_ID, PICKUP_ID, PRODUCT_LOT_ID, QUANTITY, PICK_UP_STATUS
FROM pickup_line
END
