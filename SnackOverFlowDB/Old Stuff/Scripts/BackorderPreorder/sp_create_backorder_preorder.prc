USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_backorder_preorder')
BEGIN
DROP PROCEDURE sp_create_backorder_preorder
Print '' print  ' *** dropping procedure sp_create_backorder_preorder'
End
GO

Print '' print  ' *** creating procedure sp_create_backorder_preorder'
GO
Create PROCEDURE sp_create_backorder_preorder
(
@ORDER_ID[INT],
@CUSTOMER_ID[INT],
@AMOUNT[DECIMAL](10,2),
@DATE_PLACED[DATETIME],
@DATE_EXPECTED[DATETIME],
@HAS_ARRIVED[BIT],
@ADDRESS_1[NVARCHAR](50),
@ADDRESS_2[NVARCHAR](50),
@CITY[NVARCHAR](50),
@STATE[NCHAR](2),
@ZIP[NVARCHAR](10)
)
AS
BEGIN
INSERT INTO BACKORDER_PREORDER (ORDER_ID, CUSTOMER_ID, AMOUNT, DATE_PLACED, DATE_EXPECTED, HAS_ARRIVED, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP)
VALUES
(@ORDER_ID, @CUSTOMER_ID, @AMOUNT, @DATE_PLACED, @DATE_EXPECTED, @HAS_ARRIVED, @ADDRESS_1, @ADDRESS_2, @CITY, @STATE, @ZIP)
END
