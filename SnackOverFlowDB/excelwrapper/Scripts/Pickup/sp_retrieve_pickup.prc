USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_pickup')
BEGIN
DROP PROCEDURE sp_retrieve_pickup
Print '' print  ' *** dropping procedure sp_retrieve_pickup'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_pickup'
GO
Create PROCEDURE sp_retrieve_pickup
(
@PICKUP_ID[INT]
)
AS
BEGIN
SELECT PICKUP_ID, SUPPLIER_ID, WAREHOUSE_ID, DRIVER_ID, EMPLOYEE_ID
FROM pickup
WHERE PICKUP_ID = @PICKUP_ID
END
