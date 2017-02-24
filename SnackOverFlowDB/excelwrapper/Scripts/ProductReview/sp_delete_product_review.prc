USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_product_review')
BEGIN
DROP PROCEDURE sp_delete_product_review
Print '' print  ' *** dropping procedure sp_delete_product_review'
End
GO

Print '' print  ' *** creating procedure sp_delete_product_review'
GO
Create PROCEDURE sp_delete_product_review
(
@REVIEW_ID[INT]
)
AS
BEGIN
DELETE FROM product_review
WHERE REVIEW_ID = @REVIEW_ID
END
