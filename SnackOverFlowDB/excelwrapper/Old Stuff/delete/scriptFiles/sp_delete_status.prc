USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_status')
BEGIN
DROP PROCEDURE sp_delete_status
Print '' print  ' *** dropping procedure sp_delete_status'
End
GO

Print '' print  ' *** creating procedure sp_delete_status'
GO
Create PROCEDURE sp_delete_status
(
@STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
DELETE FROM status
WHERE STATUS_ID = @STATUS_ID
END
