uses crt;
const
  n=20;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,buf: integer;
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

  //Меняем местами первый и последний элементы
  buf:=a[1];
  a[1]:=a[n];
  a[n]:=buf;

  writeln('Новый массив:');
  //Выводим
  for i:=1 to n do
  begin
  write(a[i], ' ') ;
  end;
readln;

end.
