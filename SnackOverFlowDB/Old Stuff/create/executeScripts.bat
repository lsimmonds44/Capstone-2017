@echo off
setlocal enableDelayedExpansion
set x=20
echo !x!
for %%f in (create\scriptFiles\*) DO (
set /a x = !x! - 1
if !x! EQU 0 (
PAUSE
set /a x = 20)
set filename=%%f
set filename=!filename:.\=!
echo !filename!
sqlcmd -S localhost -E -i !filename!
)
PAUSE
exit