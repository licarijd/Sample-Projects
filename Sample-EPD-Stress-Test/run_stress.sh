#!/bin/bash

sleep 5

pkill "httpd &"
httpd &

date > reboot.txt

#Log in the background
logread -f >> /mnt/onboard/.test/log.txt 2>&1 &

while true
do	
	#Top is logged seperate
	date >> log_top.txt
	top -n 1 -b >> /mnt/onboard/.test/log_top.txt 2>&1 &
	
	tail -n 10000 /mnt/onboard/.test/log.txt > /mnt/onboard/.test/trunclog.txt
	sed -i '1,5d' log.txt
	
	#automation script is run if not already active
	/mnt/onboard/.test/automatic_test /mnt/onboard/.test/<device>_stress >> /mnt/onboard/.test/automation_log.txt 2>&1
	
	sync
	sleep 5
done

