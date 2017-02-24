@echo off
setlocal enableDelayedExpansion
set x=20
for /d %%d in (.\Scripts\*) DO (
    for %%f in (%%d\*) DO (
        set filename=%%f
        set filename=!filename:.\=!
        echo !filename!
        sqlcmd -S localhost -E -i !filename!
    )
)
PAUSE