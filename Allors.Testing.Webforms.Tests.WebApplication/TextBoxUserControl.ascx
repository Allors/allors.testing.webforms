<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxUserControl.ascx.cs" Inherits="Allors.Testing.Webforms.Tests.TextBoxUserControl" %>

<div>
    <asp:TextBox ID="TextBox" runat="server" EnableViewState="false"></asp:TextBox>
    <asp:Button ID="Button" runat="server" OnClick="ButtonClick" Text="TextBox" />
    <asp:TextBox ID="AutoPostBackTextBox" runat="server" AutoPostBack="True" ontextchanged="AutoPostBackTextBoxTextChanged" EnableViewState="false">AutoPostBack Data</asp:TextBox>
    <asp:Label ID="Label" runat="server"></asp:Label>
</div>
