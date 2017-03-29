USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_shipping_address')
BEGIN
DROP PROCEDURE sp_create_shipping_address
Print '' print  ' *** dropping procedure sp_create_shipping_address'
End
GO

Print '' print  ' *** creating procedure sp_create_shipping_address'
GO
Create PROCEDURE sp_create_shipping_address
(
@USER_ID[INT],
@ADDRESS1[NVARCHAR](100),
@ADDRESS2[NVARCHAR](100)= NULL,
@CITY[NVARCHAR](50),
@STATE[NCHAR](2),
@ZIP[NVARCHAR](10),
@ADDRESS_NAME[NVARCHAR](50)
)
AS
BEGIN
INSERT INTO SHIPPING_ADDRESS (USER_ID, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ADDRESS_NAME)
VALUES
(@USER_ID, @ADDRESS1, @ADDRESS2, @CITY, @STATE, @ZIP, @ADDRESS_NAME)
END
