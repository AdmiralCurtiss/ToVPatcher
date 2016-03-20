mkdir scenario.dat.ext.ex
mkdir scenariotext
HyoutaTools Tales.Vesperia.Scenario.Extract scenario.dat scenario.dat.ext
FOR /R %%G IN (scenario.dat.ext\*) DO (
comptoe -d scenario.dat.ext\%%~nG scenario.dat.ext.ex\%%~nG

IF EXIST db\VScenario%%~nG (
GraceNote_ScenarioDatTxt db\VScenario%%~nG db\VScenarioMissing scenariotext\%%~nG scenario.dat.ext.ex\%%~nG %%~nG db\GracesJapanese
scenario_string_reinserter.exe scenario.dat.ext.ex\%%~nG scenariotext\%%~nG
IF "%%~nG"=="192" HyoutaTools ByteFix scenario.dat.ext.ex\192.new 1306B-73 1306C-75 1306D-6E 1306E-00 13074-53 13075-75 13076-6E 13077-00 1307D-53 1307E-55 1307F-4E 13080-00
)
IF NOT EXIST db\VScenario%%~nG (
copy /b scenario.dat.ext.ex\%%~nG scenario.dat.ext.ex\%%~nG.new
)

comptoe -c1 scenario.dat.ext.ex\%%~nG.new scenario.dat.ext\%%~nG
del /F scenario.dat.ext.ex\%%~nG
del /F scenariotext\%%~nG
del /F scenario.dat.ext.ex\%%~nG.new
)
REM IF "%%~nG"=="1026" HyoutaTools ByteFix scenario.dat.ext.ex\1026.new 12A3B-36 12A41-34
del /F scenario.dat.ext\0
copy /b assets\0.bin scenario.dat.ext\0
HyoutaTools Tales.Vesperia.Scenario.Pack scenario.dat.ext scenario.dat.new
rd /S /Q scenariotext
rd /S /Q scenario.dat.ext.ex
rd /S /Q scenario.dat.ext

mkdir new
move scenario.dat.new new\scenario.dat