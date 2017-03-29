USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_dispatcher_message_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_dispatcher_message_from_search
Print '' print  ' *** dropping procedure sp_retrieve_dispatcher_message_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_dispatcher_message_from_search'
GO
Create PROCEDURE sp_retrieve_dispatcher_message_from_search
(
@DISPATCHER_MESSAGE_ID[INT]=NULL,
@EMPLOYEE_ID[INT]=NULL,
@MESSAGE_NAME[NVARCHAR](100)=NULL,
@DRIVER_ID[INT]=NULL
)
AS
BEGIN
Select DISPATCHER_MESSAGE_ID, EMPLOYEE_ID, MESSAGE_NAME, DRIVER_ID
FROM DISPATCHER_MESSAGE
WHERE (DISPATCHER_MESSAGE.DISPATCHER_MESSAGE_ID=@DISPATCHER_MESSAGE_ID OR @DISPATCHER_MESSAGE_ID IS NULL)
AND (DISPATCHER_MESSAGE.EMPLOYEE_ID=@EMPLOYEE_ID OR @EMPLOYEE_ID IS NULL)
AND (DISPATCHER_MESSAGE.MESSAGE_NAME=@MESSAGE_NAME OR @MESSAGE_NAME IS NULL)
AND (DISPATCHER_MESSAGE.DRIVER_ID=@DRIVER_ID OR @DRIVER_ID IS NULL)
END