USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_product_category')
BEGIN
DROP PROCEDURE sp_create_product_category
Print '' print  ' *** dropping procedure sp_create_product_category'
End
GO

Print '' print  ' *** creating procedure sp_create_product_category'
GO
Create PROCEDURE sp_create_product_category
(
@PRODUCT_ID[INT],
@CATEGORY_ID[NVARCHAR](200)
)
AS
BEGIN
INSERT INTO PRODUCT_CATEGORY (PRODUCT_ID, CATEGORY_ID)
VALUES
(@PRODUCT_ID, @CATEGORY_ID)
END
