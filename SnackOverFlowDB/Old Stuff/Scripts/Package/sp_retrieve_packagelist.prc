USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_package_list')
BEGIN
Drop PROCEDURE sp_retrieve_package_list
Print '' print  ' *** dropping procedure sp_retrieve_package_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_package_list'
GO
Create PROCEDURE sp_retrieve_package_list
AS
BEGIN
SELECT PACKAGE_ID, DELIVERY_ID, ORDER_ID
FROM package
END
