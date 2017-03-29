USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_warehouse')
BEGIN
DROP PROCEDURE sp_create_warehouse
Print '' print  ' *** dropping procedure sp_create_warehouse'
End
GO

Print '' print  ' *** creating procedure sp_create_warehouse'
GO
Create PROCEDURE sp_create_warehouse
(
@ADDRESS_1[NVARCHAR](50),
@ADDRESS_2[NVARCHAR](50),
@CITY[NVARCHAR](50),
@STATE[NCHAR](2),
@ZIP[NVARCHAR](10)
)
AS
BEGIN
INSERT INTO WAREHOUSE (ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP)
VALUES
(@ADDRESS_1, @ADDRESS_2, @CITY, @STATE, @ZIP)
END
