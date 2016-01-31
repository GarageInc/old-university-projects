import scipy.io.wavfile as sw
import numpy as np
import pylab as pb
import wave
import struct

print "----------------------------------------------------------------------"

name = "voice.wav"
f=open(name,'rb')
[fr, ati] = sw.read(f)
f.close()

N = len(ati)
#N = 300
outN = ati[0:N]
print len(outN)

print "\n Sampling frequency = ", fr
print "\n Signal array: \n" , np.float32(outN)
twrt = wave.open("voiceNew.wav", 'wb')
pck = []

sum = 0.# сюда просто суммирую всё кадраты амплитуд, независимо от интервала. для интереса

energyN = []# Сюда складываются значения сумм квадратов
# амплитуд на каждом интервале длины intervalN
intervalN=100000;
summator = 0.# В переменную суммируем квадраты амплитуд на интервале
counter = 0
first = True # то, что это первый полуинтервал

for i in range(len(outN)):
    val = outN[i];
    
    if first:
        if counter<intervalN/2:
            summator += val*val        
            counter = counter + 1
        else:
            print "The sum of the squares on the range = ", summator
            counter = 0
            energyN.append(summator)
            summator = 0
            first = False
    else:
        if counter<intervalN:
            summator += val*val        
            counter = counter + 1
        else:
            print summator
            counter = 0
            energyN.append(summator)
            summator = 0
            
        
    sum+=val*val
    pck.append(struct.pack('h',val))

energy = sum/N
print "\n Short-term signal energy = ", energy 


strOut=' '.join(pck)
twrt.setparams((1,2,fr,0,"NONE", "not compressed"))
twrt.writeframes(strOut)
twrt.close()

pb.figure()
pb.plot(outN)

pb.figure()
pb.plot(energyN)