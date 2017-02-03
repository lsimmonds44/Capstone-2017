USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_list')
BEGIN
Drop PROCEDURE sp_retrieve_employee_list
Print '' print  ' *** dropping procedure sp_retrieve_employee_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_list'
GO
Create PROCEDURE sp_retrieve_employee_list
AS
BEGIN
SELECT EMPLOYEE_ID, USER_ID, SALARY, ACTIVE, DATE_OF_BIRTH
FROM employee
END
