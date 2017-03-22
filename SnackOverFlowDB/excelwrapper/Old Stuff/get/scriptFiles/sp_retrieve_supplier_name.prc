USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_supplier_name')
BEGIN
DROP PROCEDURE sp_retrieve_supplier_name
Print '' print  ' *** dropping procedure sp_retrieve_supplier_name'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_supplier_name'
GO
Create PROCEDURE sp_retrieve_supplier_name
(
@USER_ID[INT]
)
AS
BEGIN
SELECT FIRST_NAME, LAST_NAME
FROM APP_USER
WHERE USER_ID = @USER_ID
END
