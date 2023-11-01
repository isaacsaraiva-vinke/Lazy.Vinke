# Release

# set echo off

CURRENTDIR=$(pwd)
cd $(dirname "$0")

echo Building Lazy.Vinke.Database...
dotnet clean &>/dev/null
dotnet build --configuration Release &>/dev/null

cd $CURRENTDIR
