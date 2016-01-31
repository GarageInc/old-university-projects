uses crt;
const
  n=10;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,buf,j: integer; //buf, j - соответственно переменные,чтобы менять местами элементы массива методом простых обменов

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
  //Сортируем
  j:=0;
  while j<=n do
      begin
      for i:=4 to n do
          //По невозрастанию и начиная с четных позиций
          if (i mod 2=0) and (a[i]>a[i-2]) then
             begin
             //Меняем местами
             buf:=a[i];
             a[i]:=a[i-2];
             a[i-2]:=buf;
             end;
      inc(j);
      end;

  writeln('Новый отсортированный массив:');
  //Выводим
  for i:=1 to n do
  begin
  write('a(', i, ')=', a[i], ' ') ;
  end;
readln;

end.
