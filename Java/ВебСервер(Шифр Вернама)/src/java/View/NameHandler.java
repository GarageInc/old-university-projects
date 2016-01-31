/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package View;

import Controller.Contr;
import Controller.IContr;
import java.io.IOException;
import java.io.UnsupportedEncodingException;

/**
 *
 * @author 7
 */
public class NameHandler {

    private String userName;
    private String textString="textString";
    private String encryptedField="<пусто>";
    private String decryptedField="<пусто>";
    private String messageField="<пусто>";
    
    Contr contr;
    public NameHandler() throws UnsupportedEncodingException {
                        // Создаем шифратор
                         contr= IContr.CreateContr();
    }

    /**
     * @return the userName
     */
    public String getUserName() {
        return userName;
    }

    /**
     * @param userName the userName to set
     */
    public void setUserName(String userName) throws UnsupportedEncodingException {
                        this.userName = userName;
        
                        
                        // Получим ключ пользователя
                        String key = contr.GetKey(userName);

                            if (!"error".equals(key))
                            {
                                try{
                                    contr.InitUser(textString,key);
                                    
                                    encryptedField=(contr.codedString);
                                    decryptedField=(contr.decodedString); 
                                    
                                }
                                catch(IOException e)
                                {
                                    messageField=("Ошибка шифрования текста"+textString+" ключом "+key);
                                    
                                }
                                
                            }
                            else
                            {
                                messageField=("Файл пользователя "+userName+" не найден ");
                            }
    }

    /**
     * @return the textString
     */
    public String getTextString() {
        return textString;
    }

    /**
     * @param textString the textString to set
     */
    public void setTextString(String textString) {
        this.textString = textString;
    }

    /**
     * @return the encryptedField
     */
    public String getEncryptedField() {
        return encryptedField;
    }

    /**
     * @param encryptedField the encryptedField to set
     */
    public void setEncryptedField(String encryptedField) {
        this.encryptedField = encryptedField;
    }

    /**
     * @return the decryptedField
     */
    public String getDecryptedField() {
        return decryptedField;
    }

    /**
     * @param decryptedField the decryptedField to set
     */
    public void setDecryptedField(String decryptedField) {
        this.decryptedField = decryptedField;
    }

    /**
     * @return the messageField
     */
    public String getMessageField() {
        return messageField;
    }

    /**
     * @param messageField the messageField to set
     */
    public void setMessageField(String messageField) {
        this.messageField = messageField;
    }

   
}
