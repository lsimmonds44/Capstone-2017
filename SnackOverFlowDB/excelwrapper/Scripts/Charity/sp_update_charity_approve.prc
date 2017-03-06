USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_charity_approve')
BEGIN
DROP PROCEDURE sp_update_charity_approve
Print '' print  ' *** dropping procedure sp_update_charity_approve'
End
GO

Print '' print  ' *** creating procedure sp_update_charity_approve'
GO
Create PROCEDURE [dbo].[sp_update_charity_approve]
(
@old_CHARITY_ID[INT]
)
AS
	BEGIN
		UPDATE [CHARITY]
			SET STATUS = "Approved"
			WHERE CHARITY_ID = @old_CHARITY_ID
			RETURN @@ROWCOUNT
	END
GO
