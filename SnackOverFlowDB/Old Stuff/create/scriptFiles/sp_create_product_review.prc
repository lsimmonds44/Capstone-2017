USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_product_review')
BEGIN
DROP PROCEDURE sp_create_product_review
Print '' print  ' *** dropping procedure sp_create_product_review'
End
GO

Print '' print  ' *** creating procedure sp_create_product_review'
GO
Create PROCEDURE sp_create_product_review
(
@PRODUCT_ID[INT],
@USER_ID[INT],
@SUPPLIER_ID[INT],
@RATING[INT],
@NOTES[NVARCHAR](1000)
)
AS
BEGIN
INSERT INTO PRODUCT_REVIEW (PRODUCT_ID, USER_ID, SUPPLIER_ID, RATING, NOTES)
VALUES
(@PRODUCT_ID, @USER_ID, @SUPPLIER_ID, @RATING, @NOTES)
END
