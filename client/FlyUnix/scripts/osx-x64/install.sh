#!/bin/sh
# Script for installing fly client

ID=`id -u`

# Checkf if we have needed privileges
if test "$ID" -ne "0"
    then
    echo "Please run this script as root"
    exit 1
fi

rm -rf /usr/local/fly 2> /dev/null
rm /usr/local/bin/fly 2> /dev/null

mkdir /usr/local/fly
cp -rp * /usr/local/fly/
mkdir /usr/local/bin/ 2> /dev/null
ln -s /usr/local/fly/FlyUnix /usr/local/bin/fly
echo "Fly client is installed..."
echo "If you will have any problems with fly client, check prerequisites for running .NET Core apps at:"
echo "https://docs.microsoft.com/en-us/dotnet/core/linux-prerequisites?tabs=netcore2x"
