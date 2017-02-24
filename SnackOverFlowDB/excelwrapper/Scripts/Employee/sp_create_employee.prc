USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_employee')
BEGIN
DROP PROCEDURE sp_create_employee
Print '' print  ' *** dropping procedure sp_create_employee'
End
GO

Print '' print  ' *** creating procedure sp_create_employee'
GO
Create PROCEDURE sp_create_employee
(
@USER_ID[INT],
@SALARY[DECIMAL](8,2)= NULL,
@ACTIVE[BIT],
@DATE_OF_BIRTH[DATE]
)
AS
BEGIN
INSERT INTO EMPLOYEE (USER_ID, SALARY, ACTIVE, DATE_OF_BIRTH)
VALUES
(@USER_ID, @SALARY, @ACTIVE, @DATE_OF_BIRTH)
END
