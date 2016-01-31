# -*- coding: utf-8 -*-


import numpy as np
import numpy.fft as nf
import scipy.signal as sgn
import pylab as plb
import scipy.io.wavfile as sw

w0 = 5000
name = 'voice.wav'
f = open(name, 'rb')
Fs, x = sw.read(f)
f.close

x=x[26000:58000]
plb.figure()
plb.plot(x)


sv = sgn.hilbert(x)
ex = np.exp(2 * np.pi * 1j * w0 * np.float64(range(0, len(x))) / Fs)
z = np.real(sv * ex)
plb.figure()
plb.plot(np.int32(z ))

fc = nf.fft(x)
w = np.float64(range(len(x) / 2)) / len(x) * Fs
plb.figure()
plb.plot(w, abs(fc[:len(x) / 2]))

fc1 = nf.fft(z)
plb.figure()
plb.plot(w, abs(fc1[:len(x) / 2]))

fwrt = open('out_analytic.wav','wb')
sw.write(fwrt, 16000, np.int32(z))
fwrt.close()