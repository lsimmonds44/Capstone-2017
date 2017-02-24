USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_user_address_list')
BEGIN
Drop PROCEDURE sp_retrieve_user_address_list
Print '' print  ' *** dropping procedure sp_retrieve_user_address_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_user_address_list'
GO
Create PROCEDURE sp_retrieve_user_address_list
AS
BEGIN
SELECT USER_ADDRESS_ID, USER_ID, ADDRESS_LINE_1, ADDRESS_LINE_2, CITY, STATE, ZIP
FROM user_address
END
