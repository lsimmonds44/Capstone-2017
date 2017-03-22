USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_commercial_list')
BEGIN
Drop PROCEDURE sp_retrieve_commercial_list
Print '' print  ' *** dropping procedure sp_retrieve_commercial_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_commercial_list'
GO
Create PROCEDURE sp_retrieve_commercial_list
AS
BEGIN
SELECT COMMERCIAL_ID, USER_ID
FROM commercial
END
