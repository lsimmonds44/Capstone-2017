USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_grade_price_list')
BEGIN
Drop PROCEDURE sp_retrieve_product_grade_price_list
Print '' print  ' *** dropping procedure sp_retrieve_product_grade_price_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_grade_price_list'
GO
Create PROCEDURE sp_retrieve_product_grade_price_list
AS
BEGIN
SELECT PRODUCT_ID, GRADE_ID, PRICE
FROM product_grade_price
END
