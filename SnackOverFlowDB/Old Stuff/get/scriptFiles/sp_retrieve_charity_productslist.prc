USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_charity_products_list')
BEGIN
Drop PROCEDURE sp_retrieve_charity_products_list
Print '' print  ' *** dropping procedure sp_retrieve_charity_products_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_charity_products_list'
GO
Create PROCEDURE sp_retrieve_charity_products_list
AS
BEGIN
SELECT CHARITY_ID, PRODUCT_LOT_ID, QUANTITY
FROM charity_products
END
