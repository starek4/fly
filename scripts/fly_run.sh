#!/bin/sh
# Script for executing fly client
# Checks for entered credentials and prints help, if invalid credentials were provided
# 	or if -h / any invalid parameters were specified

PrintHelp()
{
	echo "Usage: fly -p <login> -l <password>"
	echo "fly -h or --help displays this help."
}

# Reset in case getopts has been used previously in the shell.
OPTIND=1

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

# If getopts isn't triggered
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
	(>&2 echo "Dotnet isn't isntalled! Please install dotnet fisrt.")
	exit
fi

# Launch client
echo "Starting fly client..."
dotnet ./fly_dir/FlyUnix.dll -l $login -p $password &
