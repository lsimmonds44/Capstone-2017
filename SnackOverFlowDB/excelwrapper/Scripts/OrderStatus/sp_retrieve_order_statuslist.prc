USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_order_status_list')
BEGIN
Drop PROCEDURE sp_retrieve_order_status_list
Print '' print  ' *** dropping procedure sp_retrieve_order_status_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_order_status_list'
GO
Create PROCEDURE sp_retrieve_order_status_list
AS
BEGIN
SELECT ORDER_STATUS_ID
FROM order_status
END
