USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_inspection')
BEGIN
DROP PROCEDURE sp_update_inspection
Print '' print  ' *** dropping procedure sp_update_inspection'
End
GO

Print '' print  ' *** creating procedure sp_update_inspection'
GO
Create PROCEDURE sp_update_inspection
(
@old_INSPECTION_ID[INT],
@old_EMPLOYEE_ID[INT],
@new_EMPLOYEE_ID[INT],
@old_PRODUCT_LOT_ID[INT],
@new_PRODUCT_LOT_ID[INT],
@old_GRADE_ID[NVARCHAR](250),
@new_GRADE_ID[NVARCHAR](250),
@old_DATE_PERFORMED[DATETIME],
@new_DATE_PERFORMED[DATETIME],
@old_EXPIRATION_DATE[DATETIME],
@new_EXPIRATION_DATE[DATETIME]
)
AS
BEGIN
UPDATE inspection
SET EMPLOYEE_ID = @new_EMPLOYEE_ID, PRODUCT_LOT_ID = @new_PRODUCT_LOT_ID, GRADE_ID = @new_GRADE_ID, DATE_PERFORMED = @new_DATE_PERFORMED, EXPIRATION_DATE = @new_EXPIRATION_DATE
WHERE (INSPECTION_ID = @old_INSPECTION_ID)
AND (EMPLOYEE_ID = @old_EMPLOYEE_ID)
AND (PRODUCT_LOT_ID = @old_PRODUCT_LOT_ID)
AND (GRADE_ID = @old_GRADE_ID)
AND (DATE_PERFORMED = @old_DATE_PERFORMED)
AND (EXPIRATION_DATE = @old_EXPIRATION_DATE)
END