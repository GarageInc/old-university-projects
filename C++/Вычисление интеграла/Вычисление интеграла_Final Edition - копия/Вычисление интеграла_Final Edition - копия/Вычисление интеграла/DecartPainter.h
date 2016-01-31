#pragma once
#include "Converter.h"
using namespace System::Drawing;

ref class DecartPainter//рисует ось и делние на осях
{
protected:
	PanelParameters^ pp;
public:
	DecartPainter(void);
	DecartPainter(PanelParameters^p);
	void PaintAxes(Graphics^g);//два метода:оси и деление
	void PaintDiv(Graphics^ g);

};

