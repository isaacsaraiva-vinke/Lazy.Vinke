:: Debug

@echo off

echo Builder Debug
echo:

echo Copying Defaults...
xcopy /d /e /y .\Defaults ..\..\..\Debug\1.0.x\ >> nul
del /s /q ..\..\..\Debug\1.0.x\.gitkeep >> nul
echo:

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Debug.bat
cd ..\..\..\

cd .\Modules\Lazy.Vinke.Database\Sources\
call .\Lazy.Vinke.Database.Builder.Debug.bat
cd ..\..\..\
