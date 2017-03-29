USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_charity')
BEGIN
DROP PROCEDURE sp_delete_charity
Print '' print  ' *** dropping procedure sp_delete_charity'
End
GO

Print '' print  ' *** creating procedure sp_delete_charity'
GO
Create PROCEDURE sp_delete_charity
(
@CHARITY_ID[INT]
)
AS
BEGIN
DELETE FROM charity
WHERE CHARITY_ID = @CHARITY_ID
END
