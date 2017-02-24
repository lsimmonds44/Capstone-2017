USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_commercial')
BEGIN
DROP PROCEDURE sp_create_commercial
Print '' print  ' *** dropping procedure sp_create_commercial'
End
GO

Print '' print  ' *** creating procedure sp_create_commercial'
GO
Create PROCEDURE sp_create_commercial
(
@USER_ID[INT],
@IS_APPROVED[BIT],
@APPROVED_BY[INT],
@FEDERAL_TAX_ID[INT],
@ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO COMMERCIAL ([USER_ID],[IS_APPROVED],[APPROVED_BY],[FEDERAL_TAX_ID],[ACTIVE])
VALUES
(@USER_ID,@IS_APPROVED,@APPROVED_BY,@FEDERAL_TAX_ID,@ACTIVE)
END
