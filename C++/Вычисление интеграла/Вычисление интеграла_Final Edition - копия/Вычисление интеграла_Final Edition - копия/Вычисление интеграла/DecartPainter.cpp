#include "DecartPainter.h"

#include "math.h"

using namespace System::Drawing;

using namespace Convertation;


DecartPainter::DecartPainter(void)//конструктор по умолчанию,дает стандартное
{
	pp = gcnew PanelParameters(-1,1,-1,1,500,500);	
}

DecartPainter::DecartPainter(PanelParameters^p){
	pp=p;//записывает в поле класса
}

void DecartPainter::PaintAxes(Graphics^ g){//рисует оси,метод,как параметр принимает график
	Pen^p = gcnew Pen(Color::Black,1);//ручка
	Converter^ con = gcnew Converter(pp);//создаем конвертор
	int x0 = con->XtoPx(0);//это координаты центра
	int y0 = con->YtoPy(0);

	g->DrawLine(p,0,y0,pp->W,y0);//рисуют линиии осей через центр
	g->DrawLine(p,x0,0,x0,pp->H);

	g->DrawLine(p,x0-4,10,x0,0);//просто рисуют стрелочки
	g->DrawLine(p,x0,0,x0+4,10);

	g->DrawLine(p,pp->W-10,y0+4,pp->W,y0)//рисует стрелочки
	g->DrawLine(p,pp->W,y0,pp->W-10,y0-4);
}

void DecartPainter::PaintDiv(Graphics^ g){//метод PaintDiv рисует деление через 0.1
	Pen^p = gcnew Pen(Color::Black,1);//ручка
	PanelParameters^ tmp = gcnew PanelParameters(pp->xmin*10,pp->xmax*10,pp->ymin*10,pp->ymax*10,pp->W,pp->H);//создаем новые параметры окна все в 10 раз увеличиваем кроме высоты и ширины
	
	Converter^ con = gcnew Converter(tmp);//создаем конвертор
	int ed = con->XtoPx(1)-con->XtoPx(0);//единичный отрезок на панели
	int x0 = con->XtoPx(0);//координаты цетра
	int y0 = con->YtoPy(0);
	Font^ fnt = gcnew Font("Cambria", 12);//шрифт,чтоб ы писать
	Brush^br = gcnew SolidBrush(Color::Black);//кисть
	for(int x = 10*pp->xmin+1;x<10*pp->xmax;x+=1){//цикл,который рисует деление
		int px = con->XtoPx(x);//для текущего значения икса переводим в экранные координаты
		if(x%10==0){//если икс делится на 10,то мы рисуем длинную линию
			
			g->DrawLine(p,px,y0-6,px,y0+6);	//рисуем длинную черту		
			System::String^ s=((double)x/10).ToString();//переводим в строку икс деленный на 10
			if(x>0){//чтобы ровно занимали место с минусом тоже					
				g->DrawString(s,fnt,br,px-8,y0+5);
				}
			else {
				g->DrawString(s,fnt,br,px-12,y0+5);//если меньше нуля берем другой отступ
			}
		}
		else if(x%5==0){//если делится на 5,то мы рисуем среднюю черточку
			
			g->DrawLine(p,px,y0-3,px,y0+3);
		}
		else{
			g->DrawLine(p,px,y0-1,px,y0+1);//рисуем совсем короткую черточку 0.1
		}

	}

	for(int y = 10*pp->ymin+1;y<10*pp->ymax;y+=1){// то же самое только для у
		int py = con->YtoPy(y);
		if(y%10==0){
			
			g->DrawLine(p,x0-6,py,x0+6,py);			
			System::String^ s=((double)y/10).ToString();
			if(y>0){					
				g->DrawString(s,fnt,br,x0-20,py-8);
				}
			else if(y<0){
				g->DrawString(s,fnt,br,x0-25,py-8);
			}
		}
		else if(y%5==0){
			
			g->DrawLine(p,x0-3,py,x0+3,py);
		}
		else{
			g->DrawLine(p,x0-1,py,x0+1,py);
		}

	}

}
	