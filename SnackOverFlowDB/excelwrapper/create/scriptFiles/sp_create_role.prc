USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_role')
BEGIN
DROP PROCEDURE sp_create_role
Print '' print  ' *** dropping procedure sp_create_role'
End
GO

Print '' print  ' *** creating procedure sp_create_role'
GO
Create PROCEDURE sp_create_role
(
@ROLE_ID[NVARCHAR](250),
@DESCRIPTION[NVARCHAR](1000)
)
AS
BEGIN
INSERT INTO ROLE (ROLE_ID, DESCRIPTION)
VALUES
(@ROLE_ID, @DESCRIPTION)
END
