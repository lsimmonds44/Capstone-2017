USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_vehicle_type')
BEGIN
DROP PROCEDURE sp_retrieve_vehicle_type
Print '' print  ' *** dropping procedure sp_retrieve_vehicle_type'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_vehicle_type'
GO
Create PROCEDURE sp_retrieve_vehicle_type
(
@VEHICLE_TYPE_ID[NVARCHAR](50)
)
AS
BEGIN
SELECT VEHICLE_TYPE_ID
FROM vehicle_type
WHERE VEHICLE_TYPE_ID = @VEHICLE_TYPE_ID
END
