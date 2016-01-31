using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Diana
{
class Program
{
static void NumOfVectors(string name)
{

int n = 0;//Длина отдельного вектора
double k = 0;//Отдельная координата вектора - дробная,не целая. По условию

//Поток для чтения
StreamReader sr = new StreamReader(name, Encoding.GetEncoding(1251));//Encoding - для корректного считывания с файла

//Считываем всё
string[] str = sr.ReadLine().Split(' ');//Считываем все числа в массив строк. Как разделитель - берется знак пробела ' ';

int kol_vectors = 0;//Количество векторов

for (int i = 0; i < str.Length; i++)
{
//Если мы дошли до конца и прочитали пробел - это выход из цикла
if (str[i].CompareTo(" ") == 0) break; ;
n = int.Parse(str[i]);//Переводим в число
i++;

for (int j = 0; j < n; j++, i++)//Идем по координатам вектора
{
k = double.Parse(str[j]);//Эту строку можно убрать даже.
//Тут ничего не происходит. Просто проходим по вектору
}

//Прочитали вектор->увеличиваем счетчик на единицу
kol_vectors++;
}

//Закрываем поток чтения
sr.Close();

//Выводим результат
Console.WriteLine("Количество векторов = " + kol_vectors);
}

static void Main(string[] args)
{
Console.WriteLine("Введите имя файла(как файл.txt):");
string s = "";
s = Console.ReadLine();

NumOfVectors(s);//Вызываем функцию

//Делаем задержку
Console.ReadKey();

}
}
}