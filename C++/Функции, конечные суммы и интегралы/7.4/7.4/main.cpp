#include <Windows.h>//объявления Win32 API
#include <stdlib.h>
#include <stdio.h>
#include <math.h>//Чтобы использовать функции, получающие дробь
 
int main(void)
{
	//Чертим функцию sin в консоли
    float x=0,xmin=0,xmax=0;//Аргумент функции
	//Введите данные по x
	printf("Xmin=");
		scanf("%f",&xmin);
	printf("Xmax=");
		scanf("%f",&xmax);
	printf("y=cos(x), %f<x<%f\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n",xmin,xmax);//Заголовок функции
	//Консольное рисование
    HDC hDC = GetDC(GetConsoleWindow());
	 int x_max, y_max;//Максимальные значения осей графиков
	 int center_x, center_y; //Центры графиков
	   x_max = GetSystemMetrics(SM_CXSCREEN)/3;//Получаем системные данные о параметрах экрана и делим на 3 - чтобы влезло в консоли
	   y_max = GetSystemMetrics(SM_CYSCREEN)/3;
	   center_x = x_max / 2;//Получаем центр рисунка
	   center_y = y_max / 2;
	
	   
	//Создаем ручку - след от линии функции, которой и будем рисовать
    HPEN Pen = CreatePen( PS_SOLID, 2, RGB(255, 255, 255));//Кисть и цвет

	//Выбираем объекты рисования- консоль и ручка
    SelectObject( hDC, Pen );//Установка цвета фона и текста. SelectObject() выбирает в контекст дисплея (hDC) белую кисть Pen

	/*
	Далее используем стандартные GDI-функции ядра Windows для вывода.
	Здесь используются две функции: MoveToEx и LineTo.
	Первая служит для перемещения графического курсора на контексте, заданном в первом параметре,
	в точку с координатами во втором и третьем параметрах. 
	Четвертый параметр обычно не используется (NULL). 
	По умолчанию ось OX проходит слева направо, OY - сверху вниз, а отсчет ведется в пикселях. 
	LineTo() рисует линию на контексте hDC из текущей позиции курсора в точку, заданную вторым и третьим параметрами текущей кистью (у нас она белая),
	дополнительно передвигая графический курсор. 
	*/			// 0-x 85-y
    MoveToEx( hDC, 0, center_y, NULL );
    LineTo( hDC, center_x*2, center_y );
    MoveToEx( hDC, center_x, 15, NULL );
    LineTo( hDC, center_x, center_y*2 );
    
	for (x = xmin; x <= xmax; x += 0.01f ) // O(100,85) - center, 0.01f - шаг аргумента
    {
        MoveToEx( hDC, 10*x+center_x, -10*cos(x)+center_y, NULL );//10 - шкала-градация
        LineTo( hDC, 10*x+center_x, -10*cos(x)+center_y );
    }


    system("pause");//Пауза - чтобы после выполнения программы экран не погас
    return 0;
}