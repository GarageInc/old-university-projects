uses crt;
const
  n=10;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,kol,k,j,buf: integer;    //buf - для обмена

begin

randomize;
//Забьем рандомными числами
  for i:=1 to n do
  begin
    //Забиваем рандомными числами
    a[i]:=random(20)-random(20);
    //И выводим одновременно
    write('a(', i, ')=', a[i], ' ') ;
  end;
  
  //Сортируем
  kol:=i-1;
  for i:= kol downto 2 do {процедура сортировки}
  begin
       k:=0;
       for j:=1 to i do    //Учитываем по условию задачи - положительный элементы
           if ((k=0) or (a[k]> //Сортируем
  kol:=i-1;
  for i:= kol downto 2 do {процедура сортировки}
  begin
       k:=0;
       for j:=1 to i do    //Учитываем по условию задачи - положительный элементы
           if ((k=0) or (M[k]<M[j])) and (M[j]>0) then k:=j;
           if (k>0) and (k<>i) and (M[i]>0) then
           begin
                //Обмен
                z:=M[i];
                M[i]:=M[k];
                M[k]:=z;
           end;
  end;a[j])) and (a[j]<0) then k:=j;
           if (k>0) and (k<>i) and (a[i]<0) then
           begin
                //Обмен
                buf:=a[i];
                a[i]:=a[k];
                a[k]:=buf;
           end;
  end;

  writeln('Новый отсортированный массив:');
  //Выводим
  for i:=1 to n do
  begin
  write('a(', i, ')=', a[i], ' ') ;
  end;
readln;

end.
