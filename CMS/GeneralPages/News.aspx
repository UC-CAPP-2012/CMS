<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="CMS.CMSPages.News" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Don't run suver side code and do nothing when cancel button on confirmation is clicked.
        function CancelClick() { }
    </script>

    <div class="wrapper1">
    <div class="contentLeftMenu">
        <h1> News </h1>
    </div>
    <div class="wrapper2">
    <div class="contentList">
        <asp:GridView ID="GridViewNews" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            DataKeyNames="NewsID" DataSourceID="ObjectDataSourceNews" ForeColor="Black" 
            GridLines="Vertical"  Width="358px"  CssClass="gridViewList"
            onrowdatabound="GridViewNews_RowDataBound" 
            onselectedindexchanged="GridViewNews_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True"  SelectText="" />
                <asp:BoundField DataField="NewsHeading" HeaderText="Title" 
                    SortExpression="NewsHeading" />
                <asp:BoundField DataField="NewsDateTime" HeaderText="Published Date" 
                    SortExpression="NewsDateTime" />
                <asp:BoundField DataField="NewsPublisher" HeaderText="Publisher" 
                    SortExpression="NewsPublisher" />
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
        <asp:ObjectDataSource ID="ObjectDataSourceNews" runat="server" 
            DeleteMethod="DeleteNews" InsertMethod="InsertNews" SelectMethod="getAllNews" 
            TypeName="CMS.BLL.CMSBLClass" UpdateMethod="UpdateNews" 
            OldValuesParameterFormatString="original_{0}">
            <DeleteParameters>
                <asp:Parameter Name="original_NewsID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="newsHeading" Type="String" />
                <asp:Parameter Name="newsDateTime" Type="DateTime" />
                <asp:Parameter Name="newsBody" Type="String" />
                <asp:Parameter Name="newsMediaURL" Type="String" />
                <asp:Parameter Name="newsPublisher" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="newsHeading" Type="String" />
                <asp:Parameter Name="newsDateTime" Type="DateTime" />
                <asp:Parameter Name="newsBody" Type="String" />
                <asp:Parameter Name="newsMediaURL" Type="String" />
                <asp:Parameter Name="newsPublisher" Type="String" />
                <asp:Parameter Name="original_NewsID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <!-- Data Detail -->
        <div class="contentDetail">
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            <asp:MultiView ID="NewsMultiView" runat="server">

                <!-- default detail display -->
                <asp:View ID="DetailView" runat="server">
                    <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="100px" 
                        onclick="UpdateButton_Click" CssClass="detailButtons" />
                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="100px" 
                        onclick="DeleteButton_Click" CssClass="detailButtons"/>                
                    <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Are you sure you want to delete the selected news?" 
                        Enabled="True" TargetControlID="DeleteButton">
                    </asp:ConfirmButtonExtender>
                    <div class="news-head">
                        <h2 runat="server" id="newsTitle" class="news-title"></h2>
                        <hr>
                    </div>  
                    <div runat="server" id="newsLabel" class="news-item-labels"> </div>
                    <div class="news-content">
                        <div runat="server" id="newsImage" class="news-image"></div>
                        <div runat="server" id="newsBody"  class="news-body"></div>
                    </div>
                </asp:View>

                <!-- update display (Visible when update button is clicked) -->
                <asp:View ID="UpdateView" runat="server">
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" 
                        CssClass="detailButtons" onclick="SubmitButton_Click"  />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                        TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                    </asp:ConfirmButtonExtender>
                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" 
                        CssClass="detailButtons" onclick="CancelButton_Click" CausesValidation="False" />
                    <h2> Update Category Details </h2>  
                    <asp:Label ID="NameUpdateLabel" CssClass="label" runat="server" Text="Category Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategoryName" runat="server" 
                        ErrorMessage="Category name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                    </p>
                </asp:View>

                <!-- insert new display (Visible when insert link button is clicked) -->
                <asp:View ID="InsertView" runat="server">
                    <asp:Button ID="SubmitNewButton" runat="server" Text="Insert New" Width="100px" 
                        CssClass="detailButtons" onclick="SubmitNewButton_Click" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Do you want to submit the new category?" Enabled="True" 
                        TargetControlID="SubmitNewButton" ConfirmOnFormSubmit="True">
                    </asp:ConfirmButtonExtender>
                    <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" 
                        CssClass="detailButtons" CausesValidation="False" 
                        onclick="InsertCancelButton_Click" />
                    <h2> Insert New Category </h2>
                    <asp:Label ID="InsertNameLabel" CssClass="label" runat="server" Text="Category Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="InsertNameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                        ErrorMessage="Category name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    </div>
</asp:Content>
