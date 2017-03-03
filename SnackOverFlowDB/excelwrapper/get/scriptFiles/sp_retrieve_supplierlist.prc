USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_supplier_list')
BEGIN
Drop PROCEDURE sp_retrieve_supplier_list
Print '' print  ' *** dropping procedure sp_retrieve_supplier_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_supplier_list'
GO
Create PROCEDURE sp_retrieve_supplier_list
AS
BEGIN
SELECT SUPPLIER_ID, USER_ID, IS_APPROVED, APPROVED_BY, FARM_NAME, FARM_CITY, FARM_STATE, FARM_TAX_ID, ACTIVE
FROM supplier
END
