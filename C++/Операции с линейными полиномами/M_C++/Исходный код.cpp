#include <iostream>
#include <string>
#include <algorithm>
#include <fstream>
#include <vector>

using namespace std;

class Polynom
{
public:
    vector<float> koeff;
    vector<int> degrees;
    void Collect()
    {
        for(int i = 0;i < degrees.size();i++)
            for(int j = 0;j < degrees.size() - i - 1;j++)
                if(degrees[j] > degrees[j + 1])
                {
                    int d = degrees[j];
                    degrees[j] = degrees[j + 1];
                    degrees[j + 1] = d;
                    double t = koeff[i];
                    koeff[i] = koeff[i + 1];
                    koeff[i + 1] = t;
                }
        vector<float> s1;
        vector<int> s2;
        for (int i = 0; i < degrees.size(); i++)
        {
            s2.push_back(degrees[i]);
            s1.push_back(koeff[i]);
            for (; (i < degrees.size() - 1) && (degrees[i] == degrees[i + 1]); i++)
                s1[s1.size() - 1] += koeff[i + 1];
        }
        degrees = s2;
        koeff = s1;
    }
};

class LinearPolynom
{
public:
    double koeff[2];
};

Polynom operator *(Polynom s1, LinearPolynom s2)
{
    Polynom s;
    Polynom t1;
    Polynom t2;
    for (int i = 0; i < s1.degrees.size(); i++)
    {
        if (s1.koeff[i] * s2.koeff[0] != 0)
        {
            t1.degrees.push_back(s1.degrees[i]);                    
            t1.koeff.push_back(s1.koeff[i] * s2.koeff[0]);
        }
        if (s1.koeff[i] * s2.koeff[1] != 0)
        {
            t2.degrees.push_back(s1.degrees[i] + 1);
            t2.koeff.push_back(s1.koeff[i] * s2.koeff[1]);
        }
    }
    int k = 0, j = 0;
    for (;(k < t1.degrees.size()) && (j < t2.degrees.size());)
    {
        if (t1.degrees[k] == t2.degrees[j])
        {
            s.degrees.push_back(t1.degrees[k]);
            s.koeff.push_back(t2.koeff[j] + t1.koeff[k]);
            k++;
            j++;
        }
        else
        {
            if (t1.degrees[k] > t2.degrees[j])
            {
                s.degrees.push_back(t2.degrees[j]);
                s.koeff.push_back(t2.koeff[j]);
                j++;
            }
            else
            {
                if (t1.degrees[k] < t2.degrees[j])
                {
                    s.degrees.push_back(t1.degrees[k]);
                    s.koeff.push_back(t1.koeff[k]);
                    k++;
                }
            }
        }
    }
    for (;k < t1.degrees.size();)
    {
        s.degrees.push_back(t1.degrees[k]);
        s.koeff.push_back(t1.koeff[k]);
        k++;
    }
    for (;j < t2.degrees.size();)
    {
        s.degrees.push_back(t2.degrees[j]);
        s.koeff.push_back(t2.koeff[j]);
        j++;
    }
    return s;
}

Polynom operator /(Polynom s1, LinearPolynom s2)
{
    Polynom s;
    if (s2.koeff[1] == 0)
    {
        if (s2.koeff[0] == 0)
        {
            cout << "Divided by zero.";
            return Polynom();
        }
        for (int i = 0; i < s1.degrees.size(); i++)
        {
            s.degrees.push_back(s1.degrees[i]);
            s.koeff.push_back(s1.koeff[i] / s2.koeff[0]);
        }
        return s;
    }
    if (s2.koeff[0] == 0)
    {
        if (s1.degrees[0] > 0)
        {
            s.degrees.push_back(s1.degrees[0] - 1);
            s.koeff.push_back(s1.koeff[0] / s2.koeff[1]);
        }
        for (int i = 1; i < s1.degrees.size(); i++)
        {
            s.degrees.push_back(s1.degrees[i] - 1);
            s.koeff.push_back(s1.koeff[i] / s2.koeff[1]);
        }
        return s;
    }
    double x = - s2.koeff[0] / s2.koeff[1];
    s.degrees.push_back(s1.degrees[s1.degrees.size() - 1] - 1);
    s.koeff.push_back(s1.koeff[s1.koeff.size() - 1]);
    for (int i = s1.degrees[s1.degrees.size() - 1] - 1, j = s1.degrees.size() - 1; i > 0; i--)
    {
        for (;(j >= 0) && (s1.degrees[j] > i); j--);
        if ((j >= 0) && (s1.degrees[j] == i))
        {
            if (s.koeff[s.koeff.size() - 1] * x + s1.koeff[j] != 0)
            {
                if (s.degrees[s.degrees.size() - 1] == i)
                {
                    s.degrees.push_back(i - 1);
                    s.koeff.push_back(s.koeff[s.koeff.size() - 1] * x + s1.koeff[j]);
                }
                else
                {
                    s.degrees.push_back(i - 1);
                    s.koeff.push_back(s1.koeff[j]);
                }
            }
        }
        else
        {
            if (s.degrees[s.degrees.size() - 1] == i)
            {
                s.degrees.push_back(i - 1);
                s.koeff.push_back(s.koeff[s.koeff.size() - 1] * x);
            }
        }
    }
    for(int i = 0;i < s.degrees.size() / 2;i++)
        s.degrees[i] = s.degrees[s.degrees.size() - i - 1];
    for(int i = 0;i < s.koeff.size() / 2;i++)
        s.koeff[i] = s.koeff[s.koeff.size() - i - 1];
    for (int i = 0; i < s.koeff.size(); i++)
        s.koeff[i] /= s2.koeff[1];
    return s;
}

int Choice();
void Read(Polynom &sc, FILE* rd);
void Read(LinearPolynom &sc);
void Write(Polynom sc, FILE* wr);

int main()
{
    FILE* wr;
    wr = fopen ("result.txt","w");
    Polynom scl;
    LinearPolynom scl1;
    int n = 0;
    while (n != 4)
    {
        n = Choice();
        if (n != 4)
        {
            switch (n)
            {
                case 1:
                {
                    FILE* rd;
                    rd = fopen ("polynom.txt","r");
                    Read(scl, rd);
                    Read(scl1);
                    fprintf(wr, "Polynom:\r\n");
                    Write(scl, wr);
                    fprintf(wr, "Linear Polynom:\r\n%f %f\r\n", scl1.koeff[0], scl1.koeff[1]);
                    Polynom t = scl * scl1;
                    fprintf(wr, "Result:\r\n");
                    Write(t, wr);
                    fclose (rd);
                }
                break;
                case 2:
                {
                    FILE* rd;
                    rd = fopen ("polynom.txt","r");
                    Read(scl, rd);
                    Read(scl1);
                    fprintf(wr, "Polynom:\r\n");
                    Write(scl, wr);
                    fprintf(wr, "Linear Polynom:\r\n%f %f\r\n", scl1.koeff[0], scl1.koeff[1]);
                    Polynom t = scl / scl1;
                    fprintf(wr, "Result:\r\n");
                    Write(t, wr);
                    fclose(rd);
                }
                break;
                case 3:
                {
                    FILE* rd;
                    rd = fopen ("polynom1.txt","r");
                    Read(scl, rd);
                    scl.Collect();
                    fprintf(wr, "Result:\r\n");
                    Write(scl, wr);
                    fclose (rd);
                }
                break;
            }
        }
    }
    fclose (wr);
    return 0;
}

int Choice()
{
    cout << "Enter number for action:" << endl;
    cout << "1 - for multiplication on linear polynom;" << endl;
    cout << "2 - for division on linear polynom;" << endl;
    cout << "3 - for collecting similar terms;" << endl;
    cout << "4 - for exit." << endl;
    int n;
    cin >> n;
    return n;
}

void Read(Polynom &sc, FILE* rd)
{
    sc.degrees.clear();
    sc.koeff.clear();
    int a;
    float b;
    while (fscanf(rd, "%d %f", &a, &b) != EOF)
    {
        sc.degrees.push_back(a);
        sc.koeff.push_back(b);
    }
}

void Read(LinearPolynom &sc)
{
    float a;
    cout << "Enter the first koefficient:" << endl;
    cin >> sc.koeff[0];
    cout << "Enter the second koefficient:" << endl;
    cin >> sc.koeff[1];
    cout << sc.koeff[0] << ' ' << sc.koeff[1] << endl;
}

void Write(Polynom sc, FILE* wr)
{
    fprintf(wr, "Koefficients of polynom:\r\n");
    for (int i = 0, j = 0; i < sc.degrees.size(); i++)
        {
            for (; j < sc.degrees[i]; j++)
                fprintf(wr, "0 ");
            fprintf(wr, "%f ", sc.koeff[i]);
            j++;
        }
    fprintf(wr, "\r\n");
}
