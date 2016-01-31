//ЛАБА19
uses crt;
const
  n=20;//Длина массивов

//Переменные
var
ar: array [1..n] of integer; // массив

i,A,B:integer;//i - Просто переменная для счета элементов массива

begin

//Вводим числа А и Б
writeln('Введите А=');
readln(A);

writeln('Введите B=');
readln(B);

randomize;
//Забьем рандомными числами
  for i:=1 to n do
  begin
    //Забиваем рандомными числами от -20 до 20
    ar[i]:=random(10)-random(15);//Генератор рандомных чисел
  end;


  writeln('Массив :');
  //Выводим первый массив А
  for i:=1 to n do
  begin
  write(ar[i], ' ') ;
  end;

//Вносим изменения по условию задачи - к четным индексам прибавляем А, к нечетным - B
//Одновременно печатаем
writeln('');
  writeln('Новый массив :');
  for i:=1 to n do
  begin
  if i mod 2=0
     then ar[i]:=ar[i]+A
     else ar[i]:=ar[i]+B;
  write(ar[i], ' ') ;
  end;

readln;

end.
