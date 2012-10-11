using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

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
            this.TourMultiView.ActiveViewIndex = 0;            
            bool hasVideo = false;
            bool hasImages = false;
            var media = dataAccess.getMediaByTourID(TourID);
            string[] separator = { "v=" };
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    poiImages.InnerHtml += "<img class='itemImage' src='" + mediaRow.MediaURL + "' />";
                    hasImages = true;
                }
                else
                {
                    poiVideo.InnerHtml = "<iframe width='460' height='260' src='http://www.youtube.com/embed/" + mediaRow.MediaURL.Split(separator, StringSplitOptions.None)[1].Substring(0, 11)
                                        + "' frameborder='0' allowfullscreen></iframe>";
                    hasVideo = true;
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
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            List<ListItem> lst = new List<ListItem>();
            for (int i = 1; i < 11; i++)
            {
                ListItem item = new ListItem(i.ToString(), "true");
                lst.Add(item);
            }

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

            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            poiImagesAddUpdate.InnerHtml = "";

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

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
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
                else
                {
                    this.VideoTextBox.Text = mediaRow.MediaURL;
                }

            }

            this.ButtonMultiView.ActiveViewIndex = 0;
            this.TourMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

            dataAccess.DeleteMediaByTourID(tourID);
            dataAccess.deleteTour(tourID);
            this.TourGridView.DataBind();
            this.TourMultiView.ActiveViewIndex = -1;
        }

        //submit update
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.InsertRequiredFieldValidator.IsValid && RequiredFieldValidator2.IsValid)
            {
                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                int TourCost = this.Rating.CurrentRating;
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;
                String Agent = this.AgentTextBox.Text;
                int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

                dataAccess.updateTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail,Agent, TourID);

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


                this.TourGridView.DataBind();

                Session.Remove("LocationTable");
                this.TourMultiView.ActiveViewIndex = -1;

            }
        }

        //cancel update
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.TourMultiView.ActiveViewIndex = 0;
        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (this.InsertRequiredFieldValidator.IsValid && RequiredFieldValidator2.IsValid)
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
                CurrentImagesFileName.Value = "";
 
                this.TourGridView.DataBind();
                Session.Remove("LocationTable");
                this.TourMultiView.ActiveViewIndex = -1;
            }
        }

        //cancel insert
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
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
    }
}