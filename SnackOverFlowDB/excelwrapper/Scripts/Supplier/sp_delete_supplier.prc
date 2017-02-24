USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_supplier')
BEGIN
DROP PROCEDURE sp_delete_supplier
Print '' print  ' *** dropping procedure sp_delete_supplier'
End
GO

Print '' print  ' *** creating procedure sp_delete_supplier'
GO
Create PROCEDURE sp_delete_supplier
(
@SUPPLIER_ID[INT]
)
AS
BEGIN
DELETE FROM supplier
WHERE SUPPLIER_ID = @SUPPLIER_ID
END
