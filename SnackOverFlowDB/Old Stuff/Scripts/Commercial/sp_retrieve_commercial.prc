USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_commercial')
BEGIN
DROP PROCEDURE sp_retrieve_commercial
Print '' print  ' *** dropping procedure sp_retrieve_commercial'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_commercial'
GO
Create PROCEDURE sp_retrieve_commercial
(
@COMMERCIAL_ID[INT]
)
AS
BEGIN
SELECT COMMERCIAL_ID, USER_ID
FROM commercial
WHERE COMMERCIAL_ID = @COMMERCIAL_ID
END
