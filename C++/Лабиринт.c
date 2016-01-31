//Поиск выхода из лабиринта по алгоритму правой (левой) руки

#include <stdio.h>

const int N = 5;

enum move {NONE= 0, LEFT, RIGHT, UP, DOWN};
const int hope = 5;
const int thereisNOhope = 10;

int main(int argc, const char * argv[]) {
    int a[N][N];
    int end_of_story = 0;
    int current_i, current_j;
    enum move current_move;
    int field[N][N] = {0};
    printf("Введите матрицу лабиринта\n");
    for (int i = 0; i < N; i++)
        for (int j = 0; j < N; j++)
            scanf("%i", &a[i][j]);
    printf ("Введите начальные координаты\n");
    scanf("%i", &current_i);
    scanf("%i", &current_j);
    current_move = UP;
    field[current_i][current_j] = 1;
    while (!end_of_story) {
        if (current_i == 0 || current_j == 0 || current_i == N - 1 || current_j == N - 1) {
            printf("Выход есть");//Выходим из лабиринта сразу
            end_of_story = 1;
            break;
        }
        if (a[current_i - 1][current_j] && a[current_i + 1][current_j]
            && a[current_i][current_j - 1] && a[current_i][current_j + 1]) {
            printf("Выхода нет");//Сразу сдаемся, мы замурованы
            end_of_story = 1;
            break;
        }
        if (current_move == UP) //Идем вверх
            while (current_i > 0) {
                
                if (a[current_i - 1][current_j]) { //Встретили стенку, повернуть налево
                    field[current_i][current_j]++;
                    current_move = LEFT;
                    break;
                }
                if (!a[current_i - 1][current_j] && current_i == 1){//Нашли выход
                    printf("Выход есть");
                    end_of_story = 1;
                    break;
                }
                current_i--;
                if (field[current_i][current_j] == thereisNOhope) {//Скорее всего, мы замурованы, сдаемся
                    end_of_story = 1;
                    printf("Выхода нет");
                    break;
                }
                if (field[current_i][current_j] == hope) {//Возможно, мы заблудились, пойдем по левой руке
                    current_move = RIGHT;
                    break;
                }
            }
        if (current_move == LEFT) //Идем влево
            while (current_j > 0) {
                
                if (a[current_i][current_j - 1]) { //Встретили стенку, повернуть вниз
                    field[current_i][current_j]++;
                    current_move = DOWN;
                    break;
                }
                if (!a[current_i][current_j - 1] && current_j == 1){//Нашли выход
                    printf("Выход есть");
                    end_of_story = 1;
                    break;
                }
                current_j--;
                if (field[current_i][current_j] == thereisNOhope) {//Скорее всего, мы замурованы, сдаемся
                    end_of_story = 1;
                    printf("Выхода нет");
                    break;
                }
                if (field[current_i][current_j] == hope) {//Возможно, мы заблудились, пойдем по левой руке
                    current_move = UP;
                    break;
                }
            }
        if (current_move == DOWN) //Идем вниз
            while (current_i < N - 1) {
                
                if (a[current_i + 1][current_j]) { //Встретили стенку, повернуть направо
                    field[current_i][current_j]++;
                    current_move = RIGHT;
                    break;
                }
                if (!a[current_i + 1][current_j] && current_i == N - 2){//Нашли выход
                    printf("Выход есть");
                    end_of_story = 1;
                    break;
                }
                current_i++;
                if (field[current_i][current_j] == thereisNOhope) {//Скорее всего, мы замурованы, сдаемся
                    end_of_story = 1;
                    printf("Выхода нет");
                    break;
                }
                if (field[current_i][current_j] == hope) {//Возможно, мы заблудились, пойдем по левой руке
                    current_move = LEFT;
                    break;
                }
            }
        if (current_move == RIGHT) //Идем направо
            while (current_j < N - 1) {
                
                if (a[current_i][current_j + 1]) { //Встретили стенку, повернуть вверх
                    field[current_i][current_j]++;
                    current_move = UP;
                    break;
                }
                if (!a[current_i][current_j + 1] && current_j == N - 2){//Нашли выход
                    printf("Выход есть");
                    end_of_story = 1;
                    break;
                }
                current_j++;
                if (field[current_i][current_j] == thereisNOhope) {//Скорее всего, мы замурованы, сдаемся
                    end_of_story = 1;
                    printf("Выхода нет");
                    break;
                }
                if (field[current_i][current_j] == hope) {//Возможно, мы заблудились, пойдем по левой руке
                    current_move = DOWN;
                    break;
                }
            }
        
    }
    printf("\n");
    return 0;
}