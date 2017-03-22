USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_maintenance_schedule')
BEGIN
DROP PROCEDURE sp_delete_maintenance_schedule
Print '' print  ' *** dropping procedure sp_delete_maintenance_schedule'
End
GO

Print '' print  ' *** creating procedure sp_delete_maintenance_schedule'
GO
Create PROCEDURE sp_delete_maintenance_schedule
(
@MAINTENANCE_SCHEDULE_ID[INT]
)
AS
BEGIN
DELETE FROM maintenance_schedule
WHERE MAINTENANCE_SCHEDULE_ID = @MAINTENANCE_SCHEDULE_ID
END
