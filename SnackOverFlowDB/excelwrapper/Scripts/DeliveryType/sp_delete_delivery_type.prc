USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_delivery_type')
BEGIN
DROP PROCEDURE sp_delete_delivery_type
Print '' print  ' *** dropping procedure sp_delete_delivery_type'
End
GO

Print '' print  ' *** creating procedure sp_delete_delivery_type'
GO
Create PROCEDURE sp_delete_delivery_type
(
@DELIVERY_TYPE_ID[NVARCHAR](50)
)
AS
BEGIN
DELETE FROM delivery_type
WHERE DELIVERY_TYPE_ID = @DELIVERY_TYPE_ID
END
