<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBoxListPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.CheckBoxListPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:CheckBoxList ID="CheckBoxList" runat="server">
            <asp:ListItem Value="wValue">wText</asp:ListItem>
            <asp:ListItem Value="xValue">xText</asp:ListItem>
            <asp:ListItem Value="yValue">yText</asp:ListItem>
            <asp:ListItem Value="zValue">zText</asp:ListItem>            
        </asp:CheckBoxList>
        <asp:Button ID="Button" runat="server" OnClick="Button_Click" 
            Text="Button" />
        <asp:CheckBoxList AutoPostBack="true" ID="AutoPostBackCheckBoxList" runat="server" 
            onselectedindexchanged="AutoPostBackRadioButtonList_SelectedIndexChanged">
            <asp:ListItem Value="aValue">aText</asp:ListItem>
            <asp:ListItem Value="bValue">bText</asp:ListItem>
            <asp:ListItem Value="cValue">cText</asp:ListItem>
            <asp:ListItem Value="dValue">cText</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
