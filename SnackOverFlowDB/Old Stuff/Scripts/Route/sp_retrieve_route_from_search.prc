USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_route_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_route_from_search
Print '' print  ' *** dropping procedure sp_retrieve_route_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_route_from_search'
GO
Create PROCEDURE sp_retrieve_route_from_search
(
@ROUTE_ID[INT]=NULL,
@VEHICLE_ID[INT]=NULL,
@DRIVER_ID[INT]=NULL,
@ASSIGNED_DATE[DATETIME]=NULL
)
AS
BEGIN
Select ROUTE_ID, VEHICLE_ID, DRIVER_ID, ASSIGNED_DATE
FROM ROUTE
WHERE (ROUTE.ROUTE_ID=@ROUTE_ID OR @ROUTE_ID IS NULL)
AND (ROUTE.VEHICLE_ID=@VEHICLE_ID OR @VEHICLE_ID IS NULL)
AND (ROUTE.DRIVER_ID=@DRIVER_ID OR @DRIVER_ID IS NULL)
AND (ROUTE.ASSIGNED_DATE=@ASSIGNED_DATE OR @ASSIGNED_DATE IS NULL)
END