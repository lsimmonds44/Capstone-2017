USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_employee')
BEGIN
DROP PROCEDURE sp_delete_employee
Print '' print  ' *** dropping procedure sp_delete_employee'
End
GO

Print '' print  ' *** creating procedure sp_delete_employee'
GO
Create PROCEDURE sp_delete_employee
(
@EMPLOYEE_ID[INT]
)
AS
BEGIN
DELETE FROM employee
WHERE EMPLOYEE_ID = @EMPLOYEE_ID
END
