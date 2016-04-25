#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <thread>
#include <future>
#include <fstream>
#include <vector>
#include <algorithm>

enum MOVE
{
	NORTH = 0,
	WEST = 1,
	SOUTH = 2,
	EAST = 3
};

class Point {
public:
	int x = -1; // Координаты
	int y = -1;
};

// Класс Дырка
class Cell:Point {
public:
	int ballNumber = -1;// Одновременно является показателем: если положительное число, то указывает на номер шарика, иначе - это обычная ячейка
};

class Ball:Point {
public:
	int number = -1;

	Ball(int number) {
		this->number = number;
	};

	bool CanMove(int moveType) {


		return true;
	}
};

// Ничего и нигде не сортируется, прямо равномерный просмотр

std::mutex locker;

// Размер доски
int SIZE = 0;
int COUNT = 0;

// Шарики
std::vector <Ball> balls(0);

// Вертикальные и горизонтальные границы вправо-вниз. Т.е. номер клетки: если вертикально, то граница справа, если горизонтально, то граница снизу
std::vector<std::pair<int, std::vector<int>>> vertical_borders(0);
std::vector<std::pair<int, std::vector<int>>> horizontal_borders(0);

// Дырки, индекс идет вниз, вектор - представляет собой номера клеток.
std::vector<std::pair<int, std::vector<Cell>>> cells(0);

void BuildFrom( char * fileName) {
	std::ifstream fin;// файл для записи
	fin.open( fileName ); // связываем объект с файлом
		
	fin >> SIZE;	
	fin >> COUNT;
		
	int x0, y0, x1, y1;

	// Считываем шарики
	for (int i = 0; i < COUNT; i++) {
		fin >> x0 >> y0;
		
		Ball ball( i );
		ball.x = x0;
		ball.y = y0;

		balls.push_back( ball );
	}
	
	// Считали дырки
	for (int i = 0; i < COUNT; i++) {
		fin >> x0 >> y0;

		Cell cell;
		cell.x = x0;
		cell.y = y0;

		cell.ballNumber = i;
		
		// Найдем слот, если он существует. Не делаем псевдосортировку, она не нужна.
		int k = 0;
		for (; k < cells.size; k++) {
			if ( cells[ k ].first == y  ) {
				break;
			}// pass
		}

		if ( cells.size <= k ) {
	
			// Если дошли до конца - просто добавляем, искать место дальше смысла нет
			std::pair<int, std::vector<Cell >> newPair;

			newPair.first = y;
			newPair.second = std::vector<Cell>(0);

			newPair.second.push_back(cell);

			continue;
		}
		
		int j = 0;

		// а вот тут сортированность вектора сильно упростит задачу
		for (; j < cells[k].second.size; j++) {
			if (x0 > cells[k].second[j].x) {
				break;
			} // pass
		}

		// правильную вставку!
		cells[k].second.insert(cell после j-го);
	}
		
	// Считываем границы
	while (!fin.eof()) {

		fin >> x0 >> y0;// Учитываем только первую клетку по порядку
		fin >> x1 >> y1;

		// Добавляем вертикальную
		if ( y0 == y1 ) {
			
			for(int i=0; i<vertical_borders.size; i)
			
		}
		// Добавляем горизонтальную
		else if ( x0 == x1 ) {

		}
		else {
			throw "error";
		}


		// Иначе выбросим исключение - не соседние клетки
	}
}

void Init() {

}

void Solve() {

}

int main() {
	
	try
	{
		BuildFrom("input.txt");


		printf("%i\n", &balls.size);
		
		printf("%i\n", &cells.size);
	}
	catch (const std::exception& error)
	{
		printf("Ошибка в чтении файла: %s", (&error)->what());
	}
	

	return 0;
	system("pause");
}
