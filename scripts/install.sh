#!/bin/sh
# Script for installing fly client

# Checkf if we have needed privileges
if [ "$EUID" -ne 0 ]
    then 
    echo "Please run this script as root"
    exit 1
fi

# Check if skip parameter was provided
while getopts ":hs" opt; do
  case $opt in
	h)
		(>&2 echo "Run this script without any parameters to install Fly client. This script needs root privileges.")
        (>&2 echo "Fly client needs dotnet core framework and also ip or ifconfig. You can skip these requirements by passing parameter '-s'.")
        (>&2 echo "However you need to satisfy said dependecies in order to run Fly client. All invalid parameters will be ignored.")
		exit 0
		;;
	s)
		skipDependency=1
		;;
  esac
done

# Check dotnet installation if skip parameter was not set
if [ "$skipDependency" != "1" ]; 
then
    (>&2 echo "Checking dotnet installation...")
    which dotnet 2> /dev/null 1> /dev/null
    if [ $? -ge 1 ]; 
    then
        (>&2 echo "Dotnet wasn't found! Ensure you have dotnet installed and set its executable into \$PATH for superuser.")
        (>&2 echo "Current \$PATH for this script:    $PATH")
        read -p "Do you want to check this dependency check? (y/N) " answer;
        if [ "$answer" != "y" ] && [ "$answer" != "Y" ];
        then
            (>&2 echo "Installation aborted. If you want to skip dependency check in the future, run this script with '-s' parameter")
            exit 1
        else
            (>&2 echo "Dependency check skipped. Please install necessary dependencies before running client.")
        fi
    else
        (>&2 echo "Dotnet installation is OK. Proceeding with installation.")
    fi
fi

# Clear answer variable
answer=""

# Check ip / ifconfig presence if skip parameter was not set
if [ "$skipDependency" != "1" ]; 
then
    (>&2 echo "Checking 'ip' utility availability...")
    which ip 2> /dev/null 1> /dev/null
    if [ $? -ge 1 ]; 
    then
        (>&2 echo "Ip utility not found! Checking 'ifconfig' utility availability...")
        which ifconfig 2> /dev/null 1> /dev/null
        if [ $? -ge 1 ]; 
        then
            (>&2 echo "Ifconfig utility not found!")
            read -p "Do you want to check this dependency check? (y/n) " answer;
            if [ "$answer" != "y" ] && [ "$answer" != "Y" ];
            then
                (>&2 echo "Installation aborted. If you want to skip dependency checks in the future, run this script with '-s' parameter.")
                exit 1
            else
                (>&2 echo "Dependency check skipped. Please install necessary dependencies before running client.")
            fi
        else
            (>&2 echo "Found ifconfig utility. Proceeding with installation.")
        fi
    else
        (>&2 echo "Found ip utility. Proceeding with installation.")
    fi
fi

echo "Installing fly client..."

# Copy files
cp -r fly_dir /usr/local/bin/
cp fly_run.sh /usr/local/bin/fly

echo "Done"
