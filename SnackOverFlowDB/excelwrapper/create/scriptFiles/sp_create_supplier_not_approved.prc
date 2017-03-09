USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_supplier_not_approved')
BEGIN
DROP PROCEDURE sp_create_supplier_not_approved
Print '' print  ' *** dropping procedure sp_create_supplier_not_approved'
End
GO

Print '' print  ' *** creating procedure sp_create_supplier_not_approved'
GO
Create PROCEDURE sp_create_supplier_not_approved
(
@USER_ID[INT],
@IS_APPROVED[BIT],
@FARM_NAME[NVARCHAR](300),
@FARM_ADDRESS[NVARCHAR](300),
@FARM_CITY[NVARCHAR](50),
@FARM_STATE[NCHAR](2),
@FARM_TAX_ID[NVARCHAR](64)
)
AS
BEGIN
INSERT INTO SUPPLIER (USER_ID, IS_APPROVED, FARM_NAME, FARM_ADDRESS, FARM_CITY, FARM_STATE, FARM_TAX_ID)
VALUES
(@USER_ID, @IS_APPROVED, @FARM_NAME, @FARM_ADDRESS, @FARM_CITY, @FARM_STATE, @FARM_TAX_ID)

RETURN @@ROWCOUNT
END
