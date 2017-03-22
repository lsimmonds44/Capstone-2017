USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_dispatcher_message')
BEGIN
DROP PROCEDURE sp_create_dispatcher_message
Print '' print  ' *** dropping procedure sp_create_dispatcher_message'
End
GO

Print '' print  ' *** creating procedure sp_create_dispatcher_message'
GO
Create PROCEDURE sp_create_dispatcher_message
(
@EMPLOYEE_ID[INT],
@MESSAGE_NAME[NVARCHAR](100),
@DRIVER_ID[INT]
)
AS
BEGIN
INSERT INTO DISPATCHER_MESSAGE (EMPLOYEE_ID, MESSAGE_NAME, DRIVER_ID)
VALUES
(@EMPLOYEE_ID, @MESSAGE_NAME, @DRIVER_ID)
END
