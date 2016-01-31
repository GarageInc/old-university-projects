# -*- coding: utf-8 -*-
"""
Created on Thu Sep 10 11:27:28 2015

@author: NigmatullinR
"""

import scipy.io.wavfile as sw
import numpy as np
import pylab as plb



name = 'voice.wav'

f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

outN = dti[5:300]

print 'length', len(dti)

print 'sampling frequency', fr

print  'signal example', np.float32(dti[5:300])

plb.figure()

plb.plot(outN)

