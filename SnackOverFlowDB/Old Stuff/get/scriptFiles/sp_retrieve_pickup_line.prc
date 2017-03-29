USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_pickup_line')
BEGIN
DROP PROCEDURE sp_retrieve_pickup_line
Print '' print  ' *** dropping procedure sp_retrieve_pickup_line'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_pickup_line'
GO
Create PROCEDURE sp_retrieve_pickup_line
(
@PICKUP_LINE_ID[INT]
)
AS
BEGIN
SELECT PICKUP_LINE_ID, PICKUP_ID, PRODUCT_LOT_ID, QUANTITY, PICK_UP_STATUS
FROM pickup_line
WHERE PICKUP_LINE_ID = @PICKUP_LINE_ID
END
