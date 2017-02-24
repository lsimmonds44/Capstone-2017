USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_product_grade_price')
BEGIN
DROP PROCEDURE sp_update_product_grade_price
Print '' print  ' *** dropping procedure sp_update_product_grade_price'
End
GO

Print '' print  ' *** creating procedure sp_update_product_grade_price'
GO
Create PROCEDURE sp_update_product_grade_price
(
@old_PRODUCT_ID[INT],
@old_GRADE_ID[NVARCHAR](250),
@old_PRICE[DECIMAL](5,2),
@new_PRICE[DECIMAL](5,2)
)
AS
BEGIN
UPDATE product_grade_price
SET PRICE = @new_PRICE
WHERE (PRODUCT_ID = @old_PRODUCT_ID)
AND (GRADE_ID = @old_GRADE_ID)
AND (PRICE = @old_PRICE)
END
