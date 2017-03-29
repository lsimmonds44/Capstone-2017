USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_maintenance_schedule_line')
BEGIN
DROP PROCEDURE sp_update_maintenance_schedule_line
Print '' print  ' *** dropping procedure sp_update_maintenance_schedule_line'
End
GO

Print '' print  ' *** creating procedure sp_update_maintenance_schedule_line'
GO
Create PROCEDURE sp_update_maintenance_schedule_line
(
@old_MAINTENANCE_SCHEDULE_LINE_ID[INT],
@old_MAINTENANCE_SCHEDULE_ID[INT],
@new_MAINTENANCE_SCHEDULE_ID[INT],
@old_DESCRIPTION[NVARCHAR](250),
@new_DESCRIPTION[NVARCHAR](250),
@old_MAINTENANCE_DATE[DATE],
@new_MAINTENANCE_DATE[DATE]
)
AS
BEGIN
UPDATE maintenance_schedule_line
SET MAINTENANCE_SCHEDULE_ID = @new_MAINTENANCE_SCHEDULE_ID, DESCRIPTION = @new_DESCRIPTION, MAINTENANCE_DATE = @new_MAINTENANCE_DATE
WHERE (MAINTENANCE_SCHEDULE_LINE_ID = @old_MAINTENANCE_SCHEDULE_LINE_ID)
AND (MAINTENANCE_SCHEDULE_ID = @old_MAINTENANCE_SCHEDULE_ID)
AND (DESCRIPTION = @old_DESCRIPTION)
AND (MAINTENANCE_DATE = @old_MAINTENANCE_DATE)
END
