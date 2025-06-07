#!/bin/sh
scriptDir=$(dirname -- "$( readlink -f -- "$0"; )")

if ! pwsh -v > /dev/null; then
	echo "Please install powershell core for linux"
	exit 1
fi

pwsh -NoProfile "$scriptDir/start.ps1"
