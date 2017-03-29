USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_delivery')
BEGIN
DROP PROCEDURE sp_create_delivery
Print '' print  ' *** dropping procedure sp_create_delivery'
End
GO

Print '' print  ' *** creating procedure sp_create_delivery'
GO
Create PROCEDURE sp_create_delivery
(
@ROUTE_ID[INT],
@DEVLIVERY_DATE[DATETIME],
@VERIFICATION[VARBINARY]= NULL,
@STATUS_ID[NVARCHAR](50),
@DELIVERY_TYPE_ID[NVARCHAR](50),
@ORDER_ID[INT]
)
AS
BEGIN
INSERT INTO DELIVERY (ROUTE_ID, DEVLIVERY_DATE, VERIFICATION, STATUS_ID, DELIVERY_TYPE_ID, ORDER_ID)
VALUES
(@ROUTE_ID, @DEVLIVERY_DATE, @VERIFICATION, @STATUS_ID, @DELIVERY_TYPE_ID, @ORDER_ID)
END
