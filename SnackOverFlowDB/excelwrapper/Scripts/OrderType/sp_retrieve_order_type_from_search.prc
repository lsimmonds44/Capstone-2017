USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_order_type_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_order_type_from_search
Print '' print  ' *** dropping procedure sp_retrieve_order_type_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_order_type_from_search'
GO
Create PROCEDURE sp_retrieve_order_type_from_search
(
@ORDER_TYPE_ID[NVARCHAR](250)=NULL
)
AS
BEGIN
Select ORDER_TYPE_ID
FROM ORDER_TYPE
WHERE (ORDER_TYPE.ORDER_TYPE_ID=@ORDER_TYPE_ID OR @ORDER_TYPE_ID IS NULL)
END