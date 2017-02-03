USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_vehicle_type_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_vehicle_type_from_search
Print '' print  ' *** dropping procedure sp_retrieve_vehicle_type_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_vehicle_type_from_search'
GO
Create PROCEDURE sp_retrieve_vehicle_type_from_search
(
@VEHICLE_TYPE_ID[NVARCHAR](50)=NULL
)
AS
BEGIN
Select VEHICLE_TYPE_ID
FROM VEHICLE_TYPE
WHERE (VEHICLE_TYPE.VEHICLE_TYPE_ID=@VEHICLE_TYPE_ID OR @VEHICLE_TYPE_ID IS NULL)
END