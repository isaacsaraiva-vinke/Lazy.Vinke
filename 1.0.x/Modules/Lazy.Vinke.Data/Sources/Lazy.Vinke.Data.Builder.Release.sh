# Release

CURRENTDIR=$(pwd)
cd $(dirname "$0")

echo Building Lazy.Vinke.Data...
dotnet clean &>/dev/null
dotnet build --configuration Release &>/dev/null

cd $CURRENTDIR
