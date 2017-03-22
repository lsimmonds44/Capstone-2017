USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_agreement_application')
BEGIN
DROP PROCEDURE sp_create_agreement_application
Print '' print  ' *** dropping procedure sp_create_agreement_application'
End
GO

Print '' print  ' *** creating procedure sp_create_agreement_application'
GO
Create PROCEDURE sp_create_agreement_application
(
@PRODUCT_ID[INT],
@SUPPLIER_ID[INT],
@DATE_SUBMITTED[DATETIME],
@IS_APPROVED[BIT]
)
AS
BEGIN
INSERT INTO AGREEMENT (PRODUCT_ID, SUPPLIER_ID, DATE_SUBMITTED, IS_APPROVED)
VALUES
(@PRODUCT_ID, @SUPPLIER_ID, @DATE_SUBMITTED, @IS_APPROVED)
END
