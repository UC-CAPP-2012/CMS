using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace CMS.GeneralPages
{
    public partial class POI : System.Web.UI.Page
    {
        //MySQL Data Access Class
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        /// <summary>
        /// Reset the address multi view when it is not postback and maintain selected audio file between postback. 
        /// This method runs everytime when the page loads.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                this.AddressMultiView.ActiveViewIndex = 0;

            // If new audio file is selected store it using session.
            if (Session["AudioFileUpload"] == null && this.AudioFileUpload.HasFile)
            {
                //store audio file to session
                Session["AudioFileUpload"] = this.AudioFileUpload;
                this.SelectAudioLabel.Text = "Selected Audio File : " + AudioFileUpload.FileName; 
            }
            // If session has audio file but no new file is selected.
            else if (Session["AudioFileUpload"] != null && (!this.AudioFileUpload.HasFile))
            {
                //get audio file form session 
                this.AudioFileUpload = (FileUpload)Session["AudioFileUpload"];
                this.SelectAudioLabel.Text = "Selected Audio File : " + AudioFileUpload.FileName; 
            }
            // If new audio file is selected over old selected audio file
            else if (AudioFileUpload.HasFile)
            {
                //reset session with new audio file.
                Session["AudioFileUpload"] = this.AudioFileUpload;
                this.SelectAudioLabel.Text = "Selected Audio File : " + AudioFileUpload.FileName;
            }
        }

        /// <summary>
        /// Add mouse event attributes to each row to change the background color when moving the mouse over it.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void POIGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(POIGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Display detail view for the selected POI.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void POIGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            poiImages.InnerHtml = "";
            poiVideo.InnerHtml = "";
            this.detailAudio.InnerHtml = "";
            AudioURLHiddenField.Value = "";
            int id = (Int32)this.POIGridView.SelectedDataKey.Value;

            //Retrieve data for the selected POI
            DAL.CMSDBDataSet.POIItemRow row = dataAccess.getPOIByItemID(id);

            //Set the detail lables with the retrieved POIs data
            Session.Remove("AudioFileUpload");
            this.SelectAudioLabel.Text = "mp3 files only.";

            this.NameDataLabel.Text = row["ItemName"].ToString();
            this.CategoryDataLabel.Text = row["CategoryName"].ToString();
            if (row["SubTypeID"].ToString() != "")
                this.SubtypeDataLabel.Text = dataAccess.getSubtypeName(Convert.ToInt32(row["SubTypeID"].ToString()));
            else
                this.SubtypeDataLabel.Text = "None";
            if (row["MajorRegionID"].ToString() != "")
                this.MajorRegionDataLabel.Text = dataAccess.getMajorRegionName(Convert.ToInt32(row["MajorRegionID"].ToString()));
            else
                this.MajorRegionDataLabel.Text = "None";
            this.PhoneDataLabel.Text = row["Phone"].ToString();
            this.EmailDataLabel.Text = row["Email"].ToString();
            this.WebsiteDataLabel.Text = row["Website"].ToString();
            this.OpeningHoursDataLabel.Text = row["OpeningHours"].ToString();
            this.RatingData.CurrentRating = Convert.ToInt32(row["Cost"].ToString());
            if (this.RatingData.CurrentRating == 0)
                this.FreeRatingData.CurrentRating = 1;
            else
                this.FreeRatingData.CurrentRating = 0;
            this.DescriptionDataLabel.Text = row["Detais"].ToString();
            this.PostcodeDataLabel.Text = row["Postcode"].ToString();
            this.SuburbDataLabel.Text = row["Suburb"].ToString();
            this.AddressDataLabel.Text = row["Address"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();
            this.CategoryIDHiddenField.Value = row["CategoryID"].ToString();

            this.POIMultiView.ActiveViewIndex = 0;
            bool hasVideo=false;
            bool hasImages = false;
            bool hasAudio = false;

            var media = dataAccess.getMediaByItemID(id);
            string[] separator = { "v=" };
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                //Display image thumb nail 
                if (mediaRow.MediaType == "Images")
                {
                    poiImages.InnerHtml += "<img class='itemImage' src='" + mediaRow.MediaURL + "' />";
                    hasImages = true;
                }
                //Display youtube video player 
                if (mediaRow.MediaType == "Video")
                {
                    if (mediaRow.MediaURL.Contains("www.youtube.com") && mediaRow.MediaURL.Contains("v="))
                    {
                        poiVideo.InnerHtml = "<iframe width='460' height='260' src='http://www.youtube.com/embed/" + mediaRow.MediaURL.Split(separator, StringSplitOptions.None)[1].Substring(0, 11)
                                            + "' frameborder='0' allowfullscreen></iframe>";
                    }
                    else
                    {
                        poiVideo.InnerHtml = "URL - " + mediaRow.MediaURL + "</br>(Sorry, only Videos from Youtube can be played on the CMS.)";
                    }
                    hasVideo = true;               
                }
                //Display flash audio player 
                if (mediaRow.MediaType == "Audio")
                {
                    this.detailAudio.InnerHtml = "<object type='application/x-shockwave-flash' data='../Resources/emff_old_noborder.swf' width='91' height='25'>"
                                               + "<param name='movie' value='../Resources/emff_old_noborder.swf'>"
                                               + "<param name='FlashVars' value='src=" + mediaRow.MediaURL + "'>"
                                               + "</object>";
                    AudioURLHiddenField.Value = mediaRow.MediaURL;
                    hasAudio = true;
                }
            }

            //Display empty media messages.
            if(!hasImages){
                poiImages.InnerHtml="This POI does not have any images.";
            }

            if(!hasVideo){
                poiVideo.InnerHtml = "This POI does not have any video.";
            }

            if (!hasAudio)
            {
                this.detailAudio.InnerHtml = "This Tour does not have any audio.";
            }
        }

        /// <summary>
        /// Display insert form view with empty input controls.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            //Reset all input controls.
            this.NameTextBox.Text = "";
            this.CategoryDropDownList.DataBind();
            this.SubtypeDropDownList.DataBind();
            this.MajorRegionDropDownList.DataBind();
            this.PhoneTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.WebsiteTextBox.Text = "";
            this.OpeningHoursTextBox.Text = "";            
            this.Rating.CurrentRating = 0;
            this.FreeRating.CurrentRating = 1;
            this.DescriptionTextBox.Text = "";
            this.PostcodeTextBox.Text = "";
            this.SuburbTextBox.Text = "";
            this.AddressTextBox.Text = "";
            this.EditTitleLabel.Text = "Insert New Point Of Interest";
            EditCurrentAudio.InnerHtml = "";
            this.AudioRemoveLinkButton.Visible = false;
            Session.Remove("AudioFileUpload");
            this.SelectAudioLabel.Text = "mp3 files only.";

            this.StatusLabel.Text = "";
            this.VideoTextBox.Text = "";
            ImageUploadFileName.Value = "";
            this.poiImagesAddUpdate.InnerHtml = "";
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5).ToString());
            this.ButtonMultiView.ActiveViewIndex = 1;
            this.POIMultiView.ActiveViewIndex = 1;            
            
        }

        /// <summary>
        /// Insert new POI using the user inputs then refresh the POI gridview and display the empty view. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            //Check whether all vaildators confirm vaild inputs.
            if (this.Page.IsValid)
            { 
                String address;
                String suburb;
                double latitude;
                double longitude;

                if (this.AddressMultiView.ActiveViewIndex == 0)
                {
                    //Get address values form the auto address view.
                    address = this.AddressTextBox.Text;
                    suburb = this.SuburbTextBox.Text;
                    latitude = Convert.ToDouble(this.LatitudeHiddenField.Value);
                    longitude = Convert.ToDouble(this.LongitudeHiddenField.Value);

                }
                else
                {
                    //Get address values form the manual address view.
                    address = this.ManualStNoTextBox.Text + " " + this.ManualStNameTextBox.Text + ", " 
                                    + this.ManualSuburbTextBox.Text + ", Australian Capital Territory";
                    suburb = this.ManualSuburbTextBox.Text;
                    latitude = Convert.ToDouble(this.ManualLatTextBox.Text);
                    longitude = Convert.ToDouble(this.ManualLogTextBox.Text);
                }

                //Convert inputs into correct format.
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int cost = this.Rating.CurrentRating;
                int categotyID = Convert.ToInt32(this.CategoryDropDownList.SelectedValue);
                int? subtypeID;
                if(this.SubtypeDropDownList.SelectedValue != "")
                    subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                else
                    subtypeID = null;
                int? majorRegion;
                if (this.MajorRegionDropDownList.SelectedValue != "")
                    majorRegion = Convert.ToInt32(this.MajorRegionDropDownList.SelectedValue);
                else
                    majorRegion = null;

                //Perform insert and get the new POI ID as a return value.
                int newItemId= dataAccess.InsertPOI(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.PhoneTextBox.Text, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, address, latitude, longitude, postCode, suburb, subtypeID, categotyID, majorRegion);

                //Insert images for the new POI
                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count-1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(newItemId, "../Media/" + filename, "Images", null);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                //Insert video for the new POI
                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(newItemId, VideoTextBox.Text, "Video", null);
                }
                CurrentImagesFileName.Value = "";

                //Insert audio for the new POI
                if (this.AudioFileUpload.PostedFile.ContentLength != 0)
                {
                    HttpPostedFile posFile = this.AudioFileUpload.PostedFile;
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    int numIterations = 0;
                    numIterations = rand.Next(1000000000, 2147483647);
                    Guid id = new Guid();
                    id = Guid.NewGuid();
                    String fileName = "Audio_" + numIterations.ToString() + id.ToString() + posFile.FileName;
                    posFile.SaveAs(Server.MapPath("~/Media/") + fileName);

                    dataAccess.InsertMedia(newItemId, "../Media/" + fileName, "Audio", null);
                }

                this.POIGridView.DataBind();
                this.POIMultiView.ActiveViewIndex = -1;
            }
        }

        /// <summary>
        /// To cancel insert, display the empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.IsAudioRemovedHiddenField.Value = "False";
            this.POIMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Validate number inputs
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void numberInputValidate(object sender, ServerValidateEventArgs e)
        {
            String senderID = ((CustomValidator)sender).ID;
            bool isNum = false;
            //Check if the entered postcode is 4 digit numbers.
            if (senderID.Equals("PostcodeTextBox_CustomValidator") && (this.PostcodeTextBox.Text.Length==4))
            {
                Int32 num;
                isNum = Int32.TryParse(this.PostcodeTextBox.Text, out num);
            }
            //Check for auto address inputs(should not be empty)
            if (senderID.Equals("AutoAddressTextBox_CustomValidator"))
            {
                if (this.AddressMultiView.ActiveViewIndex == 1)
                {
                    isNum = true;
                }
                if ((this.AddressMultiView.ActiveViewIndex == 0) && (this.AddressTextBox.Text.Length > 0) &&
                    (this.LatitudeHiddenField.Value.Length > 0) && (this.LongitudeHiddenField.Value.Length > 0))
                {
                    isNum = true;
                }
            }
            //Check for manual address inputs(should not be empty)
            if (senderID.Equals("ManualAddressTextBox_CustomValidator"))
            {
                Int32 intNum;
                double doubleNum;

                if (this.AddressMultiView.ActiveViewIndex == 0)
                {
                    isNum = true;
                }
                //All the text boxes must not be empty and street number, lat and long should have correct type of number inputs.
                if ((this.AddressMultiView.ActiveViewIndex == 1)
                    && (this.ManualStNoTextBox.Text.Length > 0) && Int32.TryParse(this.ManualStNoTextBox.Text, out intNum) 
                    && (this.ManualStNameTextBox.Text.Length > 0) && (this.ManualSuburbTextBox.Text.Length > 0)
                    && (this.ManualLatTextBox.Text.Length > 0) && Double.TryParse(this.ManualLatTextBox.Text, out doubleNum)
                    && (this.ManualLogTextBox.Text.Length > 0) && Double.TryParse(this.ManualLogTextBox.Text, out doubleNum))
                {
                    isNum = true;
                }
            }
            //Ckeck is the entered phone number contains less than 11 digit numbers(Only numbers).
            if (senderID.Equals("PhoneTextBox_CustomValidator"))
            {
                Int32 num;
                if (Int32.TryParse(this.PhoneTextBox.Text, out num) && (this.PhoneTextBox.Text.Length<11))
                {
                    isNum = true;
                }
            }
            e.IsValid = isNum;
        }

        /// <summary>
        /// Display update form view with input controls filled with the original data of the selected POI.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //Reset all hidden fields, file uploaders and address multi view
            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            this.EditTitleLabel.Text = "Update Point Of Interest";
            this.AddressMultiView.ActiveViewIndex = 0;
            this.ManualLatTextBox.Text = "";
            this.ManualLogTextBox.Text = "";
            this.ManualStNoTextBox.Text = "";
            this.ManualStNameTextBox.Text = "";
            this.ManualSuburbTextBox.Text = "";
            poiImagesAddUpdate.InnerHtml = "";
            EditCurrentAudio.InnerHtml = "";
            this.AudioRemoveLinkButton.Visible = false;
            Session.Remove("AudioFileUpload");
            this.SelectAudioLabel.Text = "mp3 files only.";
            int id = (Int32)this.POIGridView.SelectedDataKey.Value;

            //Retrieve data for selected Event.
            DAL.CMSDBDataSet.POIItemRow row = dataAccess.getPOIByItemID(id);

            //Fill input controls with the retrieved data.
            this.NameTextBox.Text = row["ItemName"].ToString();
            this.CategoryDropDownList.SelectedValue = row["CategoryID"].ToString();
            if (row["SubTypeID"].ToString() != "")
                this.SubtypeDropDownList.SelectedValue = row["SubTypeID"].ToString();
            else
                this.SubtypeDropDownList.DataBind();
            if (row["MajorRegionID"].ToString() != "")
                this.MajorRegionDropDownList.SelectedValue = row["MajorRegionID"].ToString();
            else
                this.MajorRegionDropDownList.DataBind();
            this.PhoneTextBox.Text = row["Phone"].ToString();
            this.EmailTextBox.Text = row["Email"].ToString();
            this.WebsiteTextBox.Text = row["Website"].ToString();
            this.OpeningHoursTextBox.Text = row["OpeningHours"].ToString();
            this.Rating.CurrentRating = Convert.ToInt32(row["Cost"].ToString());
            if (this.Rating.CurrentRating == 0)
                this.FreeRating.CurrentRating = 1;
            else
                this.FreeRating.CurrentRating = 0;
            this.DescriptionTextBox.Text = row["Detais"].ToString();
            this.PostcodeTextBox.Text = row["Postcode"].ToString();
            this.SuburbTextBox.Text = row["Suburb"].ToString();
            this.AddressTextBox.Text = row["Address"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();

            ImageUploadDelete.Value = "0";
            int numOfMedia = dataAccess.CountImagesMediaByItemId(id);
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength",(5-numOfMedia).ToString());

            //Retrieve stored images for selected Event. 
            var media = dataAccess.getMediaByItemID(id);

            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                //Display image thumb nails
                if (mediaRow.MediaType == "Images")
                {
                    CurrentImagesFileName.Value += mediaRow.MediaURL.Split('/')[2]+';';
                    poiImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + mediaRow.MediaURL.Split('/')[2] + "' ><img class='itemImage' src='" + mediaRow.MediaURL + "'/><a class='delete-image' rel='" + mediaRow.MediaURL.Split('/')[2] + "'><div class='close_image' title='close'></div></a></div>";
                }
                //Display video URL
                if (mediaRow.MediaType == "Video")
                {
                    this.VideoTextBox.Text = mediaRow.MediaURL;
                }
                //Display flash audio player
                if (mediaRow.MediaType == "Audio")
                {
                    EditCurrentAudio.InnerHtml = "<object type='application/x-shockwave-flash' data='../Resources/emff_old_noborder.swf' width='91' height='25'>"
                                               + "<param name='movie' value='../Resources/emff_old_noborder.swf'>"
                                               + "<param name='FlashVars' value='src=" + mediaRow.MediaURL + "'>"
                                               + "</object>";
                    this.AudioRemoveLinkButton.Visible = true;
                }
            }
            this.ButtonMultiView.ActiveViewIndex = 0;
            this.POIMultiView.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Delete selected POI then refresh the POI gridview and display empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int itemID = (Int32)this.POIGridView.SelectedDataKey.Value;

            //get the urls of all related medias
            DAL.CMSDBDataSet.MediaDataTable URL = dataAccess.getMediaURLByItemID(itemID);
            foreach (DAL.CMSDBDataSet.MediaRow row in URL)
            {
                //delete media files
                System.IO.File.Delete(Server.MapPath("~/Media/" + row["MediaURL"].ToString().Substring(row["MediaURL"].ToString().LastIndexOf("/") + 1)));
            }

            // delete related medias and event from the database.
            dataAccess.DeleteMediaByItemID(itemID);
            dataAccess.DeletePOI(itemID);
            //refresh the gridview and display empty view.
            this.POIGridView.DataBind();
            this.POIMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Update the selected POI using the user inputs then refresh the POI gridview 
        /// and display the detail view for the updated POI. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            //Check whether all vaildators confirm vaild inputs.
            if (this.Page.IsValid)
            {
                String address;
                String suburb;
                double latitude;
                double longitude;

                if (this.AddressMultiView.ActiveViewIndex == 0)
                {
                    //Get address values form the auto address view.
                    address = this.AddressTextBox.Text;
                    suburb = this.SuburbTextBox.Text;
                    latitude = Convert.ToDouble(this.LatitudeHiddenField.Value);
                    longitude = Convert.ToDouble(this.LongitudeHiddenField.Value);

                }
                else
                {
                    //Get address values form the manual address view.
                    address = this.ManualStNoTextBox.Text + " " + this.ManualStNameTextBox.Text + ", " 
                        + this.ManualSuburbTextBox.Text + ", Australian Capital Territory";
                    suburb = this.ManualSuburbTextBox.Text;
                    latitude = Convert.ToDouble(this.ManualLatTextBox.Text);
                    longitude = Convert.ToDouble(this.ManualLogTextBox.Text);
                }
                //convert inputs into correct format.
                int cost = this.Rating.CurrentRating;
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int categotyID = Convert.ToInt32(this.CategoryDropDownList.SelectedValue);
                int? subtypeID;
                if (this.SubtypeDropDownList.SelectedValue != "")
                    subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                else
                    subtypeID = null;
                int? majorRegionID;
                if (this.MajorRegionDropDownList.SelectedValue != "")
                    majorRegionID = Convert.ToInt32(this.MajorRegionDropDownList.SelectedValue);
                else
                    majorRegionID = null;
                int originalCategoryID = Convert.ToInt32(this.CategoryIDHiddenField.Value);

                int itemID = Convert.ToInt32(this.POIGridView.SelectedDataKey.Value);

                //Perform update.
                dataAccess.UpdatePOI(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.PhoneTextBox.Text, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, address, latitude, longitude, postCode,
                    suburb, subtypeID, majorRegionID, categotyID, itemID, originalCategoryID);

                //Delete the images that user requested to delete
                int imageDeleteCount = ImageDeleteFileName.Value != "" ? ImageDeleteFileName.Value.Split(';').Length : 0;
                for (int y = 0; y < imageDeleteCount - 1; y++)
                {
                    string deletedFilename = ImageDeleteFileName.Value.Split(';')[y];
                    dataAccess.DeleteMediaByMediaURL(itemID, "../Media/"+deletedFilename);
                    System.IO.File.Delete(Server.MapPath("~/Media/" + deletedFilename));
                }

                //Insert images for the selected Event
                int count = ImageUploadFileName.Value !="" ? ImageUploadFileName.Value.Split(';').Length : 0;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(itemID, "../Media/" + filename, "Images", null);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                //delete old video
                dataAccess.DeleteVideoMediaByItemId(itemID);
                //Insert new video
                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(itemID, VideoTextBox.Text, "Video", null);
                }

                //delete old audio if exists then Insert new audio
                if (this.AudioFileUpload.PostedFile.ContentLength != 0)
                {
                    String url = dataAccess.getAudioURLByItemID(itemID);
                    if (url != null)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Media/" + url.Substring(url.LastIndexOf("/") + 1)));
                        dataAccess.DeleteAudioByItemID(itemID);
                    }

                    HttpPostedFile posFile = this.AudioFileUpload.PostedFile;
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    int numIterations = 0;
                    numIterations = rand.Next(1000000000, 2147483647);
                    Guid id = new Guid();
                    id = Guid.NewGuid();
                    String fileName = "Audio_" + numIterations.ToString() + id.ToString() + posFile.FileName;
                    posFile.SaveAs(Server.MapPath("~/Media/") + fileName);

                    dataAccess.InsertMedia(itemID, "../Media/" + fileName, "Audio", null);
                }
                else
                {
                    if (IsAudioRemovedHiddenField.Value.Equals("True"))
                    {
                        String url = dataAccess.getAudioURLByItemID(itemID);
                        System.IO.File.Delete(Server.MapPath("~/Media/" + url.Substring(url.LastIndexOf("/") + 1)));
                        dataAccess.DeleteAudioByItemID(itemID);
                    }
                }

                this.POIGridView.DataBind();
                this.POIMultiView.ActiveViewIndex = -1;
            }
        }

        /// <summary>
        /// To cancel update, go back to the detail view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.IsAudioRemovedHiddenField.Value = "False";
            this.POIMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Upload images that user selected using file uploader.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.Text = "";
                int count = ImageUploadFileName.Value != "" ? ImageUploadFileName.Value.Split(';').Length : 0;
                //Display thumb nails for currently stored images
                if (count - 1 > 0 || ImageUploadDelete.Value=="1")
                {
                    poiImagesAddUpdate.InnerHtml = "";
                    int countExisting = CurrentImagesFileName.Value.Split(';').Length;
                    for (int x = 0; x < countExisting - 1; x++)
                    {
                        poiImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + CurrentImagesFileName.Value.Split(';')[x] + "' ><img class='itemImage' src='../Media/" +
                        CurrentImagesFileName.Value.Split(';')[x] + "' id='" + CurrentImagesFileName.Value.Split(';')[x] + "' /><a class='upload-images' rel='" + CurrentImagesFileName.Value.Split(';')[x] + "'><div class='close_image ' title='close'></div></a></div>";
                    }
                }
                //Display thumb nails for previously uploaded images
                for (int i = 0; i < count - 1; i++)
                { 
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    string target = "<div class='poi-images'  id='" + filename + "' ><img class='itemImage' src='../Temp_Media/" +
                        filename + "' id='" + filename + "' /><a class='upload-images' rel='" + filename + "'><div class='close_image ' title='close'></div></a></div>";
                    poiImagesAddUpdate.InnerHtml += target;
                }

                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                bool uploadStatus = true;

                for (int i = 0; i < hfc.Count; i++)
                {
                    //Check if the file is uploaded using the image file uploader (not the audio file uploader.)
                    if (hfc.AllKeys[i].Equals("ctl00$MainContent$FileUpload"))
                    {
                        HttpPostedFile hpf = hfc[i];
                        //Check the image size.
                        if (!(hpf.ContentLength < 51200))
                        {
                            uploadStatus = false;
                            StatusLabel.Text = "One of the files is larger than 50 kb! Please try again.";
                        }
                    }
                }
                //Start uploading if the size of all selected images are ok.
                if (uploadStatus)
                {
                    int uploadImgCount = 0;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        if (hfc.AllKeys[i].Equals("ctl00$MainContent$FileUpload") && hfc[i].ContentLength>0)
                        {
                            HttpPostedFile hpf = hfc[i];
                            uploadImgCount++;
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            Guid id = new Guid();

                            //-- Create new GUID and echo to the console
                            id = Guid.NewGuid();
                            hpf.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + id.ToString() + hpf.FileName);
                            ImageUploadFileName.Value = ImageUploadFileName.Value + numIterations.ToString() + id.ToString() + hpf.FileName + ';';
                            poiImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + numIterations.ToString() + id.ToString() + hpf.FileName + "' ><img class='itemImage' src='../Temp_Media/" + numIterations.ToString() + id.ToString() +
                                hpf.FileName + "' id='" + numIterations.ToString() + id.ToString() + hpf.FileName + "' /><a class='upload-images' rel='" + numIterations.ToString() + id.ToString() + hpf.FileName + "'><div class='close_image ' title='close'></div></a></div>";

                        }
                    }

                    // Count the total number of images.
                    int thumbNailCount = 0;
                    String thumbNail = poiImagesAddUpdate.InnerHtml.ToString();
                    while (thumbNail.Contains("poi-images"))
                    {
                        thumbNail = thumbNail.Remove(thumbNail.IndexOf("poi-images"), 10);
                        thumbNailCount++;
                    }

                    //Set the maximum number of images that can be inserted.
                    int maxlength = 5 - thumbNailCount;
                    FileUpload.Attributes.Remove("maxlength");
                    FileUpload.Attributes.Add("maxlength", maxlength.ToString());
                    StatusLabel.Text = "Uploaded Successfully.";
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Delete all temporarily saved images 
        /// </summary>
        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../Temp_Media")))
                System.IO.File.Delete(f);
        }

        /// <summary>
        /// Display auto address input form view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void AutoLinkButton_Click(object sender, EventArgs e)
        {
            this.AutoLinkButton.BackColor = Color.LightGray;
            this.ManualLinkButton.BackColor = Color.Gray;
            this.AddressMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Display manual address input form view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void ManualLinkButton_Click(object sender, EventArgs e)
        {
            this.AutoLinkButton.BackColor = Color.Gray;
            this.ManualLinkButton.BackColor = Color.LightGray;
            this.AddressMultiView.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Insert 'none' list item to subtype dorp down list.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubtypeDropDownList_DataBound(object sender, EventArgs e)
        {
            ListItem item = new ListItem("----- none -----", "");
            this.SubtypeDropDownList.Items.Insert(0, item);
        }

        /// <summary>
        /// Insert 'none' list item to major region dorp down list.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void MajorRegionDropDownList_DataBound(object sender, EventArgs e)
        {
            ListItem item = new ListItem("----- none -----", "");
            this.MajorRegionDropDownList.Items.Insert(0, item);
        }

        /// <summary>
        /// Set Star Rating to 0 when free sign is clicked.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void FreeRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            this.Rating.CurrentRating = 0;
        }

        /// <summary>
        /// Set free sign to off if changed star rating is higher than 0.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void Rating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            this.FreeRating.CurrentRating = 0;
        }

        /// <summary>
        /// Reserve the audio file to be deleted when user complete insert or update.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void AudioRemoveLinkButton_Click(object sender, EventArgs e)
        {
            EditCurrentAudio.InnerHtml = "Current Audio Removed.";
            this.AudioRemoveLinkButton.Visible = false;
            this.IsAudioRemovedHiddenField.Value = "True";
        }

        /// <summary>
        /// Check if the type of audio file is mp3.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void audioFileTypeCheck(object sender, ServerValidateEventArgs e)
        {
            bool isOK = true;
            String fileExtension = this.AudioFileUpload.PostedFile.FileName.Substring(this.AudioFileUpload.PostedFile.FileName.LastIndexOf("."));
            if (!fileExtension.Equals(".mp3"))
            {
                isOK = false;
            }
            e.IsValid = isOK;
        }
    }
}