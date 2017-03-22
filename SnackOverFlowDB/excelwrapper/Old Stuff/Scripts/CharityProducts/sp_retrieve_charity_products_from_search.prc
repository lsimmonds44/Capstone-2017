USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_charity_products_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_charity_products_from_search
Print '' print  ' *** dropping procedure sp_retrieve_charity_products_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_charity_products_from_search'
GO
Create PROCEDURE sp_retrieve_charity_products_from_search
(
@CHARITY_ID[INT]=NULL,
@PRODUCT_LOT_ID[INT]=NULL,
@QUANTITY[INT]=NULL
)
AS
BEGIN
Select CHARITY_ID, PRODUCT_LOT_ID, QUANTITY
FROM CHARITY_PRODUCTS
WHERE (CHARITY_PRODUCTS.CHARITY_ID=@CHARITY_ID OR @CHARITY_ID IS NULL)
AND (CHARITY_PRODUCTS.PRODUCT_LOT_ID=@PRODUCT_LOT_ID OR @PRODUCT_LOT_ID IS NULL)
AND (CHARITY_PRODUCTS.QUANTITY=@QUANTITY OR @QUANTITY IS NULL)
END