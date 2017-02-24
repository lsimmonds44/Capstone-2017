USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_location')
BEGIN
DROP PROCEDURE sp_update_location
Print '' print  ' *** dropping procedure sp_update_location'
End
GO

Print '' print  ' *** creating procedure sp_update_location'
GO
Create PROCEDURE sp_update_location
(
@old_LOCATION_ID[INT],
@old_DESCRIPTION[NVARCHAR](250),
@new_DESCRIPTION[NVARCHAR](250)
)
AS
BEGIN
UPDATE location
SET DESCRIPTION = @new_DESCRIPTION
WHERE (LOCATION_ID = @old_LOCATION_ID)
AND (DESCRIPTION = @old_DESCRIPTION)
END
