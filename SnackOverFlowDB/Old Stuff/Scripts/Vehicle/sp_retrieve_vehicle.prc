USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_vehicle')
BEGIN
DROP PROCEDURE sp_retrieve_vehicle
Print '' print  ' *** dropping procedure sp_retrieve_vehicle'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_vehicle'
GO
Create PROCEDURE sp_retrieve_vehicle
(
@VEHICLE_ID[INT]
)
AS
BEGIN
SELECT VEHICLE_ID, VIN, MAKE, MODEL, MILEAGE, YEAR, COLOR, ACTIVE, LATEST_REPAIR_DATE, LAST_DRIVER_ID, VEHICLE_TYPE_ID
FROM vehicle
WHERE VEHICLE_ID = @VEHICLE_ID
END
