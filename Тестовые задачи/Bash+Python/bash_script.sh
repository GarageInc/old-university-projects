#!bin/bash

getRandom()
{
#RANDOM=$(od -An -N2 -i /dev/random)
#RANDOM=$(head -1 /dev/urandom | od -N 1 | awk '{ print $2 }')
#RANDOM=$(printf "0.%03d%02d" $(( $RANDOM % 1000 )) $(( $RANDOM % 100)))
RANDOM="$(od -vAn -N4 -tu4 < /dev/urandom)"
echo "$RANDOM"
}

destdir=result.txt

while true;
do
random_int=$(getRandom)

random_str=$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 5 | head -n 1)

mcs=$(($(date +%s%N)/1000))

random_float=$(getRandom)
random_float=$random_float.$(getRandom)

ms=$1
ms=$(awk "BEGIN {printf \"%.2f\",${ms}/1000}")
#ms=$((ms / 1000))

echo "$random_float, $random_int, $random_str, $mcs"
echo "$random_float, $random_int, $random_str, $mcs" >> "$destdir"

sleep $ms

done
