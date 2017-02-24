USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_pickup')
BEGIN
DROP PROCEDURE sp_create_pickup
Print '' print  ' *** dropping procedure sp_create_pickup'
End
GO

Print '' print  ' *** creating procedure sp_create_pickup'
GO
Create PROCEDURE sp_create_pickup
(
@SUPPLIER_ID[INT],
@WAREHOUSE_ID[INT],
@DRIVER_ID[INT]= NULL,
@EMPLOYEE_ID[INT]= NULL
)
AS
BEGIN
INSERT INTO PICKUP (SUPPLIER_ID, WAREHOUSE_ID, DRIVER_ID, EMPLOYEE_ID)
VALUES
(@SUPPLIER_ID, @WAREHOUSE_ID, @DRIVER_ID, @EMPLOYEE_ID)
END
