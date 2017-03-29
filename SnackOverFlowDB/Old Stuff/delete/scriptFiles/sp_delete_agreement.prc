USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_agreement')
BEGIN
DROP PROCEDURE sp_delete_agreement
Print '' print  ' *** dropping procedure sp_delete_agreement'
End
GO

Print '' print  ' *** creating procedure sp_delete_agreement'
GO
Create PROCEDURE sp_delete_agreement
(
@AGREEMENT_ID[INT]
)
AS
BEGIN
DELETE FROM agreement
WHERE AGREEMENT_ID = @AGREEMENT_ID
END
