#pragma once

//класс который преобразует из декартовых в экранную и наоборот
ref struct PanelParameters{//структура параметры самого окошка
	double xmin,xmax,ymin,ymax;
	int W,H;//ширина и высота окна
public:PanelParameters(double xmin, double xmax,double ymin,double ymax,int W,int H){//конструктор ,присваиваем все значения полям
	   this->xmin=xmin;
	   this->xmax=xmax;
	   this->ymin=ymin;
	   this->ymax=ymax;
	   this->W=W;
	   this->H=H;
	   }
};


namespace Convertation{//в самом си+++ есть класс конвертор встроенный,чтобы они не пересеклись,прост-во имен

ref class Converter{
protected:
	PanelParameters^pp;//единственное поле класса,чтобы преобразовывать длига,ширина...
	
public:
	Converter(void);//два конструктора
	Converter(PanelParameters^p);
	int XtoPx(double x);//из декартовых в экранное
	int YtoPy(double y);
	double PxtoX(int X);//тут наоборот
	double PytoY(int Y);

};
}