#include <iostream>
#include <fstream>
#include <cstdio>
#include <stdio.h>
#include <math.h>
#include <time.h>
#include <conio.h>

using namespace std;

bool FileIsExist(char filePath[]);
void Write();
double F(double x);
double leftrec(double x[], double y[], int size);
double midrec(double x[], double y[], int size);
double trapez(double x[], double y[], int size);

int main(void)
{ 
    Write();
    freopen("input.txt", "r", stdin);
    double* x;
    double* y;
    int n;
    cin >> n;
    x = new double[n];
    y = new double[n];
    for(int i = 0;i < n;i++)
    {
        cin >> x[i];
        bool q = true;
        for(int j = 0;j < i;j++)
            if(x[i] == x[j])
            {
                x[i] = 0;
                n--;
                i--;
                q = false;
            }
        if(q)
            cin >> y[i];
    }
    for(int i = 0;i < n;i++)
        for(int j = 0;j < n - i - 1;j++)
            if(x[j] > x[j + 1])
            {
                double a = x[j];
                x[j] = x[j + 1];
                x[j + 1] = a;
                a = y[j];
                y[j] = y[j + 1];
                y[j + 1] = a;
            }
    cout << leftrec(x, y, n) << endl;
    cout << midrec(x, y, n) << endl;
    cout << trapez(x, y, n) << endl;
	_getch();
    return 0;
}

bool FileIsExist(char filePath[])
{
    return ifstream(filePath).good();
}

double F(double x)
{
    return 1 / sqrt(1 + x * x) / (1 + x * x);
}

void Write()
{
    if(!FileIsExist("input.txt"))
    {
        FILE* f = freopen("input.txt", "w", stdout);
        double x;
        double x1 = sqrt((double)3);
        srand(time(NULL));
        int n;
        cin >> n;
        cout << n << "\r\n";
        for(int i = 0;i < n;i++)
        {
            x = abs(rand() % (3) - x1);
            cout << x << ' ' << F(x) << "\r\n";
        }
        fclose(f);
    }
}

double leftrec(double x[], double y[], int size)
{
    double sum = 0;
    for(int i = 1;i < size;i++)
        sum+= y[i - 1] * (x[i] - x[i - 1]);
    return sum;
}

double midrec(double x[], double y[], int size)
{
    double sum = 0;
    for(int i = 1;i < size;i++)
    {
        double a = (x[i] + x[i - 1]) / 2;
        sum+= F(a) * (x[i] - x[i - 1]);
    }
    return sum;
}

double trapez(double x[], double y[], int size)
{
    double sum = 0;
    for(int i = 1;i < size;i++)
        sum+= (y[i] + y[i - 1]) * (x[i] - x[i - 1]) / 2;
    return sum;
}
