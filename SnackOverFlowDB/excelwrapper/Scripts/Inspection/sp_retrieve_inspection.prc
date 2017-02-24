USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_inspection')
BEGIN
DROP PROCEDURE sp_retrieve_inspection
Print '' print  ' *** dropping procedure sp_retrieve_inspection'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_inspection'
GO
Create PROCEDURE sp_retrieve_inspection
(
@INSPECTION_ID[INT]
)
AS
BEGIN
SELECT INSPECTION_ID, EMPLOYEE_ID, PRODUCT_LOT_ID, GRADE_ID, DATE_PERFORMED, EXPIRATION_DATE
FROM inspection
WHERE INSPECTION_ID = @INSPECTION_ID
END
