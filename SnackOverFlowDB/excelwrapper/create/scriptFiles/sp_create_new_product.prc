USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_product')
BEGIN
DROP PROCEDURE sp_create_new_product
Print '' print  ' *** dropping procedure sp_create_new_product'
End
GO

print '' print '*** Creating sp_create_new_product'
GO
CREATE PROCEDURE [dbo].[sp_create_new_product]
	(
			@Name				[nvarchar](50)	,
			@Description		[nvarchar](200)	,
			@Unit_Price			[decimal](10,2)	,
			@Image_Binary		[image]			,
			@Active				[bit]			,
			@Unit_Of_Measurement [nvarchar](20)	,
			@DELIVERY_CHARGE_PER_UNIT	[decimal](5,2)	
	)
AS
	BEGIN
		INSERT INTO [dbo].[Product]
		(
			Name						,				
			Description					,
			Unit_Price					,
			Image_Binary				,
			Active						,
			Unit_Of_Measurement 		,
			DELIVERY_CHARGE_PER_UNIT	
		)
		VALUES
		(
			@Name						,				
			@Description				,
			@Unit_Price					,
			@Image_Binary				,
			@Active						,
			@Unit_Of_Measurement 		,
			@DELIVERY_CHARGE_PER_UNIT	
		)	
		RETURN @@ROWCOUNT
	END
GO
