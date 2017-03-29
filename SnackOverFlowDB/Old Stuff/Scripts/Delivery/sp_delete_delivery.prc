USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_delivery')
BEGIN
DROP PROCEDURE sp_delete_delivery
Print '' print  ' *** dropping procedure sp_delete_delivery'
End
GO

Print '' print  ' *** creating procedure sp_delete_delivery'
GO
Create PROCEDURE sp_delete_delivery
(
@DELIVERY_ID[INT]
)
AS
BEGIN
DELETE FROM delivery
WHERE DELIVERY_ID = @DELIVERY_ID
END
