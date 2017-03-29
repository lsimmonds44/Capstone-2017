USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_driver')
BEGIN
DROP PROCEDURE sp_delete_driver
Print '' print  ' *** dropping procedure sp_delete_driver'
End
GO

Print '' print  ' *** creating procedure sp_delete_driver'
GO
Create PROCEDURE sp_delete_driver
(
@DRIVER_ID[INT]
)
AS
BEGIN
DELETE FROM driver
WHERE DRIVER_ID = @DRIVER_ID
END
