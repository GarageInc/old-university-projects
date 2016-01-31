<%-- 
    Document   : response
    Created on : 05.05.2015, 9:53:39
    Author     : 7
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Страница ответа</title>
    <body>
        <jsp:useBean id="mybean" scope="session" class="View.NameHandler" />
        <jsp:setProperty name="mybean" property="textString" />
        <jsp:setProperty name="mybean" property="userName" />
        
        <table border="0" align=center>
            <thead>
                <tr>
                    <th>Шифр Вернама(шифроблокнот)</th>
                    <th> - результат шифрования</th>
                </tr>
                
            </thead>
            <tbody>
                <tr>
                    <td>Привет,</td>
                    <td> <jsp:getProperty name="mybean" property="userName" /> !</td>                    
                </tr>
                <tr>
                    <td>Лог: </td>
                    <td><jsp:getProperty name="mybean" property="messageField" /></td>                    
                </tr>
                <tr>
                    <td>Зашифрованное сообщение: </td>
                    <td><jsp:getProperty name="mybean" property="encryptedField" /></td>                    
                </tr>
                <tr>
                    <td>Расшифрованное сообщение:</td>
                    <td><jsp:getProperty name="mybean" property="decryptedField" /></td>                    
                </tr>
            </tbody>
        </table>
     
    </body>
</html>
