USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_customer_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_customer_from_search
Print '' print  ' *** dropping procedure sp_retrieve_customer_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_customer_from_search'
GO
Create PROCEDURE sp_retrieve_customer_from_search
(
@CUSTOMER_ID[INT]=NULL
)
AS
BEGIN
Select CUSTOMER_ID
FROM CUSTOMER
WHERE (CUSTOMER.CUSTOMER_ID=@CUSTOMER_ID OR @CUSTOMER_ID IS NULL)
END