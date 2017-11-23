# Publish and pack application

PUBLISH_PATH=bin/Release/netcoreapp2.0/linux-x64/publish

pack: publish
	cp scripts/install.sh ${PUBLISH_PATH}
	cd ${PUBLISH_PATH} && zip fly.zip *
	cp ${PUBLISH_PATH}/fly.zip .

publish: clean
	dotnet restore
	dotnet publish -c Release -r linux-x64

clean:
	$(RM) fly.zip
	$(RM) -rf bin obj
	$(RM) -rf test

test: pack
	mkdir test
	cp fly.zip test/
	cd test && unzip fly.zip

.PHONY: publish pack
