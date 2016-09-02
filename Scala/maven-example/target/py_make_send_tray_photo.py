
import os.path
import subprocess
import time
import shutil
import datetime
import math

from shutil import copyfile

screenshotPath="/tmp/image.jpg"

def executeCommand( command ):
    print( command )
    subprocess.call( command, shell=True, stderr=subprocess.STDOUT)
	
def makeScreenshot():
    global screenshotPath
    command = "/usr/bin/scrot {0}".format(
		screenshotPath)
    executeCommand( command )
    
def uploadScreenshot():
    GATEWAY_UPLOAD_URL = "127.0.0.1:80/upload_image/"
    ID_TERMINAL = 1

    global screenshotPath

    command = " /usr/bin/curl -i \
 -F cmd=bb.upload_tray_photo \
 -F id_terminal={0} \
 -F name=file \
 -F file=@{1} {2}".format( 
     ID_TERMINAL, 
     screenshotPath, 
     GATEWAY_UPLOAD_URL )
    
    executeCommand( command )


makeScreenshot()
print("maked screenshot...")
uploadScreenshot()
print("...uploaded screenshot")

def testScreenshotsDirs():
	url = "127.0.0.1:9001/"
	command = " /usr/bin/curl -i \
-F cmd=bb.tray_photos_dates \
-F id_terminal={0} {1}".format(1,url)

	executeCommand( command )

testScreenshotsDirs()
