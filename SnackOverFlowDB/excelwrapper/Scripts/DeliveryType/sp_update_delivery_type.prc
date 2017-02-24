USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_delivery_type')
BEGIN
DROP PROCEDURE sp_update_delivery_type
Print '' print  ' *** dropping procedure sp_update_delivery_type'
End
GO

Print '' print  ' *** creating procedure sp_update_delivery_type'
GO
Create PROCEDURE sp_update_delivery_type
(
@old_DELIVERY_TYPE_ID[NVARCHAR](50),
@old_ACTIVE[BIT],
@new_ACTIVE[BIT]
)
AS
BEGIN
UPDATE delivery_type
SET ACTIVE = @new_ACTIVE
WHERE (DELIVERY_TYPE_ID = @old_DELIVERY_TYPE_ID)
AND (ACTIVE = @old_ACTIVE)
END
