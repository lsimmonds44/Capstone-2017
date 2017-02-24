USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal_list')
BEGIN
Drop PROCEDURE sp_retrieve_deal_list
Print '' print  ' *** dropping procedure sp_retrieve_deal_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal_list'
GO
Create PROCEDURE sp_retrieve_deal_list
AS
BEGIN
SELECT DEAL_ID, DESCRIPTION, CODE, AMOUNT, PERCENT_OFF
FROM deal
END
