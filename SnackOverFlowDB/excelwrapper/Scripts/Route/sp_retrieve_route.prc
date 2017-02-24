USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_route')
BEGIN
DROP PROCEDURE sp_retrieve_route
Print '' print  ' *** dropping procedure sp_retrieve_route'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_route'
GO
Create PROCEDURE sp_retrieve_route
(
@ROUTE_ID[INT]
)
AS
BEGIN
SELECT ROUTE_ID, VEHICLE_ID, DRIVER_ID, ASSIGNED_DATE
FROM route
WHERE ROUTE_ID = @ROUTE_ID
END
