USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_customer')
BEGIN
DROP PROCEDURE sp_retrieve_customer
Print '' print  ' *** dropping procedure sp_retrieve_customer'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_customer'
GO
Create PROCEDURE sp_retrieve_customer
(
@CUSTOMER_ID[INT]
)
AS
BEGIN
SELECT CUSTOMER_ID
FROM customer
WHERE CUSTOMER_ID = @CUSTOMER_ID
END
