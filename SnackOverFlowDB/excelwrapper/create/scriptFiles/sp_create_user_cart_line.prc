USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_user_cart_line')
BEGIN
DROP PROCEDURE sp_create_user_cart_line
Print '' print  ' *** dropping procedure sp_create_user_cart_line'
End
GO

Print '' print  ' *** creating procedure sp_create_user_cart_line'
GO
Create PROCEDURE sp_create_user_cart_line
(
@PRODUCT_ID[INT],
@USER_ID[INT]
)
AS
BEGIN
INSERT INTO USER_CART_LINE (PRODUCT_ID, USER_ID)
VALUES
(@PRODUCT_ID, @USER_ID)
END
