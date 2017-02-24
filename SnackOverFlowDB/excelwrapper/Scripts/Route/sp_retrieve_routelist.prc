USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_route_list')
BEGIN
Drop PROCEDURE sp_retrieve_route_list
Print '' print  ' *** dropping procedure sp_retrieve_route_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_route_list'
GO
Create PROCEDURE sp_retrieve_route_list
AS
BEGIN
SELECT ROUTE_ID, VEHICLE_ID, DRIVER_ID, ASSIGNED_DATE
FROM route
END
