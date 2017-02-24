USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_route')
BEGIN
DROP PROCEDURE sp_create_route
Print '' print  ' *** dropping procedure sp_create_route'
End
GO

Print '' print  ' *** creating procedure sp_create_route'
GO
Create PROCEDURE sp_create_route
(
@VEHICLE_ID[INT],
@DRIVER_ID[INT],
@ASSIGNED_DATE[DATETIME]
)
AS
BEGIN
INSERT INTO ROUTE (VEHICLE_ID, DRIVER_ID, ASSIGNED_DATE)
VALUES
(@VEHICLE_ID, @DRIVER_ID, @ASSIGNED_DATE)
END
