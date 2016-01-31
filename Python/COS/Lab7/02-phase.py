# -*- coding: utf-8 -*-
"""
Created on Thu Nov 19 11:24:24 2015

@author: NigmatullinR
"""
 #вычисл фазу
import numpy as np
import scipy.signal as sgn
import pylab as plb

order=10;
freq=0.9

b=sgn.firwin(order, freq, pass_zero=True)

print 'coefficients:', b

w, h = sgn.freqz(b) #w-частоты, h-знач передаточной фкц,h-комплексн-берем модуль
 
plb.plot(w/np.pi,abs(h))

hn=h/(abs(h)) #нормируем

plb.figure()

plb.plot(w, np.angle(hn)) #угол соотв фазе
plb.plot(w, np.angle(hn)/w) #фазовая задержка
 #синие-фаза
 #зелен-фаз задержка