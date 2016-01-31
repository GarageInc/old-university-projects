# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""

def getE(x):
    dVal=1.0
    dTemp=1.0
    nStep = 1;
    
    print(dVal); 
    while nStep < 1000:
        dTemp = dTemp *( x) / nStep
        dVal = dVal + dTemp
        nStep=nStep+1
    
        
    
    print(dVal); 
    
    
a = 1 # int(input())
getE(a)
