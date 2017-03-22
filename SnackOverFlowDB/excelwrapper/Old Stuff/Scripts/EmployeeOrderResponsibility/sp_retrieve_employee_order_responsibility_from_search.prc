USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_employee_order_responsibility_from_search')
BEGIN
Drop PROCEDURE sp_retrieve_employee_order_responsibility_from_search
Print '' print  ' *** dropping procedure sp_retrieve_employee_order_responsibility_from_search'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_employee_order_responsibility_from_search'
GO
Create PROCEDURE sp_retrieve_employee_order_responsibility_from_search
(
@ORDER_ID[INT]=NULL,
@EMPLOYEE_ID[INT]=NULL,
@DESCRIPTION[NVARCHAR](200)=NULL
)
AS
BEGIN
Select ORDER_ID, EMPLOYEE_ID, DESCRIPTION
FROM EMPLOYEE_ORDER_RESPONSIBILITY
WHERE (EMPLOYEE_ORDER_RESPONSIBILITY.ORDER_ID=@ORDER_ID OR @ORDER_ID IS NULL)
AND (EMPLOYEE_ORDER_RESPONSIBILITY.EMPLOYEE_ID=@EMPLOYEE_ID OR @EMPLOYEE_ID IS NULL)
AND (EMPLOYEE_ORDER_RESPONSIBILITY.DESCRIPTION=@DESCRIPTION OR @DESCRIPTION IS NULL)
END