USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_order_line')
BEGIN
DROP PROCEDURE sp_create_order_line
Print '' print  ' *** dropping procedure sp_create_order_line'
End
GO

Print '' print  ' *** creating procedure sp_create_order_line'
GO
Create PROCEDURE sp_create_order_line
(
@PRODUCT_ORDER_ID[INT],
@PRODUCT_ID[INT],
@PRODUCT_NAME[NVARCHAR](100),
@QUANTITY[INT],
@GRADE_ID[NVARCHAR](250),
@PRICE[DECIMAL](5,2),
@UNIT_DISCOUNT[DECIMAL](5,2)
)
AS
BEGIN
INSERT INTO ORDER_LINE (PRODUCT_ORDER_ID, PRODUCT_ID, PRODUCT_NAME, QUANTITY, GRADE_ID, PRICE, UNIT_DISCOUNT)
VALUES
(@PRODUCT_ORDER_ID, @PRODUCT_ID, @PRODUCT_NAME, @QUANTITY, @GRADE_ID, @PRICE, @UNIT_DISCOUNT)
SELECT SCOPE_IDENTITY()
END
