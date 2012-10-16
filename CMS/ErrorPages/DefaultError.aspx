<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="DefaultError.aspx.cs" Inherits="CMS.ErrorPages.DefaultError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="ErrorMaster">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/404.png" CssClass="ErrorImg" />
    <div class="ErrorText">
        <asp:Label ID="Label1" runat="server" Text="ERROR" Font-Size="90px" 
            ForeColor="#FF8A00" Font-Names='Century Gothic,Calibri'></asp:Label><br/>
        <asp:Label ID="Label2" runat="server" Text="Unexpected Error" Font-Size="40px" 
            ForeColor="#FF6000" Font-Names="Century Gothic,Calibri"></asp:Label><br/>
        <asp:Label ID="Label3" runat="server" Text="
        Sorry, unexpected error has occurred.&lt;br/&gt;
        Please go back or try again.
        " Font-Size="18px" ForeColor="#404040" Font-Names="Century Gothic,Calibri"></asp:Label>
    </div>
</div>
</asp:Content>
