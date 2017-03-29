USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_deal_product')
BEGIN
DROP PROCEDURE sp_delete_deal_product
Print '' print  ' *** dropping procedure sp_delete_deal_product'
End
GO

Print '' print  ' *** creating procedure sp_delete_deal_product'
GO
Create PROCEDURE sp_delete_deal_product
(
@DEAL_ID[INT],
@PRODUCT_ID[INT]
)
AS
BEGIN
DELETE FROM deal_product
WHERE DEAL_ID = @DEAL_ID
AND PRODUCT_ID = @PRODUCT_ID
END
