//Поиск седловых точек

#include <stdio.h>

int main(int argc, const char * argv[]) {
    
    int n, m;
    printf ("Введите размерность матрицы \n");
    scanf("%i", &n);
    scanf("%i", &m);
    int a[n][m];
    int sedlo;
    int min, nomer_min;
    printf("Введите элементы матрицы\n");
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            scanf("%i", &a[i][j]);
    printf("Найденные седловые точки\n");
    for (int i = 0; i < n; i++) {
        min = a[i][0];
        nomer_min = 0;
        sedlo = 1;
        for (int j = 1; j < m; j++)
            if (a[i][j] < min){
                min = a[i][j]; //Ищем минимальный элемент строки
                nomer_min = j; //Запоминаем столбца минимального элемента
            }
        for (int k = 0; k < n; k++)
            //Если найдем элемент, больший или равный найденному min, то min - не седловая точка
            if ((a[k][nomer_min] >= min) && (k != i)){
                sedlo = 0;
                break;
            }
        if (sedlo) {
            printf("%i", a[i][nomer_min]);
            printf(" Координаты: ");
            printf("%i",i);
            printf(" ");
            printf("%i",nomer_min);
        }
    }
    printf("\n");
    return 0;
}



