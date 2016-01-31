uses crt;
//Переменные
var
chislo, fact: integer; //fact - факториал числа

begin
  writeln('Введите число:');
  readln(chislo);
  
  //Запоминаем заранее
  fact:=chislo;
  //Считаем факториал/ отщипываем от числа по единичке вниз по циклу
  while (chislo>1)do
  begin
       chislo:= chislo - 1;
       fact:=chislo*fact;
  end;


  //Выводим
  writeln('Факториал = ',fact);

readln;

end.
