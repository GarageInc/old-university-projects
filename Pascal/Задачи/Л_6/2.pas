uses crt;
//Переменные
var
chislo, cifra, kol,p: integer; //число, цифра и количество раз , p - переменная для отщипывания от числа цифр.

begin
  writeln('Введите число:');
  readln(chislo);

  writeln('Введите цифру:');
  readln(cifra);
  
  //Считаем количество раз
  while (chislo>0)do
  begin
       p:=chislo mod 10;
       chislo:= chislo div 10;

       if p=cifra
       then kol:=kol+1;
  end;

  
  //Выводим
  writeln('Встречается количество раз:',kol);
  
readln;

end.
