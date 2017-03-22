USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_products_to_customer')
BEGIN
DROP PROCEDURE sp_retrieve_products_to_customer
Print '' print  ' *** dropping procedure sp_retrieve_products_to_customer'
End
GO

print '' print'*** Creating sp_retrieve_products_to_customer'
GO
CREATE PROCEDURE [dbo].[sp_retrieve_products_to_customer]
AS
	BEGIN
		SELECT pd.product_id, pd.name, pd.description, pgp.grade_id, pgp.price, sp.supplier_id, sp.farm_name, pc.category_id, pd.image_binary
		FROM Product pd 
		LEFT JOIN Product_Lot pl ON (pl.product_id = pd.product_id)
		LEFT JOIN Supplier sp ON (sp.supplier_id = pl.supplier_id)
		LEFT JOIN Product_Grade_Price pgp ON (pgp.product_id = pd.product_id)
		LEFT JOIN Product_Category pc ON (pc.product_id = pd.product_id)
		WHERE pd.active = 1
	END
GO
