USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_location_list')
BEGIN
Drop PROCEDURE sp_retrieve_location_list
Print '' print  ' *** dropping procedure sp_retrieve_location_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_location_list'
GO
Create PROCEDURE sp_retrieve_location_list
AS
BEGIN
SELECT LOCATION_ID, DESCRIPTION, IS_ACTIVE
FROM location
END

