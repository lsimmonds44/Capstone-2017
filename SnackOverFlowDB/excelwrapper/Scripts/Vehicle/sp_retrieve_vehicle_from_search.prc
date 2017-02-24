USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_vehicle_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_vehicle_from_search
Print '' print  ' *** dropping procedure sp_retrieve_vehicle_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_vehicle_from_search'
GO
Create PROCEDURE sp_retrieve_vehicle_from_search
(
@VEHICLE_ID[INT]=NULL,
@VIN[NVARCHAR](20)=NULL,
@MAKE[NVARCHAR](15)=NULL,
@MODEL[NVARCHAR](20)=NULL,
@MILEAGE[INT]=NULL,
@YEAR[NVARCHAR](4)=NULL,
@COLOR[NVARCHAR](20)=NULL,
@ACTIVE[BIT]=NULL,
@LATEST_REPAIR_DATE[DATE]=NULL,@LATEST_REPAIR_DATE_ESCAPE[BIT] = NULL,
@LAST_DRIVER_ID[INT]=NULL,@LAST_DRIVER_ID_ESCAPE[BIT] = NULL,
@VEHICLE_TYPE_ID[NVARCHAR](50)=NULL
)
AS
BEGIN
Select VEHICLE_ID, VIN, MAKE, MODEL, MILEAGE, YEAR, COLOR, ACTIVE, LATEST_REPAIR_DATE, LAST_DRIVER_ID, VEHICLE_TYPE_ID
FROM VEHICLE
WHERE (VEHICLE.VEHICLE_ID=@VEHICLE_ID OR @VEHICLE_ID IS NULL)
AND (VEHICLE.VIN=@VIN OR @VIN IS NULL)
AND (VEHICLE.MAKE=@MAKE OR @MAKE IS NULL)
AND (VEHICLE.MODEL=@MODEL OR @MODEL IS NULL)
AND (VEHICLE.MILEAGE=@MILEAGE OR @MILEAGE IS NULL)
AND (VEHICLE.YEAR=@YEAR OR @YEAR IS NULL)
AND (VEHICLE.COLOR=@COLOR OR @COLOR IS NULL)
AND (VEHICLE.ACTIVE=@ACTIVE OR @ACTIVE IS NULL)
AND (VEHICLE.LATEST_REPAIR_DATE=@LATEST_REPAIR_DATE OR @LATEST_REPAIR_DATE IS NULL OR @LATEST_REPAIR_DATE_ESCAPE = 1)
AND (VEHICLE.LAST_DRIVER_ID=@LAST_DRIVER_ID OR @LAST_DRIVER_ID IS NULL OR @LAST_DRIVER_ID_ESCAPE = 1)
AND (VEHICLE.VEHICLE_TYPE_ID=@VEHICLE_TYPE_ID OR @VEHICLE_TYPE_ID IS NULL)
END