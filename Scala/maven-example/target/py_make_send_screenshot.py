
import os.path
import subprocess
import time
import shutil
import datetime
import math

from shutil import copyfile

photoPath="/work/work/image.jpg"

def executeCommand( command ):
    print( command )
    subprocess.call( command, shell=True, stderr=subprocess.STDOUT)
	
def makePhoto():
    global photoPath
    command = "/usr/bin/scrot {0}".format(
		photoPath)
    executeCommand( command )
    
def uploadPhoto():
    GATEWAY_UPLOAD_URL = "127.0.0.1:80/upload_image/"
    ID_TERMINAL = 3

    global photoPath

    command = " /usr/bin/curl -i \
 -F cmd=myservice.func \
 -F id_terminal=3 \
 -F name=file \
 -F file=@{1} {2}".format( 
     ID_TERMINAL, 
     photoPath, 
     GATEWAY_UPLOAD_URL )
    
    executeCommand( command )


makePhoto()
print("maked photo...")
uploadPhoto()
print("...uploaded photo")
