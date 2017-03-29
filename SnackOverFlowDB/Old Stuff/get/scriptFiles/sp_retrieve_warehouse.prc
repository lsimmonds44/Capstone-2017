USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_warehouse')
BEGIN
DROP PROCEDURE sp_retrieve_warehouse
Print '' print  ' *** dropping procedure sp_retrieve_warehouse'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_warehouse'
GO
Create PROCEDURE sp_retrieve_warehouse
(
@WAREHOUSE_ID[INT]
)
AS
BEGIN
SELECT WAREHOUSE_ID, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP
FROM warehouse
WHERE WAREHOUSE_ID = @WAREHOUSE_ID
END
