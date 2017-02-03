USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_message_line')
BEGIN
DROP PROCEDURE sp_retrieve_message_line
Print '' print  ' *** dropping procedure sp_retrieve_message_line'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_message_line'
GO
Create PROCEDURE sp_retrieve_message_line
(
@MESSAGE_LINE_ID[INT]
)
AS
BEGIN
SELECT MESSAGE_LINE_ID, DISPATCHER_MESSAGE_ID, MESSAGE_LINE_TEXT
FROM message_line
WHERE MESSAGE_LINE_ID = @MESSAGE_LINE_ID
END
