<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBoxPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.TextBoxPage" %>
<%@ Register TagPrefix="cc1" Namespace="Allors.Testing.Webforms.Tests" Assembly="Allors.Testing.Webforms.Tests.WebApplication" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
        <asp:Button ID="Button" runat="server" OnClick="ButtonClick" Text="TextBox" />
        <asp:TextBox ID="AutoPostBackTextBox" runat="server" AutoPostBack="True" ontextchanged="AutoPostBackTextBoxTextChanged">AutoPostBack Data</asp:TextBox>
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    <cc1:OnTextChangedTextBox ID="OnTextChangedTextBox" AutoPostBack="true" runat="server"></cc1:OnTextChangedTextBox>
    </form>
</body>
</html>
