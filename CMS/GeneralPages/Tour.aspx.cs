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
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TourGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(TourGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void TourGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            poiImages.InnerHtml = "";
            poiVideo.InnerHtml = "";
            this.detailAudio.InnerHtml = "";
            AudioURLHiddenField.Value = "";

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(TourID);
            this.NameDataLabel.Text = tour["TourName"].ToString();
            this.PhoneDataLabel.Text = tour["TourPhone"].ToString();
            this.EmailDataLabel.Text = tour["TourEmail"].ToString();
            this.WebsiteDataLabel.Text = tour["TourWebsite"].ToString();
            this.RatingData.CurrentRating = Convert.ToInt32(tour["TourCost"].ToString());
            if (this.RatingData.CurrentRating == 0)
                this.FreeRatingData.CurrentRating = 1;
            else
                this.FreeRatingData.CurrentRating = 0;
            this.DescriptionDataLabel.Text = tour["TourDetail"].ToString();
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
                if (mediaRow.MediaType == "Images")
                {
                    poiImages.InnerHtml += "<img class='itemImage' src='" + mediaRow.MediaURL + "' />";
                    hasImages = true;
                }
                if (mediaRow.MediaType == "Video")
                {
                    poiVideo.InnerHtml = "<iframe width='460' height='260' src='http://www.youtube.com/embed/" + mediaRow.MediaURL.Split(separator, StringSplitOptions.None)[1].Substring(0, 11)
                                        + "' frameborder='0' allowfullscreen></iframe>";
                    hasVideo = true;
                }
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

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            List<ListItem> lst = new List<ListItem>();
            for (int i = 1; i < 11; i++)
            {
                ListItem item = new ListItem(i.ToString(), "true");
                lst.Add(item);
            }

            ListItemCollection collection = new ListItemCollection();
            this.SelectedPOIListBox.DataSource = collection;
            this.SelectedPOIListBox.DataBind();
            Session["SelectedPOIList"] = collection;

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

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            poiImagesAddUpdate.InnerHtml = "";
            IsAudioRemovedHiddenField.Value = "false";
            this.locationErrorLabel.Visible = false;

            EditCurrentAudio.InnerHtml = "";
            this.AudioRemoveLinkButton.Visible = false;

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

            DAL.CMSDBDataSet.TourPOIListDataTable list = dataAccess.getTourPOIListByTourID(TourID);
            ListItemCollection collection = new ListItemCollection();

            foreach (DAL.CMSDBDataSet.TourPOIListRow row in list)
            {
                collection.Add(new ListItem(row["ItemName"].ToString(), row["ItemID"].ToString()));
            }

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
            this.SearchPOITextBox.Text = "";

            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(TourID);
            this.EditTitleLabel.Text = "Update Tour";
            this.NameTextBox.Text = tour["TourName"].ToString();
            this.PhoneTextBox.Text = tour["TourPhone"].ToString();
            this.EmailTextBox.Text = tour["TourEmail"].ToString();
            this.WebsiteTextBox.Text = tour["TourWebsite"].ToString();
            this.AgentTextBox.Text = tour["TourAgent"].ToString();
            this.Rating.CurrentRating = Convert.ToInt32(tour["TourCost"].ToString());
            if (this.Rating.CurrentRating == 0)
                this.FreeRating.CurrentRating = 1;
            else
                this.FreeRating.CurrentRating = 0;
            this.DescriptionTextBox.Text = tour["TourDetail"].ToString();

            var media = dataAccess.getMediaByTourID(TourID);
            ImageUploadDelete.Value = "0";
            int numOfMedia = dataAccess.CountImagesMediaByTourID(TourID);
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5 - numOfMedia).ToString());
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    CurrentImagesFileName.Value += mediaRow.MediaURL.Split('/')[2] + ';';
                    poiImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + mediaRow.MediaURL.Split('/')[2] + "' ><img class='itemImage' src='" + mediaRow.MediaURL + "'/><a class='delete-image' rel='" + mediaRow.MediaURL.Split('/')[2] + "'><div class='close_image' title='close'></div></a></div>";
                }
                if (mediaRow.MediaType == "Video")
                {
                    this.VideoTextBox.Text = mediaRow.MediaURL;
                }
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

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

            dataAccess.DeleteMediaByTourID(tourID);
            dataAccess.deleteTourPOIListByTourID(tourID);
            dataAccess.deleteTour(tourID);
            this.TourGridView.DataBind();
            this.TourMultiView.ActiveViewIndex = -1;
        }

        //submit update
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.InsertRequiredFieldValidator.IsValid && RequiredFieldValidator2.IsValid && this.CustomValidator1.IsValid
                && (this.SelectedPOIListBox.Items.Count > 0))
            {
                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                int TourCost = this.Rating.CurrentRating;
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;
                String Agent = this.AgentTextBox.Text;
                int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

                dataAccess.updateTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, Agent, TourID);

                dataAccess.deleteTourPOIListByTourID(TourID);

                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                foreach (ListItem item in collection)
                {
                    dataAccess.insertTourPOIList(Convert.ToInt32(item.Value), TourID, collection.IndexOf(item));
                }

                //TourImages

                int imageDeleteCount = ImageDeleteFileName.Value != "" ? ImageDeleteFileName.Value.Split(';').Length : 0;
                for (int y = 0; y < imageDeleteCount - 1; y++)
                {
                    string deletedFilename = ImageDeleteFileName.Value.Split(';')[y];
                    dataAccess.DeleteMediaByMediaURLAndTourID(TourID, "../Media/" + deletedFilename);
                    System.IO.File.Delete(Server.MapPath("~/Media/" + deletedFilename));
                }

                int count = ImageUploadFileName.Value != "" ? ImageUploadFileName.Value.Split(';').Length : 0;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", TourID);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                dataAccess.DeleteVideoMediaByTourId(TourID);
                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(null, VideoTextBox.Text, "Video", TourID);
                }

                if (this.AudioFileUpload.PostedFile.ContentLength != 0)
                {
                    dataAccess.DeleteAudioByTourID(TourID);

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

        //cancel update
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.IsAudioRemovedHiddenField.Value = "False";
            this.TourMultiView.ActiveViewIndex = 0;
        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (this.InsertRequiredFieldValidator.IsValid && RequiredFieldValidator2.IsValid && this.CustomValidator1.IsValid
                && (this.SelectedPOIListBox.Items.Count > 0))
            {
                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                int TourCost = this.Rating.CurrentRating;
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;
                String Agent = this.AgentTextBox.Text;

                dataAccess.insertTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, Agent);
                int TourID = dataAccess.getNewlyInsertedTourID();

                ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
                foreach (ListItem item in collection)
                {
                    dataAccess.insertTourPOIList(Convert.ToInt32(item.Value), TourID, collection.IndexOf(item));
                }

                //tourImages
                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", TourID);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(null, VideoTextBox.Text, "Video", TourID);
                }

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

        //cancel insert
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.IsAudioRemovedHiddenField.Value = "False";
            this.TourMultiView.ActiveViewIndex = -1;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.Text = "";
                //poiImagesAddUpdate.InnerHtml += "";
                int count = ImageUploadFileName.Value != "" ? ImageUploadFileName.Value.Split(';').Length : 0;
                if (count - 1 > 0 || ImageUploadDelete.Value == "1")
                {
                    poiImagesAddUpdate.InnerHtml = "";
                    int countExisting = CurrentImagesFileName.Value.Split(';').Length;
                    for (int x = 0; x < countExisting - 1; x++)
                    {
                        poiImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + CurrentImagesFileName.Value.Split(';')[x] + "' ><img class='itemImage' src='../Media/" +
                        CurrentImagesFileName.Value.Split(';')[x] + "' id='" + CurrentImagesFileName.Value.Split(';')[x]
                        + "' /><a class='upload-images' rel='" + CurrentImagesFileName.Value.Split(';')[x]
                        + "'><div class='close_image ' title='close'></div></a></div>";
                    }
                }
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    string target = "<div class='poi-images'  id='" + filename + "' ><img class='itemImage' src='../Temp_Media/" +
                        filename + "' id='" + filename + "' /><a class='upload-images' rel='" + filename
                        + "'><div class='close_image ' title='close'></div></a></div>";
                    poiImagesAddUpdate.InnerHtml += target;
                }

                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                bool uploadStatus = true;

                for (int i = 0; i < hfc.Count; i++)
                {
                    if (hfc.AllKeys[i].Equals("ctl00$MainContent$FileUpload"))
                    {
                        HttpPostedFile hpf = hfc[i];

                        if (!(hpf.ContentLength < 51200))
                        {
                            uploadStatus = false;
                        }
                    }
                }
                if (uploadStatus)
                {
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        if (hfc.AllKeys[i].Equals("ctl00$MainContent$FileUpload"))
                        {
                            HttpPostedFile hpf = hfc[i];

                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            Guid id = new Guid();

                            //-- Create new GUID and echo to the console
                            id = Guid.NewGuid();
                            hpf.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + id.ToString() + hpf.FileName);
                            ImageUploadFileName.Value = ImageUploadFileName.Value + numIterations.ToString() + id.ToString() + hpf.FileName + ';';
                            poiImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + numIterations.ToString() + id.ToString() + hpf.FileName + "' ><img class='itemImage' src='../Temp_Media/" + numIterations.ToString() + id.ToString() +
                                hpf.FileName + "' id='" + numIterations.ToString() + id.ToString() + hpf.FileName
                                + "' /><a class='upload-images' rel='" + numIterations.ToString() + id.ToString() + hpf.FileName
                                + "'><div class='close_image ' title='close'></div></a></div>";
                        }
                    }
                    FileUpload.Attributes.Remove("maxlength");
                    FileUpload.Attributes.Add("maxlength", (5 - hfc.Count).ToString());
                    StatusLabel.Text = "Uploaded Successfully.";
                }
                else
                {
                    StatusLabel.Text = "One of the files is larger than 50 kb! Please try again.";
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../Temp_Media")))
                System.IO.File.Delete(f);
        }

        protected void FreeRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            this.Rating.CurrentRating = 0;
        }

        protected void Rating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            this.FreeRating.CurrentRating = 0;
        }

        protected void SearchLinkButton_Click(object sender, EventArgs e)
        {
            DAL.CMSDBDataSet.POIListDataTable poi = dataAccess.searchPOI(this.SearchPOITextBox.Text);
            ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
            foreach (ListItem item in collection)
            {
                if (poi.Rows.Find(item.Value) != null)
                    poi.Rows.Remove(poi.Rows.Find(item.Value));
            }
            this.POIListBox.DataSource = poi;
            this.POIListBox.DataBind();
        }

        protected void ViewAllLinkButton_Click(object sender, EventArgs e)
        {
            DAL.CMSDBDataSet.POIListDataTable poi = dataAccess.getAllPOIList();
            ListItemCollection collection = Session["SelectedPOIList"] as ListItemCollection;
            foreach (ListItem item in collection)
            {
                if (poi.Rows.Find(item.Value) != null)
                    poi.Rows.Remove(poi.Rows.Find(item.Value));
            }
            this.POIListBox.DataSource = poi;
            this.POIListBox.DataBind();
        }

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

        protected void AudioRemoveLinkButton_Click(object sender, EventArgs e)
        {
            EditCurrentAudio.InnerHtml = "Current Audio Removed.";
            this.AudioRemoveLinkButton.Visible = false;
            this.IsAudioRemovedHiddenField.Value = "True";
        }

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