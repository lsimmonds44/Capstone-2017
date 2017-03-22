USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_vehicle')
BEGIN
DROP PROCEDURE sp_delete_vehicle
Print '' print  ' *** dropping procedure sp_delete_vehicle'
End
GO

Print '' print  ' *** creating procedure sp_delete_vehicle'
GO
Create PROCEDURE sp_delete_vehicle
(
@VEHICLE_ID[INT]
)
AS
BEGIN
DELETE FROM vehicle
WHERE VEHICLE_ID = @VEHICLE_ID
END
