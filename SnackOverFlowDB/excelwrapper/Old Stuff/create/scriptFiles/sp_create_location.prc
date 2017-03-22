USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_location')
BEGIN
DROP PROCEDURE sp_create_location
Print '' print  ' *** dropping procedure sp_create_location'
End
GO

Print '' print  ' *** creating procedure sp_create_location'
GO
Create PROCEDURE sp_create_location
(
@DESCRIPTION[NVARCHAR](250),
@IS_ACTIVE[BIT]
)
AS
BEGIN
INSERT INTO LOCATION (DESCRIPTION, IS_ACTIVE)
VALUES
(@DESCRIPTION, @IS_ACTIVE)
END
