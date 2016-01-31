# -*- coding: utf-8 -*-
"""
Created on Thu Nov 19 12:25:38 2015

@author: NigmatullinR
"""
 #групповая задержка
import numpy as np
import scipy.signal as sgn
import pylab as plb

order=10;
freq=0.9

b=sgn.firwin(order, freq, pass_zero=True)

print 'coefficients:', b

w, h = sgn.freqz(b)
 
plb.plot(w/np.pi,abs(h))

hn=h/(abs(h))
ha=np.angle(hn)
gd=np.diff(ha)/np.diff(w) #групповая задержка, разность от фазы и от частот
plb.figure()

plb.plot(w[:-1],gd)