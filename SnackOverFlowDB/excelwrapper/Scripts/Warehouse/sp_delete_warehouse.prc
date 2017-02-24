USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_warehouse')
BEGIN
DROP PROCEDURE sp_delete_warehouse
Print '' print  ' *** dropping procedure sp_delete_warehouse'
End
GO

Print '' print  ' *** creating procedure sp_delete_warehouse'
GO
Create PROCEDURE sp_delete_warehouse
(
@WAREHOUSE_ID[INT]
)
AS
BEGIN
DELETE FROM warehouse
WHERE WAREHOUSE_ID = @WAREHOUSE_ID
END
