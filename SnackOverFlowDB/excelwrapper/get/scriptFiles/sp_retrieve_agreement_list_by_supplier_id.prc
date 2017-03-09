USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_agreement_list_by_supplier_id')
BEGIN
Drop PROCEDURE sp_retrieve_agreement_list_by_supplier_id
Print '' print  ' *** dropping procedure sp_retrieve_agreement_list_by_supplier_id'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_agreement_list_by_supplier_id'
GO
Create PROCEDURE sp_retrieve_agreement_list_by_supplier_id
(
@SUPPLIER_ID [INT]
)
AS
BEGIN
SELECT AGREEMENT_ID, PRODUCT_ID, SUPPLIER_ID, DATE_SUBMITTED, IS_APPROVED, APPROVED_BY, ACTIVE
FROM agreement
WHERE SUPPLIER_ID = @SUPPLIER_ID
AND ACTIVE = 1
END
