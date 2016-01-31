uses crt;
const
  n=10;//Длина массива
//Переменные
var
a: array [1..n] of integer;

i,j,k: integer; // k - счетчик чисел,находящихсяя на НЕчетных индексах

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

  //Удаляем нечетные позиции - сдвигаем массив
  k:=0;
  for i:=1 to n do
  begin

       if i mod 2=1
       then
       begin
           for j:=i-k to n-k-1 do
           begin
                a[j]:=a[j+1];
           end;
           k:=k+1;
       end;
  end;
  
      //Выводим
       writeln('Новый массив:');
       for i:=1 to n-k do
       write('a(', i, ')=', a[i], ' ') ;

readln;

end.
