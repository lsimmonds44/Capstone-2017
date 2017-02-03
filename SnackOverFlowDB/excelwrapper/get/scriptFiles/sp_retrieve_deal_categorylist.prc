USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal_category_list')
BEGIN
Drop PROCEDURE sp_retrieve_deal_category_list
Print '' print  ' *** dropping procedure sp_retrieve_deal_category_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal_category_list'
GO
Create PROCEDURE sp_retrieve_deal_category_list
AS
BEGIN
SELECT DEAL_ID, CATEGORY_ID, ACTIVE
FROM deal_category
END
