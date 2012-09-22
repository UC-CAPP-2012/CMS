<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="CMS.CMSPages.Category" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    //Don't run suver side code and do nothing when cancel button on confirmation is clicked.
    function CancelClick() {}
</script>
<div class="wrapper1">
    <!-- The menu on the left -->
    <div class="contentLeftMenu">
        <h1> Category </h1>
        <asp:LinkButton ID="InsertLinkButton" runat="server" onclick="InsertLinkButton_Click"> Insert New Category </asp:LinkButton>
    </div>
    <div class="wrapper2">
    <!-- Data List -->
    <div class="contentList">
        <asp:GridView ID="GridViewCategory" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CategoryID" 
            DataSourceID="ObjectDataSourceCategory" ForeColor="Black"  CssClass="gridViewList"
            GridLines="Vertical" onrowdatabound="GridViewCategory_RowDataBound" 
            onselectedindexchanged="GridViewCategory_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White"  />
            <Columns>           
                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                    <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="CategoryID" 
                        Visible="False" >
                    <ItemStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CategoryName" HeaderText="CategoryName" 
                        SortExpression="CategoryName" >
                    <ItemStyle Width="200px" />
                    </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE"  />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceCategory" runat="server" 
            DeleteMethod="DeleteCategory" InsertMethod="InsertCategory" 
            SelectMethod="getAllCategory" TypeName="CMS.BLL.CMSBLClass" 
            UpdateMethod="UpdateCategory" 
            OldValuesParameterFormatString="original_{0}">
            <DeleteParameters>
                <asp:Parameter Name="original_CategoryID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="categoryName" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="categoryName" Type="String" />
                <asp:Parameter Name="original_CategoryID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <!-- Data Detail -->
        <div class="contentDetail">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            <asp:MultiView ID="CategoryMultiView" runat="server">

                <!-- default detail display -->
                <asp:View ID="DetailView" runat="server">
                    <h1> Category Details
                        <span class = "detailButtons">
                            <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="70px" onclick="UpdateButton_Click" />
                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="70px" onclick="DeleteButton_Click"/>                
                            <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                runat="server" ConfirmText="Are you sure you want to delete the selected category?" 
                                Enabled="True" TargetControlID="DeleteButton">
                            </asp:ConfirmButtonExtender>                  
                        </span>
                    </h1>
                    <hr />
                    <asp:Label ID="NameLabel" runat="server" CssClass="label" Font-Bold="True" 
                        Text="Category Name : " Width="150px"></asp:Label>
                    <asp:Label ID="NameDataLabel" runat="server"></asp:Label>
                      
                </asp:View>

                <!-- update display (Visible when update button is clicked) -->
                <asp:View ID="UpdateView" runat="server">
                    <h1> Update Category Details </h1> 
                    <hr /> 
                    <asp:Label ID="NameUpdateLabel" CssClass="label" runat="server" Text="Category Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="NameTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="NameTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender>
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategoryName" runat="server" 
                        ErrorMessage="Category name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                    </p>
                    <div class="detailButtons bottom">
                        <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" 
                            onclick="SubmitButton_Click"  />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick"
                            runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                            TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                        </asp:ConfirmButtonExtender>
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" 
                            onclick="CancelButton_Click" CausesValidation="False" />
                    </div>
                </asp:View>

                <!-- insert new display (Visible when insert link button is clicked) -->
                <asp:View ID="InsertView" runat="server">
                    <h1> Insert New Category </h1>
                    <hr />
                    <asp:Label ID="InsertNameLabel" CssClass="label" runat="server" Text="Category Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="InsertNameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="InsertNameTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="InsertNameTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender>
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                        ErrorMessage="Category name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>
                    <div class="detailButtons bottom">
                        <asp:Button ID="SubmitNewButton" runat="server" Text="Confirm" Width="100px" 
                            onclick="SubmitNewButton_Click" />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" OnClientCancel="CancelClick"
                            runat="server" ConfirmText="Do you want to submit the new category?" Enabled="True" 
                            TargetControlID="SubmitNewButton" ConfirmOnFormSubmit="True">
                        </asp:ConfirmButtonExtender>
                        <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" 
                            CausesValidation="False" 
                            onclick="InsertCancelButton_Click" />
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        </div>
    </div>
</asp:Content>
