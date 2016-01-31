#ifdef __cplusplus
    #include <cstdlib>
#else
    #include <stdlib.h>
#endif

#include <SDL/SDL.h>

#include <time.h>

using namespace std;
SDL_Surface* buttons;

enum {Trash = -10,      //именуем наши коды, чтобы проще было понять
        WrongTurn = -4,
        NonChecked = -3,
        Mine = -1,
        Closed = 1,
        Opened = 0,
        Marked = 2,
        Defeat = 0,
        RightTurn = 1,
        Victory = 2,
        EasyLevel = 1,
        MediumLevel = 2,
        HardLevel = 3};

class Field //класс поля
{
    SDL_Surface* Texture; //текстура с клетками
    int Number_of_cells; //количество клеток
    int** CellValue; //Матрица со значениями клеток (бомба, сколько бомб вокруг)
    int** CellStatus; //матрица состояний клеток (открыта, закрыта, помечена)
    int Number_of_mines; //количество мин
    int Length_of_cell; //длина клетки в пикселях
    int Number_of_flags; //количество флагов
    int Number_of_closed; //количество закрытых клеток
    bool First_turn; //переменная, отвечающая за то, сделали ли мы уже первых ход

    int getValue(int x, int y) //функция получения значения CellValue по указанным индексам (с проверкой попадаем ли мы вообще в матрицу CellValue)
    {
        if( x >= 0 && x < Number_of_cells)
        {
            if(y >= 0 && y < Number_of_cells)
            {
                return CellValue[x][y];
            }
        }
        return Trash; //возвращаем мусор, если индексы не верны
    }

    int isMine(int x, int y) //возвращает единицу, если в клетке с указанными индексами лежит мина и 0 - иначе
    {
        return getValue(x,y) == -1 ? 1 : 0;
    }

    void create(int x0, int y0) //функция заполнения матрицы CellValue (х0,у0 - координаты первого нажатия)
    {
        srand(time(NULL)); //это нужно для того, чтобы rand() выдавал разные значения

        for(int k = 0; k < Number_of_mines; k++) //расставляем мины
        {
            int x = rand()%(Number_of_cells); //выбираем рандомные координаты
            int y = rand()%(Number_of_cells);

            if(CellValue[x][y] == Trash && (x != x0 || y != y0)) CellValue[x][y] = Mine; //если не попали в мину или в уже открытую клетку - ставим мину
            else k--;
        }

        for(int i = 0; i < Number_of_cells; i++) //заполняем остальные клетки
        {
            for(int j = 0; j < Number_of_cells; j++)
            {
                if(CellValue[i][j] != Mine) //если в клетке не мина - подсчитываем число мин вокруг
                {
                    CellValue[i][j] = 0;

                    for(int k = 0; k < 9; k++)
                        CellValue[i][j] += isMine(i - 1 + k / 3, j - 1 + k % 3);

                    if(CellValue[i][j] == 0)
                        CellValue[i][j] = NonChecked; //если мин вокруг нет - ставим NonChecked (нужно будет для другой функции)
                }
            }
        }
    }

public:

    Field(int num_of_cells, int length, int num_of_mines, SDL_Surface* bitmap) //конструктор с параметрами
    {
        Texture = bitmap;

        Number_of_cells = num_of_cells;

        CellValue = new int*[Number_of_cells]; //создаем динамические матрицы CellValue и CellStatus
        for(int i = 0; i < Number_of_cells; i++)
            CellValue[i] = new int[Number_of_cells];

        CellStatus = new int*[Number_of_cells];
        for(int i = 0; i < Number_of_cells; i++)
            CellStatus[i] = new int[Number_of_cells];

        First_turn = false; //заполняем остальные поля
        Number_of_closed = Number_of_cells * Number_of_cells;
        Length_of_cell = length;
        Number_of_mines = num_of_mines;
        Number_of_flags = Number_of_mines;
        for(int i = 0; i < Number_of_cells; i++) //забиваем клетки Trash, чтобы create() работала корректно
        {
            for(int j = 0; j < Number_of_cells; j++)
            {
              CellStatus[i][j] = Closed;
              CellValue[i][j] = Trash;
            }
        }
    }

    ~Field() //деструктор
    {
        for(int i = 0; i < Number_of_cells; i++) //удаляем обе матрицы
        {
            delete[] CellValue[i];
            delete[] CellStatus[i];
        }
        delete[] CellValue;
        delete[] CellStatus;
    }
    void drawCell (int x, int y, SDL_Surface* screen) //отрисовка открытой клетки
    {
        if(CellStatus[x][y] == Marked)  //если была помечена - убераем флаг
            Number_of_flags++;

        if(CellStatus[x][y] != Opened) //если была закрыта до этого - уменьшаем Number_of_closed
            Number_of_closed--;

        CellStatus[x][y] = Opened; //открываем клетку

        if(CellValue[x][y] == NonChecked) //если клетка NonChecked - ищем и открываем NonChecked клетки вокруг
        {

            CellValue[x][y] = 0; //записываем в клетку 0 (чтобы больше ее не проверять)

            for(int i = 0; i < 9; i++) //ищем вокруг пустые клетки
            {
                if(getValue(x - 1 + i / 3,y - 1 + i % 3) != Trash) //если рядом есть клетка - вызываем для нее drawCell()
                    drawCell(x - 1 + i / 3,y - 1 + i % 3,screen);
            }
        }
        drawFigure(x,y,CellValue[x][y],screen); //рисуем клетку

    }
    void drawFigure(int x, int y, int n, SDL_Surface* screen) //сама отрисовка клетки с выбором рисунка из текстуры
    {
        SDL_Rect* cell = new SDL_Rect(); //координаты для орисовки на экране
                        cell->x = 10 + x * Length_of_cell;
                        cell->y = 10 + y * Length_of_cell;

        SDL_Rect* working_area = new SDL_Rect(); //прямоугольник с областью из текстуры (пока задаем только длину и высоту
                        working_area->w = 10 * (Length_of_cell / 11);
                        working_area->h = 10 * (Length_of_cell / 11);
        if(n > 0) //проверяем в каком ряду в текстуре надо искать область
        {
            working_area->x = (n-1)*10 * (Length_of_cell / 11);
            working_area->y = 0;
            SDL_BlitSurface(Texture, working_area, screen, cell); //рисуем на экране
        }
        else
        {
            working_area->x = -n * 10 * (Length_of_cell / 11);
            working_area->y = 10 * (Length_of_cell / 11);
            SDL_BlitSurface(Texture, working_area, screen, cell);
        }
    }

    void draw(SDL_Surface* screen) //отрисовка всего поля
    {
        for(int i = 0; i < Number_of_cells; i++)
        {
            for(int j = 0; j < Number_of_cells; j++)
            {
                if(CellStatus[i][j] == Opened) //если клетка открыта
                {
                    drawCell(i,j,screen);
                }
                else
                {
                    drawFigure(i,j,-(CellStatus[i][j] + 1),screen);
                }
            }
        }

    }

    int turn(int xCursor, int yCursor, Uint8 button) //ход (параметры - координаты курсора и значение клавиши мыши
    {
        int x = (xCursor - 10) / Length_of_cell; //определяем в какую улетку мы попали
        int y = (yCursor - 10) / Length_of_cell;

        if(button == SDL_BUTTON_LEFT) //если нажата левая кнопка мыши
        {
            if(CellStatus[x][y] == Opened) return RightTurn; //если она уже открыта - ничего не делаем

            if(!First_turn) //если нажали в первый раз
            {
                First_turn = true;
                create(x,y); //заполяняем поле
            }

            if(CellStatus[x][y] == Marked) //если она была помечена
                Number_of_flags++; //убираем флаг

            CellStatus[x][y] = Opened; //открываем клетку

            if(CellValue[x][y] == Mine) //если "наступили" на мину
            {
                CellValue[x][y] = WrongTurn; //ставим соотвествующее значение (чтобы отрисововывалась другое изображение)

                for(int i = 0; i < Number_of_cells; i++) //открываем все мины, чтобы было видно
                    for(int j = 0; j < Number_of_cells; j++)
                        if(CellValue[i][j] == Mine)
                                CellStatus[i][j] = Opened;
                return Defeat; //возвращаем сообщение о поражении
            }

            Number_of_closed--; //уменьшаем счетчик закрытых клеток

            if(Number_of_closed == Number_of_mines) //если закрыты тоько мины
                for(int i = 0; i < Number_of_cells; i++) //открываем все клетки
                    for(int j = 0; j < Number_of_cells; j++)
                        CellStatus[i][j] = Opened;
        }
        if(button == SDL_BUTTON_RIGHT) //если нажали на правую кнопку мыши
        {
            if(CellStatus[x][y] == Opened) return RightTurn; //если кдетка уже открыта - ничего не делаем
            if(CellStatus[x][y] == Closed && Number_of_flags > 0) //если закрыта и еще остались флаги - помечаем и уменьшаем число свободных флагов
            {
                CellStatus[x][y] = Marked;
                Number_of_flags--;
            }
            else
                if(CellStatus[x][y] == Marked) //если уже помечена
                {
                    CellStatus[x][y] = Closed; //убераем пометку
                    Number_of_flags++; //увеличиваем число свободных флагов
                }
        }
        if(button == SDL_BUTTON_MIDDLE) //если средняя кнопка мыши
        {
            if(CellStatus[x][y] != Opened) return RightTurn; //если клетка закрыта - ничего не делаем

            int num_of_flags = 0; //число флагов вокруг нашей клетки

            for(int i = 0; i < 9; i++)
            {
                if(getValue(x-1 + i/3,y-1 + i%3) != Trash && CellStatus[x-1 + i/3][y-1 + i%3] == Marked) //если рядом есть клетка - и она помечена флагом - увеличиваем счетчик
                    num_of_flags ++;
            }
            int res = 1; //проверка на то, что мы все флаги поставили правильно
            if(num_of_flags == getValue(x,y)) //если число флагов вокруг совпадает с числом мин
            {
                for(int i = 0; i < 9; i++)
                {
                    int value = getValue(x-1 + i/3,y-1 + i%3);
                    if(value != Trash && CellStatus[x-1 + i/3][y-1 + i%3] == 1) //проверяем все непомеченные клетки вокруг
                    {
                        res *= turn((x - 1 + i / 3) * Length_of_cell + 10, (y - 1 + i % 3) * Length_of_cell + 10, SDL_BUTTON_LEFT); //если "наступин" на мину, то res станет равным 0
                    }
                }
            }
            if(res == 0) //если "наступили" на мину - возвращаем сообщение об ошибке
                return Defeat;
        }
        if(Number_of_closed == Number_of_mines) //если открыли все клетки - передаем сообщение о победе, иначе - о том, что ход был совершен
                return Victory;
            else
                return RightTurn;
    }

};


class lvlButton //класс для кнопки выбора уровня
{
    SDL_Surface* Texture; //текстура поля
    int LVL; //уровень сложности
    int Number_of_mines; //количество мин
    int Number_of_cells; //количество клеток в поле
    int Length_of_cell; //длина одной клетки в пикселях
    SDL_Rect* working_area; //прямоугольник для вырезания нужной области из текстуры
public:
    SDL_Rect* Position; //координаты нашей кнопки
    lvlButton(int lvl, SDL_Rect* pos) //конструктор с параметрами
    {
        LVL = lvl;
        Position = new SDL_Rect(*pos);
        working_area = new SDL_Rect();
        working_area->y = 0;
        working_area->x = (LVL-1) * 100;
        working_area->h = 100;
        working_area->w = 100;
        switch (LVL) //проверяем какой уровень сложности и в зависимости от этого выбираем текстуру, количество мин, клеток и размер клетки
        {
            case EasyLevel:
                {
                    Texture = SDL_LoadBMP("figuresEasy.bmp");
                    Number_of_cells = 9;
                    Length_of_cell = 55;
                    Number_of_mines = 10;
                }
                break;
            case MediumLevel:
                {
                    Texture = SDL_LoadBMP("figuresMedium.bmp");
                    Number_of_cells = 11;
                    Length_of_cell = 45;
                    Number_of_mines = 25;
                }
                break;
            case HardLevel:
                {
                    Texture = SDL_LoadBMP("figuresHard.bmp");
                    Number_of_cells = 15;
                    Length_of_cell = 33;
                    Number_of_mines = 50;
                }
                break;
        }
    }
    ~lvlButton()
    {
        SDL_FreeSurface(Texture);
    }
    void press(Field* &F) //обработчик нажатия на кнопку
    {
        if(F != NULL) //если поле было до этого - удаляем
            delete F;
        F = new Field(Number_of_cells, Length_of_cell, Number_of_mines, Texture); //создаем новое поле
    }
    void draw(SDL_Surface* screen) //отрисовщик кнопки
    {
        SDL_BlitSurface(buttons, working_area, screen, Position);
    }
};



int main ( int argc, char** argv )
{
    if ( SDL_Init( SDL_INIT_VIDEO ) < 0 )
        return 1;
    atexit(SDL_Quit);

    SDL_Surface* screen = SDL_SetVideoMode(630, 515, 16,
                                           SDL_HWSURFACE|SDL_DOUBLEBUF); //создаем окно
    if ( !screen )
        return 1;

    buttons = SDL_LoadBMP("buttons.bmp"); //загружаем картинку с кнопками + сообщениями о победе/поражении

    SDL_Rect dstrect; //будущие координаты курсора мыши

    Field* F = NULL; //наше поле
    SDL_Rect* Position = new SDL_Rect; //координаты кнопок
        Position->x = 515;
        Position->y = 10;
    lvlButton* Easy = new lvlButton(EasyLevel,Position); //кнопка простого уровня сложности
        Position->x = 515;
        Position->y = 120;
    lvlButton* Medium = new lvlButton(MediumLevel,Position); //кнопка среднего уровня сложности
        Position->x = 515;
        Position->y = 230;
    lvlButton* Hard = new lvlButton(HardLevel,Position); //кнопка тяжелого уровня сложности

    SDL_Rect* message = new SDL_Rect(); //прямоугольник для вырезания сообщения о победе/поражении из общей текстуры
        message->h = 100;
        message->w = 200;

    Position->x = 150; //координаты сообщения о победе/поражении
    Position->y = 200;

    Easy->press(F); //создаем поле

    int result = RightTurn; //переменная для хранения результата хода


    bool done = false; //переменная для общего цикла программы

    while ( !done )
    {
        dstrect.x = 0; //обнуляем координаты курсора
        dstrect.y = 0;
        Uint8 btn; //переменная для хранения того, какую кнопку мыши мы нажали

        SDL_Event event; //переменная для события

        while (SDL_PollEvent(&event)) //обрабатываем событие
        {
            switch (event.type)
            {
            case SDL_QUIT:
                done = true;
                break;

            case SDL_KEYDOWN: //если нажали какую-то клавишу на клавиатуре
                {
                    if (event.key.keysym.sym == SDLK_ESCAPE) //если нажали ESCAPE - закрываем приложение
                        done = true;
                    break;
                }

            case SDL_MOUSEBUTTONDOWN: //если нажали кнопку мыши
                {
                    dstrect.x = event.button.x; //получаем координаты курсора
                    dstrect.y = event.button.y;

                    btn = event.button.button;

                    if(dstrect.x > 10 && dstrect.x < 500 && dstrect.y  > 10 && dstrect.y <500 &&  result == RightTurn) //проверяем попали ли мы в поле и можем ли еще ходить
                    {
                        result = F->turn(dstrect.x, dstrect.y, btn); //ходим
                    }
                    else
                    {
                        if(dstrect.x > 515 && dstrect.x < 615 && dstrect.y  > 10 && dstrect.y <110) //проверяем, попали ли в кнопку Easy
                        {
                            result = RightTurn; //разрешаем ходить
                            Easy->press(F); //создаем новое поле с легким уровнем сложности
                        }
                        if(dstrect.x > 515 && dstrect.x < 615 && dstrect.y  > 110 && dstrect.y <230) //проверяем, попали ли в кнопку Medium
                        {
                            result = RightTurn;
                            Medium->press(F); //создаем новое поле со средним уровнем сложности
                        }
                        if(dstrect.x > 515 && dstrect.x < 615 && dstrect.y  > 230 && dstrect.y <340) //проверяем, попали ли в кнопку Hard
                        {
                            result = RightTurn;
                            Hard->press(F); //создаем новое поле с тяжелым уровнем сложности
                        }
                    }
                    break;
                }
            }
        }

        SDL_FillRect(screen, 0, SDL_MapRGB(screen->format, 255, 255, 255)); //закрашиваем окно

        Easy->draw(screen); //рисуем кнопки
        Medium->draw(screen);
        Hard->draw(screen);
        F->draw(screen); //рисуем поле

        if(result != RightTurn) //проверяем не выиграли ли мы или проиграли
            if(result == Defeat) //если поражение
            {
                message->y = 0;
                message->x = 500; //выбираем область для сообщения о пораженни
                SDL_BlitSurface(buttons, message, screen, Position); //рисуем сообщение
            }
            else
            {
                message->y = 0;
                message->x = 300; //выбираем область для сообщения о победе
                SDL_BlitSurface(buttons, message, screen, Position);//рисуем сообщение
            }

        SDL_Flip(screen); //обновляем экран
    }

    SDL_FreeSurface(buttons);
    SDL_Quit();
    return 0;
}
