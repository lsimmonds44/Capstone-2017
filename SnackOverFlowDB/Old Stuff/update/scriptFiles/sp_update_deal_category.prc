USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_deal_category')
BEGIN
DROP PROCEDURE sp_update_deal_category
Print '' print  ' *** dropping procedure sp_update_deal_category'
End
GO

Print '' print  ' *** creating procedure sp_update_deal_category'
GO
Create PROCEDURE sp_update_deal_category
(
@old_DEAL_ID[INT],
@old_CATEGORY_ID[NVARCHAR](200),
@old_ACTIVE[BIT],
@new_ACTIVE[BIT]
)
AS
BEGIN
UPDATE deal_category
SET ACTIVE = @new_ACTIVE
WHERE (DEAL_ID = @old_DEAL_ID)
AND (CATEGORY_ID = @old_CATEGORY_ID)
AND (ACTIVE = @old_ACTIVE)
END
