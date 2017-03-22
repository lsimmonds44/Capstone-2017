USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_order_status_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_order_status_from_search
Print '' print  ' *** dropping procedure sp_retrieve_order_status_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_order_status_from_search'
GO
Create PROCEDURE sp_retrieve_order_status_from_search
(
@ORDER_STATUS_ID[NVARCHAR](50)=NULL
)
AS
BEGIN
Select ORDER_STATUS_ID
FROM ORDER_STATUS
WHERE (ORDER_STATUS.ORDER_STATUS_ID=@ORDER_STATUS_ID OR @ORDER_STATUS_ID IS NULL)
END