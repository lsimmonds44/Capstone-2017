USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal')
BEGIN
DROP PROCEDURE sp_retrieve_deal
Print '' print  ' *** dropping procedure sp_retrieve_deal'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal'
GO
Create PROCEDURE sp_retrieve_deal
(
@DEAL_ID[INT]
)
AS
BEGIN
SELECT DEAL_ID, DESCRIPTION, CODE, AMOUNT, PERCENT_OFF
FROM deal
WHERE DEAL_ID = @DEAL_ID
END
