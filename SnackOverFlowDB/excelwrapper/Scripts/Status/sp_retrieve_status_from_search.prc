USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_status_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_status_from_search
Print '' print  ' *** dropping procedure sp_retrieve_status_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_status_from_search'
GO
Create PROCEDURE sp_retrieve_status_from_search
(
@STATUS_ID[NVARCHAR](50)=NULL
)
AS
BEGIN
Select STATUS_ID
FROM STATUS
WHERE (STATUS.STATUS_ID=@STATUS_ID OR @STATUS_ID IS NULL)
END