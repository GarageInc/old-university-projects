#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>// Для calloc
#include <conio.h>
#include <iostream>
#include <stdlib.h>
#include <stdint.h>
#include <Windows.h>


#pragma pack(push, 2)
struct BmpHeader {
	unsigned char b1, b2; // Символы BM (2 байта)
	unsigned long size; // Размер файла (4 байта)
	unsigned short notUse1; // (2 байта)
	unsigned short notUse2; // (2 байта)
	unsigned long massPos; // Местанахождение данных растрового массива (4 байта)

	unsigned long headerLength; // Длинна этого заголовка (4 байта)
	unsigned long width; // Ширина изображения (4 байта)
	unsigned long height; // Высота изображения (4 байта)
	unsigned short colorPlaneNumber; // Число цветовых плоскостей (2 байта)
	unsigned short bitPixel; // Бит/пиксель (2 байта)
	unsigned long compressMethod; // Метод сжатия (4 байта)
	unsigned long massLength; // Длинна массива с мусоро (4 байта)
	unsigned long massWidth; // Ширина массива с мусором (4 байта)
	unsigned long massHeight; // Высота массива с мусором (4 байта)
	unsigned long colorNumber; // Число цветов изображения (4 байта)
	unsigned long generalColorNumber; // Число основных цветов (4 байта)

} bmpHeader;
#pragma pack(pop)

// В этот массив засунем все цвета(пиксели
unsigned char* img;


void Change()
{
	int i, j;
	int bytes = bmpHeader.bitPixel / 8;
	int width = (bytes * bmpHeader.width + bytes) & (-4);

	// Значение изменены(X-это игрек и т.п., т.к. центр координат у компьютера обратный к человеку)
	int x = 100;// Начальная точка координата игрек
	int y = 200;// Начальная точка координата икс
	int h = 10;// ширина
	int w = 100;// высота
	
	printf("Введите начальную координату ИКС от 1 до %i: ",bmpHeader.width);
	scanf("%i", &x);
	printf("Введите начальную координату ИГРЕК от 1 до %i: ", bmpHeader.height);
	scanf("%i", &y);
	printf("Введите ширину смещения вправо от 1 до %i:", bmpHeader.width-x);
	scanf("%i", &h);
	printf("Введите высоту смещения вверх от 1 до %i:", bmpHeader.height-y);
	scanf("%i", &w);
	printf("\nВыберите цвет: White=1, Red=2, Blue=3, Black=4\n");
	int color = 0;
	scanf("%i", &color);
	int c1 = 0, c2 = 0, c3 = 0;
	switch (color)
	{
	case 1:
		//c = (char)(RGB(255, 250, 250));
		c1 = 255;
		c2 = 250;
		c3 = 250;
		break;
	case 2:
		c1 = 00;
		c2 = 00;
		c3 = 255;
		break;
	case 3:
		c1 = 255;
		c2 = 00;
		c3 = 00;
		break;
	case 4:
		//c = (char)(RGB(00, 00, 00));
		c1 = 00;
		c2 = 00;
		c3 = 00;
		break;
	}

	for (i = x; i < x + w; i++)
		for (j = y; j < y + h; j++)
		{
			*(img + (i*width) + j*bytes) = (char)c1;
			*(img + (i*width) + j*bytes + 1) = (char)c2;
			*(img + (i*width) + j*bytes + 2) = (char)c3;
		};
}

void Create()
{

}


int readBMP(char* path) {
	FILE* file;
	long long i, j, u;
	long long v;
	file = fopen(path, "rb");
	if (file == NULL) return 0;
	fread(&bmpHeader, sizeof(bmpHeader), 1, file);
	if (bmpHeader.b1 != 'B' || bmpHeader.b2 != 'M' || bmpHeader.bitPixel != 24) {
		printf("corrupted file");
		return 0;
	}

	int mx = bmpHeader.width;
	int my = bmpHeader.height;
	int mx3 = (3 * mx + 3) & (-4);
	img = (unsigned char*)calloc(mx3*my, sizeof(char));
	fread(img, 1, mx3*my, file);
	
	// Теперь изменим
	Change();
	fclose(file);

	return 1;
}


int saveBMP(char* path) {
	FILE* file;
	int i, j;
	file = fopen(path, "wb");
	if (file == NULL) return 0;

	int mx = bmpHeader.width;
	int my = bmpHeader.height;
	int mx3 = (3 * mx + 3) & (-4);
	int filesize = 54 + my*mx3;

	fwrite(&bmpHeader, sizeof(bmpHeader), 1, file);
	fwrite(img, sizeof(char), mx3*my, file);
	fclose(file);
	return 1;
}

int main()
{
	//Чтобы выводился русский язык:
	setlocale(LC_ALL, "Russian");

	// Читаем файл
	if (readBMP("D:\\1.bmp") == 1) {
		printf("Read!\n");
		// Если прочитали - сохраняем файл
		if (saveBMP("D:\\_1.bmp")==1) {
			printf("Write!\n");
		}
		else
		{
			printf("Write error!\n");
		}
	}
	else
	{
		printf("Read error!\n");
	}


	_getch();
	return 0;
}
