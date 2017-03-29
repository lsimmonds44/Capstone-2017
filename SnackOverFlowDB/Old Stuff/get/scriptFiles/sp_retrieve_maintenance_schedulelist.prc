USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_maintenance_schedule_list')
BEGIN
Drop PROCEDURE sp_retrieve_maintenance_schedule_list
Print '' print  ' *** dropping procedure sp_retrieve_maintenance_schedule_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_maintenance_schedule_list'
GO
Create PROCEDURE sp_retrieve_maintenance_schedule_list
AS
BEGIN
SELECT MAINTENANCE_SCHEDULE_ID, VEHICLE_ID
FROM maintenance_schedule
END
