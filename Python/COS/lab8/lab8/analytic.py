# -*- coding: utf-8 -*-
"""
Created on Thu Nov 26 12:28:16 2015

@author: NigmatullinR
"""
#
import numpy as np
import pylab as plb
import scipy.signal as sgn

Fs=100
N=200
f=5

t=np.float32(range(0,100))/100

S1=np.sin(2*np.pi*10*np.arange(N)/N)
S2=np.sin(2*np.pi*30*np.arange(N)/N)

S=S1+S2

S_a=sgn.hilbert(S)#берем аналитич сигнал

plb.figure()
plb.plot(t,S, color='blue', label='Original')
plb.plot(t,abs(S_a), color='red', label='Analytic')#взяли модель= мгновенная амплитуда каждый раз=1

fs=np.fft.fft(S)#пр Ф от Ориг
fa=np.fft.fft(S_a)#пр Ф от аналитич сигн-удваение спектра, обнуляетс сигнал во второй поовине

plb.figure()
plb.plot(abs(fs), color='blue', label='Original')
plb.plot(abs(fa), color='red', label='Analytic')