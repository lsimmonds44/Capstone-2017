USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_repair_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_repair_from_search
Print '' print  ' *** dropping procedure sp_retrieve_repair_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_repair_from_search'
GO
Create PROCEDURE sp_retrieve_repair_from_search
(
@REPAIR_ID[INT]=NULL,
@VEHICLE_ID[INT]=NULL
)
AS
BEGIN
Select REPAIR_ID, VEHICLE_ID
FROM REPAIR
WHERE (REPAIR.REPAIR_ID=@REPAIR_ID OR @REPAIR_ID IS NULL)
AND (REPAIR.VEHICLE_ID=@VEHICLE_ID OR @VEHICLE_ID IS NULL)
END