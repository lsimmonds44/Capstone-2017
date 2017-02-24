USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_repair_line')
BEGIN
DROP PROCEDURE sp_create_repair_line
Print '' print  ' *** dropping procedure sp_create_repair_line'
End
GO

Print '' print  ' *** creating procedure sp_create_repair_line'
GO
Create PROCEDURE sp_create_repair_line
(
@REPAIR_ID[INT],
@REPAIR_DESCRIPTION[NVARCHAR](250)
)
AS
BEGIN
INSERT INTO REPAIR_LINE (REPAIR_ID, REPAIR_DESCRIPTION)
VALUES
(@REPAIR_ID, @REPAIR_DESCRIPTION)
END
