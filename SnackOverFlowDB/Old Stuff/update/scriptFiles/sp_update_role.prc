USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_role')
BEGIN
DROP PROCEDURE sp_update_role
Print '' print  ' *** dropping procedure sp_update_role'
End
GO

Print '' print  ' *** creating procedure sp_update_role'
GO
Create PROCEDURE sp_update_role
(
@old_ROLE_ID[NVARCHAR](250),
@old_DESCRIPTION[NVARCHAR](1000),
@new_DESCRIPTION[NVARCHAR](1000)
)
AS
BEGIN
UPDATE role
SET DESCRIPTION = @new_DESCRIPTION
WHERE (ROLE_ID = @old_ROLE_ID)
AND (DESCRIPTION = @old_DESCRIPTION)
END
