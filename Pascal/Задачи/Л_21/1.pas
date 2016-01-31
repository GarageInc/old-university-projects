uses crt;
const
  n=10;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,min,k: integer;

begin

randomize;
//Забьем рандомными числами
  for i:=1 to n do
  begin
    //Забиваем рандомными числами
    a[i]:=random(20);
    //И выводим одновременно
    write('a(', i, ')=', a[i], ' ') ;
  end;
  //Принимаем первый элемент за минимальный
  min:=a[1]; k:=1;//k - индекс минимального

  for i:=2 to n do
  begin
    if a[i]<min then
    begin
      min:=a[i];
      k:=i;
    end;
  end;
  
  //Нашли минимальный - сдвигаем массив,начиная с индекса минимального
  for i:=k to n-1 do
    a[i]:=a[i+1];

  
  writeln('Новый массив:');
  //Выводим
  for i:=1 to n-1 do
  begin
  write('a(', i, ')=', a[i], ' ') ;
  end;
readln;

end.
