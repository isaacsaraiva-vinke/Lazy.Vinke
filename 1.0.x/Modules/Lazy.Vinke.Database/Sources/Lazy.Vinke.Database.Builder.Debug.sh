# Debug

CURRENTDIR=$(pwd)
cd $(dirname "$0")

echo Building Lazy.Vinke.Database...
dotnet clean &>/dev/null
dotnet build --configuration Debug &>/dev/null

cd $CURRENTDIR
