#!/bin/sh
# Script for executing fly client

PrintHelp()
{
	echo "Usage: fly -p <login> -l <password>"
	echo "fly -h or --help displays this help."
}

# Reset in case getopts has been used previously in the shell.
OPTIND=1

# Check for passed arguments
while getopts ":hl:p:" opt; do
  case $opt in
	h)
		PrintHelp
		exit 0
		;;
	l)
		login=$OPTARG
		;;
	p)
		password=$OPTARG
		;;
	\? | *)
		echo "Invalid option: -$OPTARG" >&2
		PrintHelp
		exit 1
		;;
	:)
		echo "Option -$OPTARG requires an argument." >&2
		PrintHelp
		exit 1
		;;
  esac
done

# If getopts isn't triggered (invalid parameters)
if [ $OPTIND -eq 1 ];
then
	if [ "$1" == "" ];
	then
		echo "No arguments were passed."
	else
		echo "Invalid argument: " ${1}
	fi
	PrintHelp
    exit 1
fi

# Verify dotnet installation
which dotnet 2> /dev/null 1> /dev/null
if [ $? -ge 1 ]; 
then
	(>&2 echo "Dotnet wasn't found! Ensure you have dotnet installed and set it's executable into \$PATH.")
	exit 1
fi

# Verify ip / ifconfig presence
which ip 2> /dev/null 1> /dev/null
if [ $? -ge 1 ]; 
then
	which ifconfig 2> /dev/null 1> /dev/null
	if [ $? -ge 1 ]; 
	then
		(>&2 echo "Couldn't find 'ip' or 'ifconfig' utilities installed! Aborting.")
		exit 1
	fi
fi

# Launch client
echo "Starting fly client..."
dotnet ./fly_dir/FlyUnix.dll -l $login -p $password &
