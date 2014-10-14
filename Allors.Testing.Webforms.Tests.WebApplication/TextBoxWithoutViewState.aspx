<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBoxWithoutViewState.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.TextBoxWithoutViewStatePage" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>

<%@ Register src="TextBoxUserControl.ascx" tagname="TextBoxWebUserControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:TextBox ID="TextBox" runat="server" EnableViewState="false"></asp:TextBox>
        <asp:Button ID="Button" runat="server" OnClick="ButtonClick" Text="TextBox" />
        <asp:TextBox ID="AutoPostBackTextBox" runat="server" AutoPostBack="True" ontextchanged="AutoPostBackTextBoxTextChanged" EnableViewState="false">AutoPostBack Data</asp:TextBox>
        <asp:Label ID="Label" runat="server"></asp:Label>
    </div>
    <uc1:TextBoxWebUserControl ID="TextBoxUserControl" runat="server" />
    </form>
</body>
</html>
