USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_message_line')
BEGIN
DROP PROCEDURE sp_update_message_line
Print '' print  ' *** dropping procedure sp_update_message_line'
End
GO

Print '' print  ' *** creating procedure sp_update_message_line'
GO
Create PROCEDURE sp_update_message_line
(
@old_MESSAGE_LINE_ID[INT],
@old_DISPATCHER_MESSAGE_ID[INT],
@new_DISPATCHER_MESSAGE_ID[INT],
@old_MESSAGE_LINE_TEXT[NVARCHAR](250),
@new_MESSAGE_LINE_TEXT[NVARCHAR](250)
)
AS
BEGIN
UPDATE message_line
SET DISPATCHER_MESSAGE_ID = @new_DISPATCHER_MESSAGE_ID, MESSAGE_LINE_TEXT = @new_MESSAGE_LINE_TEXT
WHERE (MESSAGE_LINE_ID = @old_MESSAGE_LINE_ID)
AND (DISPATCHER_MESSAGE_ID = @old_DISPATCHER_MESSAGE_ID)
AND (MESSAGE_LINE_TEXT = @old_MESSAGE_LINE_TEXT)
END
