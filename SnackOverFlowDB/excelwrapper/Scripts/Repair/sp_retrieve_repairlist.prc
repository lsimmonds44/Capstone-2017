USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_repair_list')
BEGIN
Drop PROCEDURE sp_retrieve_repair_list
Print '' print  ' *** dropping procedure sp_retrieve_repair_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_repair_list'
GO
Create PROCEDURE sp_retrieve_repair_list
AS
BEGIN
SELECT REPAIR_ID, VEHICLE_ID
FROM repair
END
