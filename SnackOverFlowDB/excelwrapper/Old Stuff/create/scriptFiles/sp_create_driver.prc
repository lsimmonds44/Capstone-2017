USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_driver')
BEGIN
DROP PROCEDURE sp_create_driver
Print '' print  ' *** dropping procedure sp_create_driver'
End
GO

Print '' print  ' *** creating procedure sp_create_driver'
GO
Create PROCEDURE sp_create_driver
(
@DRIVER_ID[INT],
@DRIVER_LICENSE_NUMBER[NVARCHAR](9),
@LICENSE_EXPIRATION[DATETIME],
@ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO DRIVER (DRIVER_ID, DRIVER_LICENSE_NUMBER, LICENSE_EXPIRATION, ACTIVE)
VALUES
(@DRIVER_ID, @DRIVER_LICENSE_NUMBER, @LICENSE_EXPIRATION, @ACTIVE)
END
