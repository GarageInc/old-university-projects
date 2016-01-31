# -*- coding: utf-8 -*-
"""
Created on Thu Nov 12 11:00:56 2015

@author: NigmatullinR
"""

import numpy as np
import scipy.signal as sgn
import scipy.io.wavfile as sw
import pylab as plb

#FIR фильтр
order=21;
freq=0.6

b=sgn.firwin(order, freq, pass_zero=False)

name = 'voice.wav'

f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

dti=np.float32(dti)/32767.0

outN = sgn.lfilter(b,1,dti)#фаза сдвинута, но не интересует  нас

outN=np.int16(np.round(outN*32767.0))/max(outN)#хотим перевести обратно

plb.figure()
plb.plot(outN)

plb.figure()
plb.plot(dti)
fwrt = open('sndNew2.wav','wb')

sw.write(fwrt, 16000, outN)
fwrt.close()