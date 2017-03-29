USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_message_list')
BEGIN
Drop PROCEDURE sp_retrieve_employee_message_list
Print '' print  ' *** dropping procedure sp_retrieve_employee_message_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_message_list'
GO
Create PROCEDURE sp_retrieve_employee_message_list
AS
BEGIN
SELECT MESSAGE_ID, SENDER_ID, RECEIVER_ID, SENT, VIEWED, MESSAGE
FROM employee_message
END
