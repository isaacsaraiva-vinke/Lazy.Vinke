:: Debug

@echo off

echo Builder Debug
echo:

echo Copying Defaults...
xcopy /d /e /y .\Defaults ..\..\..\Debug\1.0.0\ >> nul
del /s /q ..\..\..\Debug\1.0.0\.gitkeep >> nul
echo:

cd .\Modules\Lazy.Vinke.Json\Sources\
call .\Lazy.Vinke.Json.Builder.Debug.bat
cd ..\..\..\
