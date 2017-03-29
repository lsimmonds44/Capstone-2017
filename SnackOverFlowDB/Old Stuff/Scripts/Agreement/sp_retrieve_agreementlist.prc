USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_agreement_list')
BEGIN
Drop PROCEDURE sp_retrieve_agreement_list
Print '' print  ' *** dropping procedure sp_retrieve_agreement_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_agreement_list'
GO
Create PROCEDURE sp_retrieve_agreement_list
AS
BEGIN
SELECT AGREEMENT_ID, PRODUCT_ID, SUPPLIER_ID, DATE_SUBMITTED, IS_APPROVED, APPROVED_BY
FROM agreement
END
