# -*- coding: utf-8 -*-
"""
Created on Thu Oct 08 14:41:24 2015

"""
import numpy as np
import pylab as plb

#######################################################
def ht(t,T):
    ht = np.sin(np.pi*t/T)/(t*np.pi/T)
    return ht

#######################################################
fr=2
N=6020
tmass=np.float32(range(-N/2,N/2,10))/1000


#######################################################
h = ht(tmass,np.float32(1)/fr)
plb.figure()
plb.plot(h)

#######################################################
M=5
x=np.sin(2*M*np.pi*tmass)
plb.figure()
plb.plot(tmass,x)

yt=[]    
shagi=10
N=N*shagi
t2mass=np.float32(range(-N/2,N/2,10))/10000

T=np.float32(1)/fr
#print t2mass[0],t2mass[1]
for t2 in t2mass:
    summa=0.0
    for nT in tmass:
        val=t2-nT
        summa += np.sin(2*M*np.pi*nT) * np.sin(np.pi*val/T)/(val*np.pi/T)
    yt.append(T*summa)

#for t in tmass[1:]:
#    summa=0.0
#    T=np.float32(1)/fr
#    for n in range(shagi):  
#        nT = n*t2
#        val=t-nT
#        
#        summa += (np.sin(2*M*np.pi*nT) * np.sin(np.pi*val/T)/(val*np.pi/T))
#        
#    yt.append(T*summa)

plb.figure()
plb.plot(t2mass,yt)
