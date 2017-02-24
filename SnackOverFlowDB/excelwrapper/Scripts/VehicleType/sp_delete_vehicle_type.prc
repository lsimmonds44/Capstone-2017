USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_vehicle_type')
BEGIN
DROP PROCEDURE sp_delete_vehicle_type
Print '' print  ' *** dropping procedure sp_delete_vehicle_type'
End
GO

Print '' print  ' *** creating procedure sp_delete_vehicle_type'
GO
Create PROCEDURE sp_delete_vehicle_type
(
@VEHICLE_TYPE_ID[NVARCHAR](50)
)
AS
BEGIN
DELETE FROM vehicle_type
WHERE VEHICLE_TYPE_ID = @VEHICLE_TYPE_ID
END
