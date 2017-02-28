@echo off
setlocal enableDelayedExpansion

for %%f in (delete\scriptFiles\*) DO (

set filename=%%f
set filename=!filename:.\=!
echo !filename!
sqlcmd -S localhost -E -i !filename!
)

exit