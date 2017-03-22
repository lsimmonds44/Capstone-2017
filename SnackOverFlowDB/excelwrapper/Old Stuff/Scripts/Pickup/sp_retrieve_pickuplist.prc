USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_pickup_list')
BEGIN
Drop PROCEDURE sp_retrieve_pickup_list
Print '' print  ' *** dropping procedure sp_retrieve_pickup_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_pickup_list'
GO
Create PROCEDURE sp_retrieve_pickup_list
AS
BEGIN
SELECT PICKUP_ID, SUPPLIER_ID, WAREHOUSE_ID, DRIVER_ID, EMPLOYEE_ID
FROM pickup
END
