USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_vehicle_list')
BEGIN
Drop PROCEDURE sp_retrieve_vehicle_list
Print '' print  ' *** dropping procedure sp_retrieve_vehicle_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_vehicle_list'
GO
Create PROCEDURE sp_retrieve_vehicle_list
AS
BEGIN
SELECT VEHICLE_ID, VIN, MAKE, MODEL, MILEAGE, YEAR, COLOR, ACTIVE, LATEST_REPAIR_DATE, LAST_DRIVER_ID, VEHICLE_TYPE_ID
FROM vehicle
END
