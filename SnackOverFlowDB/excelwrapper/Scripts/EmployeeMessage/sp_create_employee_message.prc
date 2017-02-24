USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_employee_message')
BEGIN
DROP PROCEDURE sp_create_employee_message
Print '' print  ' *** dropping procedure sp_create_employee_message'
End
GO

Print '' print  ' *** creating procedure sp_create_employee_message'
GO
Create PROCEDURE sp_create_employee_message
(
@SENDER_ID[INT],
@RECEIVER_ID[INT],
@SENT[DATETIME],
@VIEWED[BIT],
@MESSAGE[NVARCHAR](4000)
)
AS
BEGIN
INSERT INTO EMPLOYEE_MESSAGE (SENDER_ID, RECEIVER_ID, SENT, VIEWED, MESSAGE)
VALUES
(@SENDER_ID, @RECEIVER_ID, @SENT, @VIEWED, @MESSAGE)
END
