USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_employee_message')
BEGIN
DROP PROCEDURE sp_delete_employee_message
Print '' print  ' *** dropping procedure sp_delete_employee_message'
End
GO

Print '' print  ' *** creating procedure sp_delete_employee_message'
GO
Create PROCEDURE sp_delete_employee_message
(
@MESSAGE_ID[INT]
)
AS
BEGIN
DELETE FROM employee_message
WHERE MESSAGE_ID = @MESSAGE_ID
END
