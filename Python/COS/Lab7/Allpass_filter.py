# -*- coding: utf-8 -*-
"""
All pass filter
"""
import numpy as np
import scipy.signal as sgn
import pylab as plb
import math

order=10;
freq=0.9

b=sgn.firwin(order, freq, pass_zero=True)

#print 'coefficients:', b

w, h = sgn.freqz(b) #w-частоты, h-знач передаточной фкц,h-комплексн-берем модуль
 
#plb.plot(w/np.pi,abs(h))

#hn=h/(abs(h)) #нормируем

#plb.figure()

#plb.plot(w, np.angle(hn)) #угол соотв фазе
#plb.plot(w, np.angle(hn)/w) #фазовая задержка


#----------------------------------------#
def letsPrint(Hw):
    #w= np.range(0,1)
    Fs=100
    t=np.float32(range(100))/Fs
    #
    #print len(Hw)
    #print w
    #print abs(Hw)
    plb.figure()
    plb.plot(w,Hw) #угол соотв фазе
    

a=0.5 #должно быть меньше по модулю единицы
e=math.e
Hw=[]

for element in w:
    two = 0+2j
    r = (-1) * two * np.pi *element
    #print r
    Hw.append( ( (a-e**r) / (1-a*(e**r)) ) )
    
#print Hw
letsPrint(Hw)







