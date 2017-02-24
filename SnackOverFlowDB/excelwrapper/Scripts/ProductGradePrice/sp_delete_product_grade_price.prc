USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_product_grade_price')
BEGIN
DROP PROCEDURE sp_delete_product_grade_price
Print '' print  ' *** dropping procedure sp_delete_product_grade_price'
End
GO

Print '' print  ' *** creating procedure sp_delete_product_grade_price'
GO
Create PROCEDURE sp_delete_product_grade_price
(
@PRODUCT_ID[INT],
@GRADE_ID[NVARCHAR](250)
)
AS
BEGIN
DELETE FROM product_grade_price
WHERE PRODUCT_ID = @PRODUCT_ID
AND GRADE_ID = @GRADE_ID
END
