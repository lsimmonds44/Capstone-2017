USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_delivery_type')
BEGIN
DROP PROCEDURE sp_create_delivery_type
Print '' print  ' *** dropping procedure sp_create_delivery_type'
End
GO

Print '' print  ' *** creating procedure sp_create_delivery_type'
GO
Create PROCEDURE sp_create_delivery_type
(
@DELIVERY_TYPE_ID[NVARCHAR](50),
@ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO DELIVERY_TYPE (DELIVERY_TYPE_ID, ACTIVE)
VALUES
(@DELIVERY_TYPE_ID, @ACTIVE)
END
