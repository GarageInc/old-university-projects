#include "Converter.h"
using namespace Convertation;

Converter::Converter(void){//конструктор 
	pp = gcnew PanelParameters(-1,1,-1,1,500,500);//параметры по умолчанию
}

Converter::Converter(PanelParameters^ p){//второй конструктор
	if(p->xmin>=p->xmax) p->xmin = p->xmax-1;//выплняем проверку минимум был меньше максимума а высота и ширина были больше нуля
	if(p->ymin>=p->ymax) p->ymin = p->ymax-1;
	if(p->W<=0) p->W = 500;
	if(p->H<=0) p->H = 500;
	pp=p;//уже присваиваем полю класса вот это значение
}

int Converter::XtoPx(double x){//переводит из декартову в пиксельную
	if (x<pp->xmin){//делаем так,чтобы если икс не лежит в видимой области панели,тоделаем так,чтобы он туда не попал
		x=pp->xmin-1;
	}
	if(x>pp->xmax){//то же самое
		x=pp->xmax+1;
	}
	return  (x-pp->xmin)*pp->W/(pp->xmax-pp->xmin);//возвращае,формула перевода
}


int Converter::YtoPy(double y){//то же самое с у
	if (y<pp->ymin){
		y=pp->ymin-1;
	}
	if(y>pp->ymax){
		y=pp->ymax+1;
	}
	
	return (pp->ymax-y)*pp->H/(pp->ymax-pp->ymin);
}


double Converter::PxtoX(int X){//тут наоборот
	if(X<0) X=-1;//то же самое,что и для обратного перевода
	if(X>pp->W) X=pp->W+1;
	
	return pp->xmin+X*(double)(pp->xmax-pp->xmin)/pp->W;
}


double Converter::PytoY(int Y){
	if(Y<0) Y=-1;
	if(Y>pp->H) Y=pp->H+1;
	return pp->ymax-Y*(double)(pp->ymax-pp->ymin)/pp->H;
}