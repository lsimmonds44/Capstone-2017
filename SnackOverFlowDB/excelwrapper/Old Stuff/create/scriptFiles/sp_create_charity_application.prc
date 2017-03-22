USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_charity_application')
BEGIN
DROP PROCEDURE sp_create_charity_application
Print '' print  ' *** dropping procedure sp_create_charity_application'
End
GO

Print '' print  ' *** creating procedure sp_create_charity_application'
GO
Create PROCEDURE sp_create_charity_application
(
@USER_ID[INT],
@CHARITY_NAME[NVARCHAR](200),
@CONTACT_FIRST_NAME[NVARCHAR](150),
@CONTACT_LAST_NAME[NVARCHAR](150),
@PHONE_NUMBER[NVARCHAR](20),
@EMAIL[NVARCHAR](100),
@CONTACT_HOURS[NVARCHAR](150)
)
AS
BEGIN
INSERT INTO CHARITY (USER_ID, CHARITY_NAME, CONTACT_FIRST_NAME, CONTACT_LAST_NAME, PHONE_NUMBER, EMAIL, CONTACT_HOURS)
VALUES
(@USER_ID, @CHARITY_NAME, @CONTACT_FIRST_NAME, @CONTACT_LAST_NAME, @PHONE_NUMBER, @EMAIL, @CONTACT_HOURS)
END
