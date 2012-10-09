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
        DAL.CMSDBDataSet.TourLocationDataTable locationTable;

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

            this.EditLocationGridView.DataBind();
            this.LocationGridView.DataBind();
            this.EditLocationGridView.DataBind();
            this.LocationMultiView.ActiveViewIndex = 0;
            this.LocationViewMultiView.ActiveViewIndex = 0;

            this.ViewListLinkButton.BackColor = Color.LightGray;
            this.ViewDetailLinkButton.BackColor = Color.Gray;

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(TourID);
            this.NameDataLabel.Text = tour["TourName"].ToString();
            this.PhoneDataLabel.Text = tour["TourPhone"].ToString();
            this.EmailDataLabel.Text = tour["TourEmail"].ToString();
            this.WebsiteDataLabel.Text = tour["TourWebsite"].ToString();
            this.CostDataLabel.Text = tour["TourCost"].ToString();
            this.DescriptionDataLabel.Text = tour["TourDetail"].ToString();
            this.TourIDHiddenField.Value = tour["TourID"].ToString();
            this.LocationGridView.DataBind();
            this.LocationGridView.SelectedIndex = -1;
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


            this.SeqDropDownList.DataSource = lst;
            this.SeqDropDownList.DataBind();

            this.TourIDHiddenField.Value = "-1";
            this.EditLocationGridView.DataBind();
            this.NameTextBox.Text = "";
            this.PhoneTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.WebsiteTextBox.Text = "";
            this.CostTextBox.Text = "";
            this.DescriptionTextBox.Text = "";

            this.AddressTextBox.Text = "";
            this.PostcodeTextBox.Text = "";
            this.LocationNameTextBox.Text = "";
            this.SeqDropDownList.SelectedIndex = 0;

            this.VideoTextBox.Text = "";
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5).ToString());

            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            poiImagesAddUpdate.InnerHtml = "";

            AddedImagesHiddenField.Value = "";
            DeletedImagesHiddenField.Value = "";
            AddedImagesForNewLocationHiddenField.Value = "";
            LocationImagesList.InnerHtml = "";
            AddedVideosForNewLocationHiddenField.Value = "";
            AddedVideosHiddenField.Value = "";

            this.EditTitleLabel.Text = "Insert New Tour";
            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            
            this.LocationMultiView.ActiveViewIndex = 1;
            this.LocationButtonMultiView.ActiveViewIndex = 1;
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

            AddedImagesHiddenField.Value = "";
            DeletedImagesHiddenField.Value = "";
            AddedImagesForNewLocationHiddenField.Value = "";
            AddedVideosForNewLocationHiddenField.Value = "";
            AddedVideosHiddenField.Value = "";

            int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
            locationTable = dataAccess.getTourLocationByTourID(TourID);
            this.EditLocationGridView.DataSource = locationTable;
            this.EditLocationGridView.DataBind();
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(TourID);
            this.EditTitleLabel.Text = "Update Tour";
            this.NameTextBox.Text = tour["TourName"].ToString();
            this.PhoneTextBox.Text = tour["TourPhone"].ToString();
            this.EmailTextBox.Text = tour["TourEmail"].ToString();
            this.WebsiteTextBox.Text = tour["TourWebsite"].ToString();
            this.CostTextBox.Text = tour["TourCost"].ToString();
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

            this.LocationMultiView.ActiveViewIndex = 0;
            this.ViewLinkButton.BackColor = Color.LightGray;
            this.AddNewLinkButton.BackColor = Color.Gray;
            this.ButtonMultiView.ActiveViewIndex = 0;
            this.TourMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
            DAL.CMSDBDataSet.TourLocationDataTable locations = dataAccess.getTourLocationByTourID(tourID);
            foreach (DAL.CMSDBDataSet.TourLocationRow row in locations.Rows)
            {
                dataAccess.DeleteMediaByTourLocationID(Convert.ToInt32(row["TourLocationID"].ToString()));
            }
            dataAccess.deleteTourLocationByTourID(tourID);
            dataAccess.DeleteMediaByTourID(tourID);
            dataAccess.deleteTour(tourID);
            this.TourGridView.DataBind();
            this.TourMultiView.ActiveViewIndex = -1;
        }

        //submit update
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.InsertRequiredFieldValidator.IsValid && this.RequiredFieldValidator1.IsValid && this.CostTextBox_CustomValidator.IsValid
                && RequiredFieldValidator2.IsValid && this.EditLocationGridView.Rows.Count > 0)
            {
                if (this.EditLocationIndexHiddenField.Value.Length > 0)
                {
                    string[] locationIndex = this.EditLocationIndexHiddenField.Value.ToString().Split(',');
                    foreach (string index in locationIndex)
                    {
                        if (!index.Equals("") && !this.DeletedLocationIndexHiddenField.Value.Contains(index))
                        {
                            int tourLocationID = Convert.ToInt32(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[1].Text);
                            String locationName = this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[4].Text;
                            int tourID = Convert.ToInt32(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[2].Text);
                            short sequence = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[3].Text);
                            short postcode = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[7].Text);
                            double lat = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[5].Text);
                            double lon = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[6].Text);
                            String address = this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[8].Text;
                            String suburb = this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[9].Text;

                            dataAccess.updateTourLocation(tourID, sequence, locationName, lat, lon, address, suburb, postcode, tourLocationID);
                        }
                    }
                }
                if (this.InsertedLocationIndexHiddenField.Value.Length > 0)
                {
                    string[] locationIndex = this.InsertedLocationIndexHiddenField.Value.ToString().Split(',');
                    foreach (string id in locationIndex)
                    {
                        if (!id.Equals("") && !this.DeletedLocationIndexHiddenField.Value.Contains(id))
                        {
                            String locationName = this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[4].Text;
                            int tourID = Convert.ToInt32(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[2].Text);
                            short sequence = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[3].Text);
                            short postcode = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[7].Text);
                            double lat = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[5].Text);
                            double lon = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[6].Text);
                            String address = this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[8].Text;
                            String suburb = this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[9].Text;

                            dataAccess.insertTourLocation(tourID, sequence, locationName, lat, lon, address, suburb, postcode);
                        }
                    }
                }

                if (this.DeletedLocationIDHiddenField.Value.Length > 0)
                {
                    string[] locationIDs = this.DeletedLocationIDHiddenField.Value.ToString().Split(',');
                    foreach (string id in locationIDs)
                    {
                        if (!id.Equals(""))
                        {
                            dataAccess.DeleteMediaByTourLocationID(Convert.ToInt32(id));
                            dataAccess.deleteTourLocationByLocationID(Convert.ToInt32(id));
                        }
                    }
                }

                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                Decimal TourCost = Convert.ToDecimal(this.CostTextBox.Text);
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;
                int TourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);

                dataAccess.updateTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourID);

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
                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", TourID, null);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                dataAccess.DeleteVideoMediaByTourId(TourID);
                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(null, VideoTextBox.Text, "Video", TourID, null);
                }

                //LocationImages
                int locationImageDeleteCount = DeletedImagesHiddenField.Value != "" ? DeletedImagesHiddenField.Value.Split(';').Length : 0;
                for (int y = 0; y < locationImageDeleteCount - 1; y++)
                {
                    int locationID = Convert.ToInt32(DeletedImagesHiddenField.Value.Split(';')[y].Split('/')[0]);
                    string deletedFilename = DeletedImagesHiddenField.Value.Split(';')[y].Split('/')[1];
                    dataAccess.DeleteMediaByMediaURLAndTourLocationID(locationID, "../Media/" + deletedFilename);
                    System.IO.File.Delete(Server.MapPath("~/Media/" + deletedFilename));
                }

                int locationCount = AddedImagesHiddenField.Value != "" ? AddedImagesHiddenField.Value.Split(';').Length : 0;
                for (int i = 0; i < locationCount - 1; i++)
                {
                    int locationID = Convert.ToInt32(AddedImagesHiddenField.Value.Split(';')[i].Split('/')[0]);
                    string filename = AddedImagesHiddenField.Value.Split(';')[i].Split('/')[1];
                    if (!this.DeletedImagesHiddenField.Value.Contains(filename))
                    {
                        dataAccess.InsertMedia(null, "../Media/" + filename, "Images", null, locationID);
                        System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                    }
                }                
                
                foreach(string locationImg in AddedImagesForNewLocationHiddenField.Value.Split('@'))
                {
                    if (locationImg.Length > 0)
                    {
                        int tourID = Convert.ToInt32(locationImg.Split('+')[0]);
                        short seq = Convert.ToInt16(locationImg.Split('+')[1]);
                        int locationID = dataAccess.getTourLocationIDByTourIDAndTourSeqNum(tourID, seq);
                        foreach (string img in locationImg.Split('+')[2].Split(';'))
                        {
                            if (img.Length > 0)
                            {
                                string filename = img.Split('/')[1];
                                if (!this.DeletedImagesHiddenField.Value.Contains(filename))
                                {
                                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", null, locationID);
                                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                                }
                            }
                        }
                    }
                }
                
                foreach (string video in AddedVideosHiddenField.Value.Split(';'))
                {
                    if (video.Length > 0)
                    {
                        int locationID = Convert.ToInt32(video.Split('#')[0]);
                        string videoURL = video.Split('#')[1];
                        dataAccess.DeleteVideoMediaByTourId(locationID);
                        dataAccess.InsertMedia(null, videoURL, "Video", null, locationID);
                    }
                }

                foreach (string video in AddedVideosForNewLocationHiddenField.Value.Split(';'))
                {
                    if (video.Length > 0)
                    {
                        int tourId = Convert.ToInt32(video.Split('#')[0]);
                        short seq = Convert.ToInt16(video.Split('#')[1]);
                        int locationID = dataAccess.getTourLocationIDByTourIDAndTourSeqNum(tourId, seq);
                        dataAccess.DeleteVideoMediaByTourId(locationID);
                        dataAccess.InsertMedia(null, video.Split('#')[3], "Video", null, locationID);
                    }
                }

                this.TourGridView.DataBind();
                this.LocationGridView.DataBind();
                this.DeletedLocationIDHiddenField.Value = "";
                this.DeletedLocationIndexHiddenField.Value = "";
                this.InsertedLocationIndexHiddenField.Value = "";

                Session.Remove("LocationTable");
                this.TourMultiView.ActiveViewIndex = -1;

            }
            else
            {
                if (this.EditLocationGridView.Rows.Count == 0)
                    this.LocationListErrorLabel.Visible = true;
                else
                    this.LocationListErrorLabel.Visible = false;
            }

        }

        //cancel update
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.DeletedLocationIDHiddenField.Value = "";
            this.DeletedLocationIndexHiddenField.Value = "";
            this.EditLocationIDHiddenField.Value = "";
            this.EditLocationIndexHiddenField.Value = "";
            this.InsertedLocationIndexHiddenField.Value = "";
            this.SelectedLocationIDHiddenField.Value = "";
            this.SelectedLocationIndexHiddenField.Value = "";

            this.TourMultiView.ActiveViewIndex = 0;
        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (this.InsertRequiredFieldValidator.IsValid && this.RequiredFieldValidator1.IsValid && this.CostTextBox_CustomValidator.IsValid
                && RequiredFieldValidator2.IsValid && this.EditLocationGridView.Rows.Count > 0)
            {

                String TourName = this.NameTextBox.Text;
                String TourDetail = this.DescriptionTextBox.Text;
                Decimal TourCost = Convert.ToDecimal(this.CostTextBox.Text);
                String TourPhone = this.PhoneTextBox.Text;
                String TourWebsite = WebsiteTextBox.Text;
                String TourEmail = this.EmailTextBox.Text;

                dataAccess.insertTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail);
                int TourID = dataAccess.getNewlyInsertedTourID();

                if (this.EditLocationIndexHiddenField.Value.Length > 0)
                {
                    string[] locationIndex = this.EditLocationIndexHiddenField.Value.ToString().Split(',');
                    foreach (string index in locationIndex)
                    {
                        if (!index.Equals("") && !this.DeletedLocationIndexHiddenField.Value.Contains(index))
                        {
                            int tourLocationID = Convert.ToInt32(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[1].Text);
                            String locationName = this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[4].Text;
                            int tourID = TourID;
                            short sequence = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[3].Text);
                            short postcode = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[7].Text);
                            double lat = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[5].Text);
                            double lon = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[6].Text);
                            String address = this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[8].Text;
                            String suburb = this.EditLocationGridView.Rows[Convert.ToInt32(index)].Cells[9].Text;

                            dataAccess.updateTourLocation(tourID,sequence, locationName, lat, lon, address, suburb, postcode, tourLocationID);
                        }
                    }
                }
                if (this.InsertedLocationIndexHiddenField.Value.Length > 0)
                {
                    string[] locationIndex = this.InsertedLocationIndexHiddenField.Value.ToString().Split(',');
                    foreach (string id in locationIndex)
                    {
                        if (!id.Equals("") && !this.DeletedLocationIndexHiddenField.Value.Contains(id))
                        {
                            String locationName = this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[4].Text;
                            int tourID = TourID;
                            short sequence = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[3].Text);
                            short postcode = Convert.ToInt16(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[7].Text);
                            double lat = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[5].Text);
                            double lon = Convert.ToDouble(this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[6].Text);
                            String address = this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[8].Text;
                            String suburb = this.EditLocationGridView.Rows[Convert.ToInt32(id)].Cells[9].Text;

                            dataAccess.insertTourLocation(tourID, sequence, locationName, lat, lon, address, suburb, postcode);
                        }
                    }
                }

                if (this.DeletedLocationIDHiddenField.Value.Length > 0)
                {
                    string[] locationIDs = this.DeletedLocationIDHiddenField.Value.ToString().Split(',');
                    foreach (string id in locationIDs)
                    {
                        if (!id.Equals(""))
                        {
                            dataAccess.DeleteMediaByTourLocationID(Convert.ToInt32(id));
                            dataAccess.deleteTourLocationByLocationID(Convert.ToInt32(id));
                        }
                    }
                }

                //tourImages
                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(null, "../Media/" + filename, "Images", TourID, null);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(null, VideoTextBox.Text, "Video", TourID, null);
                }
                CurrentImagesFileName.Value = "";
                
                //locationImages
                foreach (string locationImg in AddedImagesForNewLocationHiddenField.Value.Split('@'))
                {
                    if (locationImg.Length > 0)
                    {
                        if (locationImg.Split('+')[0].Equals("-1"))
                        {
                            short seq = Convert.ToInt16(locationImg.Split('+')[1]);
                            int locationID = dataAccess.getTourLocationIDByTourIDAndTourSeqNum(TourID, seq);
                            foreach (string img in locationImg.Split('+')[2].Split(';'))
                            {
                                if (img.Length > 0)
                                {
                                    string filename = img.Split('/')[1];
                                    if (!this.DeletedImagesHiddenField.Value.Contains(filename))
                                    {
                                        dataAccess.InsertMedia(null, "../Media/" + filename, "Images", null, locationID);
                                        System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (string video in AddedVideosHiddenField.Value.Split(';'))
                {
                    if (video.Length > 0)
                    {
                        int locationID = Convert.ToInt32(video.Split('#')[0]);
                        string videoURL = video.Split('#')[1];
                        dataAccess.DeleteVideoMediaByTourId(locationID);
                        dataAccess.InsertMedia(null, videoURL, "Video", null, locationID);
                    }
                }

                foreach (string video in AddedVideosForNewLocationHiddenField.Value.Split(';'))
                {
                    if (video.Length > 0)
                    {
                        short seq = Convert.ToInt16(video.Split('#')[1]);
                        int locationID = dataAccess.getTourLocationIDByTourIDAndTourSeqNum(TourID, seq);
                        dataAccess.DeleteVideoMediaByTourId(locationID);
                        dataAccess.InsertMedia(null, video.Split('#')[3], "Video", null, locationID);
                    }
                }

                CurrentImagesHiddenField.Value = "";

                this.TourGridView.DataBind();
                this.LocationGridView.DataBind();
                this.DeletedLocationIDHiddenField.Value = "";
                this.DeletedLocationIndexHiddenField.Value = "";
                this.InsertedLocationIndexHiddenField.Value = "";
                Session.Remove("LocationTable");
                this.TourMultiView.ActiveViewIndex = -1;
            }
            else
            {
                if (this.EditLocationGridView.Rows.Count == 0)
                    this.LocationListErrorLabel.Visible = true;
                else
                    this.LocationListErrorLabel.Visible = false;
            }
        }

        //cancel insert
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.DeletedLocationIDHiddenField.Value = "";
            this.DeletedLocationIndexHiddenField.Value = "";
            this.EditLocationIDHiddenField.Value = "";
            this.EditLocationIndexHiddenField.Value = "";
            this.InsertedLocationIndexHiddenField.Value = "";
            this.SelectedLocationIDHiddenField.Value = "";
            this.SelectedLocationIndexHiddenField.Value = "";

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
                        CurrentImagesFileName.Value.Split(';')[x] + "' id='" + CurrentImagesFileName.Value.Split(';')[x] + "' /><a class='upload-images' rel='" + CurrentImagesFileName.Value.Split(';')[x] + "'><div class='close_image ' title='close'></div></a></div>";
                    }
                }
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

        protected void ViewLinkButton_Click(object sender, EventArgs e)
        {
            this.ViewLinkButton.BackColor = Color.LightGray;
            this.AddNewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.Text = "Add New";
            this.UpdateLocationItemButton.Enabled = false;
            this.DeleteLocationItemButton.Enabled = false;
            this.EditLocationGridView.SelectedIndex = -1;
            this.LocationMultiView.ActiveViewIndex = 0;
        }

        //add location
        protected void AddNewLinkButton_Click(object sender, EventArgs e)
        {
            List<ListItem> lst = new List<ListItem>();
            for (int i = 1; i < 11; i++)
            {
                ListItem item = new ListItem(i.ToString(), "true");
                lst.Add(item);
            }
            this.SeqDropDownList.DataSource = lst;
            this.SeqDropDownList.DataBind();

            if (this.TourIDHiddenField.Value != "-1")
            {
                if (Session["LocationTable"] == null)
                    locationTable = dataAccess.getTourLocationByTourID(Convert.ToInt32(this.TourIDHiddenField.Value));
                else
                    locationTable = Session["LocationTable"] as DAL.CMSDBDataSet.TourLocationDataTable;

                foreach (DAL.CMSDBDataSet.TourLocationRow row in locationTable)
                {
                    if (!this.DeletedLocationIDHiddenField.Value.Contains(row["TourLocationID"].ToString()))
                    {
                        SeqDropDownList.Items[Convert.ToInt32(row["TourSeqNum"].ToString()) - 1].Enabled = false;
                    }
                }
            }

            this.LocationVideoTextBox.Text = "";
            CurrentImagesHiddenField.Value = "";
            TempAddedImagesHiddenField.Value = "";
            TempDeletedImagesHiddenField.Value = "";
            TempAddedImagesForNewLocationHiddenField.Value = "";

            LocationImageUploadStatusLabel.Text = "";
            LocationImagesList.InnerHtml = "";
            AddedVideosHiddenField.Value = "";
            AddedVideosForNewLocationHiddenField.Value = "";
            SelectedLocationIDHiddenField.Value = "";
            SelectedLocationIndexHiddenField.Value = "";

            LocationFileUpload.Attributes.Remove("maxlength");
            LocationFileUpload.Attributes.Add("maxlength", (5).ToString());

            this.AddressTextBox.Text = "";
            this.PostcodeTextBox.Text = "";
            this.LocationNameTextBox.Text = "";
            this.SeqDropDownList.SelectedIndex = 0;            
            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            this.LocationMultiView.ActiveViewIndex = 1;
            this.LocationButtonMultiView.ActiveViewIndex = 1;
        }

        //update confirm
        protected void UpdateLocationButton_Click(object sender, EventArgs e)
        {
            if (this.AddressTextBox_CustomValidator.IsValid && this.CostTextBox_CustomValidator.IsValid
                && this.LocatinNameTextBox_CustomValidator.IsValid && this.PostcodeTextBox_CustomValidator.IsValid)
            {
                int tourID = Convert.ToInt32(this.TourIDHiddenField.Value);
                Decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                short sequence = Convert.ToInt16(this.SeqDropDownList.SelectedItem.ToString());
                short postcode = Convert.ToInt16(this.PostcodeTextBox.Text);
                double lat = Convert.ToDouble(this.LatitudeHiddenField.Value);
                double lon = Convert.ToDouble(this.LongitudeHiddenField.Value);
                String suburb = this.AddressTextBox.Text.Remove(0, this.AddressTextBox.Text.IndexOf(",") + 2);
                suburb = this.AddressTextBox.Text.Substring(0, suburb.IndexOf(","));

                if (Session["LocationTable"] == null)
                    locationTable = dataAccess.getTourLocationByTourID(Convert.ToInt32(this.TourIDHiddenField.Value));
                else
                    locationTable = Session["LocationTable"] as DAL.CMSDBDataSet.TourLocationDataTable;

                int index = Convert.ToInt32(this.SelectedLocationIndexHiddenField.Value);
                locationTable.Rows[index]["TourID"] = tourID;
                locationTable.Rows[index]["TourSeqNum"] = sequence;
                locationTable.Rows[index]["LocationName"] = this.LocationNameTextBox.Text;
                locationTable.Rows[index]["Latitude"] = lat;
                locationTable.Rows[index]["Longitude"] = lon;
                locationTable.Rows[index]["Address"] = this.AddressTextBox.Text;
                locationTable.Rows[index]["Suburb"] = suburb;
                locationTable.Rows[index]["Postcode"] = postcode;

                if (!this.EditLocationIndexHiddenField.Value.ToString().Contains(this.SelectedLocationIndexHiddenField.Value.ToString())
                    && !this.InsertedLocationIndexHiddenField.Value.ToString().Contains(this.SelectedLocationIndexHiddenField.Value.ToString()))
                {
                    this.EditLocationIndexHiddenField.Value += this.SelectedLocationIndexHiddenField.Value + ",";
                    this.EditLocationIDHiddenField.Value += this.SelectedLocationIDHiddenField.Value + ",";
                }

                Session["LocationTable"] = locationTable;
                this.EditLocationGridView.DataSource = locationTable;
                this.EditLocationGridView.DataBind();

                if (this.DeletedLocationIndexHiddenField.Value.Length > 0)
                {
                    string[] locationIndex = this.DeletedLocationIndexHiddenField.Value.ToString().Split(',');
                    foreach (string id in locationIndex)
                    {
                        if (!id.Equals(""))
                        {
                            this.EditLocationGridView.Rows[Convert.ToInt32(id)].Visible = false;
                        }
                    }
                }

                this.AddedImagesHiddenField.Value += this.TempAddedImagesHiddenField.Value;
                this.DeletedImagesHiddenField.Value += this.TempDeletedImagesHiddenField.Value;

                this.AddedImagesForNewLocationHiddenField.Value += tourID + "+" + sequence + "+"
                                                                + this.TempAddedImagesForNewLocationHiddenField.Value + "@";

                if (!this.SelectedLocationIDHiddenField.Value.Equals("-1"))
                    this.AddedVideosHiddenField.Value += this.SelectedLocationIDHiddenField.Value+"#"+this.LocationVideoTextBox.Text + ";";
                else
                    this.AddedVideosForNewLocationHiddenField.Value += tourID + "#" + sequence + "#"
                                                                + this.SelectedLocationIDHiddenField.Value + "#" + this.LocationVideoTextBox.Text + ";";



                this.AddNewLinkButton.Text = "Add New";
                this.ViewLinkButton.BackColor = Color.LightGray;
                this.AddNewLinkButton.BackColor = Color.Gray;
                this.LocationMultiView.ActiveViewIndex = 0;
            }
        }

        //cancel update
        protected void CancelUpdateLocationButton_Click(object sender, EventArgs e)
        {
            this.TempAddedImagesHiddenField.Value = "";
            this.TempDeletedImagesHiddenField.Value = "";
            this.TempAddedImagesForNewLocationHiddenField.Value = "";
            this.LocationMultiView.ActiveViewIndex = 0;
        }

        //insert confirm
        protected void InsertLocationButton_Click(object sender, EventArgs e)
        {
            if(this.AddressTextBox_CustomValidator.IsValid && this.CostTextBox_CustomValidator.IsValid 
                && this.LocatinNameTextBox_CustomValidator.IsValid && this.PostcodeTextBox_CustomValidator.IsValid)
            {
                int tourID;
                if (this.ButtonMultiView.ActiveViewIndex == 0)
                {
                    tourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
                }
                else
                {
                    tourID = -1;                    
                }

                this.TourIDHiddenField.Value = tourID.ToString();

                Decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                short sequence = Convert.ToInt16(this.SeqDropDownList.SelectedItem.ToString());
                short postcode = Convert.ToInt16(this.PostcodeTextBox.Text);
                double lat = Convert.ToDouble(this.LatitudeHiddenField.Value);
                double lon = Convert.ToDouble(this.LongitudeHiddenField.Value);
                String suburb = this.AddressTextBox.Text.Remove(0, this.AddressTextBox.Text.IndexOf(",")+2);
                suburb = this.AddressTextBox.Text.Substring(0, suburb.IndexOf(","));

                if (Session["LocationTable"] == null)
                {
                    if(this.TourIDHiddenField.Value != "-1")
                        locationTable = dataAccess.getTourLocationByTourID(Convert.ToInt32(this.TourIDHiddenField.Value));
                    else
                        locationTable = new DAL.CMSDBDataSet.TourLocationDataTable();
                }
                else
                    locationTable = Session["LocationTable"] as DAL.CMSDBDataSet.TourLocationDataTable;

                locationTable.Rows.Add(locationTable.NewRow());
                locationTable.Rows[locationTable.Rows.Count - 1]["TourID"] = tourID;
                locationTable.Rows[locationTable.Rows.Count - 1]["TourSeqNum"] = sequence;
                locationTable.Rows[locationTable.Rows.Count - 1]["LocationName"] = this.LocationNameTextBox.Text;
                locationTable.Rows[locationTable.Rows.Count - 1]["Latitude"] = lat;
                locationTable.Rows[locationTable.Rows.Count - 1]["Longitude"] = lon;
                locationTable.Rows[locationTable.Rows.Count - 1]["Address"] = this.AddressTextBox.Text;
                locationTable.Rows[locationTable.Rows.Count - 1]["Suburb"] = suburb;
                locationTable.Rows[locationTable.Rows.Count - 1]["Postcode"] = postcode;

                this.InsertedLocationIndexHiddenField.Value += locationTable.Rows.Count - 1 + ",";

                Session["LocationTable"] = locationTable;
                this.EditLocationGridView.DataSource = locationTable;
                this.EditLocationGridView.DataBind();

                if (this.DeletedLocationIndexHiddenField.Value.Length > 0)
                {
                    string[] locationIndex = this.DeletedLocationIndexHiddenField.Value.ToString().Split(',');
                    foreach (string id in locationIndex)
                    {
                        if (!id.Equals(""))
                        {
                            this.EditLocationGridView.Rows[Convert.ToInt32(id)].Visible = false;
                        }
                    }
                }

                this.AddedImagesHiddenField.Value += this.TempAddedImagesHiddenField.Value;
                this.DeletedImagesHiddenField.Value += this.TempDeletedImagesHiddenField.Value;

                this.AddedImagesForNewLocationHiddenField.Value += this.TourIDHiddenField.Value + "+" + sequence + "+"
                                                                + this.TempAddedImagesForNewLocationHiddenField.Value + "@";

                this.AddedVideosForNewLocationHiddenField.Value += this.TourIDHiddenField.Value + "#" + sequence + "#"
                                                                + "-1" + "#" + this.LocationVideoTextBox.Text + ";";

                this.ViewLinkButton.BackColor = Color.LightGray;
                this.AddNewLinkButton.BackColor = Color.Gray;
                this.LocationMultiView.ActiveViewIndex = 0;
            }
        }

        //cancel insert
        protected void CancelInsertLocationButton_Click(object sender, EventArgs e)
        {
            this.TempAddedImagesHiddenField.Value = "";
            this.TempDeletedImagesHiddenField.Value = "";
            this.TempAddedImagesForNewLocationHiddenField.Value = "";
            this.LocationMultiView.ActiveViewIndex = 0;
        }

        protected void EditLocationGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(EditLocationGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void EditLocationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EditLocationGridView.SelectedIndex > -1)
            {
                this.DeleteLocationItemButton.Enabled = true;
                this.UpdateLocationItemButton.Enabled = true;
            }
        }

        protected void LocatinNameTextBox_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.LocationNameTextBox.Text.Length > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void PostcodeTextBox_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Int32 num;
            if (Int32.TryParse(this.PostcodeTextBox.Text, out num) && (this.PostcodeTextBox.Text.Length == 4))
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void AddressTextBox_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Double num;
            if ((this.LatitudeHiddenField.Value.Length > 0) && (this.LongitudeHiddenField.Value.Length > 0)
                && Double.TryParse(this.LatitudeHiddenField.Value, out num) && Double.TryParse(this.LongitudeHiddenField.Value, out num))
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CostTextBox_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Decimal num;
            if (Decimal.TryParse(this.CostTextBox.Text, out num))
                args.IsValid = true;
            else
                args.IsValid = false;
        }
        
        protected void DeleteLocationItemButton_Click(object sender, EventArgs e)
        {
            int tourLocationID = Convert.ToInt32(this.EditLocationGridView.SelectedDataKey.Value);
            int index = this.EditLocationGridView.SelectedIndex;

            DeletedLocationIndexHiddenField.Value += index + ",";
            DeletedLocationIDHiddenField.Value += tourLocationID + ",";
            this.EditLocationGridView.Rows[index].Visible = false;
        }

        protected void UpdateLocationItemButton_Click(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(this.TourIDHiddenField.Value);
            int locationID = Convert.ToInt32(this.EditLocationGridView.SelectedDataKey.Value);
            this.SelectedLocationIDHiddenField.Value = locationID.ToString();
            this.SelectedLocationIndexHiddenField.Value = this.EditLocationGridView.SelectedIndex.ToString();
            
            CurrentImagesHiddenField.Value = "";            
            TempAddedImagesHiddenField.Value = "";
            TempDeletedImagesHiddenField.Value = "";
            TempAddedImagesForNewLocationHiddenField.Value = "";
            LocationImageUploadStatusLabel.Text = "";
            LocationImagesList.InnerHtml = "";
            AddedVideosHiddenField.Value = "";

            List<ListItem> lst = new List<ListItem>();
            for (int i = 1; i < 11; i++)
            {
                ListItem item = new ListItem(i.ToString(), "true");
                lst.Add(item);
            }
            this.SeqDropDownList.DataSource = lst;
            this.SeqDropDownList.DataBind();


            if (Session["LocationTable"] == null)
                locationTable = dataAccess.getTourLocationByTourID(tourID);
            else
                locationTable = Session["LocationTable"] as DAL.CMSDBDataSet.TourLocationDataTable;

            foreach (DAL.CMSDBDataSet.TourLocationRow row in locationTable)
            {
                String i = row["TourLocationID"].ToString();
                if (!this.DeletedLocationIDHiddenField.Value.Contains(row["TourLocationID"].ToString()))
                {
                    if (this.EditLocationGridView.SelectedDataKey.Value.ToString() != row["TourLocationID"].ToString())
                    {
                        SeqDropDownList.Items[Convert.ToInt32(row["TourSeqNum"].ToString()) - 1].Enabled = false;
                    }
                }
            }           

            this.LocationNameTextBox.Text = this.EditLocationGridView.Rows[this.EditLocationGridView.SelectedIndex].Cells[4].Text;
            int seq = Convert.ToInt32(this.EditLocationGridView.Rows[this.EditLocationGridView.SelectedIndex].Cells[3].Text) - 1;
            this.SeqDropDownList.SelectedIndex = this.SeqDropDownList.Items.IndexOf(new ListItem(seq.ToString(), "true"));
            this.PostcodeTextBox.Text = this.EditLocationGridView.Rows[this.EditLocationGridView.SelectedIndex].Cells[7].Text;
            this.AddressTextBox.Text = this.EditLocationGridView.Rows[this.EditLocationGridView.SelectedIndex].Cells[8].Text;
            this.LatitudeHiddenField.Value = this.EditLocationGridView.Rows[this.EditLocationGridView.SelectedIndex].Cells[5].Text;
            this.LongitudeHiddenField.Value = this.EditLocationGridView.Rows[this.EditLocationGridView.SelectedIndex].Cells[6].Text;

            var media = dataAccess.getMediaByTourLocationID(locationID);
            LocationImageUploadDelete.Value = "0";
            int numOfMedia = dataAccess.CountImagesMediaByTourLocationID(locationID);
            LocationFileUpload.Attributes.Remove("maxlength");
            LocationFileUpload.Attributes.Add("maxlength", (5 - numOfMedia).ToString());
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    if (!this.DeletedImagesHiddenField.Value.Contains(mediaRow.MediaURL.Split('/')[2]))
                    {
                        String img = locationID + "/" + mediaRow.MediaURL.Split('/')[2];
                        CurrentImagesHiddenField.Value += img+";";
                        LocationImagesList.InnerHtml += "<div class='location-images'  id='" + img 
                            + "' ><img class='itemImage' src='" + mediaRow.MediaURL + "'/><a class='location-delete-image' rel='" 
                            + img + "'><div class='location-close-image' title='close'></div></a></div>";
                    }
                }
                else
                {
                    this.LocationVideoTextBox.Text = mediaRow.MediaURL;
                }
            }
            
            if (this.AddedImagesHiddenField.Value.Contains(locationID.ToString() + "/"))
            {
                foreach (string img in this.AddedImagesHiddenField.Value.Split(';'))
                {
                    if (img.Split('/')[0].Equals(locationID.ToString()))
                    {
                        LocationImagesList.InnerHtml += "<div class='location-images'  id='" + img
                                    + "' ><img class='itemImage' src='../Temp_Media/" + img.Split('/')[1] + "'/><a class='location-delete-image' rel='"
                                    + img + "'><div class='location-close-image' title='close'></div></a></div>";
                    }
                }
            }

            if (this.AddedImagesForNewLocationHiddenField.Value.Contains(tourID+"+"+(seq+1)+"+-1/"))
            {
                foreach (string img in this.AddedImagesForNewLocationHiddenField.Value.Split('@'))
                {
                    if (img.Length > 0)
                    {
                        if ((img.Split('+')[0].Equals(tourID.ToString())) && (img.Split('+')[1].Equals((seq+1).ToString())))
                        {
                            foreach(string id in img.Split('+')[2].Split(';'))
                            {
                                if ((id.Length > 0) && !(this.DeletedImagesHiddenField.Value.Contains(id.Split('/')[1])))
                                {
                                    LocationImagesList.InnerHtml += "<div class='location-images'  id='" + id
                                        + "' ><img class='itemImage' src='../Temp_Media/" + id.Split('/')[1] + "'/><a class='location-delete-image' rel='"
                                        + id + "'><div class='location-close-image' title='close'></div></a></div>";
                                }
                            }
                        }
                    }
                }
            }

            this.AddNewLinkButton.Text = "Update";
            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            this.LocationButtonMultiView.ActiveViewIndex = 0;
            this.LocationMultiView.ActiveViewIndex = 1;
        }

        protected void LocationGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(LocationGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void LocationGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.LocationGridView.SelectedIndex >= 0)
            {
                this.ViewLocationDetailButton.Enabled = true;
            }
        }

        protected void ViewListLinkButton_Click(object sender, EventArgs e)
        {
            this.ViewListLinkButton.BackColor = Color.LightGray;
            this.ViewDetailLinkButton.BackColor = Color.Gray;
            this.ViewLocationDetailButton.Enabled = false;

            this.LocationGridView.DataBind();
            this.LocationGridView.SelectedIndex = -1;

            this.LocationGridView.SelectedIndex = -1;
            this.LocationViewMultiView.ActiveViewIndex = 0;
        }

        protected void ViewLocationDetailButton_Click(object sender, EventArgs e)
        {
            locationImages.InnerHtml = "";
            locationVideo.InnerHtml = "";
            int selectedLocationID = Convert.ToInt32(this.LocationGridView.SelectedDataKey.Value);

            bool hasVideo = false;
            bool hasImages = false;
            var media = dataAccess.getMediaByTourLocationID(selectedLocationID);
            string[] separator = { "v=" };

            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    locationImages.InnerHtml += "<img class='itemImage' src='" + mediaRow.MediaURL + "' />";
                    hasImages = true;
                }
                else
                {
                    locationVideo.InnerHtml = "<iframe width='460' height='260' src='http://www.youtube.com/embed/" 
                        + mediaRow.MediaURL.Split(separator, StringSplitOptions.None)[1].Substring(0, 11)
                        + "' frameborder='0' allowfullscreen></iframe>";
                    hasVideo = true;
                }
            }

            if (!hasImages)
                locationImages.InnerHtml = "This Location does not have any images.";

            if (!hasVideo)
                locationVideo.InnerHtml = "This Location does not have any video.";

            DAL.CMSDBDataSet.TourLocationRow location = dataAccess.getTourLocationByTourLocationID(selectedLocationID);
            this.LocationNameLabel.Text = location["LocationName"].ToString();
            this.LocationSeqLabel.Text = location["TourSeqNum"].ToString();
            this.LocationPostcodeLabel.Text = location["Postcode"].ToString();
            this.LocationAddressLabel.Text = location["Address"].ToString();

            this.ViewListLinkButton.BackColor = Color.Gray;
            this.ViewDetailLinkButton.BackColor = Color.LightGray;
            this.LocationViewMultiView.ActiveViewIndex = 1;

        }

        protected void LocationUploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                LocationImageUploadStatusLabel.Text = "";

                int count = TempAddedImagesHiddenField.Value != "" ? TempAddedImagesHiddenField.Value.Split(';').Length : 0;
                if (count - 1 > 0 || LocationImageUploadDelete.Value == "1")
                {
                    LocationImagesList.InnerHtml = "";
                    int countExisting = CurrentImagesHiddenField.Value.Split(';').Length;
                    for (int x = 0; x < countExisting - 1; x++)
                    {
                        LocationImagesList.InnerHtml += "<div class='location-images'  id='" 
                            + CurrentImagesHiddenField.Value.Split(';')[x] 
                            + "' ><img class='itemImage' src='../Media/" 
                            + CurrentImagesHiddenField.Value.Split(';')[x].Split('/')[1] 
                            + "' id='" + CurrentImagesHiddenField.Value.Split(';')[x]
                            + "' /><a class='location-upload-images' rel='" + CurrentImagesHiddenField.Value.Split(';')[x]
                            + "'><div class='location-close-image ' title='close'></div></a></div>";
                    }
                }
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = TempAddedImagesHiddenField.Value.Split(';')[i];
                    string target = "<div class='location-images'  id='" + filename + "' ><img class='itemImage' src='../Temp_Media/" 
                        + filename.Split('/')[1] + "' id='" + filename + "' /><a class='location-upload-images' rel='" 
                        + filename + "'><div class='location-close-image ' title='close'></div></a></div>";
                    LocationImagesList.InnerHtml += target;
                }

                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                string locationID;

                if(this.SelectedLocationIDHiddenField.Value.Length == 0)
                    locationID = "-1";
                else
                    locationID = this.SelectedLocationIDHiddenField.Value;

                bool uploadStatus = true;

                for (int i = 0; i < hfc.Count; i++)
                {
                    if (hfc.AllKeys[i].Equals("ctl00$MainContent$LocationFileUpload"))
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
                        if (hfc.AllKeys[i].Equals("ctl00$MainContent$LocationFileUpload"))
                        {
                            HttpPostedFile hpf = hfc[i];
                            if (hpf.ContentLength < 51200)
                            {
                                Random rand = new Random((int)DateTime.Now.Ticks);
                                int numIterations = 0;
                                numIterations = rand.Next(1000000000, 2147483647);
                                Guid id = new Guid();

                                //-- Create new GUID and echo to the console
                                id = Guid.NewGuid();
                                hpf.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + id.ToString() + hpf.FileName);
                                String newImage = locationID + "/" + numIterations.ToString() + id.ToString() + hpf.FileName + ';';
                                if (locationID.Equals("-1"))
                                    TempAddedImagesForNewLocationHiddenField.Value += newImage;
                                else
                                    TempAddedImagesHiddenField.Value += newImage;
                                LocationImagesList.InnerHtml += "<div class='location-images'  id='" + newImage.Split(';')[0]
                                    + "' ><img class='itemImage' src='../Temp_Media/" + newImage.Split(';')[0].Split('/')[1]
                                    + "' id='" + newImage.Split(';')[0] + "' /><a class='location-upload-images' rel='" + newImage.Split(';')[0]
                                    + "'><div class='location-close-image ' title='close'></div></a></div>";
                            }                           
                        }                        
                    }
                    FileUpload.Attributes.Remove("maxlength");
                    FileUpload.Attributes.Add("maxlength", (5 - hfc.Count).ToString());
                    LocationImageUploadStatusLabel.Text = "Uploaded Successfully.";
                }
                else
                {
                    LocationImageUploadStatusLabel.Text = "One of the files is larger than 50 kb! Please try again.";
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }
    }
}