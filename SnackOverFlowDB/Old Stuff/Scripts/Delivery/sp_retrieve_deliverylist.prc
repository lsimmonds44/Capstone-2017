USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_delivery_list')
BEGIN
Drop PROCEDURE sp_retrieve_delivery_list
Print '' print  ' *** dropping procedure sp_retrieve_delivery_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_delivery_list'
GO
Create PROCEDURE sp_retrieve_delivery_list
AS
BEGIN
SELECT DELIVERY_ID, ROUTE_ID, DEVLIVERY_DATE, VERIFICATION, STATUS_ID, DELIVERY_TYPE_ID, ORDER_ID
FROM delivery
END
