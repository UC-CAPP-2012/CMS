<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="CMS.AdminPages.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contentLeftMenu adminLeftMenu">
        <h1> Administrator </h1>
        <div class="sideBarNavigation">
        <ul>
            <li><a href="AddUser.aspx">Add a new user</a></li>
            <li><a href="Admin.aspx">Remove a user</a></li>
        </ul>
        </div>
    </div>
    <div class="contentList  adminBody">
        <h1>Remove User</h1>
        <hr />
        
        <p runat="server" id="instruction_msg">Select and then click 'Delete' to remove a user from the system.</p>
        <asp:Label runat="server" ID="status_msg" Text="" ></asp:Label>
        <asp:Label runat="server" ID="nullNoUser_msg" Text="There aren't currently any users in the system." Visible="False"></asp:Label>
        <div class="allUsersGridView">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" 
            CellPadding="3" CellSpacing="2" DataKeyNames="ApplicationId,LoweredUserName" 
            DataSourceID="ObjectDataSource1" Width="700px" AllowPaging="True" 
                onrowdeleted="GridView1_RowDeleted" onrowdeleting="GridView1_RowDeleting" >
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="User Name" 
                    SortExpression="UserName" />
                <asp:BoundField DataField="LastActivityDate" HeaderText="Last Activity Date" 
                    SortExpression="LastActivityDate" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                DeleteMethod="removeUser" SelectMethod="GetAllUsers" 
                TypeName="CMS.BLL.ClassManageUser">
                <DeleteParameters>
                    <asp:Parameter DbType="Guid" Name="ApplicationId" />
                    <asp:Parameter Name="LoweredUserName" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </div>
        

    </div>
    
    
    
</asp:Content>

