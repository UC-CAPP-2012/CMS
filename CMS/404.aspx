<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="CMS._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="Master404">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/404.png" CssClass="img404" />
    <div class="Text404">
        <asp:Label ID="Label1" runat="server" Text="ERROR 404" Font-Size="90px" 
            ForeColor="#FF8A00" Font-Names='Century Gothic,Calibri'></asp:Label><br/>
        <asp:Label ID="Label2" runat="server" Text="Page Not Found" Font-Size="40px" 
            ForeColor="#FF6000" Font-Names="Century Gothic,Calibri"></asp:Label><br/>
        <asp:Label ID="Label3" runat="server" Text="
        Sorry, we could not find the page you were looking for.&lt;br/&gt;
        Please go back or try again.
        " Font-Size="18px" ForeColor="#404040" Font-Names="Century Gothic,Calibri"></asp:Label>
    </div>
</div>
</asp:Content>
 