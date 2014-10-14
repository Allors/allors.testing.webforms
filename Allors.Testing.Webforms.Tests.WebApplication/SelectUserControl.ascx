<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectUserControl.ascx.cs" Inherits="Allors.Testing.Webforms.Tests.SelectUserControl" %>
<%@ Import Namespace="System.Web.DynamicData" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.Expressions" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<asp:Panel ID="Panel1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button (User Control - 1)" />
    <asp:Panel ID="Panel2" runat="server">
        <asp:Button ID="Button2" runat="server" Text="Button (User Control - 2)" />
    </asp:Panel>
    <asp:Panel runat="server" ID="sameId" />
</asp:panel>
