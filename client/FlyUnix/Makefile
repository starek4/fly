# Publish and pack application for linux-x64 and osx-x64

PUBLISH_PATH_LINUX=bin/Release/netcoreapp2.0/linux-x64/publish
PUBLISH_PATH_OSX=bin/Release/netcoreapp2.0/osx-x64/publish
LINUX_ARCHIVE=fly_linux64.zip
OSX_ARCHIVE=fly_osx64.zip

pack: pack_linux pack_osx

# Linux
pack_linux: publish_linux
	cp scripts/linux-x64/install.sh ${PUBLISH_PATH_LINUX}
	cp scripts/linux-x64/uninstall.sh ${PUBLISH_PATH_LINUX}
	cd ${PUBLISH_PATH_LINUX} && zip ${LINUX_ARCHIVE} *
	cp ${PUBLISH_PATH_LINUX}/${LINUX_ARCHIVE} .

publish_linux: clean
	dotnet restore
	dotnet publish -c Release -r linux-x64

test_linux: pack_linux
	mkdir test
	cp ${LINUX_ARCHIVE} test/
	cd test && unzip ${LINUX_ARCHIVE}


# OSX
pack_osx: publish_osx
	cp scripts/osx-x64/install.sh ${PUBLISH_PATH_OSX}
	cp scripts/osx-x64/uninstall.sh ${PUBLISH_PATH_OSX}
	cd ${PUBLISH_PATH_OSX} && zip ${OSX_ARCHIVE} *
	cp ${PUBLISH_PATH_OSX}/${OSX_ARCHIVE} .

publish_osx: clean
	dotnet restore
	dotnet publish -c Release -r osx-x64



clean:
	$(RM) ${LINUX_ARCHIVE}
	$(RM) ${OSX_ARCHIVE}
	$(RM) -rf bin obj
	$(RM) -rf test

.PHONY: publish_linux pack_linux publish_osx pack_osx pack
