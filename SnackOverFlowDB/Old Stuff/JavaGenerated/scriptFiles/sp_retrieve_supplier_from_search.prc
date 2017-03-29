USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_supplier_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_supplier_from_search
Print '' print  ' *** dropping procedure sp_retrieve_supplier_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_supplier_from_search'
GO
Create PROCEDURE sp_retrieve_supplier_from_search
(
@SUPPLIER_ID[INT]=NULL,
@USER_ID[INT]=NULL,
@IS_APPROVED[BIT]=NULL,
@APPROVED_BY[INT]=NULL,
@FARM_TAX_ID[NVARCHAR](64)=NULL
)
AS
BEGIN
Select SUPPLIER_ID, USER_ID, IS_APPROVED, APPROVED_BY, FARM_TAX_ID
FROM SUPPLIER
WHERE (SUPPLIER.SUPPLIER_ID=@SUPPLIER_ID OR @SUPPLIER_ID IS NULL)
AND (SUPPLIER.USER_ID=@USER_ID OR @USER_ID IS NULL)
AND (SUPPLIER.IS_APPROVED=@IS_APPROVED OR @IS_APPROVED IS NULL)
AND (SUPPLIER.APPROVED_BY=@APPROVED_BY OR @APPROVED_BY IS NULL)
AND (SUPPLIER.FARM_TAX_ID=@FARM_TAX_ID OR @FARM_TAX_ID IS NULL)
END