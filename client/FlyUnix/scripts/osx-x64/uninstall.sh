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

echo "Fly client is now uninstalled..."
