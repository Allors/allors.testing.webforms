<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedirectToPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.RedirectToPage" %>
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
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox" runat="server"></asp:TextBox>
        <asp:Button ID="Button" runat="server" OnClick="Button_Click" Text="TextBox" />
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
