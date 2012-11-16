using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Media;

namespace CMS.GeneralPages
{
    public partial class Tour : System.Web.UI.Page
    {
        //MySQL Data Access Class
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        /// <summary>
        /// Maintain selected audio file between postback. 
        /// This method runs everytime when the page loads.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
        protected void TourGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(TourGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Display detail view for the selected Tour.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void TourGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            poiImages.InnerHtml = "";
            poiVideo.InnerHtml = "";
            this.detailAudio.InnerHtml = "";
            AudioURLHiddenField.Value = "";
            Session.Remove("AudioFileUpload");
            this.SelectAudioLabel.Text = "mp3 files only.";

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

            //Retrieve data for the selected Tour
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(TourID);

            //Set the detail lables with the retrieved POIs data
            this.NameDataLabel.Text = tour["TourName"].ToString();
            this.PhoneDataLabel.Text = tour["Tour Phone"].ToString();
            this.EmailDataLabel.Text = tour["TourEmail"].ToString();
            this.WebsiteDataLabel.Text = tour["TourWebsite"].ToString();
            this.RatingData.CurrentRating = Convert.ToInt32(tour["TourCost"].ToString());
            if (this.RatingData.CurrentRating == 0)
                this.FreeRatingData.CurrentRating = 1;
            else
                this.FreeRatingData.CurrentRating = 0;
            this.DescriptionDataLabel.Text = tour["TourDetails"].ToString();
            this.TourIDHiddenField.Value = tour["TourID"].ToString();
            this.AgentDataLabel.Text = tour["TourAgent"].ToString();
            this.ViewLocationListBox.DataBind();
            this.TourMultiView.ActiveViewIndex = 0;
            bool hasVideo = false;
            bool hasImages = false;
            bool hasAudio = false;

            var media = dataAccess.getMediaByTourID(TourID);
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
            if (!hasImages)
            {
                poiImages.InnerHtml = "This Tour does not have any images.";
            }

            if (!hasVideo)
            {
                poiVideo.InnerHtml = "This Tour does not have any video.";
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
            //Create new collection to store selected POIs for the Tour
            ListItemCollection collection = new ListItemCollection();
            this.SelectedPOIListBox.DataSource = collection;
            this.SelectedPOIListBox.DataBind();

            //Store the collection to session to maintain it between postbacks. 
            Session["SelectedPOIList"] = collection;

            //Clear session for audio file.
            Session.Remove("AudioFileUpload");

            //Reset all input controls.
            this.SelectAudioLabel.Text = "mp3 files only.";

            this.POIListBox.DataSource = dataAccess.getAllPOIList();
            this.POIListBox.DataBind();
            this.SearchPOITextBox.Text = "";
            this.TourIDHiddenField.Value = "-1";
            this.NameTextBox.Text = "";
            this.PhoneTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.WebsiteTextBox.Text = "";
            this.AgentTextBox.Text = "";
            this.Rating.CurrentRating = 0;
            this.FreeRating.CurrentRating = 1;
            this.DescriptionTextBox.Text = "";
            this.Rating.CurrentRating = 0;
            this.VideoTextBox.Text = "";
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5).ToString());
            this.locationErrorLabel.Visible = false;

            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            poiImagesAddUpdate.InnerHtml = "";
            EditCurrentAudio.InnerHtml = "";
            this.AudioRemoveLinkButton.Visible = false;

            this.EditTitleLabel.Text = "Insert New Tour";
            this.ButtonMultiView.ActiveViewIndex = 1;
            this.TourMultiView.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Display update form view with input controls filled with the original data of the selected Tour.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            //Reset all hidden fields, file uploaders
            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            poiImagesAddUpdate.InnerHtml = "";
            IsAudioRemovedHiddenField.Value = "false";
            this.locationErrorLabel.Visible = false;

            EditCurrentAudio.InnerHtml = "";
            this.AudioRemoveLinkButton.Visible = false;
            Session.Remove("AudioFileUpload");
            this.SelectAudioLabel.Text = "mp3 files only.";

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

            //Retrieve all POIs previously selected for this Tour.
            DAL.CMSDBDataSet.TourPOIListDataTable list = dataAccess.getTourPOIListByTourID(TourID);

            //Create new collection to store retrieved POI List to it.
            ListItemCollection collection = new ListItemCollection();
            foreach (DAL.CMSDBDataSet.TourPOIListRow row in list)
            {
                collection.Add(new ListItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }

            //Bind collection to list box.
            this.SelectedPOIListBox.DataSource = collection;
            this.SelectedPOIListBox.DataBind();

            //Store collection to session to maintain it between postbacks.
            Session["SelectedPOIList"] = collection;

            //Retrieve all POIs stored in the database
            DAL.CMSDBDataSet.POIListDataTable poi = dataAccess.getAllPOIList();

            //Remove the ones that has already been selected 
            foreach (ListItem item in collection)
            {
                if (poi.Rows.Find(item.Value) != null)
                    poi.Rows.Remove(poi.Rows.Find(item.Value));
            }

            //Bind data table to list box.
            this.POIListBox.DataSource = poi;
            this.POIListBox.DataBind();
            this.SearchPOITextBox.Text = "";

            //Retrieve data for selected Tour.
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(TourID);

            //Fill input controls with the retrieved data.
            this.EditTitleLabel.Text = "Update Tour";
            this.NameTextBox.Text = tour["TourName"].ToString();
            this.PhoneTextBox.Text = tour["Tour Phone"].ToString();
            this.EmailTextBox.Text = tour["TourEmail"].ToString();
            this.WebsiteTextBox.Text = tour["TourWebsite"].ToString();
            this.AgentTextBox.Text = tour["TourAgent"].ToString();
            this.Rating.CurrentRating = Convert.ToInt32(tour["TourCost"].ToString());
            if (this.Rating.CurrentRating == 0)
                this.FreeRating.CurrentRating = 1;
            else
                this.FreeRating.CurrentRating = 0;
            this.DescriptionTextBox.Text = tour["TourDetails"].ToString();

            //Retrieve stored images for selected Tour. 
            var media = dataAccess.getMediaByTourID(TourID);
            ImageUploadDelete.Value = "0";
            int numOfMedia = dataAccess.CountImagesMediaByTourID(TourID);
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5 - numOfMedia).ToString());
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                //Display image thumb nails
                if (mediaRow.MediaType == "Images")
                {
                    CurrentImagesFileName.Value += mediaRow.MediaURL.Split('/')[2] + ';';
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
            this.TourMultiView.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Delete selected Tour then refresh the Tour gridview and display empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

            //get the urls of all related medias
            DAL.CMSDBDataSet.MediaDataTable URL = dataAccess.getMediaURLByTourID(tourID);
            foreach (DAL.CMSDBDataSet.MediaRow row in URL)
            {
                //delete media files
                System.IO.File.Delete(Server.MapPath("~/Media/" + row["MediaURL"].ToString().Substring(row["MediaURL"].ToString().LastIndexOf("/") + 1)));
            }

            // delete related medias and event from the database.
            dataAccess.DeleteMediaByTourID(tourID);
            dataAccess.deleteTourPOIListByTourID(tourID);
            dataAccess.deleteTour(tourID);
            //refresh the gridview and display empty view.
            this.TourGridView.DataBind();
            this.TourMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Update the selected POI using the user inputs then refresh the POI gridview 
        /// and display the detail view for the updated POI. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            //Check whether all vaildators confirm vaild inputs and at least on POI is selected for the Tour.
            if (this.Page.IsValid && (this.SelectedPOIListBox.Items.Count > 0))
            {
                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                int TourCost = this.Rating.CurrentRating;
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;
                String Agent = this.AgentTextBox.Text;
                int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

                //Perform update.
                dataAccess.updateTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, Agent, TourID);

                //Delete all TourPOIs for this Tour
                dataAccess.deleteTourPOIListByTourID(TourID);

                //Retrieve new selected TourPOIs from session and store them to the database
                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                foreach (ListItem item in collection)
                {
                    dataAccess.insertTourPOIList(Convert.ToInt32(item.Value), TourID, collection.IndexOf(item));
                }

                //Delete the images that user requested to delete
                int imageDeleteCount = ImageDeleteFileName.Value != "" ? ImageDeleteFileName.Value.Split(';').Length : 0;
                for (int y = 0; y < imageDeleteCount - 1; y++)
                {
                    string deletedFilename = ImageDeleteFileName.Value.Split(';')[y];
                    dataAccess.DeleteMediaByMediaURLAndTourID(TourID, "../Media/" + deletedFilename);
                    System.IO.File.Delete(Server.MapPath("~/Media/" + deletedFilename));
                }

                //Insert images for the selected Tour
                int count = ImageUploadFileName.Value != "" ? ImageUploadFileName.Value.Split(';').Length : 0;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", TourID);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                //delete old video
                dataAccess.DeleteVideoMediaByTourId(TourID);
                //Insert new video
                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(null, VideoTextBox.Text, "Video", TourID);
                }

                //delete old audio if exists then Insert new audio
                if (this.AudioFileUpload.PostedFile.ContentLength != 0)
                {
                    String url = dataAccess.getAudioURLByTourID(TourID);
                    if (url != null)
                    {
                        System.IO.File.Delete(Server.MapPath("~/Media/" + url.Substring(url.LastIndexOf("/") + 1)));
                        dataAccess.DeleteAudioByTourID(TourID);
                    }

                    HttpPostedFile posFile = this.AudioFileUpload.PostedFile;
                    Random rand = new Random((int)DateTime.Now.Ticks);
                    int numIterations = 0;
                    numIterations = rand.Next(1000000000, 2147483647);
                    Guid id = new Guid();
                    id = Guid.NewGuid();
                    String fileName = "Audio_" + numIterations.ToString() + id.ToString() + posFile.FileName;
                    posFile.SaveAs(Server.MapPath("~/Media/") + fileName);

                    dataAccess.InsertMedia(null, "../Media/" + fileName, "Audio", TourID);
                }
                else
                {
                    if (IsAudioRemovedHiddenField.Value.Equals("True"))
                    {
                        String url = dataAccess.getAudioURLByTourID(TourID);
                        System.IO.File.Delete(Server.MapPath("~/Media/" + url.Substring(url.LastIndexOf("/") + 1)));
                        dataAccess.DeleteAudioByTourID(TourID);
                    }
                }


                this.TourGridView.DataBind();

                Session.Remove("LocationTable");
                this.TourMultiView.ActiveViewIndex = -1;

            }
            else
            {
                if (this.SelectedPOIListBox.Items.Count < 1)
                {
                    this.locationErrorLabel.Visible = true;
                }
                else
                {
                    this.locationErrorLabel.Visible = false;
                }
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
            this.TourMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Insert new Tour using the user inputs then refresh the Tour gridview and display the empty view. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            //Check whether all vaildators confirm vaild inputs and at least on POI is selected for the Tour.
            if (this.Page.IsValid && (this.SelectedPOIListBox.Items.Count > 0))
            {
                //Convert inputs into correct format.
                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                int TourCost = this.Rating.CurrentRating;
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;
                String Agent = this.AgentTextBox.Text;

                //Perform insert and get the new POI ID as a return value.
                dataAccess.insertTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, Agent);
                int TourID = dataAccess.getNewlyInsertedTourID();

                //Retrieve new selected TourPOIs from session and store them to the database
                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                foreach (ListItem item in collection)
                {
                    dataAccess.insertTourPOIList(Convert.ToInt32(item.Value), TourID, collection.IndexOf(item));
                }

                //Insert images for the new Tour
                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", TourID);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                //Insert video for the new Tour
                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(null, VideoTextBox.Text, "Video", TourID);
                }

                //Insert audio for the new Tour
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

                    dataAccess.InsertMedia(null, "../Media/" + fileName, "Audio", TourID);
                }

                CurrentImagesFileName.Value = "";

                this.TourGridView.DataBind();
                Session.Remove("LocationTable");
                this.TourMultiView.ActiveViewIndex = -1;
            }
            else
            {
                if (this.SelectedPOIListBox.Items.Count < 1)
                {
                    this.locationErrorLabel.Visible = true;
                }
                else
                {
                    this.locationErrorLabel.Visible = false;
                }
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
            this.TourMultiView.ActiveViewIndex = -1;
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
                if (count - 1 > 0 || ImageUploadDelete.Value == "1")
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
                        if (hfc.AllKeys[i].Equals("ctl00$MainContent$FileUpload") && hfc[i].ContentLength > 0)
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
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../Temp_Media")))
                System.IO.File.Delete(f);
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
        /// Search POIs which have a name including the given search string.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SearchLinkButton_Click(object sender, EventArgs e)
        {
            //Retrieve POIs which have a name including the search string that the user entered.
            DAL.CMSDBDataSet.POIListDataTable poi = dataAccess.searchPOI(this.SearchPOITextBox.Text);
            
            //Retrieve the previously selected POIs from session and remove them from the POI datatable.
            ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
            foreach (ListItem item in collection)
            {
                if (poi.Rows.Find(item.Value) != null)
                    poi.Rows.Remove(poi.Rows.Find(item.Value));
            }

           //bind datatable to list box.
            this.POIListBox.DataSource = poi;
            this.POIListBox.DataBind();
        }

        /// <summary>
        /// List all POIs
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void ViewAllLinkButton_Click(object sender, EventArgs e)
        {
            //Retrieve all POIs
            DAL.CMSDBDataSet.POIListDataTable poi = dataAccess.getAllPOIList();

            //Retrieve the previously selected POIs from session and remove them from the POI datatable.
            ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
            foreach (ListItem item in collection)
            {
                if (poi.Rows.Find(item.Value) != null)
                    poi.Rows.Remove(poi.Rows.Find(item.Value));
            }

            //bind datatable to list box.
            this.POIListBox.DataSource = poi;
            this.POIListBox.DataBind();
        }

        /// <summary>
        /// Remove the selected POI form the POI list box and add it to the selected POI list.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SelectLinkButton_Click(object sender, EventArgs e)
        {
            if (this.POIListBox.SelectedIndex >= 0)
            {
                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                ListItem selectedItem = this.POIListBox.SelectedItem;
                collection.Add(selectedItem);
                this.SelectedPOIListBox.DataSource = collection;
                this.SelectedPOIListBox.DataBind();
                Session["SelectedPOIList"] = collection;

                this.POIListBox.Items.Remove(selectedItem);
                this.POIListBox.DataBind();
            }
        }

        /// <summary>
        /// Remove the selected POI form the selected POI list box and add it to the POI list.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void RemoveLinkButton_Click(object sender, EventArgs e)
        {
            if (this.SelectedPOIListBox.SelectedIndex >= 0)
            {
                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                ListItem selectedItem = this.SelectedPOIListBox.SelectedItem;
                collection.Remove(selectedItem);
                this.SelectedPOIListBox.DataSource = collection;
                this.SelectedPOIListBox.DataBind();
                Session["SelectedPOIList"] = collection;

                DAL.CMSDBDataSet.POIListDataTable poi = dataAccess.getAllPOIList();
                foreach (ListItem item in collection)
                {
                    if (poi.Rows.Find(item.Value) != null)
                        poi.Rows.Remove(poi.Rows.Find(item.Value));
                }

                this.POIListBox.DataSource = poi;
                this.POIListBox.DataBind();
            }
        }

        /// <summary>
        /// Move up the selected POI
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UpLinkButton_Click(object sender, EventArgs e)
        {
            if (this.SelectedPOIListBox.SelectedIndex > 0)
            {
                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                ListItem selectedItem = this.SelectedPOIListBox.SelectedItem;
                int selectedIndex = this.SelectedPOIListBox.SelectedIndex;
                collection.Remove(selectedItem);
                collection.Insert(selectedIndex - 1, selectedItem);
                Session["SelectedPOIList"] = collection;

                this.SelectedPOIListBox.DataSource = collection;
                this.SelectedPOIListBox.SelectedIndex = selectedIndex - 1;
                this.SelectedPOIListBox.DataBind();
            }
        }

        /// <summary>
        /// Move down the selected POI
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void DownLinkButton_Click(object sender, EventArgs e)
        {
            if (this.SelectedPOIListBox.SelectedIndex < this.SelectedPOIListBox.Items.Count - 1)
            {
                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                ListItem selectedItem = this.SelectedPOIListBox.SelectedItem;
                int selectedIndex = this.SelectedPOIListBox.SelectedIndex;
                collection.Remove(selectedItem);
                collection.Insert(selectedIndex + 1, selectedItem);
                Session["SelectedPOIList"] = collection;

                this.SelectedPOIListBox.DataSource = collection;
                this.SelectedPOIListBox.SelectedIndex = selectedIndex + 1;
                this.SelectedPOIListBox.DataBind();
            }
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

        /// <summary>
        /// Validate number inputs
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void numberInputValidate(object sender, ServerValidateEventArgs e)
        {
            String senderID = ((CustomValidator)sender).ID;
            bool isNum = false;
            //Ckeck is the entered phone number contains less than 11 digit numbers(Only numbers).
            if (senderID.Equals("PhoneTextBox_CustomValidator"))
            {
                Int32 num;
                if (Int32.TryParse(this.PhoneTextBox.Text, out num) && (this.PhoneTextBox.Text.Length < 11))
                {
                    isNum = true;
                }
            }
            e.IsValid = isNum;
        }
    }
}