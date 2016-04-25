<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Добро пожаловать<b><%: Page.User.Identity.Name %></b>!
        [ <%: Html.ActionLink("Выход", "LogOff", "Account") %> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Вход", "LogOn", "Account") %> ]
<%
    }
%>
