USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_warehouse_list')
BEGIN
Drop PROCEDURE sp_retrieve_warehouse_list
Print '' print  ' *** dropping procedure sp_retrieve_warehouse_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_warehouse_list'
GO
Create PROCEDURE sp_retrieve_warehouse_list
AS
BEGIN
SELECT WAREHOUSE_ID, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP
FROM warehouse
END
