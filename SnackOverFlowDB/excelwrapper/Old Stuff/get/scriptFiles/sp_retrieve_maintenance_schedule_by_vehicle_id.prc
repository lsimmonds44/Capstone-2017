USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_maintenance_schedule_by_vehicle_id')
BEGIN
DROP PROCEDURE sp_retrieve_maintenance_schedule_by_vehicle_id
Print '' print  ' *** dropping procedure sp_retrieve_maintenance_schedule_by_vehicle_id'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_maintenance_schedule_by_vehicle_id'
GO
Create PROCEDURE sp_retrieve_maintenance_schedule_by_vehicle_id
(
@VEHICLE_ID[INT]
)
AS
BEGIN
SELECT MAINTENANCE_SCHEDULE_ID, VEHICLE_ID
FROM maintenance_schedule
WHERE VEHICLE_ID = @VEHICLE_ID
END
