USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_repair')
BEGIN
DROP PROCEDURE sp_update_repair
Print '' print  ' *** dropping procedure sp_update_repair'
End
GO

Print '' print  ' *** creating procedure sp_update_repair'
GO
Create PROCEDURE sp_update_repair
(
@old_REPAIR_ID[INT],
@old_VEHICLE_ID[INT],
@new_VEHICLE_ID[INT]
)
AS
BEGIN
UPDATE repair
SET VEHICLE_ID = @new_VEHICLE_ID
WHERE (REPAIR_ID = @old_REPAIR_ID)
AND (VEHICLE_ID = @old_VEHICLE_ID)
END
