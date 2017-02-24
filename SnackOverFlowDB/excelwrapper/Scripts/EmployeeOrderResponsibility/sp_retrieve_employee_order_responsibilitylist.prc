USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_order_responsibility_list')
BEGIN
Drop PROCEDURE sp_retrieve_employee_order_responsibility_list
Print '' print  ' *** dropping procedure sp_retrieve_employee_order_responsibility_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_order_responsibility_list'
GO
Create PROCEDURE sp_retrieve_employee_order_responsibility_list
AS
BEGIN
SELECT ORDER_ID, EMPLOYEE_ID, DESCRIPTION
FROM employee_order_responsibility
END
