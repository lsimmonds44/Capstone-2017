USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_route')
BEGIN
DROP PROCEDURE sp_delete_route
Print '' print  ' *** dropping procedure sp_delete_route'
End
GO

Print '' print  ' *** creating procedure sp_delete_route'
GO
Create PROCEDURE sp_delete_route
(
@ROUTE_ID[INT]
)
AS
BEGIN
DELETE FROM route
WHERE ROUTE_ID = @ROUTE_ID
END
