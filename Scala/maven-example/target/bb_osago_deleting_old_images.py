#- * -coding: utf - 8 - * -


import os.path
import shutil
import datetime
import re

# / home / www / polis24.net / bb.polis24.net / terminals / 1 / screenshots / 2016 - 05 - 16 $

def removeInDir(directory, countDays):

    if(not os.path.exists( directory )):
        print("not_exist: " + directory)
	return 
        
    dirnames = os.listdir(directory)

    secondsDelta = countDays * 24 * 60 * 60
    
    for dirname in dirnames:
    
        result = re.match(r'^(\d{4})-(\d{2})-(\d{2})$', dirname)
    
        if (result):
        
            year = int(result.group(1))
            month = int(result.group(2))
            day = int(result.group(3))
            
            createdTime = datetime.datetime(year, month, day,)
            
            currentTime = datetime.datetime.today()
            
            if ((currentTime - createdTime).total_seconds() > secondsDelta):
                print("removing: " + directory + dirname)
                shutil.rmtree(directory + dirname)
            else :
                pass



###############################################################################


    
storage = "/home/www/polis24.net/bb.polis24.net/terminals/"
terminalDirs = os.listdir( storage )
    
for terminalDir in terminalDirs:
	removeInDir(storage + terminalDir + "/screenshots/", 1)
        removeInDir(storage + terminalDir + "/photos/", 1)
