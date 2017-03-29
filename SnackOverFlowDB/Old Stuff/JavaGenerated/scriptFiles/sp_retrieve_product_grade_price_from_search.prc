USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_product_grade_price_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_product_grade_price_from_search
Print '' print  ' *** dropping procedure sp_retrieve_product_grade_price_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_product_grade_price_from_search'
GO
Create PROCEDURE sp_retrieve_product_grade_price_from_search
(
@PRODUCT_ID[INT]=NULL,
@GRADE_ID[NVARCHAR](250)=NULL,
@PRICE[DECIMAL](5)=NULL
)
AS
BEGIN
Select PRODUCT_ID, GRADE_ID, PRICE
FROM PRODUCT_GRADE_PRICE
WHERE (PRODUCT_GRADE_PRICE.PRODUCT_ID=@PRODUCT_ID OR @PRODUCT_ID IS NULL)
AND (PRODUCT_GRADE_PRICE.GRADE_ID=@GRADE_ID OR @GRADE_ID IS NULL)
AND (PRODUCT_GRADE_PRICE.PRICE=@PRICE OR @PRICE IS NULL)
END