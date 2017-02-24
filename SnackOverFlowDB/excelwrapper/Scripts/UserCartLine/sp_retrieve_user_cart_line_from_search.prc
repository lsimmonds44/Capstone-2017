USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_user_cart_line_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_user_cart_line_from_search
Print '' print  ' *** dropping procedure sp_retrieve_user_cart_line_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_user_cart_line_from_search'
GO
Create PROCEDURE sp_retrieve_user_cart_line_from_search
(
@PRODUCT_ID[INT]=NULL,
@USER_ID[INT]=NULL
)
AS
BEGIN
Select PRODUCT_ID, USER_ID
FROM USER_CART_LINE
WHERE (USER_CART_LINE.PRODUCT_ID=@PRODUCT_ID OR @PRODUCT_ID IS NULL)
AND (USER_CART_LINE.USER_ID=@USER_ID OR @USER_ID IS NULL)
END