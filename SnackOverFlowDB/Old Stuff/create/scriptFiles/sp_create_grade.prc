USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_grade')
BEGIN
DROP PROCEDURE sp_create_grade
Print '' print  ' *** dropping procedure sp_create_grade'
End
GO

Print '' print  ' *** creating procedure sp_create_grade'
GO
Create PROCEDURE sp_create_grade
(
@GRADE_ID[NVARCHAR](250)
)
AS
BEGIN
INSERT INTO GRADE (GRADE_ID)
VALUES
(@GRADE_ID)
END
