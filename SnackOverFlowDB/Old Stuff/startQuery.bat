::sqlcmd -S localhost -Q "drop procedure dbo.sp_query_employees_on_active"

sqlcmd -S localhost -E -i CreateSnackOverflowDB.sql
exit