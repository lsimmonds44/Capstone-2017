USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_role')
BEGIN
DROP PROCEDURE sp_retrieve_role
Print '' print  ' *** dropping procedure sp_retrieve_role'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_role'
GO
Create PROCEDURE sp_retrieve_role
(
@ROLE_ID[NVARCHAR](250)
)
AS
BEGIN
SELECT ROLE_ID, DESCRIPTION
FROM role
WHERE ROLE_ID = @ROLE_ID
END
