USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_message_line')
BEGIN
DROP PROCEDURE sp_create_message_line
Print '' print  ' *** dropping procedure sp_create_message_line'
End
GO

Print '' print  ' *** creating procedure sp_create_message_line'
GO
Create PROCEDURE sp_create_message_line
(
@DISPATCHER_MESSAGE_ID[INT],
@MESSAGE_LINE_TEXT[NVARCHAR](250)
)
AS
BEGIN
INSERT INTO MESSAGE_LINE (DISPATCHER_MESSAGE_ID, MESSAGE_LINE_TEXT)
VALUES
(@DISPATCHER_MESSAGE_ID, @MESSAGE_LINE_TEXT)
END
