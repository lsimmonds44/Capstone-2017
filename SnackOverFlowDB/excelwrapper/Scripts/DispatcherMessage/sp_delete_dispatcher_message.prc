USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_dispatcher_message')
BEGIN
DROP PROCEDURE sp_delete_dispatcher_message
Print '' print  ' *** dropping procedure sp_delete_dispatcher_message'
End
GO

Print '' print  ' *** creating procedure sp_delete_dispatcher_message'
GO
Create PROCEDURE sp_delete_dispatcher_message
(
@DISPATCHER_MESSAGE_ID[INT]
)
AS
BEGIN
DELETE FROM dispatcher_message
WHERE DISPATCHER_MESSAGE_ID = @DISPATCHER_MESSAGE_ID
END
