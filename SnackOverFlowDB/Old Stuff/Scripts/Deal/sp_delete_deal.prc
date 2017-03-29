USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_deal')
BEGIN
DROP PROCEDURE sp_delete_deal
Print '' print  ' *** dropping procedure sp_delete_deal'
End
GO

Print '' print  ' *** creating procedure sp_delete_deal'
GO
Create PROCEDURE sp_delete_deal
(
@DEAL_ID[INT]
)
AS
BEGIN
DELETE FROM deal
WHERE DEAL_ID = @DEAL_ID
END
