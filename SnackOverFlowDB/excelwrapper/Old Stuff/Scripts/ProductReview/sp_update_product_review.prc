USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_product_review')
BEGIN
DROP PROCEDURE sp_update_product_review
Print '' print  ' *** dropping procedure sp_update_product_review'
End
GO

Print '' print  ' *** creating procedure sp_update_product_review'
GO
Create PROCEDURE sp_update_product_review
(
@old_REVIEW_ID[INT],
@old_PRODUCT_ID[INT],
@new_PRODUCT_ID[INT],
@old_USER_ID[INT],
@new_USER_ID[INT],
@old_SUPPLIER_ID[INT],
@new_SUPPLIER_ID[INT],
@old_RATING[INT],
@new_RATING[INT],
@old_NOTES[NVARCHAR](1000),
@new_NOTES[NVARCHAR](1000)
)
AS
BEGIN
UPDATE product_review
SET PRODUCT_ID = @new_PRODUCT_ID, USER_ID = @new_USER_ID, SUPPLIER_ID = @new_SUPPLIER_ID, RATING = @new_RATING, NOTES = @new_NOTES
WHERE (REVIEW_ID = @old_REVIEW_ID)
AND (PRODUCT_ID = @old_PRODUCT_ID)
AND (USER_ID = @old_USER_ID)
AND (SUPPLIER_ID = @old_SUPPLIER_ID)
AND (RATING = @old_RATING)
AND (NOTES = @old_NOTES)
END
