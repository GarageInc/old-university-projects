import scipy.io.wavfile as sw
import numpy as np
import pylab as pb

print "----------------------------------------------------------------------"

name = "voice.wav"
f=open(name,'rb')
[fr, ati] = sw.read(f)
f.close()

#N = len(ati)
N = 100# длина массив сигнала
intervalN=10; # длина интервала

outN = ati[0:N]
print len(outN)

print "\n Sampling frequency = ", fr
print "\n Signal array: \n" , np.float32(outN)

zerocrossing = [] # массив для печати(элемент - количество пересечений нуля на интервале)

summator = 0.
counter = 0
first = True # то, что это первый полуинтервал

for i in range(1,len(outN)):
    val = outN[i];
    
    if first:
        if counter<intervalN/2:
            counter = counter + 1
            if ((outN[i]<=0 and outN[i-1]>=0) or (outN[i]>=0 and outN[i-1]<=0)):
                summator += 1
        else:
            counter = 0
            zerocrossing.append(summator)
            summator = 0
            first = False
    else:
        if counter<intervalN:                  
            counter = counter + 1
            if ((outN[i]<=0 and outN[i-1]>=0) or (outN[i]>=0 and outN[i-1]<=0)):
                summator += 1
        else:
            counter = 0
            zerocrossing.append(summator)
            summator = 0
            

pb.figure()
pb.plot(outN)

pb.figure()
pb.plot(zerocrossing)