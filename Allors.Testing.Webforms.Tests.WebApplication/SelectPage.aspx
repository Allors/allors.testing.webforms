<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPage.aspx.cs" Inherits="Allors.Testing.Webforms.Tests.SelectPage" %>
<%@ Register src="SelectUserControl.ascx" tagname="SelectUserControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <asp:Button ID="Button1" runat="server" Text="Button (Page - 1)" />
        <asp:Panel ID="Panel2" runat="server">
            <asp:Button ID="Button2" runat="server" Text="Button (Page - 2)" />
            <asp:Panel ID="Panel3" runat="server">
                <asp:Button ID="Button3" runat="server" Text="Button (Page - 3 )" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    
    <asp:Literal runat="server" ID="sameId" />
    
    <uc1:SelectUserControl ID="SelectUserControl" runat="server" />
    </form>
</body>
</html>
