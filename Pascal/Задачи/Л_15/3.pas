program V;

uses crt;

var
	s: string;//Èññëåäóåìàÿ ñòğîêa
	str1,str2: string;//Ìíîæåñòâà - ïîñëåäîâàòåëüíîñòåé
    i: integer;

begin
    //Ââîäèì ñòğîêó
    writeln('Ââåäèòå ñòğîêó:');
    readln(s);
    
    //Îáğàáàòûâàåì
    //ìíîæåñòâà, ıëåìåíòàìè êîòîğûõ ÿâëÿşòñÿ âñòğå÷àşùèåñÿ â ïîñëåäîâàòåëüíîñòè
    //áóêâû îò 'A' äî 'F' (ïåğâîå ìíîæåñòâî) è îò 'X' äî 'Z' (âòîğîå ìíîæåñòâî)

    for i:=1 to length(s) do
    begin

         if ((s[i]>'a') and (s[i]<'f'))
         then
             begin
                  str1:=str1+s[i];
             end
             ;
             
         if ((s[i]>'x') and (s[i]<'z'))
         then
             begin
                 str2:=str2+s[i];  //òîëüêî 'y'
             end

    end;
    	
   	//Ïå÷àòàåì ğåçóëüòàò
   	writeln('Ïåğâîå ìíîæåñòâî:  ', str1);
   	writeln('Âòîğîå ìíîæåñòâî:  ', str2);
end.
