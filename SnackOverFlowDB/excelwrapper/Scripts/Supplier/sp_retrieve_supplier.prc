USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_supplier')
BEGIN
DROP PROCEDURE sp_retrieve_supplier
Print '' print  ' *** dropping procedure sp_retrieve_supplier'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_supplier'
GO
Create PROCEDURE sp_retrieve_supplier
(
@SUPPLIER_ID[INT]
)
AS
BEGIN
SELECT SUPPLIER_ID, USER_ID, IS_APPROVED, APPROVED_BY, FARM_TAX_ID
FROM supplier
WHERE SUPPLIER_ID = @SUPPLIER_ID
END
