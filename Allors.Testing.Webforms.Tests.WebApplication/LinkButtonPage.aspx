<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkButtonPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.LinkButtonPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
        <asp:LinkButton ID="LinkButton" runat="server" OnClick="LinkButton_Click" Text="->" />
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
