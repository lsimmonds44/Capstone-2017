USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_maintenance_schedule_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_maintenance_schedule_from_search
Print '' print  ' *** dropping procedure sp_retrieve_maintenance_schedule_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_maintenance_schedule_from_search'
GO
Create PROCEDURE sp_retrieve_maintenance_schedule_from_search
(
@MAINTENANCE_SCHEDULE_ID[INT]=NULL,
@VEHICLE_ID[INT]=NULL
)
AS
BEGIN
Select MAINTENANCE_SCHEDULE_ID, VEHICLE_ID
FROM MAINTENANCE_SCHEDULE
WHERE (MAINTENANCE_SCHEDULE.MAINTENANCE_SCHEDULE_ID=@MAINTENANCE_SCHEDULE_ID OR @MAINTENANCE_SCHEDULE_ID IS NULL)
AND (MAINTENANCE_SCHEDULE.VEHICLE_ID=@VEHICLE_ID OR @VEHICLE_ID IS NULL)
END