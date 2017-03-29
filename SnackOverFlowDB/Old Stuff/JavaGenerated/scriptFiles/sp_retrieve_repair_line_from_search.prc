USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_repair_line_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_repair_line_from_search
Print '' print  ' *** dropping procedure sp_retrieve_repair_line_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_repair_line_from_search'
GO
Create PROCEDURE sp_retrieve_repair_line_from_search
(
@REPAIR_LINE_ID[INT]=NULL,
@REPAIR_ID[INT]=NULL,
@REPAIR_DESCRIPTION[NVARCHAR](250)=NULL
)
AS
BEGIN
Select REPAIR_LINE_ID, REPAIR_ID, REPAIR_DESCRIPTION
FROM REPAIR_LINE
WHERE (REPAIR_LINE.REPAIR_LINE_ID=@REPAIR_LINE_ID OR @REPAIR_LINE_ID IS NULL)
AND (REPAIR_LINE.REPAIR_ID=@REPAIR_ID OR @REPAIR_ID IS NULL)
AND (REPAIR_LINE.REPAIR_DESCRIPTION=@REPAIR_DESCRIPTION OR @REPAIR_DESCRIPTION IS NULL)
END