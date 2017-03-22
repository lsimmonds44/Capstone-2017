USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_review_list')
BEGIN
Drop PROCEDURE sp_retrieve_product_review_list
Print '' print  ' *** dropping procedure sp_retrieve_product_review_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_review_list'
GO
Create PROCEDURE sp_retrieve_product_review_list
AS
BEGIN
SELECT REVIEW_ID, PRODUCT_ID, USER_ID, SUPPLIER_ID, RATING, NOTES
FROM product_review
END
