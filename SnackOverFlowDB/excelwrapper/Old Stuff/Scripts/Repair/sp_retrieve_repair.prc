USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_repair')
BEGIN
DROP PROCEDURE sp_retrieve_repair
Print '' print  ' *** dropping procedure sp_retrieve_repair'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_repair'
GO
Create PROCEDURE sp_retrieve_repair
(
@REPAIR_ID[INT]
)
AS
BEGIN
SELECT REPAIR_ID, VEHICLE_ID
FROM repair
WHERE REPAIR_ID = @REPAIR_ID
END
