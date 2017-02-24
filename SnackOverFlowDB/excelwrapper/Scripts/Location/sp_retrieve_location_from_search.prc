USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_location_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_location_from_search
Print '' print  ' *** dropping procedure sp_retrieve_location_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_location_from_search'
GO
Create PROCEDURE sp_retrieve_location_from_search
(
@LOCATION_ID[INT]=NULL,
@DESCRIPTION[NVARCHAR](250)=NULL
)
AS
BEGIN
Select LOCATION_ID, DESCRIPTION
FROM LOCATION
WHERE (LOCATION.LOCATION_ID=@LOCATION_ID OR @LOCATION_ID IS NULL)
AND (LOCATION.DESCRIPTION=@DESCRIPTION OR @DESCRIPTION IS NULL)
END