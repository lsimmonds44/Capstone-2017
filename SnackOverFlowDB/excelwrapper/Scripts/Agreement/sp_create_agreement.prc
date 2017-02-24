USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_agreement')
BEGIN
DROP PROCEDURE sp_create_agreement
Print '' print  ' *** dropping procedure sp_create_agreement'
End
GO

Print '' print  ' *** creating procedure sp_create_agreement'
GO
Create PROCEDURE sp_create_agreement
(
@PRODUCT_ID[INT],
@SUPPLIER_ID[INT],
@DATE_SUBMITTED[DATETIME],
@IS_APPROVED[BIT],
@APPROVED_BY[INT]
)
AS
BEGIN
INSERT INTO AGREEMENT (PRODUCT_ID, SUPPLIER_ID, DATE_SUBMITTED, IS_APPROVED, APPROVED_BY)
VALUES
(@PRODUCT_ID, @SUPPLIER_ID, @DATE_SUBMITTED, @IS_APPROVED, @APPROVED_BY)
END
