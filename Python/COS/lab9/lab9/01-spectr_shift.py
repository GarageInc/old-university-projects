# -*- coding: utf-8 -*-
"""
Created on Thu Dec 03 10:36:18 2015

@author: NigmatullinR
"""

import numpy as np
import pylab as plb
import scipy.signal as sgn

Fs=100
w=10# оригин част
w0=5# оригин част
N=100
M=2 # decimation factor
freq=0.2

t=np.float64(range(0,N))/Fs

s=np.sin(2*np.pi*w*t)

plb.figure()
plb.plot(t,s)
plb.title('Original signal')


fs=np.fft.fft(s)

plb.figure()
plb.plot(abs(fs[:N/2]))
plb.title('FFT of original signal. Fs=' + str(Fs))

ex=np.exp(2*np.pi*1j*w0*np.float64(range(0,N))/Fs)# послед

s2 = s*np.cos(2*np.pi*w0*np.float64(range(0,N))/Fs);


b=sgn.firwin(31, freq, pass_zero=True)

sig_ff = sgn.filtfilt(b, 1, s2)
fs2=np.fft.fft(sig_ff)

plb.figure()
plb.plot(t,sig_ff)


#ff2=np.fft.fft(s2)

plb.figure()
plb.plot(abs(fs2[:N/2]))
#plb.plot(t,s)
plb.title('FFT of modified signal. Fs=' + str(Fs))