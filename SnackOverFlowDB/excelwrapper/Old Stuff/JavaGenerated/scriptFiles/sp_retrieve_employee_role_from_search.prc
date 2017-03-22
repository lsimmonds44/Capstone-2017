USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_role_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_employee_role_from_search
Print '' print  ' *** dropping procedure sp_retrieve_employee_role_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_role_from_search'
GO
Create PROCEDURE sp_retrieve_employee_role_from_search
(
@EMPLOYEE_ID[INT]=NULL,
@ROLE_ID[NVARCHAR](250)=NULL
)
AS
BEGIN
Select EMPLOYEE_ID, ROLE_ID
FROM EMPLOYEE_ROLE
WHERE (EMPLOYEE_ROLE.EMPLOYEE_ID=@EMPLOYEE_ID OR @EMPLOYEE_ID IS NULL)
AND (EMPLOYEE_ROLE.ROLE_ID=@ROLE_ID OR @ROLE_ID IS NULL)
END