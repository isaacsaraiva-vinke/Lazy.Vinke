:: Release

@echo off

echo Building Lazy.Vinke.Database...
dotnet clean >> nul
dotnet build --configuration Release >> nul
