# -*- coding: utf-8 -*-
"""
Created on Thu Oct 15 12:30:54 2015

@author: NigmatullinR
"""

import numpy as np #подкл модули
import pylab as plb
import scipy.fftpack as scfft
 #количество точек в синусоиде=1000
w=10 #частота сигнала
Fs=1000 #частота стробирования
t=np.float32(range(1000))/Fs  #время, список из 1000 знач, перевод в массив флот.32
 #частота дискретизации 1000 Гц
s=np.sin(2*w*np.pi*t)
plb.figure()
plb.plot(t,s)

fc=scfft.fft(s) #преоб Ф
plb.figure()
plb.plot(abs(fc)) #абс значение Ф
plb.figure()
plb.plot(abs(fc[:50])) #более детально смотрим первый пик
