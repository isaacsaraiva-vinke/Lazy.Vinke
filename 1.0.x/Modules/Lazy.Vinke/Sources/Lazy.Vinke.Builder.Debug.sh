# Debug

CURRENTDIR=$(pwd)
cd $(dirname "$0")

echo Building Lazy.Vinke...
dotnet clean &>/dev/null
dotnet build --configuration Debug &>/dev/null

cd $CURRENTDIR
