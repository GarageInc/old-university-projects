#include "FunctionPainter.h"
using namespace System;

FunctionPainter::FunctionPainter(Function^ p,PanelParameters^pan):DecartPainter(pan)//конструктор 
{
	this->p = p;
	
}

void FunctionPainter::DrawFunc(Graphics^ g,double min,double max){//метод  рисует функцию,оси ,деление
	PaintAxes(g);
	PaintDiv(g);
	Pen^pen = gcnew Pen(Color::Green,2);//ручка
	
	Convertation::Converter^ con=gcnew Convertation::Converter(pp);//конвертор переводит координаты
	
	for(int i=0;i<pp->W;i++){//икс переводим в декартову считаеим значение ,переходим обратно в экранное
		double x1 = con->PxtoX(i); 
		double x2 = con->PxtoX(i+1);
		double y1 = p->Calc(x1);
		double y2 = p->Calc(x2);
		
		int py1 = con->YtoPy(y1);
		int py2 = con->YtoPy(y2);
		g->DrawLine(pen,i,py1,i+1,py2);//соедин€ем точки отрезками
	}
	
	pen = gcnew Pen(Color::BlueViolet);//ручка
	
	
	g->DrawLine(pen,con->XtoPx(min),con->YtoPy(p->GetInf()),con->XtoPx(min),con->YtoPy(p->GetSup()));//рисуетс€ пр€моугольник,в котором считаеис€ это значение
	g->DrawLine(pen,con->XtoPx(max),con->YtoPy(p->GetInf()),con->XtoPx(max),con->YtoPy(p->GetSup()));
	g->DrawLine(pen,con->XtoPx(min),con->YtoPy(p->GetInf()),con->XtoPx(max),con->YtoPy(p->GetInf()));
	g->DrawLine(pen,con->XtoPx(min),con->YtoPy(p->GetSup()),con->XtoPx(max),con->YtoPy(p->GetSup()));
	

}

void Function::FindInfSup(double xmin,double xmax){//ищет инфимум и супремум
	
	inf =Calc(xmin);
	sup=inf;
	for(double i=xmin;i<=xmax;i+=0.001){//провер€ем на супремум 
		double y = Calc(i);
		if(y>sup)sup=y;
		if(y<inf)inf=y;

	}
	if(Calc(xmin)>sup)sup = Calc(xmin);//на границах провер€етс€ минимум и максимум
	if(Calc(xmax)>sup)sup = Calc(xmax);
	if(Calc(xmin)<inf)inf = Calc(xmin);
	if(Calc(xmax)<inf)inf = Calc(xmax);
	if(sup<0)sup=0;
	if(inf>0)inf=0;
}

double Function::GetInf(){//просто возвращают
	return inf;
}
double Function::GetSup(){
	return sup;
}