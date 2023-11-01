:: Debug

@echo off

echo Building Lazy.Vinke...
dotnet clean >> nul
dotnet build --configuration Debug >> nul
