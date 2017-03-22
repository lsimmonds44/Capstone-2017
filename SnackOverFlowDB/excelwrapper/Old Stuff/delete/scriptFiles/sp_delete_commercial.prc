USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_commercial')
BEGIN
DROP PROCEDURE sp_delete_commercial
Print '' print  ' *** dropping procedure sp_delete_commercial'
End
GO

Print '' print  ' *** creating procedure sp_delete_commercial'
GO
Create PROCEDURE sp_delete_commercial
(
@COMMERCIAL_ID[INT]
)
AS
BEGIN
DELETE FROM commercial
WHERE COMMERCIAL_ID = @COMMERCIAL_ID
END
