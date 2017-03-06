USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_update_charity_deny')
BEGIN
DROP PROCEDURE sp_update_charity_deny
Print '' print  ' *** dropping procedure sp_update_charity_deny'
End
GO

Print '' print  ' *** creating procedure sp_update_charity_deny'
GO
Create PROCEDURE [dbo].[sp_update_charity_deny]
(
@old_CHARITY_ID[INT]
)
AS
	BEGIN
		UPDATE [CHARITY]
			SET STATUS = "Denied"
			WHERE CHARITY_ID = @old_CHARITY_ID
			RETURN @@ROWCOUNT
	END
GO
