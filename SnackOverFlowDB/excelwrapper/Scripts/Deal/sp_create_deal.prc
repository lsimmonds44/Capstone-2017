USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_deal')
BEGIN
DROP PROCEDURE sp_create_deal
Print '' print  ' *** dropping procedure sp_create_deal'
End
GO

Print '' print  ' *** creating procedure sp_create_deal'
GO
Create PROCEDURE sp_create_deal
(
@DESCRIPTION[NVARCHAR](200),
@CODE[NCHAR](10)= NULL,
@AMOUNT[DECIMAL](5,2)= NULL,
@PERCENT_OFF[DECIMAL](5,2)= NULL
)
AS
BEGIN
INSERT INTO DEAL (DESCRIPTION, CODE, AMOUNT, PERCENT_OFF)
VALUES
(@DESCRIPTION, @CODE, @AMOUNT, @PERCENT_OFF)
END
