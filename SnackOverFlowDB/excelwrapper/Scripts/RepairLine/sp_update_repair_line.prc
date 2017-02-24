USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_repair_line')
BEGIN
DROP PROCEDURE sp_update_repair_line
Print '' print  ' *** dropping procedure sp_update_repair_line'
End
GO

Print '' print  ' *** creating procedure sp_update_repair_line'
GO
Create PROCEDURE sp_update_repair_line
(
@old_REPAIR_LINE_ID[INT],
@old_REPAIR_ID[INT],
@new_REPAIR_ID[INT],
@old_REPAIR_DESCRIPTION[NVARCHAR](250),
@new_REPAIR_DESCRIPTION[NVARCHAR](250)
)
AS
BEGIN
UPDATE repair_line
SET REPAIR_ID = @new_REPAIR_ID, REPAIR_DESCRIPTION = @new_REPAIR_DESCRIPTION
WHERE (REPAIR_LINE_ID = @old_REPAIR_LINE_ID)
AND (REPAIR_ID = @old_REPAIR_ID)
AND (REPAIR_DESCRIPTION = @old_REPAIR_DESCRIPTION)
END
