# -*- coding: utf-8 -*-
"""
Created on Thu Oct 15 12:24:41 2015

@author: NigmatullinR
"""
 #целочисленные 
from numpy import *
from scipy.fftpack import *
A=range(16) #берем пслдв
print 'original signal:', A

B=fft(A) #берем преобр Ф
print 'Fourier Transform:', B

C=ifft(B) #от преобраз Ф берем обратнрое
print 'Inverse Fourier Transform:', abs(C)
