:: Release

@echo off

echo Building Lazy.Vinke.Json...
dotnet clean >> nul
dotnet build --configuration Release >> nul
