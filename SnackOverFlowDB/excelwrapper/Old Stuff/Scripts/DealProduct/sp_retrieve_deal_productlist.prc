USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal_product_list')
BEGIN
Drop PROCEDURE sp_retrieve_deal_product_list
Print '' print  ' *** dropping procedure sp_retrieve_deal_product_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal_product_list'
GO
Create PROCEDURE sp_retrieve_deal_product_list
AS
BEGIN
SELECT DEAL_ID, PRODUCT_ID, ACTIVE
FROM deal_product
END
