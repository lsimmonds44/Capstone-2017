USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_location')
BEGIN
DROP PROCEDURE sp_delete_location
Print '' print  ' *** dropping procedure sp_delete_location'
End
GO

Print '' print  ' *** creating procedure sp_delete_location'
GO
Create PROCEDURE sp_delete_location
(
@LOCATION_ID[INT]
)
AS
BEGIN
DELETE FROM location
WHERE LOCATION_ID = @LOCATION_ID
END
