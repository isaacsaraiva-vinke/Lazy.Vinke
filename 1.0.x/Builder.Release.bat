:: Release

@echo off

echo Builder Release
echo:

echo Copying Defaults...
xcopy /d /e /y .\Defaults ..\..\..\Release\1.0.x\ >> nul
del /s /q ..\..\..\Release\1.0.x\.gitkeep >> nul
echo:

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Release.bat
cd ..\..\..\
