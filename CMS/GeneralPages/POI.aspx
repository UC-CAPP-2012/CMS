<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POI.aspx.cs" Inherits="CMS.CMSPages.POI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    //Don't run suver side code and do nothing when cancel button on confirmation is clicked.
    function CancelClick() { }
</script>

    <div class="contentDetailWrapper">
        <div class="contentDetail poi">
            <asp:ScriptManager ID="ScriptManager1" runat="server"/>
            <asp:MultiView ID="POIMultiView" runat="server">

                <!-- default detail display -->
                <asp:View ID="DetailView" runat="server">
                </asp:View>

                <!-- update display (Visible when update button is clicked) -->
                <asp:View ID="UpdateView" runat="server">
                </asp:View>

                <!-- insert new display (Visible when insert link button is clicked) -->
                <asp:View ID="DeleteView" runat="server">
                    <asp:Button ID="InsertButton" runat="server" Text="Insert New" Width="100px" 
                        CssClass="detailButtons" onclick="InsertButton_Click" />
                    <asp:ConfirmButtonExtender ID="InsertButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                        runat="server" ConfirmText="Do you want to submit the new point of interest?" Enabled="True" 
                        TargetControlID="SubmitNewButton" ConfirmOnFormSubmit="True">
                    </asp:ConfirmButtonExtender>
                    <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" 
                        CssClass="detailButtons" CausesValidation="False" 
                        onclick="InsertCancelButton_Click" />

                        <h2> Insert New Point Of Interest </h2>
                    <!-- Name -->
                    <asp:Label ID="InsertNameLabel" CssClass="label" runat="server" Text="POI Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="InsertNameTextBox" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                        ErrorMessage="POI name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>
                    <!-- Category -->
                    <asp:Label ID="InsertCategoryLabel" CssClass="label" runat="server" Text="Category : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" Width="245px" 
                        DataSourceID="CategoryObjectDataSource" DataTextField="CategoryName" 
                        DataValueField="CategoryID"/>
                    <asp:ObjectDataSource ID="CategoryObjectDataSource" runat="server" 
                        SelectMethod="getAllCategory" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource>
                    <!-- Subtype -->
                    <asp:Label ID="Label2" CssClass="label" runat="server" Text="POI Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="POI name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>
                    <!-- Address -->
                    <asp:Label ID="Label3" CssClass="label" runat="server" Text="POI Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" Width="250px"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="POI name is required." ControlToValidate="InsertNameTextBox" SetFocusOnError="True" />
                    </p>

                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <div class="contentList poi">
        <asp:GridView ID="POIGridView" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            DataSourceID="POIObjectDataSource" ForeColor="Black" GridLines="Vertical" 
            Width="100%" onrowdatabound="POIGridView_RowDataBound" 
            onselectedindexchanged="POIGridView_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="" ShowSelectButton="True" />
                <asp:BoundField DataField="ItemID" HeaderText="ItemID" InsertVisible="False" 
                    SortExpression="ItemID" Visible="False" />
                <asp:BoundField DataField="ItemName" HeaderText="POI Name" 
                    SortExpression="ItemName">
                </asp:BoundField>
                <asp:BoundField DataField="CategoryName" HeaderText="Category" 
                    SortExpression="CategoryName">
                </asp:BoundField>
                <asp:BoundField DataField="Suburb" HeaderText="Suburb" SortExpression="Suburb">
                <ItemStyle/>
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
        <asp:ObjectDataSource ID="POIObjectDataSource" runat="server" 
            DeleteMethod="DeletePOI" InsertMethod="InsertPOI" SelectMethod="getAllPOIList" 
            TypeName="CMS.BLL.CMSBLClass" UpdateMethod="UpdatePOI">
            <DeleteParameters>
                <asp:Parameter Name="Original_ItemID" Type="Int32" />
                <asp:Parameter Name="Original_CategoryID" Type="Int32" />
                <asp:Parameter Name="Original_SubtypeID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ItemName" Type="String" />
                <asp:Parameter Name="Details" Type="String" />
                <asp:Parameter Name="Cost" Type="Decimal" />
                <asp:Parameter Name="Rating" Type="Int32" />
                <asp:Parameter Name="Phone" Type="Int32" />
                <asp:Parameter Name="Website" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="OpeningHours" Type="String" />
                <asp:Parameter Name="StreetNo" Type="String" />
                <asp:Parameter Name="StreetName" Type="String" />
                <asp:Parameter Name="Latitute" Type="Int32" />
                <asp:Parameter Name="Longitute" Type="Int32" />
                <asp:Parameter Name="Postcode" Type="Int32" />
                <asp:Parameter Name="Suburb" Type="String" />
                <asp:Parameter Name="SubtypeID" Type="Int32" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="ItemName" Type="String" />
                <asp:Parameter Name="Details" Type="String" />
                <asp:Parameter Name="Cost" Type="Decimal" />
                <asp:Parameter Name="Rating" Type="Int32" />
                <asp:Parameter Name="Phone" Type="Int32" />
                <asp:Parameter Name="Website" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="OpeningHours" Type="String" />
                <asp:Parameter Name="StreetNo" Type="String" />
                <asp:Parameter Name="StreetName" Type="String" />
                <asp:Parameter Name="Latitute" Type="Int32" />
                <asp:Parameter Name="Longitute" Type="Int32" />
                <asp:Parameter Name="Postcode" Type="Int32" />
                <asp:Parameter Name="Suburb" Type="String" />
                <asp:Parameter Name="SubtypeID" Type="Int32" />
                <asp:Parameter Name="CategoryID" Type="Int32" />
                <asp:Parameter Name="Original_SubtypeID" Type="Int32" />
                <asp:Parameter Name="Original_ItemID" Type="Int32" />
                <asp:Parameter Name="Original_CategoryID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="contentLeftMenu">
        <h1> Point Of Interest </h1>
        <asp:LinkButton ID="InsertLinkButton" runat="server" 
            onclick="InsertLinkButton_Click"> Insert New POI </asp:LinkButton>
    </div>
</asp:Content>
