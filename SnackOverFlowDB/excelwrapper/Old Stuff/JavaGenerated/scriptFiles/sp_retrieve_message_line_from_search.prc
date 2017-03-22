USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_message_line_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_message_line_from_search
Print '' print  ' *** dropping procedure sp_retrieve_message_line_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_message_line_from_search'
GO
Create PROCEDURE sp_retrieve_message_line_from_search
(
@MESSAGE_LINE_ID[INT]=NULL,
@DISPATCHER_MESSAGE_ID[INT]=NULL,
@MESSAGE_LINE_TEXT[NVARCHAR](250)=NULL
)
AS
BEGIN
Select MESSAGE_LINE_ID, DISPATCHER_MESSAGE_ID, MESSAGE_LINE_TEXT
FROM MESSAGE_LINE
WHERE (MESSAGE_LINE.MESSAGE_LINE_ID=@MESSAGE_LINE_ID OR @MESSAGE_LINE_ID IS NULL)
AND (MESSAGE_LINE.DISPATCHER_MESSAGE_ID=@DISPATCHER_MESSAGE_ID OR @DISPATCHER_MESSAGE_ID IS NULL)
AND (MESSAGE_LINE.MESSAGE_LINE_TEXT=@MESSAGE_LINE_TEXT OR @MESSAGE_LINE_TEXT IS NULL)
END