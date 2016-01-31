# -*- coding: utf-8 -*-
"""
Created on Thu Nov 05 12:39:22 2015

@author: NigmatullinR
"""
#IIR фильтр
import numpy as np
import scipy.signal as sgn
import pylab as plb

order=13;#порядок
freq=[ 0.3, 0.75]


b, a=sgn.iirfilter(order, freq, btype='bandpass')#bandstop

w, h = sgn.freqz(b,a)
 
plb.plot(w/np.pi,abs(h))