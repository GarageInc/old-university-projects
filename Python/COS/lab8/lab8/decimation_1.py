# -*- coding: utf-8 -*-
"""
Created on Wed Nov 25 16:56:03 2015

@author: NigmatullinR
"""

import numpy as np
import pylab as plb
import scipy.signal as sgn

Fs=1000
w=100
N=4000#4 секунды сигнал
M=2 # decimation factor

t=np.float64(range(0,N))/Fs#носитель

s=np.sin(2*np.pi*w*t)

plb.figure()
plb.plot(t[0:100],s[0:100])
plb.title('Original signal')

s1=s[:N/4]

fs=np.fft.fft(s1)#преобр Ф
freq=Fs*(np.float64(range(0,N/8))/(N/8))/2


plb.figure()
plb.plot(freq, abs(fs[:N/8]))
#plb.plot(abs(fs[:N/8]))
plb.title('FFT of original signal. Fs=' + str(Fs))

t2=range(0,N,M)#оъявляем диапазон

#y=np.delete(s,t2)
y=s[t2]#потреял сэмплы и гладкость

plb.figure()
plb.plot(y[:20])
plb.title('Signal after decimation')

s2=y[:N/4]

fs2=np.fft.fft(s2)#всплеск на том же месте
freq2=(Fs/np.float64(M))*(np.float64(range(0,N/8))/(N/8))/2


plb.figure()
plb.plot(freq2, abs(fs2[:N/8]))
plb.title('Fs=' + str(Fs/np.float64(M)))


plb.figure()
plb.plot(abs(fs2[:N/8]))#частота изменялась, стала 200
plb.title('Fs=' + str(Fs))