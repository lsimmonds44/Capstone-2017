USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_role_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_role_from_search
Print '' print  ' *** dropping procedure sp_retrieve_role_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_role_from_search'
GO
Create PROCEDURE sp_retrieve_role_from_search
(
@ROLE_ID[NVARCHAR](250)=NULL,
@DESCRIPTION[NVARCHAR](1000)=NULL
)
AS
BEGIN
Select ROLE_ID, DESCRIPTION
FROM ROLE
WHERE (ROLE.ROLE_ID=@ROLE_ID OR @ROLE_ID IS NULL)
AND (ROLE.DESCRIPTION=@DESCRIPTION OR @DESCRIPTION IS NULL)
END