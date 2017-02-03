USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_status')
BEGIN
DROP PROCEDURE sp_create_status
Print '' print  ' *** dropping procedure sp_create_status'
End
GO

Print '' print  ' *** creating procedure sp_create_status'
GO
Create PROCEDURE sp_create_status
(
@STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
INSERT INTO STATUS (STATUS_ID)
VALUES
(@STATUS_ID)
END
