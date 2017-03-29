USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_product_price')
BEGIN
DROP PROCEDURE sp_update_product_price
Print '' print  ' *** dropping procedure sp_update_product_price'
End
GO

Print '' print  ' *** creating procedure sp_update_product_price'
GO
Create PROCEDURE sp_update_product_price
(
	@PRODUCT_ID[INT],
	@old_PRICE[DECIMAL](5,2),
	@new_PRICE[DECIMAL](5,2)
)
AS
	BEGIN
		UPDATE product_grade_price
		SET PRICE = @new_PRICE
		WHERE (PRODUCT_ID = @PRODUCT_ID)
		AND (PRICE = @old_PRICE)
	END
GO
