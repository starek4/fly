#!/bin/sh
# Script for installing fly client

ID=`id -u`

# Checkf if we have needed privileges
if test "$ID" -ne "0"
    then
    echo "Please run this script as root"
    exit 1
fi

mkdir /opt/fly
mv * /opt/fly/
ln -s /opt/fly/FlyUnix /usr/bin/fly
echo "If you will have any problems with fly client, check prerequisites for running .NET Core apps at:"
echo "https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x"