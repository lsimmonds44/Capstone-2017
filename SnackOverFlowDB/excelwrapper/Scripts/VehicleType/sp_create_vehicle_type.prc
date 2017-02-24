USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_vehicle_type')
BEGIN
DROP PROCEDURE sp_create_vehicle_type
Print '' print  ' *** dropping procedure sp_create_vehicle_type'
End
GO

Print '' print  ' *** creating procedure sp_create_vehicle_type'
GO
Create PROCEDURE sp_create_vehicle_type
(
@VEHICLE_TYPE_ID[NVARCHAR](50)
)
AS
BEGIN
INSERT INTO VEHICLE_TYPE (VEHICLE_TYPE_ID)
VALUES
(@VEHICLE_TYPE_ID)
END
