using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace CMS.CMSPages
{
    public partial class POI : System.Web.UI.Page
    {
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                this.AddressMultiView.ActiveViewIndex = 0;
        }

        protected void POIGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(POIGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void POIGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            poiImages.InnerHtml = "";
            poiVideo.InnerHtml = "";
            int id = (Int32)this.POIGridView.SelectedDataKey.Value;
            DAL.CMSDBDataSet.POIItemRow row = dataAccess.getPOIByItemID(id);

            this.NameDataLabel.Text = row["ItemName"].ToString();
            this.CategoryDataLabel.Text = row["CategoryName"].ToString();
            this.SubtypeDataLabel.Text = row["SubtypeName"].ToString();
            this.PhoneDataLabel.Text = row["Phone"].ToString();
            this.EmailDataLabel.Text = row["Email"].ToString();
            this.WebsiteDataLabel.Text = row["Website"].ToString();
            this.OpeningHoursDataLabel.Text = row["OpeningHours"].ToString();
            this.CostDataLabel.Text = row["Cost"].ToString();
            this.RatingData.CurrentRating = Convert.ToInt32(row["Rating"].ToString());
            this.DescriptionDataLabel.Text = row["Details"].ToString();
            this.PostcodeDataLabel.Text = row["Postcode"].ToString();
            this.AddressDataLabel.Text = row["Address"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();

            this.CategoryIDHiddenField.Value = row["CategoryID"].ToString();
            this.SubtypeIDHiddenField.Value = row["SubtypeID"].ToString();

            this.POIMultiView.ActiveViewIndex = 0;
            bool hasVideo=false;
            bool hasImages = false;
            var media = dataAccess.getMediaByItemID(id);
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
                    poiVideo.InnerHtml = "<iframe width='560' height='315' src='http://www.youtube.com/embed/" + mediaRow.MediaURL.Split(separator, StringSplitOptions.None)[1].Substring(0, 11)
                                        + "' frameborder='0' allowfullscreen></iframe>";
                    //poiVideo.InnerHtml="<iframe width='560' height='315' src='http://www.youtube.com/embed/g8evyE9TuYk' frameborder='0' allowfullscreen></iframe>";
                    hasVideo = true;               
                }
            }
            
            if(!hasImages){
                poiImages.InnerHtml="This POI does not have any images.";
            }

            if(!hasVideo){
                poiVideo.InnerHtml = "This POI does not have any video.";
            }
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {           
            this.NameTextBox.Text = "";
            this.CategoryDropDownList.DataBind();
            this.SubtypeDropDownList.DataBind();
            this.PhoneTextBox.Text = "";
            this.EmailTextBox.Text = "";
            this.WebsiteTextBox.Text = "";
            this.OpeningHoursTextBox.Text = "";
            this.CostTextBox.Text = "";
            this.Rating.CurrentRating = 0;
            this.DescriptionTextBox.Text = "";
            this.PostcodeTextBox.Text = "";
            this.AddressTextBox.Text = "";

            this.ButtonMultiView.ActiveViewIndex = 1;
            this.POIMultiView.ActiveViewIndex = 1;            
            
        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if ((this.DescriptionTextBox.Text.Length > 0) && (this.CostTextBox.Text.Length > 0)
                && this.AutoAddressTextBox_CustomValidator.IsValid && this.ManualAddressTextBox_CustomValidator.IsValid)
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
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                int categotyID = Convert.ToInt32(this.CategoryDropDownList.SelectedValue);
                int? subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                int newItemId= dataAccess.InsertPOI(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.Rating.CurrentRating, this.PhoneTextBox.Text, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, address, latitude, longitude, postCode, suburb, subtypeID, categotyID);
                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count-1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(newItemId, "../Media/" + filename, "Images");
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(newItemId, VideoTextBox.Text, "Video");
                }

                this.POIGridView.DataBind();
                this.POIMultiView.ActiveViewIndex = -1;
            }
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.POIMultiView.ActiveViewIndex = -1;
        }
        
        protected void numberInputValidate(object sender, ServerValidateEventArgs e)
        {
            String senderID = ((CustomValidator)sender).ID;
            bool isNum = false;
            if (senderID.Equals("CostTextBox_CustomValidator"))
            {
                Decimal num;
                isNum = Decimal.TryParse(this.CostTextBox.Text, out num);          
            }
            if (senderID.Equals("PostcodeTextBox_CustomValidator") && (this.PostcodeTextBox.Text.Length==4))
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
            this.AddressMultiView.ActiveViewIndex = 0;
            this.ManualLatTextBox.Text = "";
            this.ManualLogTextBox.Text = "";
            this.ManualStNoTextBox.Text = "";
            this.ManualStNameTextBox.Text = "";
            this.ManualSuburbTextBox.Text = "";
            poiImagesAddUpdate.InnerHtml = "";
            int id = (Int32)this.POIGridView.SelectedDataKey.Value;
            DAL.CMSDBDataSet.POIItemRow row = dataAccess.getPOIByItemID(id);
            this.NameTextBox.Text = row["ItemName"].ToString();
            this.CategoryDropDownList.SelectedValue = row["CategoryID"].ToString();
            this.SubtypeDropDownList.SelectedValue = row["SubtypeID"].ToString();
            this.PhoneTextBox.Text = row["Phone"].ToString();
            this.EmailTextBox.Text = row["Email"].ToString();
            this.WebsiteTextBox.Text = row["Website"].ToString();
            this.OpeningHoursTextBox.Text = row["OpeningHours"].ToString();
            this.CostTextBox.Text = row["Cost"].ToString();
            this.Rating.CurrentRating = Convert.ToInt32(row["Rating"].ToString());
            this.DescriptionTextBox.Text = row["Details"].ToString();
            this.PostcodeTextBox.Text = row["Postcode"].ToString();
            this.AddressTextBox.Text = row["Address"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();
            var media = dataAccess.getMediaByItemID(id);
            foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
            {
                if (mediaRow.MediaType == "Images")
                {
                    poiImagesAddUpdate.InnerHtml += "<img class='itemImage' src='" + mediaRow.MediaURL + "' id='" + mediaRow.MediaURL + "' />";
                }
                else
                {
                    this.VideoTextBox.Text = mediaRow.MediaURL;
                }

            }
            this.ButtonMultiView.ActiveViewIndex = 0;
            this.POIMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int itemID = (Int32)this.POIGridView.SelectedDataKey.Value;
            dataAccess.DeleteMediaByItemID(itemID);
            dataAccess.DeletePOI(itemID, Convert.ToInt32(this.CategoryIDHiddenField.Value),
                                Convert.ToInt32(this.SubtypeIDHiddenField.Value));
            this.POIGridView.DataBind();
            this.POIMultiView.ActiveViewIndex = -1;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if ((this.DescriptionTextBox.Text.Length > 0) && (this.CostTextBox.Text.Length > 0)
                && this.AutoAddressTextBox_CustomValidator.IsValid && this.ManualAddressTextBox_CustomValidator.IsValid)
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
                decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int categotyID = Convert.ToInt32(this.CategoryDropDownList.SelectedValue);
                int? subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                int originalCategoryID = Convert.ToInt32(this.CategoryIDHiddenField.Value);
                int? originalSubtypeID = Convert.ToInt32(this.SubtypeIDHiddenField.Value);
                int itemID = Convert.ToInt32(this.POIGridView.SelectedDataKey.Value);
                dataAccess.UpdatePOI(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.Rating.CurrentRating, this.PhoneTextBox.Text, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, address, latitude, longitude, postCode,
                    suburb, subtypeID, categotyID, originalSubtypeID, itemID, originalCategoryID);
                dataAccess.DeleteMediaByItemID(itemID);

                var media = dataAccess.getMediaByItemID(itemID);
                foreach (DAL.CMSDBDataSet.MediaRow mediaRow in media.Rows)
                {
                    if (mediaRow.MediaType == "Images")
                    {
                        System.IO.File.Delete(Server.MapPath("~/Media/" + mediaRow.MediaURL.Split('/')[2]));
                    }
                    
                }
                
                int count = ImageUploadFileName.Value.Split(';').Length;
                for (int i = 0; i < count - 1; i++)
                {
                    string filename = ImageUploadFileName.Value.Split(';')[i];
                    dataAccess.InsertMedia(itemID, "../Media/" + filename, "Images");
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + filename), Server.MapPath("~/Media/" + filename));
                }

                if (VideoTextBox.Text.Length > 0)
                {
                    dataAccess.InsertMedia(itemID, VideoTextBox.Text, "Video");
                }
                this.POIGridView.DataBind();
                this.POIMultiView.ActiveViewIndex = -1;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.POIMultiView.ActiveViewIndex = 0;
        }
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                StatusLabel.Text = "";
                poiImagesAddUpdate.InnerHtml += "";
                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                DeleteAllTempFiles();
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentType == "image/jpeg" ||
                       hpf.ContentType == "image/png" ||
                       hpf.ContentType == "image/gif")
                    {
                        if (hpf.ContentLength < 51200)
                        {

                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            Guid id = new Guid();

                            //-- Create new GUID and echo to the console
                            id = Guid.NewGuid();
                            hpf.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + id.ToString() + hpf.FileName);
                            ImageUploadFileName.Value = ImageUploadFileName.Value + numIterations.ToString() + id.ToString() + hpf.FileName + ';';
                            poiImagesAddUpdate.InnerHtml += "<img class='itemImage' src='../Temp_Media/" + numIterations.ToString() + id.ToString() + hpf.FileName + "' id='"+numIterations.ToString() + id.ToString() + hpf.FileName+"' />";
                        }
                        else
                        {
                            StatusLabel.Text = "One of the files is larger than 50 kb! Please try again.";
                            DeleteAllTempFiles();
                            poiImagesAddUpdate.InnerHtml = "";
                            break;
                        }
                    }
                    else
                    {
                        StatusLabel.Text = "Only JPEG, PNG and GIF files are accepted!";
                        DeleteAllTempFiles();
                        poiImagesAddUpdate.InnerHtml = "";
                        break;
                    }
                    StatusLabel.Text = "Uploaded Successfully.";
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
    }
}