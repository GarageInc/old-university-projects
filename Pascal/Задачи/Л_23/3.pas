uses crt;
const
  n=10;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,buf,j,g: integer; //buf, j - соответственно переменные,чтобы менять местами элементы массива

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
  for i:=2 to n do
  begin
       buf:=A[i];
       j:=1;
       //Сортируются элемента на нечетных местах!
       while ((buf<a[j])) do
             Inc(j);
       for g:=i-1 downto j do
           //Смотрим на нечетность позиции:
           if j mod 2=1
           then
             a[g+1]:=a[g];
       a[j]:=buf;
  end;

  writeln('Новый отсортированный массив:');
  //Выводим
  for i:=1 to n do
  begin
  write('a(', i, ')=', a[i], ' ') ;
  end;
readln;

end.
