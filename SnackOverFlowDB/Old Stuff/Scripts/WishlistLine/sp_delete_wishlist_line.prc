USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_wishlist_line')
BEGIN
DROP PROCEDURE sp_delete_wishlist_line
Print '' print  ' *** dropping procedure sp_delete_wishlist_line'
End
GO

Print '' print  ' *** creating procedure sp_delete_wishlist_line'
GO
Create PROCEDURE sp_delete_wishlist_line
(
@CUSTOMER_ID[INT],
@PRODUCT_ID[INT]
)
AS
BEGIN
DELETE FROM wishlist_line
WHERE CUSTOMER_ID = @CUSTOMER_ID
AND PRODUCT_ID = @PRODUCT_ID
END
