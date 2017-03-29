USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_order_responsibility')
BEGIN
DROP PROCEDURE sp_retrieve_employee_order_responsibility
Print '' print  ' *** dropping procedure sp_retrieve_employee_order_responsibility'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_order_responsibility'
GO
Create PROCEDURE sp_retrieve_employee_order_responsibility
(
@ORDER_ID[INT],
@EMPLOYEE_ID[INT]
)
AS
BEGIN
SELECT ORDER_ID, EMPLOYEE_ID, DESCRIPTION
FROM employee_order_responsibility
WHERE ORDER_ID = @ORDER_ID
AND EMPLOYEE_ID = @EMPLOYEE_ID
END
