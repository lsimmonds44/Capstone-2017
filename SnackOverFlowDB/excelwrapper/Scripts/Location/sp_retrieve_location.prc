USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_location')
BEGIN
DROP PROCEDURE sp_retrieve_location
Print '' print  ' *** dropping procedure sp_retrieve_location'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_location'
GO
Create PROCEDURE sp_retrieve_location
(
@LOCATION_ID[INT]
)
AS
BEGIN
SELECT LOCATION_ID, DESCRIPTION
FROM location
WHERE LOCATION_ID = @LOCATION_ID
END
