//ЛАБА18
uses crt;
const
  n=20;//Длина массивов

//Переменные
var
a: array [1..n] of integer; // 2 массива
b: array [1..n] of integer;

i,max,index:integer;//Просто переменная для счета элементов массива
//max - максимальный элемент
//index - номер миним.элемента
begin

randomize;
//Забьем рандомными числами
  for i:=1 to n do
  begin
    a[i]:=random(20);//Генератор рандомных чисел
    b[i]:=random(20);
  end;


  writeln('Массив №1(А):');
  //Выводим первый массив А
  for i:=1 to n do
  begin
  write(a[i], ' ') ;
  end;

  writeln(' ');
  writeln('Массив №2(Б):');
  //Выводим первый массив Б
  for i:=1 to n do
  begin
  write(b[i], ' ') ;
  end;

//Ищем максимальный элемент в первом массива
  max:=a[i];
  for i:=2 to n do
  begin
       if a[i]>max
       then
           max:=a[i];
  end;
  //Выводим
  writeln('');
  writeln('Максимальный элемент в массиве А = ',max);
  
//Ищем индекс минимального элемента во втором массиве
  max:=b[1];
  index:=1;
  for i:=2 to n do
  begin
       if b[i]<max
       then
       begin
           index:=i;
           max:=b[i];
       end;
  end;
  //Выводим
  writeln('Индекс минимального элемента в массиве B = ',index);

  
readln;

end.
