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

  //Нашли минимальный - удаляем первый и минимальный элементы. Но их индексы могут совпасть! В этом случае - делаем условие
  if k=1
  then
      begin
       //Сдвигаем все элементы, начиная с первого
       for i:=1 to n-1 do
          a[i]:=a[i+1];
      //Выводим
       writeln('Новый массив:');
       for i:=1 to n-1 do
       write('a(', i, ')=', a[i], ' ') ;
      end
  else
      begin
      //Удаляем и первый элемент и минимальный(т.к. они не совпали)
       for i:=1 to n-1 do
          a[i]:=a[i+1];
       for i:=k-1 to n-1 do
          a[i]:=a[i+1];
      //Выводим
       writeln('Новый массив:');
       for i:=1 to n-2 do
       write('a(', i, ')=', a[i], ' ') ;
      end;
  
readln;

end.
