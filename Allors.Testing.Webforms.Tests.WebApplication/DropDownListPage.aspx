<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DropDownListPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.DropDownListPage" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList" runat="server">
            <asp:ListItem Value="0">0</asp:ListItem>
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="Button" runat="server" OnClick="ButtonClick" 
            Text="Button" />
        <asp:DropDownList ID="AutoPostBackDropDownList" runat="server" 
            AutoPostBack="True" 
            onselectedindexchanged="AutoPostBackDropDownListSelectedIndexChanged">
            <asp:ListItem Value="ValueA">TextA</asp:ListItem>
            <asp:ListItem Value="ValueB">TextB</asp:ListItem>
            <asp:ListItem Value="ValueC">TextC</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
