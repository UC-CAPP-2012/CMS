<%@ Page Title="CAPP CMS - Log In" Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="CMS.Account.Login" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <!--========== Login Layout ==========-->
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>                 
            <div class="accountInfoLogin">
                <div class="logInBox">
                    <fieldset class="login">
                        <p class="login-heading">Please enter User Name and Password to login to CMS</p>
                        <p>
                            <!--========== ID and Password input and validation controls ==========-->
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" placeholder="User Name" ToolTip="User Naem"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="UserName" 
                                 CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                 ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password" placeholder="Password" ToolTip="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Password" 
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator></br>
                            <asp:CheckBox ID="RememberMe" runat="server"/>
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                        </p>                        
                        <p class="submitButton">
                            <asp:Button ID="LoginButton" CssClass="logInBtn" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup"/>
                        </p>
                        <div class="failureNotification">
                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                        </div>
                    </fieldset>
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <!--========== Display application logo ==========-->
    <div class="ccapp-logo">
        <img src="Resources/ccapp-logo.png" alt="CCAPP Content Management System"/>
    </div>
</asp:Content>
