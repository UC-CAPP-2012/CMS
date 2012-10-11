<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MajorRegion.aspx.cs" Inherits="CMS.GeneralPages.MajorRegion" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    //Don't run suver side code and do nothing when cancel button on confirmation is clicked.
    function CancelClick() { }
</script>
<div class="wrapper1">
    <!-- The menu on the left -->
    <div class="contentLeftMenu">
        <h1> Major Region </h1>
        <ul>
            <li><span style="margin-left: -1em;"/><asp:LinkButton ID="InsertLinkButton" runat="server" onclick="InsertLinkButton_Click"> 
                Insert New Region </asp:LinkButton></li>
        </ul>
    </div>
    <div class="wrapper2">
    <!-- Data List -->
    <div class="contentList category">
        <asp:GridView ID="GridViewRegion" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="MajorRegionID" 
            DataSourceID="ObjectDataSourceCategory" ForeColor="#333333" 
            GridLines="None" onrowdatabound="GridViewRegion_RowDataBound" 
            onselectedindexchanged="GridViewRegion_SelectedIndexChanged" 
            Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
            <Columns>           
                    <asp:CommandField SelectText="" ShowSelectButton="True">
                    <ItemStyle Width="5px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="MajorRegionID" HeaderText="MajorRegionID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="MajorRegionID" 
                        Visible="False" >
                    </asp:BoundField>
                    <asp:BoundField DataField="MajorRegionName" HeaderText="Major Region Name" 
                        SortExpression="MajorRegionName" >
                    </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceCategory" runat="server" 
            SelectMethod="getAllMajorRegion" TypeName="CMS.BLL.CMSBLClass" 
            OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>
    </div>
    <!-- Data Detail -->
        <div class="contentDetail category">
            <div class="contentDetailWrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            <asp:MultiView ID="RegionMultiView" runat="server">

                <!-- default detail display -->
                <asp:View ID="DetailView" runat="server">
                    <h1> Major Region Details
                        <span class = "detailButtons">
                            <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="70px" onclick="UpdateButton_Click" />
                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="70px" onclick="DeleteButton_Click"/>                
                            <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                runat="server" ConfirmText="Are you sure you want to delete the selected region?" 
                                Enabled="True" TargetControlID="DeleteButton">
                            </asp:ConfirmButtonExtender>                  
                        </span>
                    </h1>
                    <hr />
                    <asp:Label ID="NameLabel" runat="server" CssClass="label" Font-Bold="True" 
                        Text="Region Name : " Width="150px"></asp:Label>
                    <asp:Label ID="NameDataLabel" runat="server"></asp:Label>
                      
                </asp:View>

                <!-- update display (Visible when update button is clicked) -->
                <asp:View ID="UpdateView" runat="server">
                    <h1> Update Category Details </h1> 
                    <hr /> 
                    <asp:Label ID="NameUpdateLabel" CssClass="label" runat="server" Text="Region Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategoryName" runat="server" 
                        ErrorMessage="Region name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
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
                    <h1> Insert New Major Region </h1>
                    <hr />
                    <asp:Label ID="InsertNameLabel" CssClass="label" runat="server" Text="Region Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="InsertNameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                        ErrorMessage="Category name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>
                    <div class="detailButtons bottom">
                        <asp:Button ID="SubmitNewButton" runat="server" Text="Confirm" Width="100px" 
                            onclick="SubmitNewButton_Click" />
                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" OnClientCancel="CancelClick"
                            runat="server" ConfirmText="Do you want to submit the new region?" Enabled="True" 
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
    </div>
</asp:Content>
