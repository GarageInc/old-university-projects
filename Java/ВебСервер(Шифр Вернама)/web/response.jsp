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
        <title>JSP Page</title>
    <body>
        <jsp:useBean id="mybean" scope="session" class="View.NameHandler" />
        <jsp:setProperty name="mybean" property="userName" />
        <h1>Hello, <jsp:getProperty name="mybean" property="userName" />!</h1>
    </body>
</html>
