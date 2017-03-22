USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_employee')
BEGIN
DROP PROCEDURE sp_update_employee
Print '' print  ' *** dropping procedure sp_update_employee'
End
GO

Print '' print  ' *** creating procedure sp_update_employee'
GO
Create PROCEDURE sp_update_employee
(
@old_EMPLOYEE_ID[INT],
@old_USER_ID[INT],
@new_USER_ID[INT],
@old_SALARY[DECIMAL](8,2)=null,
@new_SALARY[DECIMAL](8,2),
@old_ACTIVE[BIT],
@new_ACTIVE[BIT],
@old_DATE_OF_BIRTH[DATE],
@new_DATE_OF_BIRTH[DATE]
)
AS
BEGIN
UPDATE employee
SET USER_ID = @new_USER_ID, SALARY = @new_SALARY, ACTIVE = @new_ACTIVE, DATE_OF_BIRTH = @new_DATE_OF_BIRTH
WHERE (EMPLOYEE_ID = @old_EMPLOYEE_ID)
AND (USER_ID = @old_USER_ID)
AND (SALARY = @old_SALARY OR ISNULL(SALARY, @old_SALARY) IS NULL)
AND (ACTIVE = @old_ACTIVE)
AND (DATE_OF_BIRTH = @old_DATE_OF_BIRTH)
END
