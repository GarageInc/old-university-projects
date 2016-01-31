# -*- coding: utf-8 -*-
"""
Created on Thu Dec 03 10:36:18 2015

@author: NigmatullinR
"""

import numpy as np
import pylab as plb
import scipy.signal as sgn

Fs=100
w=10
w0=
N=100
M=2 # decimation factor

t=np.float64(range(0,N))/Fs

s=np.sin(2*np.pi*w*t)# будем менять реальный сигнал

plb.figure()
plb.plot(t,s)
plb.title('Original signal')

sa=sgn.hilbert(s)# постр аналитич сигнала!!!


ex=np.exp(2*np.pi*1j*w0*np.float64(range(0,N))/Fs)# послед формируется 

z = sa*ex;# формир ся сигнал
rz=np.real(z)
plb.figure()
plb.plot(t,rz)
plb.title('Real part of analytic signal')


fzr=np.fft.fft(rz)# берем преоб Фурье

plb.figure()
plb.plot(abs(fzr[:N/2]))
plb.title('FFT of real part analytic signal. Fs=' + str(Fs))