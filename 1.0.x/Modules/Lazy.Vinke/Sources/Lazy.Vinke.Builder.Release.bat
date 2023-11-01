:: Release

@echo off

echo Building Lazy.Vinke...
dotnet clean >> nul
dotnet build --configuration Release >> nul
