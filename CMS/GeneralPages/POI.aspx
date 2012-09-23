<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="POI.aspx.cs" Inherits="CMS.CMSPages.POI" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>
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
                map.setZoom(15);  // Why 17? Because it looks good.
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
            <li><span style="margin-left: -1em;"/><asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="False"
                onclick="InsertLinkButton_Click"> Insert New POI </asp:LinkButton></li>
        </ul>
    </div>
    <div class="wrapper2">
    <div class="contentList poi">
        <asp:GridView ID="POIGridView" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ItemID"
            DataSourceID="POIObjectDataSource" ForeColor="Black" GridLines="Vertical" 
            Width="122%" onrowdatabound="POIGridView_RowDataBound" 
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
        <div class="contentDetail poi">
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
                        <asp:Label ID="NameDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailCategoryLabel" runat="server" CssClass="label" Font-Bold="True" Text="Category : " Width="150px"></asp:Label>
                        <asp:Label ID="CategoryDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailSubtypeLabel" runat="server" CssClass="label" Font-Bold="True" Text="Subtype : " Width="150px"></asp:Label>
                        <asp:Label ID="SubtypeDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailPhoneLabel" runat="server" CssClass="label" Font-Bold="True" Text="Phone : " Width="150px"></asp:Label>
                        <asp:Label ID="PhoneDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailEmailLabel" runat="server" CssClass="label" Font-Bold="True" Text="Email : " Width="150px"></asp:Label>
                        <asp:Label ID="EmailDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailWebsiteLabel" runat="server" CssClass="label" Font-Bold="True" Text="Website : " Width="150px"></asp:Label>
                        <asp:Label ID="WebsiteDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailOpeningHoursLabel" runat="server" CssClass="label" Font-Bold="True" Text="Opening Hours : " Width="150px"></asp:Label>
                        <asp:Label ID="OpeningHoursDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailCostLabel" runat="server" CssClass="label" Font-Bold="True" Text="Cost : " Width="150px"></asp:Label>
                        <asp:Label ID="CostDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailRatingLabel" runat="server" CssClass="label" Font-Bold="True" Text="Rating : " Width="150px"></asp:Label>
                        <asp:Rating ID="RatingData" runat="server" MaxRating="5"  StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                CurrentRating="0" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" CssClass="Rating" ReadOnly="True"></asp:Rating><br/>
                    </p>
                    <p>
                        <asp:Label ID="DetailDescriptionLabel" runat="server" CssClass="label" Font-Bold="True" Text="Detailed Description : " Width="150px"></asp:Label>
                        <asp:Label ID="DescriptionDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailPostcodeLabel" runat="server" CssClass="label" Font-Bold="True" Text="Postcode : " Width="150px"></asp:Label>
                        <asp:Label ID="PostcodeDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailAddressLabel" runat="server" CssClass="label" Font-Bold="True" Text="Address : " Width="150px"></asp:Label>
                        <asp:Label ID="AddressDataLabel" runat="server"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailImageLabel" runat="server" CssClass="label" Font-Bold="True" Text="Images : " Width="150px"></asp:Label>
                        <asp:Label ID="ImageDataLabel" runat="server" Text="Not implemented yet"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailVideoLabel" runat="server" CssClass="label"  Enabled="False" Font-Bold="True" Text="Videos : " Width="150px"></asp:Label>
                        <asp:Label ID="VideoDataLabel" runat="server" Text="Not implemented yet"></asp:Label>
                    </p>
                    <asp:HiddenField ID="CategoryIDHiddenField" runat="server" />
                    <asp:HiddenField ID="SubtypeIDHiddenField" runat="server" />
                </asp:View>

                <!-- insert and update new display (Visible when insert link button is clicked
                                or update button is clicked) -->
                <asp:View ID="InsertView" runat="server">
                        <h1> Insert New Point Of Interest </h1> 
                        <asp:Label ID="ResultLabel" runat="server"></asp:Label>
                        <hr />
                    <!-- Name -->
                    <asp:Label ID="NameLabel" CssClass="label" runat="server" Text="POI Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="NameTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="NameTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender>
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
                        DataSourceID="SubtypeObjectDataSource" DataTextField="SubtypeName" DataValueField="SubtypeID" Width="405px">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="SubtypeObjectDataSource" runat="server" 
                        SelectMethod="getAllSubtype" TypeName="CMS.BLL.CMSBLClass"></asp:ObjectDataSource><br /><br />
                    <!-- Phone -->
                    <asp:Label ID="PhoneLabel" CssClass="label" runat="server" Text="Phone : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="PhoneTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="PhoneTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="PhoneTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender><br /><br />
                    <p class="validationError">             
                        <asp:CustomValidator ID="PhoneTextBox_CustomValidator" runat="server" ErrorMessage="Phone should include only numbers."
                        onservervalidate="numberInputValidate" ControlToValidate="PhoneTextBox"></asp:CustomValidator>
                    </p>
                    <!-- Email -->
                    <asp:Label ID="EmailLabel" CssClass="label" runat="server" Text="Email : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="EmailTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="EmailTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="EmailTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender><br /><br />
                    <!-- Website -->
                    <asp:Label ID="WebsiteLabel" CssClass="label" runat="server" Text="Website : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="WebsiteTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="WebsiteTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="WebsiteTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender><br /><br />
                    <!-- Opening Hours -->
                    <asp:Label ID="OpeningHoursLabel" CssClass="label" runat="server" Text="Opening Hours : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="OpeningHoursTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="OpeningHoursTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="OpeningHoursTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender><br /><br />
                    <!-- Cost -->
                    <asp:Label ID="CostLabel" CssClass="label" runat="server" Text="Cost : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="CostTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="CostTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="CostTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender>
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Cost is required." ControlToValidate="CostTextBox" SetFocusOnError="True" />
                        <span style="margin-left: -105px;"/>
                        <asp:CustomValidator ID="CostTextBox_CustomValidator" runat="server" ErrorMessage="Cost should include only numbers."
                        onservervalidate="numberInputValidate" ControlToValidate="CostTextBox"></asp:CustomValidator>
                    </p>
                    <!-- Rating -->
                    <asp:Label ID="RatingLabel" CssClass="label" runat="server" Text="Rating : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:Rating ID="Rating" runat="server" MaxRating="5"  StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                            CurrentRating="0" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" CssClass="Rating"></asp:Rating><br /><br />        
                    <!-- Detailed Description -->
                    <asp:Label ID="DescriptionLabel" CssClass="label" runat="server" Text="Detailed description : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Width="400px" TextMode="MultiLine" Height="100px" ></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="DescriptionTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="DescriptionTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender>
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Detailed description is required." ControlToValidate="DescriptionTextBox" SetFocusOnError="True" />
                    </p>
                    <!--Postcode -->
                    <asp:Label ID="PostcodeLabel" CssClass="label" runat="server" Text="Postcode : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="PostcodeTextBox" runat="server" Width="60px" onkeydown = "return (event.keyCode!=13);" MaxLength="4"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="PostCodeTextBox_TextBoxWatermarkExtender" runat="server" Enabled="True" 
                        TargetControlID="PostcodeTextBox" WatermarkText="postcode" WatermarkCssClass="textBoxWatermark">
                    </asp:TextBoxWatermarkExtender>
                    <p class="validationError"> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="Postcode is required." ControlToValidate="PostcodeTextBox" SetFocusOnError="True" />
                        <span style="margin-left: -115px;"/>
                        <asp:CustomValidator ID="PostcodeTextBox_CustomValidator" runat="server" ErrorMessage="Postcode should be 4 digit numbers."
                        onservervalidate="numberInputValidate" ControlToValidate="PostcodeTextBox"></asp:CustomValidator>
                    </p> 
                    <!-- Address -->
                    <asp:Label ID="AddressLabel" CssClass="label" runat="server" Text="Address : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="AddressTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="StNumTextBox_TextBoxWatermarkExtender" runat="server" Enabled="True" 
                        TargetControlID="AddressTextBox" WatermarkText="address" WatermarkCssClass="textBoxWatermark">
                    </asp:TextBoxWatermarkExtender>
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="Address is required." ControlToValidate="AddressTextBox" SetFocusOnError="True" />
                    </p>                    
                    <div class="map" id="map">
                    </div><br />
                    <!-- Images -->
                    <asp:Label ID="ImageLabel" CssClass="label" runat="server" Text="Images : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="ImageTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="ImageTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="ImageTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender><br /><br />
                    <!-- YouTube Video -->
                    <asp:Label ID="VideoLabel" CssClass="label" runat="server" Text="YouTube Video : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="VideoTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <asp:TextBoxWatermarkExtender ID="VideoTextBox_TextBoxWatermarkExtender" WatermarkCssClass="textBoxWatermark noText"
                        runat="server" Enabled="True" TargetControlID="VideoTextBox" WatermarkText="enter">
                    </asp:TextBoxWatermarkExtender>

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
          
    
          
</asp:Content>
