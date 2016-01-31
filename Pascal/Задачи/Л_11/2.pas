uses crt;
var
N,i: integer; //N - число символов,не являющихся цифрами
s: string;

begin
  writeln('Введите произвольную строку');
  readln(s);

  N:=0;
  
  for i:=1 to length(s)  do
  begin
    //Если код символа не входит от 0 до 9 - то значит,что это "буква". Значит - увеличиваем счетчик на единицу.
    if ((s[i]<'0') or (s[i]>'9'))
    then N:=N+1;
    
  end;

  //Выводим
  writeln('Количество НЕцифр N = ',N);

readln;

end.
