USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_inspection')
BEGIN
DROP PROCEDURE sp_delete_inspection
Print '' print  ' *** dropping procedure sp_delete_inspection'
End
GO

Print '' print  ' *** creating procedure sp_delete_inspection'
GO
Create PROCEDURE sp_delete_inspection
(
@INSPECTION_ID[INT]
)
AS
BEGIN
DELETE FROM inspection
WHERE INSPECTION_ID = @INSPECTION_ID
END
