USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_backorder_preorder')
BEGIN
DROP PROCEDURE sp_delete_backorder_preorder
Print '' print  ' *** dropping procedure sp_delete_backorder_preorder'
End
GO

Print '' print  ' *** creating procedure sp_delete_backorder_preorder'
GO
Create PROCEDURE sp_delete_backorder_preorder
(
@BACKORDER_PREORDER_ID[INT]
)
AS
BEGIN
DELETE FROM backorder_preorder
WHERE BACKORDER_PREORDER_ID = @BACKORDER_PREORDER_ID
END
