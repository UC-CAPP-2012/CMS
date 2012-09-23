<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="CMS.CMSPages.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper1">
        <div class="contentLeftMenu adminLeftMenu">
            <h1> User </h1>
            <div class="sideBarNavigation">
                <ul>
                    <li><asp:LinkButton ID="AllUsersLinkButton" runat="server"  onclick="AllUsersButton_Click">View all Users</asp:LinkButton></li>
                    <li><asp:LinkButton ID="SubcribedUsersLinkButton" runat="server"  onclick="SubcribedUsersButton_Click">View all Subcribed Users</asp:LinkButton></li>
                    <li><asp:LinkButton ID="UnsubcribedUsersLinkButton" runat="server"  onclick="UnsubcribedUsersButton_Click">View all Unsubcribed Users</asp:LinkButton></li>
                </ul>
            </div>
        </div>
        <div class="wrapper2Settings">
            <div class="contentDetail  adminBody">
                <asp:MultiView ID="UsersMultiView" runat="server" ActiveViewIndex="0">
                    <asp:View ID="UsersView" runat="server">
                    <h1>All Users</h1>
                    <hr />
                        <asp:GridView ID="UsersGridView" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            DataKeyNames="UserEmail" DataSourceID="UsersObjectDataSource" ForeColor="Black" 
                            GridLines="Vertical">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="UserEmail" HeaderText="User Email" ReadOnly="True" 
                                    SortExpression="UserEmail" />
                                <asp:BoundField DataField="UserFirstName" HeaderText="First Name" 
                                    SortExpression="UserFirstName" />
                                <asp:BoundField DataField="UserLastName" HeaderText="Last Name" 
                                    SortExpression="UserLastName" />
                                <asp:BoundField DataField="UserPostcode" HeaderText="Postcode" 
                                    SortExpression="UserPostcode" />
                                <asp:BoundField DataField="UserSubscribed" HeaderText="Subscribed" 
                                    SortExpression="UserSubscribed" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="UsersObjectDataSource" runat="server" 
                            SelectMethod="getAllUsers" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource>
                    </asp:View>
                    <asp:View ID="SubcribedUsersView" runat="server">
                    <h1>All Subcribed Users</h1>
                    <hr />
                    <asp:GridView ID="SubcribedUsersGridView" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            DataKeyNames="UserEmail" DataSourceID="SubcribedUsersObjectDataSource" 
                            ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="UserEmail" HeaderText="User Email" ReadOnly="True" 
                                SortExpression="UserEmail" />
                            <asp:BoundField DataField="UserFirstName" HeaderText="First Name" 
                                SortExpression="UserFirstName" />
                            <asp:BoundField DataField="UserLastName" HeaderText="Last Name" 
                                SortExpression="UserLastName" />
                            <asp:BoundField DataField="UserPostcode" HeaderText="Postcode" 
                                SortExpression="UserPostcode" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="SubcribedUsersObjectDataSource" runat="server" 
                            SelectMethod="getAllSubcribedUsers" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource>
                    </asp:View>
                    <asp:View ID="UnsubcribedUsersView" runat="server">
                    <h1>All Unsubcribed Users</h1>
                    <hr />
                    <asp:GridView ID="UnsubcribedUsersGridView" runat="server" AllowPaging="True" 
                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            DataKeyNames="UserEmail" DataSourceID="UnsubcribedUsersObjectDataSource" 
                            ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="UserEmail" HeaderText="User Email" ReadOnly="True" 
                                SortExpression="UserEmail" />
                            <asp:BoundField DataField="UserFirstName" HeaderText="First Name" 
                                SortExpression="UserFirstName" />
                            <asp:BoundField DataField="UserLastName" HeaderText="Last Name" 
                                SortExpression="UserLastName" />
                            <asp:BoundField DataField="UserPostcode" HeaderText="Postcode" 
                                SortExpression="UserPostcode" />
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#F7F7DE" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                        <SortedAscendingHeaderStyle BackColor="#848384" />
                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                        <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="UnsubcribedUsersObjectDataSource" runat="server" 
                            SelectMethod="getAllUnsubcribedUsers" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource>
                    </asp:View>
                </asp:MultiView>
            </div>
    
        </div>
    </div>
</asp:Content>
