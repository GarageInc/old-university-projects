# -*- coding: utf-8 -*-
"""
Created on Thu Nov 05 12:45:56 2015

@author: NigmatullinR
"""

import numpy as np
from numpy.random import randn#генерир пседослуч числа с норм распре
import scipy.signal as sgn
import pylab as plb

#фиияльтрация ФНЧ
order=130;
freq=0.05#частота отсечения

b=sgn.firwin(order, freq, pass_zero=True)

w, h = sgn.freqz(b)#коэф передаточн фкц
plb.figure()
plb.plot(w/np.pi,abs(h))

sig = np.cumsum(randn(800))  # Brownian noise, комулитивня сумма, след знач- сумма всех предыдущих
sig_ff = sgn.filtfilt(b, 1, sig)
sig_lf = sgn.lfilter(b, 1, sig)
plb.figure()
plb.plot(sig, color='silver', label='Original')
plb.plot(sig_ff, color='#3465a4', label='filtfilt')
plb.plot(sig_lf, color='#cc0000', label='lfilter')
plb.legend()