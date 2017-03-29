USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_package')
BEGIN
DROP PROCEDURE sp_create_package
Print '' print  ' *** dropping procedure sp_create_package'
End
GO

Print '' print  ' *** creating procedure sp_create_package'
GO
Create PROCEDURE sp_create_package
(
@DELIVERY_ID[INT],
@ORDER_ID[INT]
)
AS
BEGIN
INSERT INTO PACKAGE (DELIVERY_ID, ORDER_ID)
VALUES
(@DELIVERY_ID, @ORDER_ID)
END
