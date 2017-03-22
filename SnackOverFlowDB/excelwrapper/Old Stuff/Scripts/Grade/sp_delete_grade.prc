USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_delete_grade')
BEGIN
DROP PROCEDURE sp_delete_grade
Print '' print  ' *** dropping procedure sp_delete_grade'
End
GO

Print '' print  ' *** creating procedure sp_delete_grade'
GO
Create PROCEDURE sp_delete_grade
(
@GRADE_ID[NVARCHAR](250)
)
AS
BEGIN
DELETE FROM grade
WHERE GRADE_ID = @GRADE_ID
END
