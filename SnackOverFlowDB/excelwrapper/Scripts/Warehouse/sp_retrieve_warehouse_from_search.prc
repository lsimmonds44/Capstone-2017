USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_warehouse_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_warehouse_from_search
Print '' print  ' *** dropping procedure sp_retrieve_warehouse_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_warehouse_from_search'
GO
Create PROCEDURE sp_retrieve_warehouse_from_search
(
@WAREHOUSE_ID[INT]=NULL,
@ADDRESS_1[NVARCHAR](50)=NULL,
@ADDRESS_2[NVARCHAR](50)=NULL,
@CITY[NVARCHAR](50)=NULL,
@STATE[NCHAR](2)=NULL,
@ZIP[NVARCHAR](10)=NULL
)
AS
BEGIN
Select WAREHOUSE_ID, ADDRESS_1, ADDRESS_2, CITY, STATE, ZIP
FROM WAREHOUSE
WHERE (WAREHOUSE.WAREHOUSE_ID=@WAREHOUSE_ID OR @WAREHOUSE_ID IS NULL)
AND (WAREHOUSE.ADDRESS_1=@ADDRESS_1 OR @ADDRESS_1 IS NULL)
AND (WAREHOUSE.ADDRESS_2=@ADDRESS_2 OR @ADDRESS_2 IS NULL)
AND (WAREHOUSE.CITY=@CITY OR @CITY IS NULL)
AND (WAREHOUSE.STATE=@STATE OR @STATE IS NULL)
AND (WAREHOUSE.ZIP=@ZIP OR @ZIP IS NULL)
END