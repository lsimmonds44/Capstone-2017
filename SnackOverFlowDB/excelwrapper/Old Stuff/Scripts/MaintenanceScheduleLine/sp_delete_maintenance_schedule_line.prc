USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_maintenance_schedule_line')
BEGIN
DROP PROCEDURE sp_delete_maintenance_schedule_line
Print '' print  ' *** dropping procedure sp_delete_maintenance_schedule_line'
End
GO

Print '' print  ' *** creating procedure sp_delete_maintenance_schedule_line'
GO
Create PROCEDURE sp_delete_maintenance_schedule_line
(
@MAINTENANCE_SCHEDULE_LINE_ID[INT]
)
AS
BEGIN
DELETE FROM maintenance_schedule_line
WHERE MAINTENANCE_SCHEDULE_LINE_ID = @MAINTENANCE_SCHEDULE_LINE_ID
END
