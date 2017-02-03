USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_employee_order_responsibility')
BEGIN
DROP PROCEDURE sp_create_employee_order_responsibility
Print '' print  ' *** dropping procedure sp_create_employee_order_responsibility'
End
GO

Print '' print  ' *** creating procedure sp_create_employee_order_responsibility'
GO
Create PROCEDURE sp_create_employee_order_responsibility
(
@ORDER_ID[INT],
@EMPLOYEE_ID[INT],
@DESCRIPTION[NVARCHAR](200)
)
AS
BEGIN
INSERT INTO EMPLOYEE_ORDER_RESPONSIBILITY (ORDER_ID, EMPLOYEE_ID, DESCRIPTION)
VALUES
(@ORDER_ID, @EMPLOYEE_ID, @DESCRIPTION)
END
