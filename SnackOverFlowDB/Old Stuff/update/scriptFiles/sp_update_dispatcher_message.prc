USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_dispatcher_message')
BEGIN
DROP PROCEDURE sp_update_dispatcher_message
Print '' print  ' *** dropping procedure sp_update_dispatcher_message'
End
GO

Print '' print  ' *** creating procedure sp_update_dispatcher_message'
GO
Create PROCEDURE sp_update_dispatcher_message
(
@old_DISPATCHER_MESSAGE_ID[INT],
@old_EMPLOYEE_ID[INT],
@new_EMPLOYEE_ID[INT],
@old_MESSAGE_NAME[NVARCHAR](100),
@new_MESSAGE_NAME[NVARCHAR](100),
@old_DRIVER_ID[INT],
@new_DRIVER_ID[INT]
)
AS
BEGIN
UPDATE dispatcher_message
SET EMPLOYEE_ID = @new_EMPLOYEE_ID, MESSAGE_NAME = @new_MESSAGE_NAME, DRIVER_ID = @new_DRIVER_ID
WHERE (DISPATCHER_MESSAGE_ID = @old_DISPATCHER_MESSAGE_ID)
AND (EMPLOYEE_ID = @old_EMPLOYEE_ID)
AND (MESSAGE_NAME = @old_MESSAGE_NAME)
AND (DRIVER_ID = @old_DRIVER_ID)
END
