USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_user_cart_line')
BEGIN
DROP PROCEDURE sp_retrieve_user_cart_line
Print '' print  ' *** dropping procedure sp_retrieve_user_cart_line'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_user_cart_line'
GO
Create PROCEDURE sp_retrieve_user_cart_line
(
@PRODUCT_ID[INT],
@USER_ID[INT]
)
AS
BEGIN
SELECT PRODUCT_ID, USER_ID
FROM user_cart_line
WHERE PRODUCT_ID = @PRODUCT_ID
AND USER_ID = @USER_ID
END
