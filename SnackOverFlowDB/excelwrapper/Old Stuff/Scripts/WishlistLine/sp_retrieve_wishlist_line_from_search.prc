USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_wishlist_line_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_wishlist_line_from_search
Print '' print  ' *** dropping procedure sp_retrieve_wishlist_line_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_wishlist_line_from_search'
GO
Create PROCEDURE sp_retrieve_wishlist_line_from_search
(
@CUSTOMER_ID[INT]=NULL,
@PRODUCT_ID[INT]=NULL
)
AS
BEGIN
Select CUSTOMER_ID, PRODUCT_ID
FROM WISHLIST_LINE
WHERE (WISHLIST_LINE.CUSTOMER_ID=@CUSTOMER_ID OR @CUSTOMER_ID IS NULL)
AND (WISHLIST_LINE.PRODUCT_ID=@PRODUCT_ID OR @PRODUCT_ID IS NULL)
END