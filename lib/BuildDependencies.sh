xbuild ./LoggingServices/LoggingServices.sln /p:Configuration=Release /p:Platform="Any CPU"
mkdir -p Dependency\ Builds/LoggingServices/DLLs/
rsync -avvRr ./LoggingServices/src/LoggingServices/bin/Release/ Dependency\ Builds/LoggingServices/DLLs/

xbuild ./Lidgren/Lidgren.Network.sln /p:Configuration=Release /p:Platform="Any CPU"
mkdir -p Dependency\ Builds/Lidgren/DLLs/
rsync -avvRr ./Lidgren/Lidgren.Network/bin/Release/ Dependency\ Builds/Lidgren/DLLs/

xbuild ./Net35Essentials/Net35Essentials.sln /p:Configuration=Release /p:Platform="Any CPU"
mkdir -p Dependency\ Builds/Net35Essentials/DLLs/
rsync -avvRr ./Net35Essentials/src/Net35Essentials/bin/Release/ Dependency\ Builds/Net35Essentials/DLLs/

ls -R