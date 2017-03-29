USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_grade_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_grade_from_search
Print '' print  ' *** dropping procedure sp_retrieve_grade_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_grade_from_search'
GO
Create PROCEDURE sp_retrieve_grade_from_search
(
@GRADE_ID[NVARCHAR](250)=NULL
)
AS
BEGIN
Select GRADE_ID
FROM GRADE
WHERE (GRADE.GRADE_ID=@GRADE_ID OR @GRADE_ID IS NULL)
END