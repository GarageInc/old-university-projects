import Blender
from Blender import Draw

mystring = ""
def event(evt, val): 
	global mystring
	if not val:  # val = 0: 
		if evt in [Draw.LEFTMOUSE, Draw.MIDDLEMOUSE, Draw.RIGHTMOUSE]:
			mymsg = "You released a mouse button."
			print 'msg',mymsg
	if evt==Draw.ESCKEY:
		Draw.Exit()		
Draw.Register(None, event,None)