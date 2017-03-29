USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_commercial')
BEGIN
DROP PROCEDURE sp_update_commercial
Print '' print  ' *** dropping procedure sp_update_commercial'
End
GO

Print '' print  ' *** creating procedure sp_update_commercial'
GO
Create PROCEDURE sp_update_commercial
(
@old_COMMERCIAL_ID[INT],
@old_USER_ID[INT],
@new_USER_ID[INT]
)
AS
BEGIN
UPDATE commercial
SET USER_ID = @new_USER_ID
WHERE (COMMERCIAL_ID = @old_COMMERCIAL_ID)
AND (USER_ID = @old_USER_ID)
END
