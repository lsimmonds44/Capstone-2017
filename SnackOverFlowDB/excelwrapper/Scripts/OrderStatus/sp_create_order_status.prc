USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_order_status')
BEGIN
DROP PROCEDURE sp_create_order_status
Print '' print  ' *** dropping procedure sp_create_order_status'
End
GO

Print '' print  ' *** creating procedure sp_create_order_status'
GO
Create PROCEDURE sp_create_order_status
(
@ORDER_STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
INSERT INTO ORDER_STATUS (ORDER_STATUS_ID)
VALUES
(@ORDER_STATUS_ID)
END
