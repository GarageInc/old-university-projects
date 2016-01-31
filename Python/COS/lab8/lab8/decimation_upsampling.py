# -*- coding: utf-8 -*-
"""
Created on Wed Nov 25 18:13:27 2015

@author: NigmatullinR
"""
#пример связ с Гаусовским импульсом
import numpy as np
import pylab as plb
import scipy.signal as sgn

M=2 # decimation factor

t = np.linspace(-0.75, 0.75, 2 * 100, endpoint=False)#носитель на кот строится ГИ
g = sgn.gausspulse(t, fc=5, bw=0.3, retquad=False, retenv=False)#Гаус импульс
plb.plot(t, g)
plb.title('Original signal')

plb.figure()
plb.plot(abs(np.fft.fft(g)[:len(g)/2]))#преобр Ф
plb.title('Original signal FFT')

ind2=range(0,len(g),2)
g2=g[ind2]

plb.figure()
plb.plot(g2)
plb.title('Signal after decimation')


plb.figure()
plb.plot(abs(np.fft.fft(g)[:len(g2)/2]))
plb.title('Decimation signal FFT')


g3=np.zeros((M*len(g)))#массив заполн нулями

ind3=range(0,len(g3),M)

g3[ind3]=g#вставляем

plb.figure()
plb.plot(g3)
plb.title('Signal after upsampling')#отображ

plb.figure()
plb.plot(abs(np.fft.fft(g)[:len(g3)/2]))#взяли пр Ф
plb.title('Upsampling signal FFT')

 