<%@ Page Title="CAPP CMS - News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="CMS.GeneralPages.News" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            <h1> News </h1>
            <ul>
                <li><span style="margin-left: -1em;"></span>
                    <asp:LinkButton ID="InsertLinkButton" runat="server" onclick="InsertLinkButton_Click"> 
                    Add a News Article </asp:LinkButton></li>
            </ul>        
        </div>
        <div class="wrapper2">
            <!-- ========== News List (Grid View) in the middle ========== -->
            <div class="contentList news">
                <asp:GridView ID="GridViewNews" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                DataKeyNames="NewsID" DataSourceID="ObjectDataSourceNews" ForeColor="#333333" 
                GridLines="None"  Width="100%" onrowdatabound="GridViewNews_RowDataBound" onselectedindexchanged="GridViewNews_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                        <asp:BoundField DataField="NewsHeading" HeaderText="Title" 
                            SortExpression="NewsHeading" />
                        <asp:BoundField DataField="NewsDateTime" HeaderText="Published Date" 
                            SortExpression="NewsDateTime" />
                        <asp:BoundField DataField="NewsPublisher" HeaderText="Publisher" 
                            SortExpression="NewsPublisher" />
                        <asp:BoundField DataField="NewsAuthor" HeaderText="Author" 
                            SortExpression="NewsAuthor" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        <span/>There is no news.</span>
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
                <asp:ObjectDataSource ID="ObjectDataSourceNews" runat="server"  DeleteMethod="DeleteNews" InsertMethod="InsertNews" SelectMethod="getAllNews" 
                TypeName="CMS.BLL.CMSBLClass" UpdateMethod="UpdateNews" OldValuesParameterFormatString="original_{0}">
                    <DeleteParameters>
                        <asp:Parameter Name="original_NewsID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="newsHeading" Type="String" />
                        <asp:Parameter Name="newsDateTime" Type="DateTime" />
                        <asp:Parameter Name="newsBody" Type="String" />
                        <asp:Parameter Name="newsMediaURL" Type="String" />
                        <asp:Parameter Name="newsPublisher" Type="String" />
                        <asp:Parameter Name="newsAuthor" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="newsHeading" Type="String" />
                        <asp:Parameter Name="newsDateTime" Type="DateTime" />
                        <asp:Parameter Name="newsBody" Type="String" />
                        <asp:Parameter Name="newsMediaURL" Type="String" />
                        <asp:Parameter Name="newsPublisher" Type="String" />
                        <asp:Parameter Name="newsAuthor" Type="String" />
                        <asp:Parameter Name="original_NewsID" Type="Int32" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
            </div>
            <!-- ========== News Detail (Multi View) on the right ========== -->
            <div class="contentDetail news">
                <div class="contentDetailWrapper news">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                        <asp:MultiView ID="NewsMultiView" runat="server">
                            <!-- ========== data detail view  ========== -->
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
                                    <hr/>
                                </div>  
                                <div runat="server" id="newsLabel" class="news-item-labels"> </div>
                                <div class="news-content">
                                    <div runat="server" id="newsImage" class="news-image"></div>
                                    <div runat="server" id="newsBody"  class="news-body"></div>
                                </div>
                            </asp:View>
                            <!-- ========== update form view ========== -->
                            <asp:View ID="UpdateView" runat="server">
                                <h2> Update News Details </h2> 
                                <div class="newsUpdateForm"> 
                                    <asp:Label ID="TitleUpdateLabel" CssClass="label" runat="server" Text="Title: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <asp:TextBox ID="NameTextBox" runat="server" Width="250px"></asp:TextBox> 
                                    <p class="validationError">             
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" 
                                        ErrorMessage="News Title is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                                    </p>
                                    <asp:Label ID="PublisherUpdateLabel" CssClass="label" runat="server" Text="Publisher: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <asp:TextBox ID="PublisherTextBox" runat="server" Width="250px"></asp:TextBox> 
                                    <p class="validationError">             
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPublisher" runat="server" 
                                        ErrorMessage="News Publisher is required." ControlToValidate="PublisherTextBox" SetFocusOnError="True" />
                                    </p>
                                    <asp:Label ID="AuthorUpdateLabel" CssClass="label" runat="server" Text="Author: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <asp:TextBox ID="AuthorTextBox" runat="server" Width="250px"></asp:TextBox> 
                                    <p class="validationError">             
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ErrorMessage="News Author is required." ControlToValidate="AuthorTextBox" SetFocusOnError="True" />
                                    </p>
                                    <asp:Label ID="ImageUpdateLabel" CssClass="label" runat="server" Text="Image: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <div class="news-image-upload">
                                        <asp:Label ID="Label6" CssClass="imgLabel" runat="server" Text="Max File Size: 50kb." Font-Bold="True" Width="150px" ></asp:Label><br/>
                                        <asp:FileUpload ID="NewsImageUpload" runat="server" />
                                        <asp:Button ID="btnUpload" runat="server" onclick="btnUpload_Click" Text="Upload" />
                                        <asp:Button ID="btnRemove" runat="server"  Text="Remove" onclick="btnRemove_Click" Visible="False" />
                                        <asp:Label ID="StatusLabel" CssClass="label statusMsg imageUpload" runat="server" Text="" Font-Bold="True" Width="150px" ></asp:Label>
                                        <asp:HiddenField ID="NewsImageUploadFileName" runat="server" />
                                    </div>
                                    <div class="news-image news-image-update"><asp:Image ID="NewsImageUpdate" runat="server" /></div>
                                    <div class="clear"></div>
                                    <div  class="news-body-update">
                                        <asp:Label ID="BodyUpdateLabel" CssClass="label" runat="server" Text="Body: " Font-Bold="True" Width="150px" ></asp:Label>
                                        <asp:TextBox ID="NewsBodyTextBox" runat="server" TextMode="MultiLine" CssClass="newBodyEditor"></asp:TextBox>
                                        <asp:Label ID="UpdateNewsNoContentStatus" CssClass="label statusMsg" runat="server" Text="" Width="150px" ></asp:Label>
                                    </div>                        
                                </div>
                                <div class="detailButtons bottom">
                                    <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" onclick="SubmitButton_Click"  />
                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick"
                                        runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                                        TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                                    </asp:ConfirmButtonExtender>
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" onclick="CancelButton_Click" CausesValidation="False" />
                                </div>
                            </asp:View>
                            <!-- ========== insert form view ========== -->
                            <asp:View ID="InsertView" runat="server">
                                <h2> Insert New News </h2>
                                <div class="newsUpdateForm"> 
                                    <asp:Label ID="Label1" CssClass="label" runat="server" Text="Title: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <asp:TextBox ID="InsertNewsTitle" runat="server" Width="250px"></asp:TextBox> 
                                    <p class="validationError">             
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ErrorMessage="News Title is required." ControlToValidate="InsertNewsTitle" SetFocusOnError="True" />
                                    </p>
                                    <asp:Label ID="Label2" CssClass="label" runat="server" Text="Publisher: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <asp:TextBox ID="InsertNewsPublisher" runat="server" Width="250px"></asp:TextBox> 
                                    <p class="validationError">             
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ErrorMessage="News Publisher is required." ControlToValidate="InsertNewsPublisher" SetFocusOnError="True" />
                                    </p>
                                    <asp:Label ID="Label7" CssClass="label" runat="server" Text="Author: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <asp:TextBox ID="InsertNewsAuthor" runat="server" Width="250px"></asp:TextBox> 
                                    <p class="validationError">             
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ErrorMessage="News Author is required." ControlToValidate="InsertNewsAuthor" SetFocusOnError="True" />
                                    </p>
                                    <asp:Label ID="Label3" CssClass="label" runat="server" Text="Image: " Font-Bold="True" Width="150px" ></asp:Label>
                                    <div class="news-image-upload">
                                        <asp:Label ID="Label4" CssClass="imgLabel" runat="server" Text="Max File Size: 50kb." Font-Bold="True" Width="150px" ></asp:Label><br/>
                                        <asp:FileUpload ID="InsertNewsImageUpload" runat="server" />
                                        <asp:Button ID="btnInsertNewsUpload" runat="server" Text="Upload" onclick="btnInsertNewsUpload_Click" />
                                        <asp:Button ID="btnInsertNewsRemove" runat="server" Text="Remove" onclick="btnInsertNewsRemove_Click" Visible="False"  />
                                        <asp:Label ID="InsertStatusLabel" CssClass="label statusMsg imageUpload" runat="server" Text="" Font-Bold="True" Width="150px" ></asp:Label>
                                        <asp:HiddenField ID="InsertNewsImageName" runat="server" />
                                    </div>
                                    <div class="news-image news-image-update"><asp:Image ID="InsertNewsImage" runat="server" /></div>
                                    <div class="clear"></div>
                                    <div  class="news-body-update">
                                        <asp:Label ID="Label5" CssClass="label" runat="server" Text="Body: " Font-Bold="True" Width="150px" ></asp:Label>                            
                                        <asp:TextBox ID="InsertNewsBodyTextBox" runat="server" TextMode="MultiLine" CssClass="newBodyEditor"></asp:TextBox>
                                        <asp:Label ID="InsertNewsNoContentStatus" CssClass="label statusMsg" runat="server" Text="" Width="150px" ></asp:Label>
                                    </div>                        
                                </div>
                                <div class="detailButtons bottom">
                                    <asp:Button ID="SubmitNewButton" runat="server" Text="Insert New" Width="100px" onclick="SubmitNewButton_Click" />
                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" OnClientCancel="CancelClick"
                                     ConfirmText="Do you want to add this news article?" Enabled="True" TargetControlID="SubmitNewButton" ConfirmOnFormSubmit="True">
                                    </asp:ConfirmButtonExtender>
                                    <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px"
                                     CausesValidation="False" onclick="InsertCancelButton_Click" />
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
        </div>
    </div>    
</asp:Content>
