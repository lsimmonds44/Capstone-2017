USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_role_list')
BEGIN
Drop PROCEDURE sp_retrieve_employee_role_list
Print '' print  ' *** dropping procedure sp_retrieve_employee_role_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_role_list'
GO
Create PROCEDURE sp_retrieve_employee_role_list
AS
BEGIN
SELECT EMPLOYEE_ID, ROLE_ID
FROM employee_role
END
