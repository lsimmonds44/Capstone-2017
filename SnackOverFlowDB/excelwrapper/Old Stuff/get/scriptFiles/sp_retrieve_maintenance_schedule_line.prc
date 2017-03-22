USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_maintenance_schedule_line')
BEGIN
DROP PROCEDURE sp_retrieve_maintenance_schedule_line
Print '' print  ' *** dropping procedure sp_retrieve_maintenance_schedule_line'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_maintenance_schedule_line'
GO
Create PROCEDURE sp_retrieve_maintenance_schedule_line
(
@MAINTENANCE_SCHEDULE_LINE_ID[INT]
)
AS
BEGIN
SELECT MAINTENANCE_SCHEDULE_LINE_ID, MAINTENANCE_SCHEDULE_ID, DESCRIPTION, MAINTENANCE_DATE
FROM maintenance_schedule_line
WHERE MAINTENANCE_SCHEDULE_LINE_ID = @MAINTENANCE_SCHEDULE_LINE_ID
END
