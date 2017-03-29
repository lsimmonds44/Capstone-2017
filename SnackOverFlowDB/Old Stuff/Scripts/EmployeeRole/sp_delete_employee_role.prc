USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_employee_role')
BEGIN
DROP PROCEDURE sp_delete_employee_role
Print '' print  ' *** dropping procedure sp_delete_employee_role'
End
GO

Print '' print  ' *** creating procedure sp_delete_employee_role'
GO
Create PROCEDURE sp_delete_employee_role
(
@EMPLOYEE_ID[INT],
@ROLE_ID[NVARCHAR](250)
)
AS
BEGIN
DELETE FROM employee_role
WHERE EMPLOYEE_ID = @EMPLOYEE_ID
AND ROLE_ID = @ROLE_ID
END
