USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_delivery')
BEGIN
DROP PROCEDURE sp_retrieve_delivery
Print '' print  ' *** dropping procedure sp_retrieve_delivery'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_delivery'
GO
Create PROCEDURE sp_retrieve_delivery
(
@DELIVERY_ID[INT]
)
AS
BEGIN
SELECT DELIVERY_ID, ROUTE_ID, DEVLIVERY_DATE, VERIFICATION, STATUS_ID, DELIVERY_TYPE_ID, ORDER_ID
FROM delivery
WHERE DELIVERY_ID = @DELIVERY_ID
END
