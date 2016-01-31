
uses crt;
var
A,B,kol,kolN,i,j,N: integer; //kol, kolN - промежуточный подсчет(счетчики делителей), N - заданное число

begin
  writeln('Введите A');
  readln(A);

  writeln('Введите B');
  readln(B);

  //Ищем число N
  //Число делителей пусть вначале будет = 0
  kolN:=0;
  //Если A>B(случайно ввели,допустим)
  if A>B
  then
  begin
      N:=A;
      for i:=A downto B do
      begin
           kol:=0;
           //Считаем количество делителей для каждого числа
           for j:=1 to i-1 do
           begin
                //Если является делителем - прибавляем счетчик делителей
                if i mod j=0
                then kol:=kol+1
                     else;
                //Если количество делителей у числа больше,чем у предыдущих - запоминаем это число
                if kol>kolN
                then
                   begin
                         kolN:=kol;
                         N:=i;
                   end
           end;
      end
  end
  
  else
  
  begin
      N:=B;
      for i:=A to B do
      begin
           kol:=0;
           //Считаем количество делителей для каждого числа
           for j:=1 to i-1 do
           begin
                //Если является делителем - прибавляем счетчик делителей
                if i mod j=0
                then kol:=kol+1
                     else;
                //Если количество делителей у числа больше,чем у предыдущих - запоминаем это число
                if kol>kolN
                then
                   begin
                         kolN:=kol;
                         N:=i;
                   end
           end;
      end;
  end;

  //Выводим
  writeln('Число,с наибольшим количеством делителей = ',N);

readln;

end.
