USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_deal_category')
BEGIN
DROP PROCEDURE sp_create_deal_category
Print '' print  ' *** dropping procedure sp_create_deal_category'
End
GO

Print '' print  ' *** creating procedure sp_create_deal_category'
GO
Create PROCEDURE sp_create_deal_category
(
@DEAL_ID[INT],
@CATEGORY_ID[NVARCHAR](200),
@ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO DEAL_CATEGORY (DEAL_ID, CATEGORY_ID, ACTIVE)
VALUES
(@DEAL_ID, @CATEGORY_ID, @ACTIVE)
END
