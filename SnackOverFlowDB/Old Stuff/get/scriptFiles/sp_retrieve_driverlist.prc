USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_driver_list')
BEGIN
Drop PROCEDURE sp_retrieve_driver_list
Print '' print  ' *** dropping procedure sp_retrieve_driver_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_driver_list'
GO
Create PROCEDURE sp_retrieve_driver_list
AS
BEGIN
SELECT DRIVER_ID, DRIVER_LICENSE_NUMBER, LICENSE_EXPIRATION, ACTIVE
FROM driver
END
