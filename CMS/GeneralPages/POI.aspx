<%@ Page Title="CAPP CMS - POI" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POI.aspx.cs" Inherits="CMS.GeneralPages.POI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.MultiFile.js" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[
    //Don't run suver side code and do nothing when cancel button on confirmation is clicked.
    function CancelClick() { }

    //Map bind
    function initialize() {
        var mapOptions = {
            center: new google.maps.LatLng(-35.308235, 149.124224),
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        
        var map = new google.maps.Map(document.getElementById('map'),
          mapOptions);

        var input = document.getElementById('MainContent_AddressTextBox');
        var autocomplete = new google.maps.places.Autocomplete(input);

        autocomplete.bindTo('bounds', map);

        var infowindow = new google.maps.InfoWindow();
        var marker = new google.maps.Marker({
            map: map
        });

        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            infowindow.close();
            var place = autocomplete.getPlace();
            if (place.geometry.viewport) {
                map.fitBounds(place.geometry.viewport);
            } else {
                map.setCenter(place.geometry.location);
                map.setZoom(15); 
            }

            document.getElementById('MainContent_LatitudeHiddenField').setAttribute('value', place.geometry.location.lat());
            document.getElementById('MainContent_LongitudeHiddenField').setAttribute('value', place.geometry.location.lng());

            var image = new google.maps.MarkerImage(
              place.icon,
              new google.maps.Size(71, 71),
              new google.maps.Point(0, 0),
              new google.maps.Point(17, 34),
              new google.maps.Size(35, 35));
            marker.setIcon(image);
            marker.setPosition(place.geometry.location);

            var address = '';
            if (place.address_components) {
                address = [
              (place.address_components[0] && place.address_components[0].short_name || ''),
              (place.address_components[1] && place.address_components[1].short_name || ''),
              (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
            }
            infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
            infowindow.open(map, marker);

        });

        // Sets a listener on a radio button to change the filter type on Places
        // Autocomplete.
        function setupClickListener(id, types) {
            var radioButton = document.getElementById(id);
            google.maps.event.addDomListener(radioButton, 'click', function () {
                autocomplete.setTypes(types);
            });
        }
        setupClickListener('changetype-all', []);
        setupClickListener('changetype-establishment', ['establishment']);
        setupClickListener('changetype-geocode', ['geocode']);
    }
    google.maps.event.addDomListener(window, 'load', initialize);

     //]]>
</script>
        <div class="wrapper1">
        <div class="contentLeftMenu">
        <h1> Point Of Interest </h1>
        <ul>
            <li><span style="margin-left: -1em;" /><asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="False"
                    onclick="InsertLinkButton_Click"> Insert New POI</asp:LinkButton></li>
        </ul>
        </div>
    <div class="wrapper2">
    <div class="contentList poi">
        <asp:GridView ID="POIGridView" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ItemID"
            DataSourceID="POIObjectDataSource" ForeColor="#333333" GridLines="None" 
            Width="100%" onrowdatabound="POIGridView_RowDataBound" 
            onselectedindexchanged="POIGridView_SelectedIndexChanged" 
            AllowPaging="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
            <Columns>
                <asp:CommandField SelectText="" ShowSelectButton="True">
                <ItemStyle Width="5px" />
                </asp:CommandField>
                <asp:BoundField DataField="ItemName" HeaderText="POI Name" 
                    SortExpression="ItemName">
                <ItemStyle/>
                </asp:BoundField>
                <asp:BoundField DataField="CategoryName" HeaderText="Category" 
                    SortExpression="CategoryName" />
                <asp:BoundField DataField="Suburb" HeaderText="Suburb" 
                    SortExpression="Suburb">
                </asp:BoundField>
                <asp:BoundField DataField="ItemID" HeaderText="ItemID" 
                    SortExpression="ItemID" InsertVisible="False" Visible="False">
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
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
        <asp:ObjectDataSource ID="POIObjectDataSource" runat="server" SelectMethod="getAllPOIList" 
            TypeName="CMS.BLL.CMSBLClass" 
            OldValuesParameterFormatString="original_{0}">
        </asp:ObjectDataSource>
    </div>
        
        <div class="contentDetail poi">
            <div class="contentDetailWrapper">
                <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                <asp:MultiView ID="POIMultiView" runat="server">

                    <!-- default detail display -->
                    <asp:View ID="DetailView" runat="server">
                        <h1> Point Of Interest Details
                            <span class = "detailButtons">
                                <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="70px" onclick="UpdateButton_Click" />
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="70px" onclick="DeleteButton_Click"/>                
                                <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                    runat="server" ConfirmText="Are you sure you want to delete the selected POI?" 
                                    Enabled="True" TargetControlID="DeleteButton">
                                </asp:ConfirmButtonExtender>                  
                            </span>
                        </h1>
                        <hr />
                        <p>
                            <asp:Label ID="DetailNameLabel" runat="server" CssClass="label" Font-Bold="True" Text="POI Name : " Width="150px"></asp:Label>
                            <asp:Label ID="NameDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailCategoryLabel" runat="server" CssClass="label" Font-Bold="True" Text="Category : " Width="150px"></asp:Label>
                            <asp:Label ID="CategoryDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailSubtypeLabel" runat="server" CssClass="label" Font-Bold="True" Text="Subtype : " Width="150px"></asp:Label>
                            <asp:Label ID="SubtypeDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailMajorRegionLabel" runat="server" CssClass="label" Font-Bold="True" Text="Major Region : " Width="150px"></asp:Label>
                            <asp:Label ID="MajorRegionDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailPhoneLabel" runat="server" CssClass="label" Font-Bold="True" Text="Phone : " Width="150px"></asp:Label>
                            <asp:Label ID="PhoneDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailEmailLabel" runat="server" CssClass="label" Font-Bold="True" Text="Email : " Width="150px"></asp:Label>
                            <asp:Label ID="EmailDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailWebsiteLabel" runat="server" CssClass="label" Font-Bold="True" Text="Website : " Width="150px"></asp:Label>
                            <asp:Label ID="WebsiteDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailOpeningHoursLabel" runat="server" CssClass="label" Font-Bold="True" Text="Opening Hours : " Width="150px"></asp:Label>
                            <asp:Label ID="OpeningHoursDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailCostLabel" runat="server" CssClass="label" Font-Bold="True" Text="Cost : " Width="150px"></asp:Label>
                            <asp:Rating ID="RatingData" runat="server" MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                    CurrentRating="0" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" CssClass="Rating" ReadOnly="True"></asp:Rating>
                            <asp:Rating ID="FreeRatingData" runat="server" MaxRating="1" StarCssClass="FreeRatingStar" WaitingStarCssClass="FreeSavedRatingStar"
                                    CurrentRating="0" FilledStarCssClass="FreeFilledRatingStar" EmptyStarCssClass="FreeEmptyRatingStar" CssClass="FreeRating" ReadOnly="True"></asp:Rating><br/>
                        </p>
                        <p>
                            <asp:Label ID="DetailDescriptionLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Detailed Description : " Width="150px"></asp:Label>
                            <asp:Label ID="DescriptionDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailPostcodeLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Postcode : " Width="150px"></asp:Label>
                            <asp:Label ID="PostcodeDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailAddressLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Address : " Width="150px"></asp:Label>
                            <asp:Label ID="AddressDataLabel" runat="server" Width="460px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailAudioLabel" runat="server" CssClass="label" Font-Bold="True" Text="Audio : " Width="150px"></asp:Label>
                            <div ID="detailAudio" class="DetailAudio" runat="server"></div>
                            <asp:HiddenField ID="AudioURLHiddenField" runat="server" />
                        </p>
                        <div class="clear"></div>
                        <p>
                            <asp:Label ID="DetailImageLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Images : " Width="150px"></asp:Label>
                            <div ID="poiImages" runat="server" width="460px"></div>        
                        </p>
                        <p>
                            <asp:Label ID="DetailVideoLabel" runat="server" CssClass="label" 
                                Enabled="False" Font-Bold="True" Text="Videos : " Width="150px"></asp:Label>
                            <div ID="poiVideo" runat="server" width="460px"></div>
                        </p>
                        <asp:HiddenField ID="CategoryIDHiddenField" runat="server" />
                    </asp:View>

                    <!-- insert and update new display (Visible when insert link button is clicked
                                    or update button is clicked) -->
                    <asp:View ID="InsertView" runat="server">
                            <h1> <asp:Label ID="EditTitleLabel" runat="server"></asp:Label></h1> 
                            <hr />
                        <!-- Name -->
                        <asp:Label ID="NameLabel" CssClass="label" runat="server" Text="POI Name : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="NameTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                        <p class="validationError">             
                            <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                            ErrorMessage="POI name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                        </p>
                        <!-- Category -->
                        <asp:Label ID="InsertCategoryLabel" CssClass="label" runat="server" Text="Category : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:DropDownList ID="CategoryDropDownList" runat="server" Width="405px" DataSourceID="CategoryObjectDataSource" 
                        DataTextField="CategoryName" DataValueField="CategoryID"/>
                        <asp:ObjectDataSource ID="CategoryObjectDataSource" runat="server" 
                            SelectMethod="getAllCategory" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource><br /><br />
                        <!-- Subtype -->
                        <asp:Label ID="SubTypeLabel" CssClass="label" runat="server" Text="Subtype : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:DropDownList ID="SubtypeDropDownList" runat="server" 
                            DataSourceID="SubtypeObjectDataSource" DataTextField="SubtypeName" 
                                DataValueField="SubtypeID" Width="405px" 
                                ondatabound="SubtypeDropDownList_DataBound">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="SubtypeObjectDataSource" runat="server" 
                            SelectMethod="getAllSubtype" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource><br /><br />
                        <!-- MajorRegion -->
                        <asp:Label ID="MajorRegionLabel" CssClass="label" runat="server" Text="Major Region : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:DropDownList ID="MajorRegionDropDownList" runat="server" 
                            DataSourceID="MajorRegionObjectDataSource" DataTextField="MajorRegionName" 
                                DataValueField="MajorRegionID" Width="405px" 
                                ondatabound="MajorRegionDropDownList_DataBound">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="MajorRegionObjectDataSource" runat="server" 
                            SelectMethod="getAllMajorRegion" TypeName="CMS.BLL.CMSBLClass" 
                                OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource><br /><br />
                        <!-- Phone -->
                        <asp:Label ID="PhoneLabel" CssClass="label" runat="server" Text="Phone : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="PhoneTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox>
                        <p class="validationError">
                            <asp:CustomValidator ID="PhoneTextBox_CustomValidator" runat="server" ErrorMessage="Please enter numbers only. (Max length: 10)"
                                onservervalidate="numberInputValidate" ControlToValidate="PhoneTextBox"></asp:CustomValidator>
                        </p>
                        <!-- Email -->
                        <asp:Label ID="EmailLabel" CssClass="label" runat="server" Text="Email : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="EmailTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                        <br /><br />
                        <!-- Website -->
                        <asp:Label ID="WebsiteLabel" CssClass="label" runat="server" Text="Website : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="WebsiteTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                        <br /><br />
                        <!-- Opening Hours -->
                        <asp:Label ID="OpeningHoursLabel" CssClass="label" runat="server" Text="Opening Hours : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="OpeningHoursTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                        <br /><br />
                        <!-- Cost -->
                        <asp:Label ID="CostLabel" CssClass="label" runat="server" Text="Cost : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:Rating ID="Rating" runat="server" MaxRating="5"  StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                CurrentRating="0" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" CssClass="Rating" 
                                onchanged="Rating_Changed" AutoPostBack="True"></asp:Rating>
                        <asp:Rating ID="FreeRating" runat="server" MaxRating="1" StarCssClass="FreeRatingStar" WaitingStarCssClass="FreeSavedRatingStar"
                                CurrentRating="0" FilledStarCssClass="FreeFilledRatingStar"  EmptyStarCssClass="FreeEmptyRatingStar" CssClass="FreeRating" 
                                onchanged="FreeRating_Changed" AutoPostBack="True"></asp:Rating>        
                        <br /><br />
                        <!-- Detailed Description -->
                        <asp:Label ID="DescriptionLabel" CssClass="label" runat="server" Text="Detailed description : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="DescriptionTextBox" runat="server" Width="400px" TextMode="MultiLine" Height="100px" ></asp:TextBox> 
                        <p class="validationError">             
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="Detailed description is required." ControlToValidate="DescriptionTextBox" SetFocusOnError="True" />
                        </p>
                        <!--Postcode -->
                        <asp:Label ID="PostcodeLabel" CssClass="label" runat="server" Text="Postcode : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="PostcodeTextBox" runat="server" Width="60px" onkeydown = "return (event.keyCode!=13);" MaxLength="4"></asp:TextBox> 
                        <p class="validationError"> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ErrorMessage="Postcode is required." ControlToValidate="PostcodeTextBox" SetFocusOnError="True" />
                            <span style="margin-left: -115px;"/>
                            <asp:CustomValidator ID="PostcodeTextBox_CustomValidator" runat="server" ErrorMessage="Postcode should be 4 digit numbers."
                            onservervalidate="numberInputValidate" ControlToValidate="PostcodeTextBox"></asp:CustomValidator>
                        </p> 
                        <!-- Address -->
                        <asp:Label ID="AddressLabel" CssClass="label" runat="server" Text="Address : " Font-Bold="True" Width="150px" ></asp:Label>
                        <div class="AddressMaster">                        
                            <asp:LinkButton ID="AutoLinkButton" runat="server" CssClass="tabButton" 
                                Text="Auto Address" BackColor="LightGray" CausesValidation="false" 
                                onclick="AutoLinkButton_Click"></asp:LinkButton>
                            <asp:LinkButton ID="ManualLinkButton" runat="server" CssClass="tabButton" 
                                Text="Manual Address" BackColor="Gray" CausesValidation="false" 
                                onclick="ManualLinkButton_Click"></asp:LinkButton>
                            <div class="Address">
                                <asp:MultiView ID="AddressMultiView" runat="server">
                                    <asp:View ID="AutoView" runat="server"><br/>
                                        <asp:TextBox ID="AddressTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                                        <asp:CustomValidator ID="AutoAddressTextBox_CustomValidator" runat="server" ErrorMessage="Input valid address using autocompletion."
                                            onservervalidate="numberInputValidate" ControlToValidate="AddressTextBox" ValidateEmptyText="True" ForeColor="Red"></asp:CustomValidator>
                                        <div class="map" id="map"></div>
                                    </asp:View>
                                    <asp:View ID="ManualView" runat="server">
                                        <asp:Label ID="Label1" runat="server" Text="Street No : " Width="100px" style="text-align: right"></asp:Label>
                                        <asp:TextBox ID="ManualStNoTextBox" runat="server" Width="30px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>
                                        <asp:Label ID="Label2" runat="server" Text="Street Name : " Width="100px" style="text-align: right"></asp:Label>
                                        <asp:TextBox ID="ManualStNameTextBox" runat="server" Width="200px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>
                                        <asp:Label ID="Label3" runat="server" Text="Suburb : " Width="100px" style="text-align: right"></asp:Label>
                                        <asp:TextBox ID="ManualSuburbTextBox" runat="server" Width="200px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>
                                        <asp:Label ID="Label4" runat="server" Text="Latitude : " Width="100px" style="text-align: right"></asp:Label>
                                        <asp:TextBox ID="ManualLatTextBox" runat="server" Width="200px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>
                                        <asp:Label ID="Label5" runat="server" Text="Longitude : " Width="100px" style="text-align: right"></asp:Label>
                                        <asp:TextBox ID="ManualLogTextBox" runat="server" Width="200px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>
                                        <asp:CustomValidator ID="ManualAddressTextBox_CustomValidator" runat="server" ErrorMessage="All address fields are required for manual address."
                                            onservervalidate="numberInputValidate" ControlToValidate="ManualStNoTextBox"  ValidateEmptyText="True" ForeColor="Red"></asp:CustomValidator>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>                                      
                        <div class="clear"></div>
                        <!-- Audio -->
                        <asp:Label ID="AudioLabel" CssClass="label" runat="server" Text="Audio : " Font-Bold="True" Width="150px" ></asp:Label>
                        
                        <div class = "AudioUpload">
                            <asp:Label ID="Label7" runat="server" Text="mp3 files only." 
                                CssClass="imgLabel"></asp:Label><br />
                            <asp:FileUpload ID="AudioFileUpload" runat="server" /><br/>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="File type must be mp3." ControlToValidate="AudioFileUpload"
                                onservervalidate="audioFileTypeCheck" ForeColor="Red"></asp:CustomValidator><br/>
                            <div ID="EditCurrentAudio" class="editAudio" runat="server"></div>
                            <asp:LinkButton ID="AudioRemoveLinkButton" runat="server" CssClass="button small lightGray audio" Visible="false" 
                                onclick="AudioRemoveLinkButton_Click" CausesValidation="false" >Remove Current Audio</asp:LinkButton>
                            <asp:HiddenField ID="IsAudioRemovedHiddenField" runat="server" />
                        </div>                    
                        <div class="clear"></div>
                        <!-- YouTube Video -->
                        <asp:Label ID="VideoLabel" CssClass="label" runat="server" Text="YouTube Video : " Font-Bold="True" Width="150px" ></asp:Label>
                        <asp:TextBox ID="VideoTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                        <br /><br />

                        <!-- Images -->
                        <asp:Label ID="ImageLabel" CssClass="label" runat="server" Text="Images : " Font-Bold="True" Width="150px" ></asp:Label>
                        <div>
                            <asp:Label ID="Label6" runat="server" Text="Max Size: 50kb, Allowed Type: JPEG, PNG, GIF" 
                                    CssClass="imgLabel"></asp:Label><br />
                            <asp:FileUpload ID="FileUpload" runat="server" maxlength="5" class="multi" accept="jpeg|jpg|gif|png"/>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload All"  CssClass="poiUploadBtn"  onclick="btnUpload_Click" CausesValidation="False" />
                        </div>
                        <div class="imgUploadResult">
                            <asp:Label ID="StatusLabel" runat="server" Text="" ForeColor="Red" ></asp:Label>
                            <div runat="server" id="poiImagesAddUpdate"></div>
                        </div> 
                        <asp:HiddenField ID="ImageDeleteFileName" runat="server" />
                        <asp:HiddenField ID="CurrentImagesFileName" runat="server" />
                        <asp:HiddenField ID="ImageUploadDelete" runat="server" />
                       <asp:HiddenField ID="ImageUploadFileName" runat="server" />
                       <script type="text/javascript">


                           $(document).ready(function () {
                               $('.delete-image').live('click', function () {
                                   var id = $(this).attr('rel');
                                   $('#MainContent_FileUpload').attr('maxlength', parseInt($('#MainContent_FileUpload').attr('maxlength')) + 1);
                                   $('#MainContent_ImageDeleteFileName').val($('#MainContent_ImageDeleteFileName').val() + id + ';');
                                   var filename = $('#MainContent_CurrentImagesFileName').val();
                                   
                                   $('#MainContent_CurrentImagesFileName').val(filename.replace(id + ';', ""));
                                   var div = document.getElementById("MainContent_poiImagesAddUpdate");
                                   var olddiv = document.getElementById(id);
                                   div.removeChild(olddiv);
                                   $('#MainContent_ImageUploadDelete').val("1");
                               });

                               $('.upload-images').live('click', function () {
                                   var id = $(this).attr('rel');
                                   $('#MainContent_FileUpload').attr('maxlength', parseInt($('#MainContent_FileUpload').attr('maxlength')) + 1);
                                   var filename = $('#MainContent_ImageUploadFileName').val();
                                   $('#MainContent_ImageUploadFileName').val(filename.replace(id + ';', ""));
                                   var div = document.getElementById("MainContent_poiImagesAddUpdate");
                                   var olddiv = document.getElementById(id);
                                   div.removeChild(olddiv);
                                   $('#MainContent_ImageUploadDelete').val("1");
                               });
                           });


                        </script>
                        <!-- Buttons -->
                        <div class="detailButtons bottom">
                            <asp:MultiView ID="ButtonMultiView" runat="server">
                                <asp:View ID="UpdateButtonView" runat="server">                            
                                    <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" 
                                        onclick="SubmitButton_Click"  />
                                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick"
                                        runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                                        TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                                    </asp:ConfirmButtonExtender>
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" 
                                        onclick="CancelButton_Click" CausesValidation="False" />
                                </asp:View>
                                <asp:View ID="InsertButtonView" runat="server">
                                    <asp:Button ID="InsertButton" runat="server" Text="Confirm" Width="100px"  onclick="InsertButton_Click" />
                                    <asp:ConfirmButtonExtender ID="InsertButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                        runat="server" ConfirmText="Do you want to submit the new point of interest?" Enabled="True" 
                                        TargetControlID="InsertButton" ConfirmOnFormSubmit="True">
                                    </asp:ConfirmButtonExtender>
                                    <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" 
                                        CausesValidation="False" onclick="InsertCancelButton_Click" />                    
                                </asp:View>
                            </asp:MultiView>
                        </div>
                        <asp:HiddenField ID="LatitudeHiddenField" runat="server"/>
                        <asp:HiddenField ID="LongitudeHiddenField" runat="server" />
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>     
        
</div>     
    </span>     
</asp:Content>
