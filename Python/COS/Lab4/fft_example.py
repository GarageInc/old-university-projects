# -*- coding: utf-8 -*-
"""
Created on Thu Sep 10 11:27:28 2015

@author: NigmatullinR
"""
 #вещественные числа
import numpy.fft as nf #подкл два модуля
import pylab as pl

a= pl.randn(1,10) #генер псевдослуч числа с норм распред

print a #

f=nf.fft(a) #берется преобраз Фурье

print f #

invf = nf.ifft(f) # Берется обратное
    
print abs(invf) #выводится абсолютное значение



