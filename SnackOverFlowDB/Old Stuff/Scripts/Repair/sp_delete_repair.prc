USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_repair')
BEGIN
DROP PROCEDURE sp_delete_repair
Print '' print  ' *** dropping procedure sp_delete_repair'
End
GO

Print '' print  ' *** creating procedure sp_delete_repair'
GO
Create PROCEDURE sp_delete_repair
(
@REPAIR_ID[INT]
)
AS
BEGIN
DELETE FROM repair
WHERE REPAIR_ID = @REPAIR_ID
END
