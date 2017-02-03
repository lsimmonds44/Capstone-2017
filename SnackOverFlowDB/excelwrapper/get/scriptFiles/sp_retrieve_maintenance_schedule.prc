USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_maintenance_schedule')
BEGIN
DROP PROCEDURE sp_retrieve_maintenance_schedule
Print '' print  ' *** dropping procedure sp_retrieve_maintenance_schedule'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_maintenance_schedule'
GO
Create PROCEDURE sp_retrieve_maintenance_schedule
(
@MAINTENANCE_SCHEDULE_ID[INT]
)
AS
BEGIN
SELECT MAINTENANCE_SCHEDULE_ID, VEHICLE_ID
FROM maintenance_schedule
WHERE MAINTENANCE_SCHEDULE_ID = @MAINTENANCE_SCHEDULE_ID
END
