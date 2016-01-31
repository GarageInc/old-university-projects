package Controller;

import Model.Crypt;
import Model.ICrypt;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.nio.charset.Charset;
import java.util.Scanner;

public class Contr extends IContr{
    
    public static String codedString;
    public static String decodedString;
    
    public static void InitUser(String key) throws UnsupportedEncodingException {
                        String text = "";
                        
                        Scanner sc= new Scanner(System.in);
                        
                        text = sc.next();
                        sc.close();
                        
                        ShowResult(text, key);
    }
    
    public static void ShowResult(String text, String key) throws UnsupportedEncodingException
    {
                        // Получим байтовое представление
                        byte[] bytesText = text.getBytes(Charset.defaultCharset());
                        byte[] bytesKey = key.getBytes(Charset.defaultCharset());
                        
                        // Создаем объект и инициализируем случайный массив перестановок
                        Crypt ver = ICrypt.CreateCrypt(bytesText.length);

                        // Шифруем
                        byte[] code = ver.EncodeVernamCipher(bytesText, bytesKey);
                        codedString = new String(code,"Cp1251");
                        
                        // Дешифруем
                        byte[] decoded = ver.DecodeVernamCipher(code, bytesKey);
                        decodedString = new String(decoded,Charset.defaultCharset());
    }
    
    public String GetKey(String name) {
		try{
                    File file = new File(name+".txt" );
                    String text="";
                    BufferedReader br = new BufferedReader (
                       new InputStreamReader(
                           new FileInputStream( name+".txt" ), "Cp1251"
                       )
                    );
                
                    String line = "";
                    while ((line = br.readLine()) != null) {
                        text=text+line;
                    }
                    br.close();
                    
                    System.out.println("Считывание ключа прошло успешно!");
                    
                    return text;
                }
                catch(IOException e)
                {
                    System.out.println("\n ФАЙЛ ПОЛЬЗОВАТЕЛЯ НЕ НАЙДЕН!");
                    return "error";
                }
    }

    public String TryGetKey() {                        
                        String name = "";
                        Scanner sc= new Scanner(System.in);
                        name = sc.next();
                        String key = GetKey(name);
                        
                        return key;
    }
    
    
}
