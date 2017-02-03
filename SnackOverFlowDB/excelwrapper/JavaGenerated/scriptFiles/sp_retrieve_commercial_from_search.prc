USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_commercial_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_commercial_from_search
Print '' print  ' *** dropping procedure sp_retrieve_commercial_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_commercial_from_search'
GO
Create PROCEDURE sp_retrieve_commercial_from_search
(
@COMMERCIAL_ID[INT]=NULL,
@USER_ID[INT]=NULL
)
AS
BEGIN
Select COMMERCIAL_ID, USER_ID
FROM COMMERCIAL
WHERE (COMMERCIAL.COMMERCIAL_ID=@COMMERCIAL_ID OR @COMMERCIAL_ID IS NULL)
AND (COMMERCIAL.USER_ID=@USER_ID OR @USER_ID IS NULL)
END