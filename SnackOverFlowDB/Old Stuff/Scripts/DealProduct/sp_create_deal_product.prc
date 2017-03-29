USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_deal_product')
BEGIN
DROP PROCEDURE sp_create_deal_product
Print '' print  ' *** dropping procedure sp_create_deal_product'
End
GO

Print '' print  ' *** creating procedure sp_create_deal_product'
GO
Create PROCEDURE sp_create_deal_product
(
@DEAL_ID[INT],
@PRODUCT_ID[INT],
@ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO DEAL_PRODUCT (DEAL_ID, PRODUCT_ID, ACTIVE)
VALUES
(@DEAL_ID, @PRODUCT_ID, @ACTIVE)
END
