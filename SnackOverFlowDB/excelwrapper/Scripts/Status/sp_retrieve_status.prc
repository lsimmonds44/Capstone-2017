USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_status')
BEGIN
DROP PROCEDURE sp_retrieve_status
Print '' print  ' *** dropping procedure sp_retrieve_status'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_status'
GO
Create PROCEDURE sp_retrieve_status
(
@STATUS_ID[NVARCHAR](50)
)
AS
BEGIN
SELECT STATUS_ID
FROM status
WHERE STATUS_ID = @STATUS_ID
END
