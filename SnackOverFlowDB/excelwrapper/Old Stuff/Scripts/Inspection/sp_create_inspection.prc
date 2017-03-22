USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_inspection')
BEGIN
DROP PROCEDURE sp_create_inspection
Print '' print  ' *** dropping procedure sp_create_inspection'
End
GO

Print '' print  ' *** creating procedure sp_create_inspection'
GO
Create PROCEDURE sp_create_inspection
(
@EMPLOYEE_ID[INT],
@PRODUCT_LOT_ID[INT],
@GRADE_ID[NVARCHAR](250),
@DATE_PERFORMED[DATETIME],
@EXPIRATION_DATE[DATETIME]
)
AS
BEGIN
INSERT INTO INSPECTION (EMPLOYEE_ID, PRODUCT_LOT_ID, GRADE_ID, DATE_PERFORMED, EXPIRATION_DATE)
VALUES
(@EMPLOYEE_ID, @PRODUCT_LOT_ID, @GRADE_ID, @DATE_PERFORMED, @EXPIRATION_DATE)
END
