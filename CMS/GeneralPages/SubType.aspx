﻿<%@ Page Title="CAPP CMS - Subtype" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubType.aspx.cs" Inherits="CMS.GeneralPages.SubType" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //Not to run suver side code and do nothing when cancel button on confirmation is clicked.
        function CancelClick() { }
    </script>
    <div class="wrapper1">
    <!-- ========== Sub menu on the left ==========  -->
        <div class="contentLeftMenu">
            <h1> Subtype </h1>
            <ul>
                <li><span style="margin-left: -1em;"/><asp:LinkButton ID="InsertLinkButton" runat="server"  onclick="InsertLinkButton_Click"> 
                    Insert New Subtype </asp:LinkButton></li>
            </ul>
        </div>
        <div class="wrapper2">
            <!-- ========== Subtype List (Grid View) in the middle ========== -->
            <div class="contentList subtype">
                <asp:GridView ID="SubtypeGridView" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SubtypeID" 
                    DataSourceID="SubtypeObjectDataSource" ForeColor="#333333" GridLines="None" 
                    onrowdatabound="SubtypeGridView_RowDataBound" 
                    onselectedindexchanged="SubtypeGridView_SelectedIndexChanged" Width="100%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField SelectText="" ShowSelectButton="True" >
                            <ItemStyle Width="5px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="SubTypeID" HeaderText="SubtypeID" 
                            InsertVisible="False" ReadOnly="True" SortExpression="SubTypeID"  Visible="False" />
                        <asp:BoundField DataField="SubType" HeaderText="Subtype Name" SortExpression="SubType">
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        <span/>There is no subtype.</span>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:ObjectDataSource ID="SubtypeObjectDataSource" runat="server" 
                    DeleteMethod="DeleteSubtype" InsertMethod="InsertSubtype" 
                    SelectMethod="getAllSubtype" TypeName="CMS.BLL.CMSBLClass" 
                    UpdateMethod="UpdateSubtype" OldValuesParameterFormatString="original_{0}">
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
            <!-- ========== Subtype Detail (Multi View) on the right ========== -->
            <div class="contentDetail subtype">
                <div class="contentDetailWrapper">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                    <asp:MultiView ID="SubtypeMultiView" runat="server">
                        <!-- ========== data detail view  ========== -->
                        <asp:View ID="DetailView" runat="server">
                            <h1> Subtype Details
                                <span class = "detailButtons">
                                    <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="70px" onclick="UpdateButton_Click" />
                                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="70px" onclick="DeleteButton_Click"/>                
                                    <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                        runat="server" ConfirmText="Are you sure you want to delete the selected subtype?" 
                                        Enabled="True" TargetControlID="DeleteButton">
                                    </asp:ConfirmButtonExtender>                  
                                </span>
                            </h1>
                            <hr />
                            <asp:Label ID="NameLabel" runat="server" CssClass="label" Text="Subtype Name : " Font-Bold="True" Width="150px"></asp:Label>
                            <asp:Label ID="NameDataLabel" runat="server"></asp:Label>
                        </asp:View>
                        <!-- ========== update form view ========== -->
                        <asp:View ID="UpdateView" runat="server">
                            <h1> Update Subtype Details </h1>  
                            <hr />
                            <asp:Label ID="NameUpdateLabel" CssClass="label" runat="server" Text="Subtype Name : " Font-Bold="True" Width="150px" ></asp:Label>
                            <asp:TextBox ID="NameTextBox" runat="server" Width="250px"></asp:TextBox> 
                            <p class="validationError">             
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategoryName" runat="server" 
                                ErrorMessage="Subtype name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                            </p>
                            <div class="detailButtons bottom">
                                <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" onclick="SubmitButton_Click" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick" runat="server" 
                                ConfirmText="Do you want to submit the update?" Enabled="True"   TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                                </asp:ConfirmButtonExtender>
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" CausesValidation="False" onclick="CancelButton_Click" />
                            </div>
                        </asp:View>
                        <!-- ========== insert form view ========== -->
                        <asp:View ID="InsertView" runat="server">
                            <h1> Insert New Subtype </h1>
                            <hr />
                            <asp:Label ID="InsertNameLabel" CssClass="label" runat="server" Text="Subtype Name : " Font-Bold="True" Width="150px" ></asp:Label>
                            <asp:TextBox ID="InsertNameTextBox" runat="server" Width="250px"></asp:TextBox> 
                            <p class="validationError">             
                                <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                                ErrorMessage="Subtype name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                            </p>
                            <div class="detailButtons bottom">
                                <asp:Button ID="SubmitNewButton" runat="server" Text="Confirm" Width="100px" onclick="SubmitNewButton_Click"/>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" OnClientCancel="CancelClick" runat="server" 
                                ConfirmText="Do you want to submit the new Subtype?" Enabled="True" TargetControlID="SubmitNewButton" ConfirmOnFormSubmit="True">
                                </asp:ConfirmButtonExtender>
                                <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" CausesValidation="False" onclick="InsertCancelButton_Click" />
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>        
    </div>   
</asp:Content>
