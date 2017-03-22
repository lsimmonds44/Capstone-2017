USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_driver')
BEGIN
DROP PROCEDURE sp_retrieve_driver
Print '' print  ' *** dropping procedure sp_retrieve_driver'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_driver'
GO
Create PROCEDURE sp_retrieve_driver
(
@DRIVER_ID[INT]
)
AS
BEGIN
SELECT DRIVER_ID, DRIVER_LICENSE_NUMBER, LICENSE_EXPIRATION, ACTIVE
FROM driver
WHERE DRIVER_ID = @DRIVER_ID
END
