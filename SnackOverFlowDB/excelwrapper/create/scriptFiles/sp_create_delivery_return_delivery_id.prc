USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_delivery_return_delivery_id')
BEGIN
DROP PROCEDURE sp_create_delivery_return_delivery_id
Print '' print  ' *** dropping procedure sp_create_delivery_return_delivery_id'
End
GO

Print '' print  ' *** creating procedure sp_create_delivery_return_delivery_id'
GO
Create PROCEDURE sp_create_delivery_return_delivery_id
(
@ROUTE_ID[INT],
@DEVLIVERY_DATE[DATETIME],
@VERIFICATION[VARBINARY]= NULL,
@STATUS_ID[NVARCHAR](50),
@DELIVERY_TYPE_ID[NVARCHAR](50),
@ORDER_ID[INT],
@DELIVERY_ID[INT] OUTPUT

)
AS
BEGIN
INSERT INTO DELIVERY (ROUTE_ID, DEVLIVERY_DATE, VERIFICATION, STATUS_ID, DELIVERY_TYPE_ID, ORDER_ID)
VALUES
(@ROUTE_ID, @DEVLIVERY_DATE, @VERIFICATION, @STATUS_ID, @DELIVERY_TYPE_ID, @ORDER_ID)
SELECT @DELIVERY_ID = SCOPE_IDENTITY()
END
