<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebTests._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:TextBox ID="txtTextBox" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="txtNascimento" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="txtIdade" runat="server"></asp:TextBox>
    <br />
    <asp:DropDownList ID="ddlDropDown" runat="server"></asp:DropDownList>
    <br />
    <asp:RadioButtonList ID="rblRadioList" runat="server">
        <asp:ListItem Text="M" Value="1"></asp:ListItem>
        <asp:ListItem Text="F" Value="2"></asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:DropDownList runat="server" ID="ddlEstadoCivil">
        <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
        <asp:ListItem Text="Solteiro" Value="S"></asp:ListItem>
        <asp:ListItem Text="Casado" Value="C"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Button runat="server" OnClick="Testar" />
</asp:Content>
