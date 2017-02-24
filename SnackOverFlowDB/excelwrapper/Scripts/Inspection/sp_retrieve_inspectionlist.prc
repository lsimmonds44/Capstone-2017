USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_inspection_list')
BEGIN
Drop PROCEDURE sp_retrieve_inspection_list
Print '' print  ' *** dropping procedure sp_retrieve_inspection_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_inspection_list'
GO
Create PROCEDURE sp_retrieve_inspection_list
AS
BEGIN
SELECT INSPECTION_ID, EMPLOYEE_ID, PRODUCT_LOT_ID, GRADE_ID, DATE_PERFORMED, EXPIRATION_DATE
FROM inspection
END
