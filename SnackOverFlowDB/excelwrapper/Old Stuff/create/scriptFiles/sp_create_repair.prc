USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_repair')
BEGIN
DROP PROCEDURE sp_create_repair
Print '' print  ' *** dropping procedure sp_create_repair'
End
GO

Print '' print  ' *** creating procedure sp_create_repair'
GO
Create PROCEDURE sp_create_repair
(
@VEHICLE_ID[INT]
)
AS
BEGIN
INSERT INTO REPAIR (VEHICLE_ID)
VALUES
(@VEHICLE_ID)
END
