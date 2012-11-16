<%@ Page Title="CAPP CMS - Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="CMS.AdminPages.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper1">
        <div class="contentLeftMenu adminLeftMenu">
            <h1> Administrator </h1>
            <div class="sideBarNavigation">
                <ul>
                    <li><a href="AddUser.aspx">Add a new user</a></li>
                    <li><a href="Admin.aspx">Remove a user</a></li>
                </ul>
            </div>
        </div>

        <div class="wrapper2Settings">
            <div class="contentDetail  adminBody">
                <h1>Remove User</h1>
                <hr />        
                <p runat="server" id="instruction_msg">Select and then click 'Delete' to remove a user from the system.</p>
                <asp:Label runat="server" ID="status_msg" Text="" ></asp:Label>
                <asp:Label runat="server" ID="nullNoUser_msg" Text="There aren't currently any users in the system." Visible="False"></asp:Label>
                <div class="allUsersGridView">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CellPadding="4" DataKeyNames="ApplicationId,LoweredUserName" 
                    DataSourceID="ObjectDataSource1" Width="700px" AllowPaging="True" onrowdeleted="GridView1_RowDeleted" onrowdeleting="GridView1_RowDeleting" 
                    ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                            <asp:BoundField DataField="LastActivityDate" HeaderText="Last Activity Date"  SortExpression="LastActivityDate" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#284775" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="removeUser" SelectMethod="GetAllUsers" 
                    TypeName="CMS.BLL.ClassManageUser">
                        <DeleteParameters>
                            <asp:Parameter DbType="Guid" Name="ApplicationId" />
                            <asp:Parameter Name="LoweredUserName" Type="String" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>
                </div>      
            </div>   
        </div>  
    </div>        
</asp:Content>

