
if ( ! termtosvg -h > /dev/null ) then
	echo 'Please install termtosvg'
	echo 'https://github.com/MrMarble/termsvg'
	exit 1
fi


termtosvg rec.svg \
   --command ./start.sh \
   --loop-delay 3000 \
   --template window_frame_powershell

