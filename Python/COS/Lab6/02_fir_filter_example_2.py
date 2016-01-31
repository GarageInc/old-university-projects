# -*- coding: utf-8 -*-
"""
Created on Thu Nov 05 12:35:20 2015

@author: NigmatullinR
"""
#FIR фильтры
import numpy as np
import scipy.signal as sgn
import pylab as plb

order=103;#порядок
freq=[0, 0.3, 0.5,0.6, 1]# содержит 0 и 1, добавили частоту 0.55
gain= [0, 1, 1,0, 0]#высота перед фкц #+ добавили 0 к 0.55

b=sgn.firwin2(order, freq, gain)#создаем фильтр

w, h = sgn.freqz(b)# вычисл передаточн фкц фильтра
 
plb.plot(w/np.pi,abs(h))

#чем больше коэф, тем лучше перед фкц