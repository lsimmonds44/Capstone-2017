USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_package_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_package_from_search
Print '' print  ' *** dropping procedure sp_retrieve_package_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_package_from_search'
GO
Create PROCEDURE sp_retrieve_package_from_search
(
@PACKAGE_ID[INT]=NULL,
@DELIVERY_ID[INT]=NULL,
@ORDER_ID[INT]=NULL
)
AS
BEGIN
Select PACKAGE_ID, DELIVERY_ID, ORDER_ID
FROM PACKAGE
WHERE (PACKAGE.PACKAGE_ID=@PACKAGE_ID OR @PACKAGE_ID IS NULL)
AND (PACKAGE.DELIVERY_ID=@DELIVERY_ID OR @DELIVERY_ID IS NULL)
AND (PACKAGE.ORDER_ID=@ORDER_ID OR @ORDER_ID IS NULL)
END