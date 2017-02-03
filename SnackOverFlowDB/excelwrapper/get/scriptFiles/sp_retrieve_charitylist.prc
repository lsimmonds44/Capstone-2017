USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_charity_list')
BEGIN
Drop PROCEDURE sp_retrieve_charity_list
Print '' print  ' *** dropping procedure sp_retrieve_charity_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_charity_list'
GO
Create PROCEDURE sp_retrieve_charity_list
AS
BEGIN
SELECT CHARITY_ID, USER_ID, EMPLOYEE_ID, CHARITY_NAME, CONTACT_FIRST_NAME, CONTACT_LAST_NAME, PHONE_NUMBER, EMAIL, CONTACT_HOURS
FROM charity
END
