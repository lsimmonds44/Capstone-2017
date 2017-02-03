USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_maintenance_schedule')
BEGIN
DROP PROCEDURE sp_create_maintenance_schedule
Print '' print  ' *** dropping procedure sp_create_maintenance_schedule'
End
GO

Print '' print  ' *** creating procedure sp_create_maintenance_schedule'
GO
Create PROCEDURE sp_create_maintenance_schedule
(
@VEHICLE_ID[INT]
)
AS
BEGIN
INSERT INTO MAINTENANCE_SCHEDULE (VEHICLE_ID)
VALUES
(@VEHICLE_ID)
END
