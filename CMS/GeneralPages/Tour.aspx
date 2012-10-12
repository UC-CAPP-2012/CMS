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
            <asp:ObjectDataSource ID="TourObjectDataSource" runat="server" SelectMethod="getAllTours" 
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
                        <asp:Label ID="DetailAgentLabel" runat="server" CssClass="label" Font-Bold="True" Text="Agent : " Width="150px"></asp:Label>
                        <asp:Label ID="AgentDataLabel" runat="server" Width="480px"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="DetailCostLabel" runat="server" CssClass="label" Font-Bold="True" Text="Cost : " Width="150px"></asp:Label>
                        <asp:Rating ID="RatingData" runat="server" MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                                    CurrentRating="0" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" CssClass="Rating" ReadOnly="True"></asp:Rating>
                        <asp:Rating ID="FreeRatingData" runat="server" MaxRating="1" StarCssClass="FreeRatingStar" WaitingStarCssClass="FreeSavedRatingStar"
                                    CurrentRating="0" FilledStarCssClass="FreeFilledRatingStar" EmptyStarCssClass="FreeEmptyRatingStar" CssClass="FreeRating" ReadOnly="True"></asp:Rating><br/>
                        <p>
                        </p>
                        <p>
                            <asp:Label ID="DetailDescriptionLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Detailed Description : " Width="150px"></asp:Label>
                            <asp:Label ID="DescriptionDataLabel" runat="server" Width="480px"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="DetailLocationLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Tour Locations : " Width="150px"></asp:Label>
                            <asp:ListBox ID="ViewLocationListBox" runat="server" 
                                DataSourceID="ObjectDataSource1" DataTextField="ItemName" 
                                DataValueField="ItemID" Height="200px" Width="300px"></asp:ListBox>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                SelectMethod="getTourPOIListByTourID" TypeName="CMS.BLL.CMSBLClass">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="TourGridView" Name="tourID" 
                                        PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </p>
                        <p>
                            <asp:Label ID="DetailImageLabel" runat="server" CssClass="label" 
                                Font-Bold="True" Text="Images : " Width="150px"></asp:Label>
                        </p>
                        <div ID="poiImages" runat="server" width="460px"></div>
                        <br/>
                        <p>
                            <asp:Label ID="DetailVideoLabel" runat="server" CssClass="label" 
                                Enabled="False" Font-Bold="True" Text="Video : " Width="150px"></asp:Label>
                        </p>
                        <div ID="poiVideo" runat="server" width="460px"></div>
                        <br/>
                        <asp:HiddenField ID="TourIDHiddenField" runat="server" />
                    </p>
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
                    <!-- Agent -->
                    <asp:Label ID="AgentLabel" CssClass="label" runat="server" Text="Agent : " Font-Bold="True" Width="150px" ></asp:Label>
                    <asp:TextBox ID="AgentTextBox" runat="server" Width="400px" onkeydown = "return (event.keyCode!=13);"></asp:TextBox> 
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
                    <!-- Tour Locations -->
                    <asp:Label ID="LocationLabel" CssClass="label" runat="server" Text="Tour Locations : " Font-Bold="True" Width="150px" ></asp:Label>
                    <div class="TourLocationMaster">
                        <div class="POIList">
                            <asp:Label ID="POIListLabel" runat="server" Text="Location List" CssClass="LocationLabel" 
                                BackColor="#CCCCFF" Width="250px" ForeColor="#444444" Font-Bold="True"></asp:Label><br/>
                            <asp:TextBox ID="SearchPOITextBox" runat="server" CssClass="searchTextBox" Width="199px" ></asp:TextBox>
                            <asp:LinkButton ID="SearchLinkButton" runat="server" 
                                CssClass="button small lightGray search" Width="20px" Height="11px" 
                                onclick="SearchLinkButton_Click"></asp:LinkButton>
                            <asp:ListBox ID="POIListBox" runat="server" Width="250px" CssClass="listBox"  Height="200px" 
                                DataTextField="ItemName" DataValueField="ItemID" ></asp:ListBox><br/>
                            <asp:LinkButton ID="ViewAllLinkButton" runat="server" 
                                CssClass="button small lightGray" Width="100px" 
                                onclick="ViewAllLinkButton_Click">View All</asp:LinkButton>
                            <asp:LinkButton ID="SelectLinkButton" runat="server" 
                                CssClass="button small lightGray right" Width="100px" 
                                onclick="SelectLinkButton_Click">Select</asp:LinkButton>
                        </div>
                        <div class="SelectedPOIList">
                            <asp:Label ID="SelectedPOIListLabel" runat="server" Text="Selected Location" CssClass="LocationLabel" 
                                BackColor="#CCCCFF" Width="250px" ForeColor="#444444" Font-Bold="True"></asp:Label><br/>
                            <asp:ListBox ID="SelectedPOIListBox" runat="server"  Width="250px" Height="222px" CssClass="listBox"
                                DataTextField="Text" DataValueField="Value"></asp:ListBox><br/>
                            <asp:LinkButton ID="UpLinkButton" runat="server" 
                                CssClass="button small lightGray up" Height="9px" Width="30px" 
                                onclick="UpLinkButton_Click"></asp:LinkButton>
                            <asp:LinkButton ID="DownLinkButton" runat="server" 
                                CssClass="button small lightGray down" Height="9px" Width="30px" 
                                onclick="DownLinkButton_Click"></asp:LinkButton>
                            <asp:LinkButton ID="RemoveLinkButton" runat="server" 
                                CssClass="button small lightGray right" Width="118px" 
                                onclick="RemoveLinkButton_Click">Remove</asp:LinkButton>
                        </div>
                    </div>
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
                    <div class="clear"></div><br />
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
