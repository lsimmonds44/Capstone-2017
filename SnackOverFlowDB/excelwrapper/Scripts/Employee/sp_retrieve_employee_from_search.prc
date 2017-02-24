USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_employee_from_search
Print '' print  ' *** dropping procedure sp_retrieve_employee_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_from_search'
GO
Create PROCEDURE sp_retrieve_employee_from_search
(
@EMPLOYEE_ID[INT]=NULL,
@USER_ID[INT]=NULL,
@SALARY[DECIMAL](8)=NULL,@SALARY_ESCAPE[BIT] = NULL,
@ACTIVE[BIT]=NULL,
@DATE_OF_BIRTH[DATE]=NULL
)
AS
BEGIN
Select EMPLOYEE_ID, USER_ID, SALARY, ACTIVE, DATE_OF_BIRTH
FROM EMPLOYEE
WHERE (EMPLOYEE.EMPLOYEE_ID=@EMPLOYEE_ID OR @EMPLOYEE_ID IS NULL)
AND (EMPLOYEE.USER_ID=@USER_ID OR @USER_ID IS NULL)
AND (EMPLOYEE.SALARY=@SALARY OR @SALARY IS NULL OR @SALARY_ESCAPE = 1)
AND (EMPLOYEE.ACTIVE=@ACTIVE OR @ACTIVE IS NULL)
AND (EMPLOYEE.DATE_OF_BIRTH=@DATE_OF_BIRTH OR @DATE_OF_BIRTH IS NULL)
END