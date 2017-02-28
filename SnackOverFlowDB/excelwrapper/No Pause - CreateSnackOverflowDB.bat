start /wait cmd /k CALL startQuery.bat
start /wait cmd /k CALL create\createScripts.bat
start /wait cmd /k CALL update\updateScripts.bat
start /wait cmd /k CALL get\getScripts.bat
start /wait cmd /k CALL JavaGenerated\JavaGeneratedScripts.bat
start /wait cmd /k CALL delete\deleteScripts.bat