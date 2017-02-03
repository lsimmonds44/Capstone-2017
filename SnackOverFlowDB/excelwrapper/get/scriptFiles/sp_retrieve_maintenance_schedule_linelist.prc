USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_maintenance_schedule_line_list')
BEGIN
Drop PROCEDURE sp_retrieve_maintenance_schedule_line_list
Print '' print  ' *** dropping procedure sp_retrieve_maintenance_schedule_line_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_maintenance_schedule_line_list'
GO
Create PROCEDURE sp_retrieve_maintenance_schedule_line_list
AS
BEGIN
SELECT MAINTENANCE_SCHEDULE_LINE_ID, MAINTENANCE_SCHEDULE_ID, DESCRIPTION, MAINTENANCE_DATE
FROM maintenance_schedule_line
END
