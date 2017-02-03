USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_warehouse')
BEGIN
DROP PROCEDURE sp_update_warehouse
Print '' print  ' *** dropping procedure sp_update_warehouse'
End
GO

Print '' print  ' *** creating procedure sp_update_warehouse'
GO
Create PROCEDURE sp_update_warehouse
(
@old_WAREHOUSE_ID[INT],
@old_ADDRESS_1[NVARCHAR](50),
@new_ADDRESS_1[NVARCHAR](50),
@old_ADDRESS_2[NVARCHAR](50),
@new_ADDRESS_2[NVARCHAR](50),
@old_CITY[NVARCHAR](50),
@new_CITY[NVARCHAR](50),
@old_STATE[NCHAR](2),
@new_STATE[NCHAR](2),
@old_ZIP[NVARCHAR](10),
@new_ZIP[NVARCHAR](10)
)
AS
BEGIN
UPDATE warehouse
SET ADDRESS_1 = @new_ADDRESS_1, ADDRESS_2 = @new_ADDRESS_2, CITY = @new_CITY, STATE = @new_STATE, ZIP = @new_ZIP
WHERE (WAREHOUSE_ID = @old_WAREHOUSE_ID)
AND (ADDRESS_1 = @old_ADDRESS_1)
AND (ADDRESS_2 = @old_ADDRESS_2)
AND (CITY = @old_CITY)
AND (STATE = @old_STATE)
AND (ZIP = @old_ZIP)
END
