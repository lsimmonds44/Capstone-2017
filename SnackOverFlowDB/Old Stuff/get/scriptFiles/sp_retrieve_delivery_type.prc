USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_delivery_type')
BEGIN
DROP PROCEDURE sp_retrieve_delivery_type
Print '' print  ' *** dropping procedure sp_retrieve_delivery_type'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_delivery_type'
GO
Create PROCEDURE sp_retrieve_delivery_type
(
@DELIVERY_TYPE_ID[NVARCHAR](50)
)
AS
BEGIN
SELECT DELIVERY_TYPE_ID, ACTIVE
FROM delivery_type
WHERE DELIVERY_TYPE_ID = @DELIVERY_TYPE_ID
END
