<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubType.aspx.cs" Inherits="CMS.GeneralPages.SubType" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    //Don't run suver side code and do nothing when cancel button on confirmation is clicked.
    function CancelClick() { }
</script>
<div class="wrapper1">
    <div class="contentLeftMenu">
        <h1> Subtype </h1>
        <asp:LinkButton ID="InsertLinkButton" runat="server" 
            onclick="InsertLinkButton_Click"> 
            Insert New Subtype </asp:LinkButton>
    </div>
    <div class="wrapper2">
    <div class="contentList">
        <asp:GridView ID="SubtypeGridView" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SubtypeID" 
            DataSourceID="SubtypeObjectDataSource" ForeColor="Black" GridLines="Vertical" 
            onrowdatabound="SubtypeGridView_RowDataBound" CssClass="gridViewList"
            onselectedindexchanged="SubtypeGridView_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="" ShowSelectButton="True" />
                <asp:BoundField DataField="SubtypeID" HeaderText="SubtypeID" 
                    InsertVisible="False" ReadOnly="True" SortExpression="SubtypeID" 
                    Visible="False" />
                <asp:BoundField DataField="SubtypeName" HeaderText="SubtypeName" 
                    SortExpression="SubtypeName">
                <ItemStyle Width="200px" />
                </asp:BoundField>
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
        <asp:ObjectDataSource ID="SubtypeObjectDataSource" runat="server" 
            DeleteMethod="DeleteSubtype" InsertMethod="InsertSubtype" 
            SelectMethod="getAllSubtype" TypeName="CMS.BLL.CMSBLClass" 
            UpdateMethod="UpdateSubtype">
            <DeleteParameters>
                <asp:Parameter Name="original_SubtypeID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="subtypeName" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="subtypeName" Type="String" />
                <asp:Parameter Name="original_SubtypeID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
        <div class="contentDetail">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            <asp:MultiView ID="SubtypeMultiView" runat="server">

                <!-- default detail display -->
                <asp:View ID="DetailView" runat="server">
                    <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="100px" 
                        CssClass="detailButtons" onclick="UpdateButton_Click" />
                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="100px" 
                        CssClass="detailButtons" onclick="DeleteButton_Click"/>                
                    <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Are you sure you want to delete the selected subtype?" 
                        Enabled="True" TargetControlID="DeleteButton">
                    </asp:ConfirmButtonExtender>
                    <h2> Subtype Details </h2>  
                    <asp:Label ID="NameLabel" runat="server" CssClass="label" Text="Subtype Name : " Font-Bold="True" Width="150px"></asp:Label>
                    <asp:Label ID="NameDataLabel" runat="server"></asp:Label>
                </asp:View>

                <!-- update display (Visible when update button is clicked) -->
                <asp:View ID="UpdateView" runat="server">
                    <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" 
                        onclick="SubmitButton_Click" CssClass="detailButtons" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                        TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                    </asp:ConfirmButtonExtender>
                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" 
                        CssClass="detailButtons" CausesValidation="False" 
                        onclick="CancelButton_Click" />
                    <h2> Update Subtype Details </h2>  
                    <asp:Label ID="NameUpdateLabel" CssClass="label" runat="server" Text="Subtype Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategoryName" runat="server" 
                        ErrorMessage="Subtype name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                    </p>
                </asp:View>

                <!-- insert new display (Visible when insert link button is clicked) -->
                <asp:View ID="InsertView" runat="server">
                    <asp:Button ID="SubmitNewButton" runat="server" Text="Insert New" Width="100px" 
                        onclick="SubmitNewButton_Click" CssClass="detailButtons" />
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Do you want to submit the new Subtype?" Enabled="True" 
                        TargetControlID="SubmitNewButton" ConfirmOnFormSubmit="True">
                    </asp:ConfirmButtonExtender>
                    <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" 
                        CssClass="detailButtons" CausesValidation="False" 
                        onclick="InsertCancelButton_Click" />
                    <h2> Insert New Subtype </h2>
                    <asp:Label ID="InsertNameLabel" CssClass="label" runat="server" Text="Subtype Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="InsertNameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                        ErrorMessage="Subtype name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>
                </asp:View>
            </asp:MultiView>
        </div>
        </div>
    </div>
</asp:Content>
