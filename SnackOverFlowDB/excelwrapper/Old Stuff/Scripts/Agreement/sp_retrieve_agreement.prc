USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_agreement')
BEGIN
DROP PROCEDURE sp_retrieve_agreement
Print '' print  ' *** dropping procedure sp_retrieve_agreement'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_agreement'
GO
Create PROCEDURE sp_retrieve_agreement
(
@AGREEMENT_ID[INT]
)
AS
BEGIN
SELECT AGREEMENT_ID, PRODUCT_ID, SUPPLIER_ID, DATE_SUBMITTED, IS_APPROVED, APPROVED_BY
FROM agreement
WHERE AGREEMENT_ID = @AGREEMENT_ID
END
