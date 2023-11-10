:: Debug

@echo off

echo Building Lazy.Vinke.Data...
dotnet clean >> nul
dotnet build --configuration Debug >> nul
