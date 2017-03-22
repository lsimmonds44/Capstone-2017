USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee')
BEGIN
DROP PROCEDURE sp_retrieve_employee
Print '' print  ' *** dropping procedure sp_retrieve_employee'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee'
GO
Create PROCEDURE sp_retrieve_employee
(
@EMPLOYEE_ID[INT]
)
AS
BEGIN
SELECT EMPLOYEE_ID, USER_ID, SALARY, ACTIVE, DATE_OF_BIRTH
FROM employee
WHERE EMPLOYEE_ID = @EMPLOYEE_ID
END
