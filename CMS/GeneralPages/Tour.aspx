<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tour.aspx.cs" Inherits="CMS.GeneralPages.Tour" %>
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
    <h1> Tour </h1>
    <ul>
        <li><span style="margin-left: -1em;" /><asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="False"
                onclick="InsertLinkButton_Click"> Insert New Tour </asp:LinkButton></li>
    </ul>
    </div>
    <div class="wrapper2">
        <div class="contentList tour">
            <asp:GridView ID="TourGridView" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TourID"
                DataSourceID="TourObjectDataSource" ForeColor="#333333" GridLines="None" 
                Width="100%" onrowdatabound="TourGridView_RowDataBound" 
                onselectedindexchanged="TourGridView_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                <Columns>
                    <asp:CommandField SelectText="" ShowSelectButton="True">
                    <ItemStyle Width="5px" />
                    </asp:CommandField>
                    <asp:BoundField DataField="TourID" HeaderText="TourID" InsertVisible="False" 
                        ReadOnly="True" SortExpression="TourID" Visible="False" />
                    <asp:BoundField DataField="TourName" HeaderText="TourName" 
                        SortExpression="TourName" />
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
            <asp:ObjectDataSource ID="TourObjectDataSource" runat="server" SelectMethod="getAllTour" 
                TypeName="CMS.BLL.CMSBLClass" 
                OldValuesParameterFormatString="original_{0}">
            </asp:ObjectDataSource>
        </div>

        <div class="contentDetail tour">
            <div class="contentDetailWrapper tour">
                <asp:ScriptManager ID="ScriptManager1" runat="server"/>
                <asp:MultiView ID="TourMultiView" runat="server">

                <!-- default detail display -->
                <asp:View ID="DetailView" runat="server">
                    <h1> Tour Details
                        <span class = "detailButtons">
                            <asp:Button ID="UpdateButton" runat="server" Text="Update" Width="70px" onclick="UpdateButton_Click" />
                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" Width="70px" onclick="DeleteButton_Click"/>                
                            <asp:ConfirmButtonExtender ID="DeleteButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                runat="server" ConfirmText="Are you sure you want to delete the selected Tour?" 
                                Enabled="True" TargetControlID="DeleteButton">
                            </asp:ConfirmButtonExtender>                  
                        </span>
                    </h1>
                    <hr />
                    <p>
                        <asp:Label ID="DetailNameLabel" runat="server" CssClass="label" Font-Bold="True" Text="Tour Name : " Width="150px"></asp:Label>
                        <asp:Label ID="NameDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailPhoneLabel" runat="server" CssClass="label" Font-Bold="True" Text="Phone : " Width="150px"></asp:Label>
                        <asp:Label ID="PhoneDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailEmailLabel" runat="server" CssClass="label" Font-Bold="True" Text="Email : " Width="150px"></asp:Label>
                        <asp:Label ID="EmailDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailWebsiteLabel" runat="server" CssClass="label" Font-Bold="True" Text="Website : " Width="150px"></asp:Label>
                        <asp:Label ID="WebsiteDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailCostLabel" runat="server" CssClass="label" Font-Bold="True" Text="Cost : " Width="150px"></asp:Label>
                        <asp:Label ID="CostDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailDescriptionLabel" runat="server" CssClass="label" Font-Bold="True" Text="Detailed Description : " Width="150px"></asp:Label>
                        <asp:Label ID="DescriptionDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>                            
                    <p>
                        <asp:Label ID="DetailImageLabel" runat="server" CssClass="label" Font-Bold="True" Text="Images : " Width="150px"></asp:Label>
                        <div runat="server" id="poiImages" width="460px"></div>
                    </p>
                    <p>
                        <asp:Label ID="DetailVideoLabel" runat="server" CssClass="label"  Enabled="False" Font-Bold="True" Text="Videos : " Width="150px"></asp:Label>
                        <div runat="server" id="poiVideo" width="460px"></div>
                    </p>
                        <asp:Label ID="LocationLabel" runat="server" CssClass="label" Enabled="False" 
                            Font-Bold="True" Text="Tour Locations : " Width="150px"></asp:Label>
                        <asp:GridView ID="LocationGridView" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            DataKeyNames="TourLocationID" DataSourceID="LocationObjectDataSource" 
                            ForeColor="Black" GridLines="None"  Width="500px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="TourSeqNum" HeaderText="Sequence" 
                                    SortExpression="TourSeqNum" >
                                <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LocationName" HeaderText="Location Name" 
                                    SortExpression="LocationName" />
                                <asp:BoundField DataField="Suburb" HeaderText="Suburb" 
                                    SortExpression="Suburb" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="LocationObjectDataSource" runat="server" 
                            SelectMethod="getTourLocationByTourID" TypeName="CMS.BLL.CMSBLClass" 
                            OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="TourIDHiddenField" Name="TourID" 
                                    PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </p>
                    <asp:HiddenField ID="TourIDHiddenField" runat="server" />
                </asp:View>

                <!-- insert and update new display (Visible when insert link button is clicked
                                or update button is clicked) -->
                <asp:View ID="InsertView" runat="server">
                        <h1> <asp:Label ID="EditTitleLabel" runat="server"></asp:Label> </h1> 
                        <hr />
                    <!-- Name -->
                    <asp:Label ID="NameLabel" CssClass="label" runat="server" Text="Tour Name : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="NameTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="InsertRequiredFieldValidator" runat="server" 
                        ErrorMessage="Tour name is required." ControlToValidate="NameTextBox" SetFocusOnError="True" />
                    </p>
                    <!-- Phone -->
                    <asp:Label ID="PhoneLabel" CssClass="label" runat="server" Text="Phone : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="PhoneTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <br /><br />
                    <!-- Email -->
                    <asp:Label ID="EmailLabel" CssClass="label" runat="server" Text="Email : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="EmailTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <br /><br />
                    <!-- Website -->
                    <asp:Label ID="WebsiteLabel" CssClass="label" runat="server" Text="Website : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="WebsiteTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <br /><br />
                    <!-- Cost -->
                    <asp:Label ID="CostLabel" CssClass="label" runat="server" Text="Cost : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="CostTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Cost is required." ControlToValidate="CostTextBox" SetFocusOnError="True" />
                        <span style="margin-left: -105px;"/>
                        <asp:CustomValidator ID="CostTextBox_CustomValidator" runat="server" ErrorMessage="Cost should include only numbers."
                        onservervalidate="CostTextBox_CustomValidator_ServerValidate" 
                            ControlToValidate="CostTextBox"></asp:CustomValidator>
                    </p>  
                    <!-- Detailed Description -->
                    <asp:Label ID="DescriptionLabel" CssClass="label" runat="server" Text="Detailed description : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Width="400px" TextMode="MultiLine" Height="100px" ></asp:TextBox> 
                    <p class="validationError">             
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Detailed description is required." ControlToValidate="DescriptionTextBox" SetFocusOnError="True" />
                    </p>                                       
                   <div class="clear"></div>     
                    <!-- YouTube Video -->
                    <asp:Label ID="VideoLabel" CssClass="label" runat="server" Text="YouTube Video : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="VideoTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                    <br /><br />
                    <div class="clear"></div>
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
                    <div class="clear"></div>
                    <!-- Tour Locations -->
                    <asp:Label ID="Label1" CssClass="label" runat="server" Text="Tour Locations : " Font-Bold="True" Width="150px" ></asp:Label>
                    <div class="clear"></div>
                    <div class="LocationMaster">                        
                        <asp:LinkButton ID="ViewLinkButton" runat="server" CssClass="tabButton" 
                            Text="View List" BackColor="LightGray" CausesValidation="false" 
                            onclick="ViewLinkButton_Click"></asp:LinkButton>
                        <asp:LinkButton ID="AddNewLinkButton" runat="server" CssClass="tabButton" 
                            Text="Add New" BackColor="Gray" CausesValidation="false" 
                            onclick="AddNewLinkButton_Click"></asp:LinkButton>
                        <div class="Location">
                            <asp:MultiView ID="LocationMultiView" runat="server" ActiveViewIndex="0">
                                <asp:View ID="ViewView" runat="server">
                                    <div style="text-align:right; margin-bottom:10px;">
                                        <asp:Button ID="UpdateLocationItemButton" runat="server" Text="Update" Width="70px" 
                                        CausesValidation="False" onclick="UpdateLocationItemButton_Click" Enabled ="false" />
                                        <asp:Button ID="DeleteLocationItemButton" runat="server" Text="Delete" Width="70px" 
                                        CausesValidation="False" onclick="DeleteLocationItemButton_Click" Enabled ="false" />
                                    </div>
                                    <asp:GridView ID="EditLocationGridView" runat="server" AllowSorting="True" 
                                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                        DataKeyNames="TourLocationID" 
                                        ForeColor="Black" GridLines="None"  Width="600px"
                                        onrowdatabound="EditLocationGridView_RowDataBound"
                                        onselectedindexchanged="EditLocationGridView_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:CommandField SelectText="" ShowSelectButton="True">
                                            <ItemStyle Width="5px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="TourLocationID" HeaderText="TourLocationID" ReadOnly="True">
                                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TourID" HeaderText="TourID" >
                                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TourSeqNum" HeaderText="Seq" >
                                            <ItemStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LocationName" HeaderText="Location Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Latitude" HeaderText="Latitude">
                                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Longitude" HeaderText="Longitude" >
                                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Postcode" HeaderText="Postcode">
                                            <ItemStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Address" HeaderText="Address">
                                                <HeaderStyle CssClass="hidden"></HeaderStyle>
                                                <ItemStyle CssClass="hidden"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Suburb" HeaderText="Suburb" >
                                            </asp:BoundField>                                            
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>  
                                </asp:View>
                                <asp:View ID="AddView" runat="server">
                                    <h2> <asp:Label ID="EditLocationTitle" runat="server"></asp:Label> </h2>
                                    <asp:Label ID="Label2" runat="server" Text="Location Name : " Width="100px" style="text-align: right"></asp:Label>
                                    <asp:TextBox ID="LocationNameTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>
                                    <p class="validationError tap"> 
                                        <asp:CustomValidator ID="LocatinNameTextBox_CustomValidator" runat="server" ErrorMessage="Location name is required."
                                            ControlToValidate="LocationNameTextBox"  ValidateEmptyText="True"
                                           onservervalidate="LocatinNameTextBox_CustomValidator_ServerValidate"></asp:CustomValidator>
                                    </p>
                                    <asp:Label ID="Label3" runat="server" Text="Sequence : " Width="100px" style="text-align: right"></asp:Label>
                                    <asp:DropDownList ID="SeqDropDownList" Width="56px" runat="server"></asp:DropDownList><br/><br/>
                                    <asp:Label ID="Label4" runat="server" Text="Postcode : " Width="100px" style="text-align: right"></asp:Label>
                                    <asp:TextBox ID="PostcodeTextBox" runat="server" Width="50px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox><br/>                                    
                                    <p class="validationError tap"> 
                                        <asp:CustomValidator ID="PostcodeTextBox_CustomValidator" runat="server" ErrorMessage="Valid postcode is required."
                                            onservervalidate="PostcodeTextBox_CustomValidator_ServerValidate" 
                                            ControlToValidate="PostcodeTextBox"  ValidateEmptyText="True"></asp:CustomValidator>
                                    </p>
                                    <asp:Label ID="Label5" runat="server" Text="Address : " Width="100px" style="text-align: right"></asp:Label>
                                    <asp:TextBox ID="AddressTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
                                    <p class="validationError tap">
                                        <asp:CustomValidator ID="AddressTextBox_CustomValidator" runat="server" ErrorMessage="Input valid address using autocompletion."
                                            onservervalidate="AddressTextBox_CustomValidator_ServerValidate"
                                            ControlToValidate="AddressTextBox" ValidateEmptyText="True"></asp:CustomValidator>
                                    </p>
                                    <div class="map location" id="map"></div>
                                    <div class="clear"></div><br/>
                                    <asp:HiddenField ID="LatitudeHiddenField" runat="server"/>
                                    <asp:HiddenField ID="LongitudeHiddenField" runat="server" />
                                    <asp:HiddenField ID="DeletedLocationIDHiddenField" runat="server" />
                                    <asp:HiddenField ID="DeletedLocationIndexHiddenField" runat="server" />
                                    <asp:HiddenField ID="InsertedLocationIndexHiddenField" runat="server" />
                                    <asp:HiddenField ID="EditLocationIndexHiddenField" runat="server" />
                                    <asp:HiddenField ID="EditLocationIDHiddenField" runat="server" />
                                    <asp:HiddenField ID="SelectedLocationIndexHiddenField" runat="server" />
                                    <asp:HiddenField ID="SelectedLocationIDHiddenField" runat="server" />
                                    <div class="locationButtons">
                                        <asp:MultiView ID="LocationButtonMultiView" runat="server">
                                            <asp:View ID="AddLocationView" runat="server">
                                                <asp:Button ID="UpdateLocationButton" runat="server" Text="Submit Update" 
                                                    Width="120px" onclick="UpdateLocationButton_Click"/>
                                                <asp:ConfirmButtonExtender ID="UpdateLocationButton_ConfirmButtonExtender2" OnClientCancel="CancelClick"
                                                    runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                                                    TargetControlID="UpdateLocationButton" ConfirmOnFormSubmit="True">
                                                </asp:ConfirmButtonExtender>
                                                <asp:Button ID="CancelUpdateLocationButton" runat="server" Text="Cancel" 
                                                    Width="70px" CausesValidation="False" 
                                                    onclick="CancelUpdateLocationButton_Click" />
                                            </asp:View>
                                            <asp:View ID="EditLocationView" runat="server"> 
                                                <asp:Button ID="InsertLocationButton" runat="server" Text="Confirm" 
                                                    Width="100px" onclick="InsertLocationButton_Click"/>
                                                <asp:ConfirmButtonExtender ID="InsertLocationButton_ConfirmButtonExtender2" OnClientCancel="CancelClick"
                                                    runat="server" ConfirmText="Do you want to submit the new location?" Enabled="True" 
                                                    TargetControlID="InsertLocationButton" ConfirmOnFormSubmit="True">
                                                </asp:ConfirmButtonExtender>
                                                <asp:Button ID="CancelInsertLocationButton" runat="server" Text="Cancel" 
                                                    Width="70px" CausesValidation="False" 
                                                    onclick="CancelInsertLocationButton_Click" style="height: 26px" />     
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                        <p class="validationError location">
                            <asp:Label ID="LocationListErrorLabel" runat="server" Text="At least one location is required." Visible="false" ></asp:Label>
                        </p>
                    </div>
                    <div class="clear"></div>
                    <!-- Buttons -->
                    <div class="detailButtons bottom">
                        <asp:MultiView ID="ButtonMultiView" runat="server">
                            <asp:View ID="UpdateButtonView" runat="server">                            
                                <asp:Button ID="SubmitButton" runat="server" Text="Submit Update" Width="120px" 
                                    onclick="SubmitButton_Click"/>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" OnClientCancel="CancelClick"
                                    runat="server" ConfirmText="Do you want to submit the update?" Enabled="True" 
                                    TargetControlID="SubmitButton" ConfirmOnFormSubmit="True">
                                </asp:ConfirmButtonExtender>
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="70px" 
                                    onclick="CancelButton_Click" CausesValidation="False" />
                            </asp:View>
                            <asp:View ID="InsertButtonView" runat="server">
                                <asp:Button ID="InsertButton" runat="server" Text="Confirm" Width="100px"  
                                    onclick="InsertButton_Click" />
                                <asp:ConfirmButtonExtender ID="InsertButton_ConfirmButtonExtender" OnClientCancel="CancelClick"
                                    runat="server" ConfirmText="Do you want to submit the new tour?" Enabled="True" 
                                    TargetControlID="InsertButton" ConfirmOnFormSubmit="True">
                                </asp:ConfirmButtonExtender>
                                <asp:Button ID="InsertCancelButton" runat="server" Text="Cancel" Width="70px" 
                                    CausesValidation="False" onclick="InsertCancelButton_Click" />                    
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </asp:View>
            </asp:MultiView>
            </div>
        </div>
    </div>
</div>
    </span>
</asp:Content>
