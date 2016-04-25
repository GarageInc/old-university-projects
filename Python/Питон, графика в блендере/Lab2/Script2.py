import Blender
from Blender import Draw
def event(evt, val):    # 
  if evt == Draw.ESCKEY :	
    Draw.Exit()         # выход из скрипта после нажати€ на ESC 
    return

def button_event(evt):  # обработка нажати€ на кнопку
  if evt == 2:			# 2 Ц уникальный номер кнопки
		print "push"    # проверка работы. –езультат выводитс€ на консоль
		Draw.Redraw(1)
		return
def gui():              # основна€ функци€ 
	Draw.PushButton("Push",2,50,10,55,20,"Our push") # создание кнопки с

Draw.Register(gui, event, button_event)  