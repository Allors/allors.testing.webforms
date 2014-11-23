<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadioButtonPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.RadioButtonPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:RadioButton ID="RadioButton" runat="server" />
        <asp:Button ID="Button" runat="server" OnClick="ButtonClick" 
            Text="Button" />
        <asp:RadioButton ID="AutoPostBackRadioButton" AutoPostBack="true" runat="server" 
            oncheckedchanged="AutoPostBackRadioButtonCheckedChanged" />
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
