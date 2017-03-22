USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_employee_message')
BEGIN
DROP PROCEDURE sp_update_employee_message
Print '' print  ' *** dropping procedure sp_update_employee_message'
End
GO

Print '' print  ' *** creating procedure sp_update_employee_message'
GO
Create PROCEDURE sp_update_employee_message
(
@old_MESSAGE_ID[INT],
@old_SENDER_ID[INT],
@new_SENDER_ID[INT],
@old_RECEIVER_ID[INT],
@new_RECEIVER_ID[INT],
@old_SENT[DATETIME],
@new_SENT[DATETIME],
@old_VIEWED[BIT],
@new_VIEWED[BIT],
@old_MESSAGE[NVARCHAR](4000),
@new_MESSAGE[NVARCHAR](4000)
)
AS
BEGIN
UPDATE employee_message
SET SENDER_ID = @new_SENDER_ID, RECEIVER_ID = @new_RECEIVER_ID, SENT = @new_SENT, VIEWED = @new_VIEWED, MESSAGE = @new_MESSAGE
WHERE (MESSAGE_ID = @old_MESSAGE_ID)
AND (SENDER_ID = @old_SENDER_ID)
AND (RECEIVER_ID = @old_RECEIVER_ID)
AND (SENT = @old_SENT)
AND (VIEWED = @old_VIEWED)
AND (MESSAGE = @old_MESSAGE)
END
