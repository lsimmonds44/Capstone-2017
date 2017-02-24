USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal_product_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_deal_product_from_search
Print '' print  ' *** dropping procedure sp_retrieve_deal_product_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal_product_from_search'
GO
Create PROCEDURE sp_retrieve_deal_product_from_search
(
@DEAL_ID[INT]=NULL,
@PRODUCT_ID[INT]=NULL,
@ACTIVE[BIT]=NULL
)
AS
BEGIN
Select DEAL_ID, PRODUCT_ID, ACTIVE
FROM DEAL_PRODUCT
WHERE (DEAL_PRODUCT.DEAL_ID=@DEAL_ID OR @DEAL_ID IS NULL)
AND (DEAL_PRODUCT.PRODUCT_ID=@PRODUCT_ID OR @PRODUCT_ID IS NULL)
AND (DEAL_PRODUCT.ACTIVE=@ACTIVE OR @ACTIVE IS NULL)
END