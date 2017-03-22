USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_role')
BEGIN
DROP PROCEDURE sp_delete_role
Print '' print  ' *** dropping procedure sp_delete_role'
End
GO

Print '' print  ' *** creating procedure sp_delete_role'
GO
Create PROCEDURE sp_delete_role
(
@ROLE_ID[NVARCHAR](250)
)
AS
BEGIN
DELETE FROM role
WHERE ROLE_ID = @ROLE_ID
END
