
uses crt;

//Переменные для файлов с целыми числами
var A3,B3,C3:file of integer;
    n, a, sumPol, kolNul:integer; //sumPol - сумма положительных, kolNul - количество нулевых, a - переменная , n - количество чисел

begin

assign(A3,'A3');
rewrite(A3);
assign(B3,'B3');
rewrite(B3);
assign(C3,'C3');
rewrite(C3);

//сколько чисел будем вводить?
Writeln('Количество цифр-чисел = ');
readln(n);

//Вводим целые числа. Окончание - ноль
writeln('Введите в файл целые числа, окончание ввода ноль:');
repeat
      readln(a);
      write(A3,a);{создание A файла}
      n:=n-1;
until n=0;

writeln('Содержание исходного файла:');
reset(A3);//Переходим на начало

while not eof(A3) do   //Считываем с A файла,пока не дойдем до его конца
 begin
  read(A3,a);
  write(a,' ');
  if a > 0 then sumPol:=sumPol+a
  else
      if a = 0 then kolNul:=kolNul+1;
  
 end;

writeln;
close(A3);//Закрываем A
//Записываем сумму положительных чисел в файл B
write(B3,sumPol);
//Записываем  количество нулевых - в файл С
write(C3,kolNul);

//Выводим на экран результаты вычислений
//
writeln('Содержание второго файла(B):');
reset(B3);
  read(B3,a);
  write(a,' ');
writeln;
close(B3);

writeln('Содержание третьего файла(C):');
reset(C3);
  read(C3,a);
  write(a,' ');
writeln;
close(C3);

readln;
end.
