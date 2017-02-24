USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_category_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_product_category_from_search
Print '' print  ' *** dropping procedure sp_retrieve_product_category_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_category_from_search'
GO
Create PROCEDURE sp_retrieve_product_category_from_search
(
@PRODUCT_ID[INT]=NULL,
@CATEGORY_ID[NVARCHAR](200)=NULL
)
AS
BEGIN
Select PRODUCT_ID, CATEGORY_ID
FROM PRODUCT_CATEGORY
WHERE (PRODUCT_CATEGORY.PRODUCT_ID=@PRODUCT_ID OR @PRODUCT_ID IS NULL)
AND (PRODUCT_CATEGORY.CATEGORY_ID=@CATEGORY_ID OR @CATEGORY_ID IS NULL)
END