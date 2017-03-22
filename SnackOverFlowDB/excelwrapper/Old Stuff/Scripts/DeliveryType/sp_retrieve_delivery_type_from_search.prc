USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_delivery_type_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_delivery_type_from_search
Print '' print  ' *** dropping procedure sp_retrieve_delivery_type_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_delivery_type_from_search'
GO
Create PROCEDURE sp_retrieve_delivery_type_from_search
(
@DELIVERY_TYPE_ID[NVARCHAR](50)=NULL,
@ACTIVE[BIT]=NULL
)
AS
BEGIN
Select DELIVERY_TYPE_ID, ACTIVE
FROM DELIVERY_TYPE
WHERE (DELIVERY_TYPE.DELIVERY_TYPE_ID=@DELIVERY_TYPE_ID OR @DELIVERY_TYPE_ID IS NULL)
AND (DELIVERY_TYPE.ACTIVE=@ACTIVE OR @ACTIVE IS NULL)
END