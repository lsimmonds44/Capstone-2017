USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_employee_order_responsibility')
BEGIN
DROP PROCEDURE sp_update_employee_order_responsibility
Print '' print  ' *** dropping procedure sp_update_employee_order_responsibility'
End
GO

Print '' print  ' *** creating procedure sp_update_employee_order_responsibility'
GO
Create PROCEDURE sp_update_employee_order_responsibility
(
@old_ORDER_ID[INT],
@old_EMPLOYEE_ID[INT],
@old_DESCRIPTION[NVARCHAR](200),
@new_DESCRIPTION[NVARCHAR](200)
)
AS
BEGIN
UPDATE employee_order_responsibility
SET DESCRIPTION = @new_DESCRIPTION
WHERE (ORDER_ID = @old_ORDER_ID)
AND (EMPLOYEE_ID = @old_EMPLOYEE_ID)
AND (DESCRIPTION = @old_DESCRIPTION)
END
