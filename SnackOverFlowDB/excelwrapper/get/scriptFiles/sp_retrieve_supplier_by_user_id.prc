USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_supplier_by_user_id')
BEGIN
DROP PROCEDURE sp_retrieve_supplier_by_user_id
Print '' print  ' *** dropping procedure sp_retrieve_supplier_by_user_id'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_supplier_by_user_id'
GO
Create PROCEDURE sp_retrieve_supplier_by_user_id
(
@USER_ID[INT]
)
AS
BEGIN
SELECT SUPPLIER_ID, USER_ID, IS_APPROVED, APPROVED_BY, FARM_NAME, FARM_CITY, FARM_STATE, FARM_TAX_ID
FROM supplier
WHERE USER_ID = @USER_ID
END
