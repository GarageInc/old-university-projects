
import scipy.io.wavfile as sw 
import scipy.signal as sgn
from scipy.signal import * 
import scipy.io.wavfile as sw
import numpy as np
import pylab as plb
import scipy.fftpack as scfft 

import wave 
import struct 


# Функция записи в файл
def writeInWav(name, outN):
    fwrt = wave.open(name,'wb')
    pck = []
    for i in xrange(len(outN)):
        val = outN[i]
        pck.append(struct.pack('h',val))
    
    strOut = ''.join(pck)
    
    fwrt.setparams((1,2, fr,0, 'NONE', 'not compressed'))
    fwrt.writeframes(strOut)
    fwrt.close()

def printSignal(signal, label = ''):
    plb.figure()
    plb.plot(signal)
    plb.title(label)

# ОТКРЫВАЕМ ФАЙЛЫ ДЛЯ ЧТЕНИЯ ##################################################
name = 'voice.wav'
f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

order=4
freq=[0.1]#частота отсечения

top = 55000
bottom = 54500
t=range(top-bottom)
s= dti[bottom:top] #берем учасок с голосом

# ВЫВЕДЕМ НА ПЕЧАТЬ ##############################################

N = top - bottom
t2 = np.float32(range(N))/N
s = np.sin(2*np.pi*50*t2)
g = np.sin(2*np.pi*20*t2)

printSignal(s,'Original')
printSignal(g,'Sinus')

# возьмём обратное преобразование Фурье:#######################################
modulated = s*(1+0.5*g)
printSignal(modulated,'Ampl modulation result')

fft_mod = scfft.fft(modulated)

fft_modulated = scfft.fft(modulated)
printSignal(abs(fft_modulated[:100]), 'FFT modulated')

fft_modulated = scfft.fft(g)
printSignal(abs(fft_modulated[:100]), 'FFT sinus')


#фильтрация ФНЧ ###############################################################
order=51;
w = 45
freq = np.float32(2*w)/N#частота отсечения

b=sgn.firwin(order, freq, pass_zero=True)#, pass_zero=True)

sig_ff = sgn.filtfilt(b, 1., modulated)
sig_lf = sgn.lfilter(b, 1., modulated)

border = 20
plb.figure()
plb.plot(modulated[0:5*border], color='red', label='Original')
plb.plot(sig_ff[0:5*border], color='blue', label='filtfilt')
plb.plot(sig_lf[0:5*border], color='green', label='lfilter')
plb.title('Result FIR')
printSignal(abs(scfft.fft(sig_ff))[0:100], 'sig_ff FIR')
printSignal(abs(scfft.fft(sig_lf))[0:100], 'sig_lf FIR')

###############################################################################
# HILBERT #
###############################################################################

b=sgn.hilbert(modulated,order)
sig_ff = sgn.filtfilt(b, 1., modulated)
sig_lf = sgn.lfilter(b, 1., modulated)

border = 20
plb.figure()
plb.plot(modulated[0:5*border], color='red', label='Original')
plb.plot(sig_ff[0:5*border], color='blue', label='filtfilt')
plb.plot(sig_lf[0:5*border], color='green', label='lfilter')
plb.title('Result Hilbert')

printSignal(abs(scfft.fft(sig_ff))[0:100], 'sig_ff HILBERT')
printSignal(abs(scfft.fft(sig_lf))[0:100], 'sig_lf HILBERT')

###############################################################################
# pass
###############################################################################

w1 = 34
w2 = 66

order = 101
freqs = [  np.float32(2*w1)/N,  np.float32(2*w2)/N]

b = sgn.firwin(order, freqs, pass_zero=False)#bandstop
sig_ff = sgn.filtfilt(b, 1., modulated)
sig_lf = sgn.lfilter(b, 1., modulated)


plb.figure()
plb.plot(modulated, color='red', label='Original')
plb.plot(sig_ff, color='blue', label='filtfilt')
plb.plot(sig_lf, color='green', label='lfilter')
plb.title('Result pass')

printSignal(abs(scfft.fft(modulated))[:100], 'sig_ff FFT pass')
printSignal(abs(scfft.fft(sig_ff))[:100], 'sig_ff FFT pass')
printSignal(abs(scfft.fft(sig_lf))[:100], 'sig_lf FFT pass')











