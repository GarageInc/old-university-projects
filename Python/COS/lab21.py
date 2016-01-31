# -*- coding: utf-8 -*-
"""
Created on Thu Oct 01 14:30:55 2015

@author: Сабина
"""
import numpy.fft as nf
import pylab as pl 
a=pl.randn(1,10)
print 'a',a
f=nf.fft(a)
print f
invf=nf.ifft(f)
print abs (invf)
