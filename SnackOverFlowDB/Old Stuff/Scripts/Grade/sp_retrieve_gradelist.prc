USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_grade_list')
BEGIN
Drop PROCEDURE sp_retrieve_grade_list
Print '' print  ' *** dropping procedure sp_retrieve_grade_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_grade_list'
GO
Create PROCEDURE sp_retrieve_grade_list
AS
BEGIN
SELECT GRADE_ID
FROM grade
END
