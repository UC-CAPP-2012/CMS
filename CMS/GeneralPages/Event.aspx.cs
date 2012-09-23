using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace CMS.CMSPages
{
    public partial class Event : System.Web.UI.Page
    {
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {

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
            int id = (Int32)this.EventGridView.SelectedDataKey.Value;
            DAL.CMSDBDataSet.EventItemRow row = dataAccess.getEventByItemID(id);

            this.NameDataLabel.Text = row["ItemName"].ToString();
            this.SubtypeDataLabel.Text = row["SubtypeName"].ToString();
            this.PhoneDataLabel.Text = row["Phone"].ToString();
            this.EmailDataLabel.Text = row["Email"].ToString();
            this.WebsiteDataLabel.Text = row["Website"].ToString();
            this.StartDateDataLabel.Text = Convert.ToDateTime(row["EventStartDate"].ToString()).ToString("dd/MMMM/yyyy");
            this.EndDateDataLabel.Text = Convert.ToDateTime(row["EventEndDate"].ToString()).ToString("dd/MMMM/yyyy");
            this.OpeningHoursDataLabel.Text = row["OpeningHours"].ToString();
            this.CostDataLabel.Text = row["Cost"].ToString();
            this.RatingData.CurrentRating = Convert.ToInt32(row["Rating"].ToString());
            this.DescriptionDataLabel.Text = row["Details"].ToString();
            this.PostcodeDataLabel.Text = row["Postcode"].ToString();
            this.AddressDataLabel.Text = row["StreetNo"].ToString() + " " + row["StreetName"].ToString()
                                        + ", " + row["Suburb"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();

            this.SubtypeIDHiddenField.Value = row["SubtypeID"].ToString();

            this.EventMultiView.ActiveViewIndex = 0;
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {

            this.NameTextBox.Text = "";
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
            this.EventMultiView.ActiveViewIndex = 1;
        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            if ((this.NameTextBox.Text.Length > 0) && (this.DescriptionTextBox.Text.Length > 0)
                && (this.CostTextBox.Text.Length > 0) && (this.LatitudeHiddenField.Value.Length > 0)
                && (this.LongitudeHiddenField.Value.Length > 0))
            {
                // get streetNo, streetName, suburb from address
                String address = this.AddressTextBox.Text;
                String streetNo = address.Substring(0, address.IndexOf(" "));
                int a = address.IndexOf(" ");
                address = address.Remove(0, address.IndexOf(" ") + 1);
                String streetName = address.Substring(0, address.IndexOf(","));
                address = address.Remove(0, address.IndexOf(",") + 2);
                String suburb = address.Substring(0, address.IndexOf(","));

                //convert inputs into correct format.
                decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                int? phoneNo = Convert.ToInt32(this.PhoneTextBox.Text);
                double latitude = Convert.ToDouble(this.LatitudeHiddenField.Value);
                double longitude = Convert.ToDouble(this.LongitudeHiddenField.Value);
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int? subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                DateTime startDate = Convert.ToDateTime(this.StartDateTextBox.Text);
                DateTime endDate = Convert.ToDateTime(this.EndDateTextBox.Text);

                dataAccess.InsertEvent(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.Rating.CurrentRating, phoneNo, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, streetNo, streetName, latitude, longitude, postCode,
                    suburb, subtypeID,startDate, endDate);

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
            if (senderID.Equals("CostTextBox_CustomValidator"))
            {
                Decimal num;
                isNum = Decimal.TryParse(this.CostTextBox.Text, out num);
            }
            if (senderID.Equals("PostcodeTextBox_CustomValidator") && (this.PostcodeTextBox.Text.Length == 4))
            {
                Int32 num;
                isNum = Int32.TryParse(this.PostcodeTextBox.Text, out num);
            }
            if (senderID.Equals("PhoneTextBox_CustomValidator"))
            {
                Int32 num;
                isNum = Int32.TryParse(this.PhoneTextBox.Text, out num);
            }
            e.IsValid = isNum;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.EventGridView.SelectedDataKey.Value;
            DAL.CMSDBDataSet.EventItemRow row = dataAccess.getEventByItemID(id);

            this.NameTextBox.Text = row["ItemName"].ToString();
            this.SubtypeDropDownList.SelectedValue = row["SubtypeID"].ToString();
            this.PhoneTextBox.Text = row["Phone"].ToString();
            this.EmailTextBox.Text = row["Email"].ToString();
            this.WebsiteTextBox.Text = row["Website"].ToString();
            this.StartDateTextBox_CalendarExtender.SelectedDate = Convert.ToDateTime(row["EventStartDate"].ToString());
            this.EndDateTextBox_CalendarExtender.SelectedDate = Convert.ToDateTime(row["EventEndDate"].ToString());
            this.OpeningHoursTextBox.Text = row["OpeningHours"].ToString();
            this.CostTextBox.Text = row["Cost"].ToString();
            this.Rating.CurrentRating = Convert.ToInt32(row["Rating"].ToString());
            this.DescriptionTextBox.Text = row["Details"].ToString();
            this.PostcodeTextBox.Text = row["Postcode"].ToString();
            this.AddressTextBox.Text = row["StreetNo"].ToString() + " " + row["StreetName"].ToString()
                                        + ", " + row["Suburb"].ToString();

            this.LatitudeHiddenField.Value = row["Latitude"].ToString();
            this.LongitudeHiddenField.Value = row["Longitude"].ToString();

            this.ButtonMultiView.ActiveViewIndex = 0;
            this.EventMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int itemID = (Int32)this.EventGridView.SelectedDataKey.Value;
            dataAccess.DeleteEvent(itemID, Convert.ToInt32(this.SubtypeIDHiddenField.Value));
            this.EventGridView.DataBind();
            this.EventMultiView.ActiveViewIndex = -1;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if ((this.NameTextBox.Text.Length > 0) && (this.DescriptionTextBox.Text.Length > 0)
                && (this.CostTextBox.Text.Length > 0) && (this.LatitudeHiddenField.Value.Length > 0)
                && (this.LongitudeHiddenField.Value.Length > 0))
            {
                // get streetNo, streetName, suburb from address
                String address = this.AddressTextBox.Text;
                String streetNo = address.Substring(0, address.IndexOf(" "));
                int a = address.IndexOf(" ");
                address = address.Remove(0, address.IndexOf(" ") + 1);
                String streetName = address.Substring(0, address.IndexOf(","));
                address = address.Remove(0, address.IndexOf(",") + 2);
                String suburb;
                if(address.Contains(","))
                    suburb = address.Substring(0, address.IndexOf(","));
                else
                    suburb = address;

                DateTimeFormatInfo format = new DateTimeFormatInfo();
                format.ShortDatePattern = "dd/MMMM/yyyy";
                format.DateSeparator = "/";

                DateTime startDate = Convert.ToDateTime(this.StartDateTextBox.Text, format);
                DateTime endDate = Convert.ToDateTime(this.EndDateTextBox.Text, format);

                //convert inputs into correct format.
                decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                int? phoneNo = Convert.ToInt32(this.PhoneTextBox.Text);
                double latitude = Convert.ToDouble(this.LatitudeHiddenField.Value);
                double longitude = Convert.ToDouble(this.LongitudeHiddenField.Value);
                int postCode = Convert.ToInt32(this.PostcodeTextBox.Text);
                int? subtypeID = Convert.ToInt32(this.SubtypeDropDownList.SelectedValue);
                int? originalSubtypeID = Convert.ToInt32(this.SubtypeIDHiddenField.Value);
                int itemID = Convert.ToInt32(this.EventGridView.SelectedDataKey.Value);

                dataAccess.UpdateEvent(this.NameTextBox.Text, this.DescriptionTextBox.Text,
                    cost, this.Rating.CurrentRating, phoneNo, this.WebsiteTextBox.Text, this.EmailTextBox.Text,
                    this.OpeningHoursTextBox.Text, streetNo, streetName, latitude, longitude, postCode,
                    suburb, subtypeID, startDate, endDate, originalSubtypeID, itemID);

                this.EventGridView.DataBind();
                this.EventMultiView.ActiveViewIndex = -1;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.EventMultiView.ActiveViewIndex = 0;
        }
    }
}