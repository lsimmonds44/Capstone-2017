USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_pickup')
BEGIN
DROP PROCEDURE sp_delete_pickup
Print '' print  ' *** dropping procedure sp_delete_pickup'
End
GO

Print '' print  ' *** creating procedure sp_delete_pickup'
GO
Create PROCEDURE sp_delete_pickup
(
@PICKUP_ID[INT]
)
AS
BEGIN
DELETE FROM pickup
WHERE PICKUP_ID = @PICKUP_ID
END
