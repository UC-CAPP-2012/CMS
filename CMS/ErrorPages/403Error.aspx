<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="403Error.aspx.cs" Inherits="CMS.ErrorPages._403Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="ErrorMaster">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/404.png" CssClass="ErrorImg" />
    <div class="ErrorText">
        <asp:Label ID="Label1" runat="server" Text="ERROR 403" Font-Size="90px" 
            ForeColor="#FF8A00" Font-Names='Century Gothic,Calibri'></asp:Label><br/>
        <asp:Label ID="Label2" runat="server" Text="Forbidden" Font-Size="40px" 
            ForeColor="#FF6000" Font-Names="Century Gothic,Calibri"></asp:Label><br/>
        <asp:Label ID="Label3" runat="server" Text="
        Sorry, the website declined to show the page you requested.&lt;br/&gt;
        Please go back or try again.
        " Font-Size="18px" ForeColor="#404040" Font-Names="Century Gothic,Calibri"></asp:Label>
    </div>
</div>
</asp:Content>
