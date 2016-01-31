# -*- coding: utf-8 -*-
"""
Created on Thu Sep 10 11:27:28 2015

@author: NigmatullinR
"""

import scipy.io.wavfile as sw #ñïîñîá ÷òåíèÿ, ìîäóëü äëÿ ÷òåí è çàïèñè
import numpy as np 

import wave # ñ÷èòûâàåòñÿ ìîäóëü è çàïèñûâàåòñÿ
import struct # ñòðóêòóðà, â êîò óïàêîâûâàåì, ÷òîáû çàïèñàòü

name = 'voice.wav'#îáúÿâëåíèå èìåíè â ðàáî÷åé äèðåêòîðèè, ïóòü ïðîïèñàòü

f=open(name,'rb') #÷òåíèå 3 øàãà: îòêð, ñ÷èò,çàêð
[fr,dti] = sw.read(f)#÷àñòîòà äèêñðåòèçàöèè è ñàì ñèãíàë 44100 èëè 16000
f.close()

outN = dti[5:300000] #ó÷àñòîê, ñåìïë

print 'length', len(dti) #äëèíà

print 'sampling frequency', fr #÷àñòîòà ñåìïëèðîâàíèå

print  'signal example', np.float32(dti[5:300]) #çíà÷åíèÿ ñèãíàëà



fwrt = wave.open('sndNew.wav','wb')#îòêð ôàéë íà çàïèñü
pck = []# ñîçä ïóñò ñïèñîê
for i in xrange(len(outN)):
    val = outN[i]
    pck.append(struct.pack('h',val))#çàïèñü â ñïèñîê ïàê

strOut = ''.join(pck)#óïàêîâûâ â ïåðåì ñòðàóò

fwrt.setparams((1,2, fr,0, 'NONE', 'not compressed'))#1-êîë êàíàëîâ, 2 - êîëè÷åñòâî áàéò íà îòñ÷åò, ôð-÷àñòîòà ñòðàáèðîâàíèÿ, ïîñëåäíåå - íå ìåíÿåì
fwrt.writeframes(strOut)#çàïèñü â âûõîäíîé ôàéë
fwrt.close()#çàêðûòü