# -*- coding: utf-8 -*-
"""
Created on Thu Nov 05 12:17:29 2015

@author: NigmatullinR
"""

# fir filter design # как сконстр FIR фильтр

import numpy as np 
import scipy.signal as sgn
import pylab as plb

order=53;# кол коэф, чем больше, тем круче
freq=[0.4,0.8]#частота отчесения

b=sgn.firwin(order, freq, pass_zero=False) #true--->false ФНЧ

w, h = sgn.freqz(b) #передаточн фкц
 
plb.plot(w/np.pi,abs(h))#модуль нужен т.к.вещ число