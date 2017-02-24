USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_agreement')
BEGIN
DROP PROCEDURE sp_update_agreement
Print '' print  ' *** dropping procedure sp_update_agreement'
End
GO

Print '' print  ' *** creating procedure sp_update_agreement'
GO
Create PROCEDURE sp_update_agreement
(
@old_AGREEMENT_ID[INT],
@old_PRODUCT_ID[INT],
@new_PRODUCT_ID[INT],
@old_SUPPLIER_ID[INT],
@new_SUPPLIER_ID[INT],
@old_DATE_SUBMITTED[DATETIME],
@new_DATE_SUBMITTED[DATETIME],
@old_IS_APPROVED[BIT],
@new_IS_APPROVED[BIT],
@old_APPROVED_BY[INT],
@new_APPROVED_BY[INT]
)
AS
BEGIN
UPDATE agreement
SET PRODUCT_ID = @new_PRODUCT_ID, SUPPLIER_ID = @new_SUPPLIER_ID, DATE_SUBMITTED = @new_DATE_SUBMITTED, IS_APPROVED = @new_IS_APPROVED, APPROVED_BY = @new_APPROVED_BY
WHERE (AGREEMENT_ID = @old_AGREEMENT_ID)
AND (PRODUCT_ID = @old_PRODUCT_ID)
AND (SUPPLIER_ID = @old_SUPPLIER_ID)
AND (DATE_SUBMITTED = @old_DATE_SUBMITTED)
AND (IS_APPROVED = @old_IS_APPROVED)
AND (APPROVED_BY = @old_APPROVED_BY)
END
