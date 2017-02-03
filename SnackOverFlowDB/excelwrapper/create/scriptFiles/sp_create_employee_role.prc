USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_employee_role')
BEGIN
DROP PROCEDURE sp_create_employee_role
Print '' print  ' *** dropping procedure sp_create_employee_role'
End
GO

Print '' print  ' *** creating procedure sp_create_employee_role'
GO
Create PROCEDURE sp_create_employee_role
(
@EMPLOYEE_ID[INT],
@ROLE_ID[NVARCHAR](250)
)
AS
BEGIN
INSERT INTO EMPLOYEE_ROLE (EMPLOYEE_ID, ROLE_ID)
VALUES
(@EMPLOYEE_ID, @ROLE_ID)
END
