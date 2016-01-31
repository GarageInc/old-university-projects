uses crt;
const
  n=20;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,min,max,buf: integer;//min, max - индексы соответственно минимальных и максимальных элементов,
//buf - для обмена

begin

randomize;
//Забьем рандомными числами
  for i:=1 to n do
  begin
    //Забиваем рандомными числами
    a[i]:=random(20);
    //И выводим одновременно
    write(a[i], ' ') ;
  end;
  
  //Принимаем первый элемент за минимальный и максимальный
  min:=1;
  max:=1;
  
  for i:=2 to n do
  begin
    if a[i]<a[min] then
    begin
      min:=i;
      buf:=i;
    end;
    
    if a[i]>a[max] then
    begin
      max:=i;
      buf:=i;
    end;
  end;

  //Меняем местами минимальный и максимальный элементы
  buf:=a[min];
  a[min]:=a[max];
  a[max]:=buf;

  writeln('Новый массив:');
  //Выводим
  for i:=1 to n do
  begin
  write(a[i], ' ') ;
  end;
readln;

end.
