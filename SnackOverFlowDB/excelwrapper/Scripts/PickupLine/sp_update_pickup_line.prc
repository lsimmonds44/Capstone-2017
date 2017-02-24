USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_pickup_line')
BEGIN
DROP PROCEDURE sp_update_pickup_line
Print '' print  ' *** dropping procedure sp_update_pickup_line'
End
GO

Print '' print  ' *** creating procedure sp_update_pickup_line'
GO
Create PROCEDURE sp_update_pickup_line
(
@old_PICKUP_LINE_ID[INT],
@old_PICKUP_ID[INT],
@new_PICKUP_ID[INT],
@old_PRODUCT_LOT_ID[INT],
@new_PRODUCT_LOT_ID[INT],
@old_QUANTITY[INT],
@new_QUANTITY[INT],
@old_PICK_UP_STATUS[BIT],
@new_PICK_UP_STATUS[BIT]
)
AS
BEGIN
UPDATE pickup_line
SET PICKUP_ID = @new_PICKUP_ID, PRODUCT_LOT_ID = @new_PRODUCT_LOT_ID, QUANTITY = @new_QUANTITY, PICK_UP_STATUS = @new_PICK_UP_STATUS
WHERE (PICKUP_LINE_ID = @old_PICKUP_LINE_ID)
AND (PICKUP_ID = @old_PICKUP_ID)
AND (PRODUCT_LOT_ID = @old_PRODUCT_LOT_ID)
AND (QUANTITY = @old_QUANTITY)
AND (PICK_UP_STATUS = @old_PICK_UP_STATUS)
END
