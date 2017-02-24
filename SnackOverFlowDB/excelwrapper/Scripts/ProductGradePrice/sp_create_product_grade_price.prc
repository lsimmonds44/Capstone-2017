USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_product_grade_price')
BEGIN
DROP PROCEDURE sp_create_product_grade_price
Print '' print  ' *** dropping procedure sp_create_product_grade_price'
End
GO

Print '' print  ' *** creating procedure sp_create_product_grade_price'
GO
Create PROCEDURE sp_create_product_grade_price
(
@PRODUCT_ID[INT],
@GRADE_ID[NVARCHAR](250),
@PRICE[DECIMAL](5,2)
)
AS
BEGIN
INSERT INTO PRODUCT_GRADE_PRICE (PRODUCT_ID, GRADE_ID, PRICE)
VALUES
(@PRODUCT_ID, @GRADE_ID, @PRICE)
END
