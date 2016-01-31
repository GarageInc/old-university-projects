//ЛАБА18
uses crt;
const
  n=20;//Длина массивов
  
//Переменные
var
a: array [1..n] of integer; // 2 массива
b: array [1..n] of integer;

i:integer;//Просто переменная для счета элементов массива

begin

randomize;
//Забьем рандомными числами
  for i:=1 to n do
  begin
    //Забиваем рандомными числами от -20 до 20
    a[i]:=random(20)-random(40);//Генератор рандомных чисел
    b[i]:=random(20)-random(40);
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
  
  
readln;

end.
