USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_customer')
BEGIN
DROP PROCEDURE sp_create_customer
Print '' print  ' *** dropping procedure sp_create_customer'
End
GO

Print '' print  ' *** creating procedure sp_create_customer'
GO
Create PROCEDURE sp_create_customer
(
@CUSTOMER_ID[INT]
)
AS
BEGIN
INSERT INTO CUSTOMER (CUSTOMER_ID)
VALUES
(@CUSTOMER_ID)
END
