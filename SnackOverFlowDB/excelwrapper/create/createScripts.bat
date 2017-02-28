@echo off
setlocal enableDelayedExpansion

for %%f in (create\scriptFiles\*) DO (

set filename=%%f
set filename=!filename:.\=!
echo !filename!
sqlcmd -S localhost -E -i !filename!
)

exit