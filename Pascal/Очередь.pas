
//Опишем очередь
Type
  EXO = ^O;
  O = record
       Data : integer;//Содержимое
       Next : EXO;//Указатель на следующий элемент
  end;

//Опишем переменные для работы с очередью
Var
BeginO, EndO : EXO;//Начало и конец


//Занесение элемента в очередь
Procedure writeO(Var BeginO, EndO : EXO; c : integer);
Var
  u : EXO;
Begin
  new(u);
  u^.Data := c;
  u^.Next := Nil;
  if BeginO = Nil {проверяем, пуста ли очередь}
    then
      BeginO := u {ставим указатель начала очереди на первый созданный элемент}
    else
      EndO^.Next := u; {ставим созданный элемент в конец очереди}
  EndO := u; {переносим указатель конца очереди на последний элемент}
End;

//Процедура извлечения элемента из очереди
//Тут же проверяем - не пуста ли очередь?
Procedure readO(Var BeginO : EXO; Var c : integer);
Var
  u : EXO;
Function FreeO(x1 : EXO): boolean;
Begin
  FreeO := (x1 = Nil);
End;
Begin
  if FreeO(BeginO)
    then
      writeln('Очередь пуста')
    else
      begin
        c := BeginO^.Data; {считываем искомое значение в переменную с}
        u := BeginO; {ставим промежуточный указатель на первый элемент очереди}
        BeginO := BeginO^.Next;{указатель начала переносим на следующий элемент}
        dispose(u); {освобождаем память, занятую уже ненужным первым элементом}
      end;
End;
