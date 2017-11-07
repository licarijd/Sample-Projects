#!/bin/sh

sleep 10

while [ 1 ]
do

/mnt/onboard/.test/automatic_test /mnt/onboard/.test/batt_test

date >> /mnt/onboard/.test/batt_test.txt

#output battery level
cat /sys/devices/platform/pmic_battery.1/power_supply/mc13892_bat/capacity >> /mnt/onboard/.test/batt_test.txt

#output voltage
cat /sys/devices/platform/pmic_battery.1/power_supply/mc13892_bat/voltage_now >> /mnt/onboard/.test/batt_test.txt

sleep 60

done
