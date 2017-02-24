USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_charity')
BEGIN
DROP PROCEDURE sp_retrieve_charity
Print '' print  ' *** dropping procedure sp_retrieve_charity'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_charity'
GO
Create PROCEDURE sp_retrieve_charity
(
@CHARITY_ID[INT]
)
AS
BEGIN
SELECT CHARITY_ID, USER_ID, EMPLOYEE_ID, CHARITY_NAME, CONTACT_FIRST_NAME, CONTACT_LAST_NAME, PHONE_NUMBER, EMAIL, CONTACT_HOURS
FROM charity
WHERE CHARITY_ID = @CHARITY_ID
END
