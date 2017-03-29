USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_maintenance_schedule')
BEGIN
DROP PROCEDURE sp_update_maintenance_schedule
Print '' print  ' *** dropping procedure sp_update_maintenance_schedule'
End
GO

Print '' print  ' *** creating procedure sp_update_maintenance_schedule'
GO
Create PROCEDURE sp_update_maintenance_schedule
(
@old_MAINTENANCE_SCHEDULE_ID[INT],
@old_VEHICLE_ID[INT],
@new_VEHICLE_ID[INT]
)
AS
BEGIN
UPDATE maintenance_schedule
SET VEHICLE_ID = @new_VEHICLE_ID
WHERE (MAINTENANCE_SCHEDULE_ID = @old_MAINTENANCE_SCHEDULE_ID)
AND (VEHICLE_ID = @old_VEHICLE_ID)
END
