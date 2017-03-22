USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_pickup_line')
BEGIN
DROP PROCEDURE sp_create_pickup_line
Print '' print  ' *** dropping procedure sp_create_pickup_line'
End
GO

Print '' print  ' *** creating procedure sp_create_pickup_line'
GO
Create PROCEDURE sp_create_pickup_line
(
@PICKUP_ID[INT],
@PRODUCT_LOT_ID[INT],
@QUANTITY[INT],
@PICK_UP_STATUS[BIT]
)
AS
BEGIN
INSERT INTO PICKUP_LINE (PICKUP_ID, PRODUCT_LOT_ID, QUANTITY, PICK_UP_STATUS)
VALUES
(@PICKUP_ID, @PRODUCT_LOT_ID, @QUANTITY, @PICK_UP_STATUS)
END
