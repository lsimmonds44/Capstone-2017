USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_user_address')
BEGIN
DROP PROCEDURE sp_update_user_address
Print '' print  ' *** dropping procedure sp_update_user_address'
End
GO

Print '' print  ' *** creating procedure sp_update_user_address'
GO
Create PROCEDURE sp_update_user_address
(
@old_USER_ADDRESS_ID[INT],
@old_USER_ID[INT],
@new_USER_ID[INT],
@old_ADDRESS_LINE_1[NVARCHAR](50),
@new_ADDRESS_LINE_1[NVARCHAR](50),
@old_ADDRESS_LINE_2[NVARCHAR](50),
@new_ADDRESS_LINE_2[NVARCHAR](50),
@old_CITY[NVARCHAR](50),
@new_CITY[NVARCHAR](50),
@old_STATE[NCHAR](2),
@new_STATE[NCHAR](2),
@old_ZIP[NVARCHAR](10),
@new_ZIP[NVARCHAR](10)
)
AS
BEGIN
UPDATE user_address
SET USER_ID = @new_USER_ID, ADDRESS_LINE_1 = @new_ADDRESS_LINE_1, ADDRESS_LINE_2 = @new_ADDRESS_LINE_2, CITY = @new_CITY, STATE = @new_STATE, ZIP = @new_ZIP
WHERE (USER_ADDRESS_ID = @old_USER_ADDRESS_ID)
AND (USER_ID = @old_USER_ID)
AND (ADDRESS_LINE_1 = @old_ADDRESS_LINE_1)
AND (ADDRESS_LINE_2 = @old_ADDRESS_LINE_2)
AND (CITY = @old_CITY)
AND (STATE = @old_STATE)
AND (ZIP = @old_ZIP)
END
