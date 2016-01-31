# -*- coding: utf-8 -*-

import numpy as np
import numpy.fft as nf
import scipy.signal as sgn
import pylab as plb
import scipy.io.wavfile as sw
import wave 
import struct 

w0 = 1000 # частота на которую хотим произвести сдвиг
name = 'voice.wav' 

# открываем файл и считываем сигнал
f = open(name, 'rb')
Fs, x = sw.read(f)
f.close

# берем только часть сигнала 
s=x[26000:58000]
s=np.float64(s)/(2**15)

# выводим график сигнала
plb.figure()
plb.plot(s)

# отображаем спектр оригинального сигнала
fc=nf.fft(s)
freq=(Fs/2)*np.arange(0,len(fc)/2)/(len(fc)/2)
plb.figure()
plb.plot(freq,abs(fc[:len(fc)/2]))

# формируем последовательность sin(2*pi*w0*n*T) для сдвига спектра
sin_shift = np.sin(2 * np.pi * w0 * np.float64(range(0, len(s))) /Fs)

# выходной сигнал. пока заполняем нулями.
s_out=np.zeros(len(s))



order=11; # порядок фильтра
band_size=w0*1.5/(Fs/2) # ширина полосы в гребенке фильтров. Делим сразу на частоту найквиста, чтобы можно быо сразу использовать в построении фильтров.


# формируем первый фильтр в гребенке. Это будет ФНЧ т.к. работаем с крайней левой полосой в спектре
b, a=sgn.iirfilter(order, band_size, btype='lowpass')

# выводим передаточную функцию фильтра
w, h = sgn.freqz(b,a)
plb.figure() 
plb.plot((Fs/2)*w/np.pi,abs(h))

# производим фильтрацию. здесь мы выделили нужную полосу в спектре, а остальную его часть подавили.
s_filt=sgn.filtfilt(b,a,s)

# отобразили, выделенную полосу спектра
fc_band=nf.fft(s_filt)
plb.figure()
plb.plot(freq,abs(fc_band[:len(fc_band)/2]))

# производим сдвиг спектра с помощью умножения на послеовательность sin(2*pi*w0*n*T)
s_shift=s_filt*sin_shift   

# выводим спектр после сдвига
fc_shift=nf.fft(s_shift)
plb.figure()
plb.plot(freq,abs(fc_shift[:len(fc_shift)/2]))


# т.к. после сдвига будет 2 части спектра, одну из них надо подавить. Оставляем правую часть, а подавляем левую. Для этого используем фильтр высоких частот.
b1, a1=sgn.iirfilter(order, band_size/2, btype='highpass')
w1, h1 = sgn.freqz(b1,a1)
plb.figure()
plb.plot((Fs/2)*w1/np.pi,abs(h1))

s_out=sgn.filtfilt(b1,a1,s_shift)


# выводим спектр после подавления левоц части
fc_band2=nf.fft(s_out)
plb.figure()
plb.plot(freq,abs(fc_band2[:len(fc_band2)/2]))


# в цикле делается все тоже сомое что и для первой полосы спектра.


start_freq=band_size/2
end_freq=1.5*band_size

while (end_freq<0.9):
    frqs=[start_freq, end_freq]
    b, a=sgn.iirfilter(order, frqs, btype='bandpass')
    w, h = sgn.freqz(b,a)
    plb.figure() 
    plb.plot((Fs/2)*w/np.pi,abs(h))
    s_filt=sgn.filtfilt(b,a,s)
    
    fc_band=nf.fft(s_filt)
    plb.figure()
    plb.plot(freq,abs(fc_band[:len(fc_band)/2]))
    
    
    s_shift=s_filt*sin_shift   
    
    fc_shift=nf.fft(s_shift)
    plb.figure()
    plb.plot(freq,abs(fc_shift[:len(fc_shift)/2]))

    
    
    b1, a1=sgn.iirfilter(2*order, start_freq+band_size/2, btype='highpass')
    w1, h1 = sgn.freqz(b1,a1)
    plb.figure()
    plb.plot((Fs/2)*w1/np.pi,abs(h1))
    
    s_filt2=sgn.filtfilt(b1,a1,s_shift)
    
    
    
    
    
    fc_band2=nf.fft(s_filt2)
    plb.figure()
    plb.plot(freq,abs(fc_band2[:len(fc_band2)/2]))
    
    s_out+=s_filt2    
    
    start_freq+= band_size/2
    end_freq+= band_size/2


# в конце обрабатываем последнюю полосу в спектре. Все так же как и для других полос.

b, a=sgn.iirfilter(order, 1-band_size, btype='highpass')

w, h = sgn.freqz(b,a)
plb.figure() 
plb.plot((Fs/2)*w/np.pi,abs(h))

s_filt=sgn.filtfilt(b,a,s)

fc_band=nf.fft(s_filt)
plb.figure()
plb.plot(freq,abs(fc_band[:len(fc_band)/2]))


s_shift=s_filt*sin_shift   

fc_shift=nf.fft(s_shift)
plb.figure()
plb.plot(freq,abs(fc_shift[:len(fc_shift)/2]))



b1, a1=sgn.iirfilter(order, 1-band_size/2, btype='highpass')
w1, h1 = sgn.freqz(b1,a1)
plb.figure()
plb.plot((Fs/2)*w1/np.pi,abs(h1))

s_filt2=sgn.filtfilt(b1,a1,s_shift)


fc_band2=nf.fft(s_filt2)
plb.figure()
plb.plot(freq,abs(fc_band2[:len(fc_band2)/2]))

s_out+=s_filt2

# выводим получившийся сигнал
plb.figure()
plb.plot(s_out)
plb.plot(s)

# выводим спектр получившегося сигнала. Видим сдвиг.    
fc_out=nf.fft(s_out)
plb.figure()
plb.plot(freq,abs(fc_out[:len(fc_out)/2]))



# подготавливаем сигнал длязаписи в файл.
s_out=s_out*(2**15)
s_out=np.int32(s_out)

def writeInWav(name, outN):
    fwrt = wave.open(name,'wb')
    pck = []
    for i in xrange(len(outN)):
        val = outN[i]
        pck.append(struct.pack('h',val))
    
    strOut = ''.join(pck)
    
    fwrt.setparams((1,2, Fs,0, 'NONE', 'not compressed'))
    fwrt.writeframes(strOut)
    fwrt.close()

writeInWav('newWave.wav',s_out)
