﻿<%@ Page Title="CAPP CMS - Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="CMS.GeneralPages.ChangePassword" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="wrapper1">
        <div class="contentLeftMenu adminLeftMenu">
            <h1> Settings </h1>        
        </div>
        <div class="wrapper2Settings">
            <div class="contentDetail  adminBody">
                <h2>Change Password</h2>
                <p> Use the form below to change your password.</p>
                <p>New passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.</p>
                <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" 
                SuccessPageUrl="ChangePasswordSuccess.aspx">
                    <ChangePasswordTemplate>     
                        <div class="accountInfo">
                            <fieldset class="changePassword">
                                <legend>Account Information</legend>
                                <!-- Current password input and validation for it -->
                                <p>
                                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
                                    <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                                         CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                                         ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <!-- New password input and validation for it -->
                                <p>
                                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                                    <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                                         CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                                         ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                </p>
                                <!-- Confirm new password input and validation for it -->
                                <p>
                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                                         CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                                         ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                         CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                                         ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                                </p>
                            </fieldset>
                            <!-- Submit and Cancel buttons -->
                            <p class="submitButton">
                                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"/>
                                <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password" 
                                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
                            </p>
                            <!-- Error notification -->
                            <span class="failureNotification">
                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            </span>
                            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                            ValidationGroup="ChangeUserPasswordValidationGroup"/>
                        </div>
                    </ChangePasswordTemplate>
                </asp:ChangePassword>
            </div>
        </div>  
    </div>    
</asp:Content>