USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_category_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_category_from_search
Print '' print  ' *** dropping procedure sp_retrieve_category_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_category_from_search'
GO
Create PROCEDURE sp_retrieve_category_from_search
(
@CATEGORY_ID[NVARCHAR](200)=NULL
)
AS
BEGIN
Select CATEGORY_ID
FROM CATEGORY
WHERE (CATEGORY.CATEGORY_ID=@CATEGORY_ID OR @CATEGORY_ID IS NULL)
END