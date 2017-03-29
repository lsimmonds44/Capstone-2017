USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_grade')
BEGIN
DROP PROCEDURE sp_retrieve_grade
Print '' print  ' *** dropping procedure sp_retrieve_grade'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_grade'
GO
Create PROCEDURE sp_retrieve_grade
(
@GRADE_ID[NVARCHAR](250)
)
AS
BEGIN
SELECT GRADE_ID
FROM grade
WHERE GRADE_ID = @GRADE_ID
END
