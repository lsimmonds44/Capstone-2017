USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_pickup')
BEGIN
DROP PROCEDURE sp_update_pickup
Print '' print  ' *** dropping procedure sp_update_pickup'
End
GO

Print '' print  ' *** creating procedure sp_update_pickup'
GO
Create PROCEDURE sp_update_pickup
(
@old_PICKUP_ID[INT],
@old_SUPPLIER_ID[INT],
@new_SUPPLIER_ID[INT],
@old_WAREHOUSE_ID[INT],
@new_WAREHOUSE_ID[INT],
@old_DRIVER_ID[INT]=null,
@new_DRIVER_ID[INT],
@old_EMPLOYEE_ID[INT]=null,
@new_EMPLOYEE_ID[INT]
)
AS
BEGIN
UPDATE pickup
SET SUPPLIER_ID = @new_SUPPLIER_ID, WAREHOUSE_ID = @new_WAREHOUSE_ID, DRIVER_ID = @new_DRIVER_ID, EMPLOYEE_ID = @new_EMPLOYEE_ID
WHERE (PICKUP_ID = @old_PICKUP_ID)
AND (SUPPLIER_ID = @old_SUPPLIER_ID)
AND (WAREHOUSE_ID = @old_WAREHOUSE_ID)
AND (DRIVER_ID = @old_DRIVER_ID OR ISNULL(DRIVER_ID, @old_DRIVER_ID) IS NULL)
AND (EMPLOYEE_ID = @old_EMPLOYEE_ID OR ISNULL(EMPLOYEE_ID, @old_EMPLOYEE_ID) IS NULL)
END
