start /wait cmd /k CALL refreshQuery.bat
start /wait cmd /k CALL create\executeScripts.bat
start /wait cmd /k CALL update\executeScripts.bat
start /wait cmd /k CALL get\executeScripts.bat
start /wait cmd /k CALL JavaGenerated\executeScripts.bat
start /wait cmd /k CALL delete\executeScripts.bat