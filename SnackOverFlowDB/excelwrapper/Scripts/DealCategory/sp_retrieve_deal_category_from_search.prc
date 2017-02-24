USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_deal_category_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_deal_category_from_search
Print '' print  ' *** dropping procedure sp_retrieve_deal_category_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_deal_category_from_search'
GO
Create PROCEDURE sp_retrieve_deal_category_from_search
(
@DEAL_ID[INT]=NULL,
@CATEGORY_ID[NVARCHAR](200)=NULL,
@ACTIVE[BIT]=NULL
)
AS
BEGIN
Select DEAL_ID, CATEGORY_ID, ACTIVE
FROM DEAL_CATEGORY
WHERE (DEAL_CATEGORY.DEAL_ID=@DEAL_ID OR @DEAL_ID IS NULL)
AND (DEAL_CATEGORY.CATEGORY_ID=@CATEGORY_ID OR @CATEGORY_ID IS NULL)
AND (DEAL_CATEGORY.ACTIVE=@ACTIVE OR @ACTIVE IS NULL)
END