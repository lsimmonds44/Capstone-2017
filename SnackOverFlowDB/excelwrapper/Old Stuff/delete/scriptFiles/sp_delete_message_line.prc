USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_message_line')
BEGIN
DROP PROCEDURE sp_delete_message_line
Print '' print  ' *** dropping procedure sp_delete_message_line'
End
GO

Print '' print  ' *** creating procedure sp_delete_message_line'
GO
Create PROCEDURE sp_delete_message_line
(
@MESSAGE_LINE_ID[INT]
)
AS
BEGIN
DELETE FROM message_line
WHERE MESSAGE_LINE_ID = @MESSAGE_LINE_ID
END
