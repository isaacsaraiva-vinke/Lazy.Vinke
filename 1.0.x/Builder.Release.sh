# Release

# set echo off

echo Builder Release
echo

CURRENTDIR=$(pwd)
cd $(dirname "$0")

echo Copying Defaults...
mkdir -p ./../../../Release/1.0.x
rsync -a -u ./Defaults/ ./../../../Release/1.0.x
find ./../../../Release/1.0.x/ -name ".gitkeep" -type f -delete
echo

cd ./Modules/Lazy.Vinke.Json/Sources/
chmod +x ./Lazy.Vinke.Json.Builder.Release.sh
./Lazy.Vinke.Json.Builder.Release.sh
cd ../../../

cd ./Modules/Lazy.Vinke.Database/Sources/
chmod +x ./Lazy.Vinke.Database.Builder.Release.sh
./Lazy.Vinke.Database.Builder.Release.sh
cd ../../../

cd $CURRENTDIR
