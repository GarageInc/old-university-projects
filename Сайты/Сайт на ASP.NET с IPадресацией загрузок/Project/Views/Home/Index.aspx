<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
    
    <% 
        string ip = HttpContext.Current.Request.UserHostAddress;
       //Узнаем ip-адрес клиента
        Response.Write("Ваш IP-адрес(через UserHostAddress):" + ip+"\n\n");

       ip = Request.ServerVariables["REMOTE_ADDR"];
       Response.Write("\nВаш IP(через REMOTE_ADDR) = " + ip + "");
       
       
       
       
       
    %>
    
    </p>
</asp:Content>
