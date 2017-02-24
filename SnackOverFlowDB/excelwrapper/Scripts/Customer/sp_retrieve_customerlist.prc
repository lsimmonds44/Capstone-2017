USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_customer_list')
BEGIN
Drop PROCEDURE sp_retrieve_customer_list
Print '' print  ' *** dropping procedure sp_retrieve_customer_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_customer_list'
GO
Create PROCEDURE sp_retrieve_customer_list
AS
BEGIN
SELECT CUSTOMER_ID
FROM customer
END
