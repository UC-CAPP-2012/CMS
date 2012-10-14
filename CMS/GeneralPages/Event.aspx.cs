using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;

namespace CMS.CMSPages
{
    public partial class Event : System.Web.UI.Page
    {
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.AddressMultiView.ActiveViewIndex = 0;
        }

        protected void EventGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(EventGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void EventGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventImages.InnerHtml = "";
            eventVideo.InnerHtml = "";
            int id = (Int32)this.EventGridView.SelectedDataKey.Value;
            DAL.CMSDBDataSet.EventItemRow row = dataAccess.getEventByItemID(id);

            this.NameDataLabel.Text = row["ItemName"].ToString();
            this.SubtypeDataLabel.Text = row["SubtypeName"].ToString();
            if (row["MajorRegionID"].ToString() != "")
                this.MajorRegionDataLabel.Text = dataAccess.getMajorRegionName(Convert.ToInt32(row["MajorRegionID"].ToString()));
            else
                this.MajorRegionDataLabel.Text = "None";
            this.PhoneDataLabel.Text = row["Phone"].ToString();
            this.EmailDataLabel.Text = row["Email"].ToString();
            this.WebsiteDataLabel.Text = row["Website"].ToString();
            this.StartDateDataLabel.Text = Convert.ToDateTime(row["EventStartDate"].ToString()).ToString("dd/MMMM/yyyy");
            this.EndDateDataLabel.Text = Convert.ToDateTime(row["EventEndDate"].ToString()).ToString("dd/MMMM/yyyy");
            this.OpeningHoursDataLabel.Text = row["OpeningHours"].ToString();
            this.RatingData.CurrentRating = Convert.ToInt32(row["Cost"].ToString());
            if (this.RatingData.CurrentRating == 0)
                this.FreeRatingData.CurrentRating = 1;
            else
                this.FreeRatingData.CurrentRating = 0;
            this.DescriptionDataLabel.Text = row["Details"].ToString();
            this.PostcodeDataLabel.Text = row["Postcode"].ToString();
            this.AddressDataLabel.Text = row["Address"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();

            this.EventMultiView.ActiveViewIndex = 0;

            bool hasVideo = false;
            bool hasImages = false;
            var media = dataAccess.getMediaByItemID(id);
            string[] separator = { "v=" };
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    eventImages.InnerHtml += "<img class='itemImage' src='" + mediaRow.MediaURL + "' />";
                    hasImages = true;
                }
                else
                {
                    eventVideo.InnerHtml = "<iframe width='460' height='260' src='http://www.youtube.com/embed/" + mediaRow.MediaURL.Split(separator, StringSplitOptions.None)[1].Substring(0, 11)
                                                + "' frameborder='0' allowfullscreen></iframe>";
                    hasVideo = true;
                }
            }

            if (!hasImages)
            {
                eventImages.InnerHtml = "This POI does not have any images.";
            }

            if (!hasVideo)
            {
                eventVideo.InnerHtml = "This POI does not have any video.";
            }
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.EditTitleLabel.Text = "Insert New Event";
            this.NameTextBox.Text = "";
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
            this.AddressTextBox.Text = "";
            this.StatusLabel.Text = "";
            this.VideoTextBox.Text = "";
            ImageUploadFileName.Value = "";
            this.eventsImagesAddUpdate.InnerHtml = "";
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5).ToString());
            this.ButtonMultiView.ActiveViewIndex = 1;
            this.EventMultiView.ActiveViewIndex = 1;
        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                // get streetNo, streetName, suburb from address
                String address;
                String suburb;
                double latitude;
                double longitude;

                if (this.AddressMultiView.ActiveViewIndex == 0)
                {
                    address = this.AddressTextBox.Text;
                    suburb = address.Remove(0, address.IndexOf(",") + 2);
                    suburb = suburb.Substring(0, suburb.IndexOf(","));
                    latitude = Convert.ToDouble(this.LatitudeHiddenField.Value);
                    longitude = Convert.ToDouble(this.LongitudeHiddenField.Value);

                }
                else
                {
                    address = this.ManualStNoTextBox.Text + " " + this.ManualStNameTextBox.Text + ", "
                                    + this.ManualSuburbTextBox.Text + ", Australian Capital Territory";
                    suburb = this.ManualSuburbTextBox.Text;
                    latitude = Convert.ToDouble(this.ManualLatTextBox.Text);
                    longitude = Convert.ToDouble(this.ManualLogTextBox.Text);
                }

                //convert inputs into correct format.
                int cost = this.Rating.CurrentRating;
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int? subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                int? majorRegionID;
                if (this.MajorRegionDropDownList.SelectedValue != "")
                    majorRegionID = Convert.ToInt32(this.MajorRegionDropDownList.SelectedValue);
                else
                    majorRegionID = null;
               
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU");

                DateTime startDate = Convert.ToDateTime(this.StartDateTextBox.Text, culture);
                DateTime endDate = Convert.ToDateTime(this.EndDateTextBox.Text, culture);

                int newItemId = dataAccess.InsertEvent(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.PhoneTextBox.Text, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, address, latitude, longitude, postCode,
                    suburb, subtypeID, startDate, endDate, majorRegionID);

                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(newItemId, "../Media/" + filename, "Images", null);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(newItemId, VideoTextBox.Text, "Video", null);
                }
                CurrentImagesFileName.Value = "";
                this.EventGridView.DataBind();
                this.EventMultiView.ActiveViewIndex = -1;
            }
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.EventMultiView.ActiveViewIndex = -1;
        }

        protected void numberInputValidate(object sender, ServerValidateEventArgs e)
        {
            String senderID = ((CustomValidator)sender).ID;
            bool isNum = false;
            if (senderID.Equals("PostcodeTextBox_CustomValidator") && (this.PostcodeTextBox.Text.Length == 4))
            {
                Int32 num;
                isNum = Int32.TryParse(this.PostcodeTextBox.Text, out num);
            }
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
            if (senderID.Equals("ManualAddressTextBox_CustomValidator"))
            {
                Int32 intNum;
                double doubleNum;

                if (this.AddressMultiView.ActiveViewIndex == 0)
                {
                    isNum = true;
                }
                if ((this.AddressMultiView.ActiveViewIndex == 1)
                    && (this.ManualStNoTextBox.Text.Length > 0) && Int32.TryParse(this.ManualStNoTextBox.Text, out intNum)
                    && (this.ManualStNameTextBox.Text.Length > 0) && (this.ManualSuburbTextBox.Text.Length > 0)
                    && (this.ManualLatTextBox.Text.Length > 0) && Double.TryParse(this.ManualLatTextBox.Text, out doubleNum)
                    && (this.ManualLogTextBox.Text.Length > 0) && Double.TryParse(this.ManualLogTextBox.Text, out doubleNum))
                {
                    isNum = true;
                }
            }
            e.IsValid = isNum;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            CurrentImagesFileName.Value = "";
            StatusLabel.Text = "";
            ImageUploadFileName.Value = "";
            ImageDeleteFileName.Value = "";
            eventsImagesAddUpdate.InnerHtml = "";
            this.EditTitleLabel.Text = "Update Event";
            this.AddressMultiView.ActiveViewIndex = 0;
            this.ManualLatTextBox.Text = "";
            this.ManualLogTextBox.Text = "";
            this.ManualStNoTextBox.Text = "";
            this.ManualStNameTextBox.Text = "";
            this.ManualSuburbTextBox.Text = "";
            int id = (Int32)this.EventGridView.SelectedDataKey.Value;
            DAL.CMSDBDataSet.EventItemRow row = dataAccess.getEventByItemID(id);

            this.NameTextBox.Text = row["ItemName"].ToString();
            this.SubtypeDropDownList.SelectedValue = row["SubtypeID"].ToString();
            if (row["MajorRegionID"].ToString() != "")
                this.MajorRegionDropDownList.SelectedValue = row["MajorRegionID"].ToString();
            else
                this.MajorRegionDropDownList.DataBind();
            this.PhoneTextBox.Text = row["Phone"].ToString();
            this.EmailTextBox.Text = row["Email"].ToString();
            this.WebsiteTextBox.Text = row["Website"].ToString();
            this.StartDateTextBox_CalendarExtender.SelectedDate = Convert.ToDateTime(row["EventStartDate"].ToString());
            this.EndDateTextBox_CalendarExtender.SelectedDate = Convert.ToDateTime(row["EventEndDate"].ToString());
            this.OpeningHoursTextBox.Text = row["OpeningHours"].ToString();
            this.Rating.CurrentRating = Convert.ToInt32(row["Cost"].ToString());
            if (this.Rating.CurrentRating == 0)
                this.FreeRating.CurrentRating = 1;
            else
                this.FreeRating.CurrentRating = 0;
            this.DescriptionTextBox.Text = row["Details"].ToString();
            this.PostcodeTextBox.Text = row["Postcode"].ToString();
            this.AddressTextBox.Text = row["Address"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();

            int numOfMedia = dataAccess.CountImagesMediaByItemId(id);
            FileUpload.Attributes.Remove("maxlength");
            FileUpload.Attributes.Add("maxlength", (5 - numOfMedia).ToString());

            var media = dataAccess.getMediaByItemID(id);
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    CurrentImagesFileName.Value += mediaRow.MediaURL.Split('/')[2] + ';';
                    eventsImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + mediaRow.MediaURL.Split('/')[2] + "' ><img class='itemImage' src='" + mediaRow.MediaURL + "'/><a class='delete-image' rel='" + mediaRow.MediaURL.Split('/')[2] + "'><div class='close_image' title='close'></div></a></div>";
                }
                else
                {
                    this.VideoTextBox.Text = mediaRow.MediaURL;
                }

            }

            this.ButtonMultiView.ActiveViewIndex = 0;
            this.EventMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int itemID = (Int32)this.EventGridView.SelectedDataKey.Value;
            dataAccess.DeleteMediaByItemID(itemID);
            dataAccess.DeleteEvent(itemID);
            this.EventGridView.DataBind();
            this.EventMultiView.ActiveViewIndex = -1;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                // get streetNo, streetName, suburb from address
                String address;
                String suburb;
                double latitude;
                double longitude;

                if (this.AddressMultiView.ActiveViewIndex == 0)
                {
                    address = this.AddressTextBox.Text;
                    suburb = address.Remove(0, address.IndexOf(",") + 2);
                    suburb = suburb.Substring(0, suburb.IndexOf(","));
                    latitude = Convert.ToDouble(this.LatitudeHiddenField.Value);
                    longitude = Convert.ToDouble(this.LongitudeHiddenField.Value);

                }
                else
                {
                    address = this.ManualStNoTextBox.Text + " " + this.ManualStNameTextBox.Text + ", "
                        + this.ManualSuburbTextBox.Text + ", Australian Capital Territory";
                    suburb = this.ManualSuburbTextBox.Text;
                    latitude = Convert.ToDouble(this.ManualLatTextBox.Text);
                    longitude = Convert.ToDouble(this.ManualLogTextBox.Text);
                }

                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU");

                DateTime startDate = Convert.ToDateTime(this.StartDateTextBox.Text, culture);
                DateTime endDate = Convert.ToDateTime(this.EndDateTextBox.Text, culture);

                //convert inputs into correct format.
                int cost = this.Rating.CurrentRating;
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int? subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                int? majorRegionID;
                if (this.MajorRegionDropDownList.SelectedValue != "")
                    majorRegionID = Convert.ToInt32(this.MajorRegionDropDownList.SelectedValue);
                else
                    majorRegionID = null;
                int itemID = Convert.ToInt32(this.EventGridView.SelectedDataKey.Value);

                dataAccess.UpdateEvent(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.PhoneTextBox.Text, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, address, latitude, longitude, postCode,
                    suburb, subtypeID, startDate, endDate, majorRegionID, itemID);

                int imageDeleteCount = ImageDeleteFileName.Value != "" ? ImageDeleteFileName.Value.Split(';').Length : 0;
                for (int y = 0; y < imageDeleteCount - 1; y++)
                {
                    string deletedFilename = ImageDeleteFileName.Value.Split(';')[y];
                    dataAccess.DeleteMediaByMediaURL(itemID, "../Media/" + deletedFilename);
                    System.IO.File.Delete(Server.MapPath("~/Media/" + deletedFilename));
                }

                int count = ImageUploadFileName.Value != "" ? ImageUploadFileName.Value.Split(';').Length : 0;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(itemID, "../Media/" + filename, "Images", null);
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                dataAccess.DeleteVideoMediaByItemId(itemID);

                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(itemID, VideoTextBox.Text, "Video", null);
                }
                this.EventGridView.DataBind();
                this.EventMultiView.ActiveViewIndex = -1;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.EventMultiView.ActiveViewIndex = 0;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.Text = "";

                int count = ImageUploadFileName.Value != "" ? ImageUploadFileName.Value.Split(';').Length : 0;
                if (count - 1 > 0 || ImageUploadDelete.Value == "1")
                {
                    eventsImagesAddUpdate.InnerHtml = "";
                    int countExisting = CurrentImagesFileName.Value.Split(';').Length;
                    for (int x = 0; x < countExisting - 1; x++)
                    {
                        eventsImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + CurrentImagesFileName.Value.Split(';')[x] + "' ><img class='itemImage' src='../Media/" +
                        CurrentImagesFileName.Value.Split(';')[x] + "' id='" + CurrentImagesFileName.Value.Split(';')[x] + "' /><a class='upload-images' rel='" + CurrentImagesFileName.Value.Split(';')[x] + "'><div class='close_image ' title='close'></div></a></div>";
                    }
                }
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    string target = "<div class='poi-images'  id='" + filename + "' ><img class='itemImage' src='../Temp_Media/" +
                        filename + "' id='" + filename + "' /><a class='upload-images' rel='" + filename + "'><div class='close_image ' title='close'></div></a></div>";
                    eventsImagesAddUpdate.InnerHtml += target;
                }

                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                bool uploadStatus = true;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    if (!(hpf.ContentLength < 51200))
                    {
                        uploadStatus = false;
                    }
                }
                if (uploadStatus)
                {
                    for (int i = 0; i < hfc.Count; i++)
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
                        eventsImagesAddUpdate.InnerHtml += "<div class='poi-images'  id='" + numIterations.ToString() + id.ToString() + hpf.FileName + "' ><img class='itemImage' src='../Temp_Media/" + numIterations.ToString() + id.ToString() +
                            hpf.FileName + "' id='" + numIterations.ToString() + id.ToString() + hpf.FileName + "' /><a class='upload-images' rel='" + numIterations.ToString() + id.ToString() + hpf.FileName + "'><div class='close_image ' title='close'></div></a></div>";
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

        protected void AutoLinkButton_Click(object sender, EventArgs e)
        {
            this.AutoLinkButton.BackColor = Color.LightGray;
            this.ManualLinkButton.BackColor = Color.Gray;
            this.AddressMultiView.ActiveViewIndex = 0;
        }

        protected void ManualLinkButton_Click(object sender, EventArgs e)
        {
            this.AutoLinkButton.BackColor = Color.Gray;
            this.ManualLinkButton.BackColor = Color.LightGray;
            this.AddressMultiView.ActiveViewIndex = 1;
        }

        protected void FreeRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            this.Rating.CurrentRating = 0;
        }


        protected void Rating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            this.FreeRating.CurrentRating = 0;
        }

        protected void MajorRegionDropDownList_DataBound(object sender, EventArgs e)
        {
            ListItem item = new ListItem("----- none -----", "");
            this.MajorRegionDropDownList.Items.Insert(0, item);
        }

        protected void DateValidation(object sender, ServerValidateEventArgs e)
        {
            bool isOK = true;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU");
            DateTime datetime = new DateTime(); 
            DateTime? start = null;
            DateTime? end = null;

            if (!DateTime.TryParse(StartDateTextBox.Text, out datetime))
            {
                isOK = false;
            }
            else
            {
                start = Convert.ToDateTime(StartDateTextBox.Text, culture);
            }

            if (!DateTime.TryParse(EndDateTextBox.Text, out datetime))
            {
                isOK = false;
                this.EndDateTextBoxCustomValidator.ErrorMessage = "Invalid end date is entered";
            }
            else
            {
                end = Convert.ToDateTime(EndDateTextBox.Text, culture);
                if ((start != null) && (start > end))
                {
                    isOK = false;
                    this.EndDateTextBoxCustomValidator.ErrorMessage = "End date cannot be earlier than start date.";
                }
            }

            e.IsValid = isOK;
        }
    }
}