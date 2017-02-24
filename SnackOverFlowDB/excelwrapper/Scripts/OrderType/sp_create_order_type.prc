USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_order_type')
BEGIN
DROP PROCEDURE sp_create_order_type
Print '' print  ' *** dropping procedure sp_create_order_type'
End
GO

Print '' print  ' *** creating procedure sp_create_order_type'
GO
Create PROCEDURE sp_create_order_type
(
@ORDER_TYPE_ID[NVARCHAR](250)
)
AS
BEGIN
INSERT INTO ORDER_TYPE (ORDER_TYPE_ID)
VALUES
(@ORDER_TYPE_ID)
END
