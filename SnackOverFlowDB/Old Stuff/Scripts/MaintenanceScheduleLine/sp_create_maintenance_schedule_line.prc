USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_maintenance_schedule_line')
BEGIN
DROP PROCEDURE sp_create_maintenance_schedule_line
Print '' print  ' *** dropping procedure sp_create_maintenance_schedule_line'
End
GO

Print '' print  ' *** creating procedure sp_create_maintenance_schedule_line'
GO
Create PROCEDURE sp_create_maintenance_schedule_line
(
@MAINTENANCE_SCHEDULE_ID[INT],
@DESCRIPTION[NVARCHAR](250),
@MAINTENANCE_DATE[DATE]
)
AS
BEGIN
INSERT INTO MAINTENANCE_SCHEDULE_LINE (MAINTENANCE_SCHEDULE_ID, DESCRIPTION, MAINTENANCE_DATE)
VALUES
(@MAINTENANCE_SCHEDULE_ID, @DESCRIPTION, @MAINTENANCE_DATE)
END
