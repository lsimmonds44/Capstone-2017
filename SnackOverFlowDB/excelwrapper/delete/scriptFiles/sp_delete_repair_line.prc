USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_repair_line')
BEGIN
DROP PROCEDURE sp_delete_repair_line
Print '' print  ' *** dropping procedure sp_delete_repair_line'
End
GO

Print '' print  ' *** creating procedure sp_delete_repair_line'
GO
Create PROCEDURE sp_delete_repair_line
(
@REPAIR_LINE_ID[INT]
)
AS
BEGIN
DELETE FROM repair_line
WHERE REPAIR_LINE_ID = @REPAIR_LINE_ID
END
