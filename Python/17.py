
# СДВИГ РЕЧЕВОГО СИГНАЛА НА УКАЗАННУЮ ЧАСТОТУ
import scipy.io.wavfile as sw 
import numpy as np
import pylab as plb

# Считываем участок с голосом и выводим его
# ОТКРЫВАЕМ ФАЙЛ ДЛЯ ЧТЕНИЯ
name = 'voice.wav'
f=open(name,'rb')
[fr,dti] = sw.read(f)
f.close()

t=range(55000-54000)
voiceSig= dti[54000:55000] #берем учасок с голосом

#
s = voiceSig

#plb.figure()
#plb.plot(t,s)
#plb.title('Original signal')


plb.figure()
plb.plot(t,abs(np.fft.fft(s)))
plb.title('FFT of real signal.')

sa=sgn.hilbert(s)# постр аналитич сигнала!!!

w0=15
ex=np.exp(2*np.pi*33j*w0*np.float64(range(0,len(t)))/len(t))# послед формируется 

z = sa*ex;# формир ся сигнал
rz=np.real(z)

#plb.figure()
#plb.plot(t,z)
#plb.title('Real part of analytic signal')


fzr=np.fft.fft(z)# берем преоб Фурье

# ПРоиЗОШЕЛ СДВИГ на 500 Hz!
plb.figure()
plb.plot(abs(fzr))
plb.title('FFT of real part analytic signal.')