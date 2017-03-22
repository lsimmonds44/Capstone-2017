USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_user_address')
BEGIN
DROP PROCEDURE sp_create_user_address
Print '' print  ' *** dropping procedure sp_create_user_address'
End
GO

Print '' print  ' *** creating procedure sp_create_user_address'
GO
Create PROCEDURE sp_create_user_address
(
@USER_ADDRESS_ID[INT],
@USER_ID[INT],
@ADDRESS_LINE_1[NVARCHAR](50),
@ADDRESS_LINE_2[NVARCHAR](50),
@CITY[NVARCHAR](50),
@STATE[NCHAR](2),
@ZIP[NVARCHAR](10)
)
AS
BEGIN
INSERT INTO USER_ADDRESS (USER_ADDRESS_ID, USER_ID, ADDRESS_LINE_1, ADDRESS_LINE_2, CITY, STATE, ZIP)
VALUES
(@USER_ADDRESS_ID, @USER_ID, @ADDRESS_LINE_1, @ADDRESS_LINE_2, @CITY, @STATE, @ZIP)
END
