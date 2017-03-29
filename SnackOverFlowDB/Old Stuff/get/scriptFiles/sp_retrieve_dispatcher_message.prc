USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_dispatcher_message')
BEGIN
DROP PROCEDURE sp_retrieve_dispatcher_message
Print '' print  ' *** dropping procedure sp_retrieve_dispatcher_message'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_dispatcher_message'
GO
Create PROCEDURE sp_retrieve_dispatcher_message
(
@DISPATCHER_MESSAGE_ID[INT]
)
AS
BEGIN
SELECT DISPATCHER_MESSAGE_ID, EMPLOYEE_ID, MESSAGE_NAME, DRIVER_ID
FROM dispatcher_message
WHERE DISPATCHER_MESSAGE_ID = @DISPATCHER_MESSAGE_ID
END
