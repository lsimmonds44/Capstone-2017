USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_customer')
BEGIN
DROP PROCEDURE sp_delete_customer
Print '' print  ' *** dropping procedure sp_delete_customer'
End
GO

Print '' print  ' *** creating procedure sp_delete_customer'
GO
Create PROCEDURE sp_delete_customer
(
@CUSTOMER_ID[INT]
)
AS
BEGIN
DELETE FROM customer
WHERE CUSTOMER_ID = @CUSTOMER_ID
END
