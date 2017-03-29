USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_by_username')
BEGIN
DROP PROCEDURE sp_retrieve_employee_by_username
Print '' print  ' *** dropping procedure sp_retrieve_employee_by_username'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_by_username'
GO
Create PROCEDURE sp_retrieve_employee_by_username
(
@USER_NAME[NVARCHAR](50)
)
AS
BEGIN
SELECT e.EMPLOYEE_ID, e.USER_ID, e.SALARY, e.ACTIVE, e.DATE_OF_BIRTH
FROM employee e
WHERE e.USER_ID IN
	(SELECT a.USER_ID
	FROM APP_USER a
	WHERE a.USER_NAME = @USER_NAME)
END
