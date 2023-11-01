# Debug

echo Builder Debug
echo

CURRENTDIR=$(pwd)
cd $(dirname "$0")

echo Copying Defaults...
mkdir -p ./../../../Debug/1.0.x
rsync -a -u ./Defaults/ ./../../../Debug/1.0.x
find ./../../../Debug/1.0.x/ -name ".gitkeep" -type f -delete
echo

cd ./Modules/Lazy.Vinke/Sources/
chmod +x ./Lazy.Vinke.Builder.Debug.sh
./Lazy.Vinke.Builder.Debug.sh
cd ../../../

cd ./Modules/Lazy.Vinke.Json/Sources/
chmod +x ./Lazy.Vinke.Json.Builder.Debug.sh
./Lazy.Vinke.Json.Builder.Debug.sh
cd ../../../

cd ./Modules/Lazy.Vinke.Database/Sources/
chmod +x ./Lazy.Vinke.Database.Builder.Debug.sh
./Lazy.Vinke.Database.Builder.Debug.sh
cd ../../../

cd $CURRENTDIR
