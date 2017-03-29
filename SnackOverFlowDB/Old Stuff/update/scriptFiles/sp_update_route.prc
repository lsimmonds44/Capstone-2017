USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_route')
BEGIN
DROP PROCEDURE sp_update_route
Print '' print  ' *** dropping procedure sp_update_route'
End
GO

Print '' print  ' *** creating procedure sp_update_route'
GO
Create PROCEDURE sp_update_route
(
@old_ROUTE_ID[INT],
@old_VEHICLE_ID[INT],
@new_VEHICLE_ID[INT],
@old_DRIVER_ID[INT],
@new_DRIVER_ID[INT],
@old_ASSIGNED_DATE[DATETIME],
@new_ASSIGNED_DATE[DATETIME]
)
AS
BEGIN
UPDATE route
SET VEHICLE_ID = @new_VEHICLE_ID, DRIVER_ID = @new_DRIVER_ID, ASSIGNED_DATE = @new_ASSIGNED_DATE
WHERE (ROUTE_ID = @old_ROUTE_ID)
AND (VEHICLE_ID = @old_VEHICLE_ID)
AND (DRIVER_ID = @old_DRIVER_ID)
AND (ASSIGNED_DATE = @old_ASSIGNED_DATE)
END
