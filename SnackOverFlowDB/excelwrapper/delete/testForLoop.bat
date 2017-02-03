@echo off
setlocal enabledelayedexpansion
SET d=10
echo !d!

for /L %%t IN (0,1,9) DO (
    SET /A d = !d! + 10
    echo !d!
)
pause