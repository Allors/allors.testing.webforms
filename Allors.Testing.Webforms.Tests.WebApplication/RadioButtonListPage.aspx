<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadioButtonListPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.RadioButtonListPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:RadioButtonList ID="RadioButtonList" runat="server">
            <asp:ListItem Value="xValue">xText</asp:ListItem>
            <asp:ListItem Value="yValue">yText</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="Button" runat="server" OnClick="ButtonClick" 
            Text="Button" />
        <asp:RadioButtonList ID="AutoPostBackRadioButtonList" runat="server" 
            onselectedindexchanged="AutoPostBackRadioButtonListSelectedIndexChanged">
            <asp:ListItem Value="aValue">aText</asp:ListItem>
            <asp:ListItem Value="bValue">bText</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
