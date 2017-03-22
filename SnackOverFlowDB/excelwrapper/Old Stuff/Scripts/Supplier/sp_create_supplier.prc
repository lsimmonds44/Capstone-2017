USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_supplier')
BEGIN
DROP PROCEDURE sp_create_supplier
Print '' print  ' *** dropping procedure sp_create_supplier'
End
GO

Print '' print  ' *** creating procedure sp_create_supplier'
GO
Create PROCEDURE sp_create_supplier
(
@USER_ID[INT],
@IS_APPROVED[BIT],
@APPROVED_BY[INT],
@FARM_TAX_ID[NVARCHAR](64)
)
AS
BEGIN
INSERT INTO SUPPLIER (USER_ID, IS_APPROVED, APPROVED_BY, FARM_TAX_ID)
VALUES
(@USER_ID, @IS_APPROVED, @APPROVED_BY, @FARM_TAX_ID)
END
