using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

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
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(Convert.ToInt32(this.TourGridView.SelectedDataKey.Value));
            this.NameDataLabel.Text = tour["TourName"].ToString();
            this.PhoneDataLabel.Text = tour["TourPhone"].ToString();
            this.EmailDataLabel.Text = tour["TourEmail"].ToString();
            this.WebsiteDataLabel.Text = tour["TourWebsite"].ToString();
            this.CostDataLabel.Text = tour["TourCost"].ToString();
            this.DescriptionDataLabel.Text = tour["TourDetail"].ToString();
            this.TourIDHiddenField.Value = tour["TourID"].ToString();
            this.TourMultiView.ActiveViewIndex = 0;
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {

            this.EditTitleLabel.Text = "Insert New Tour";
            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            this.LocationMultiView.ActiveViewIndex = 1;
            this.ButtonMultiView.ActiveViewIndex = 1;
            this.TourMultiView.ActiveViewIndex = 1;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(Convert.ToInt32(this.TourIDHiddenField.Value));
            this.EditTitleLabel.Text = "Update Tour";
            this.NameTextBox.Text = tour["TourName"].ToString();
            this.PhoneTextBox.Text = tour["TourPhone"].ToString();
            this.EmailTextBox.Text = tour["TourEmail"].ToString();
            this.WebsiteTextBox.Text = tour["TourWebsite"].ToString();
            this.CostTextBox.Text = tour["TourCost"].ToString();
            this.DescriptionTextBox.Text = tour["TourDetail"].ToString();
            this.EditLocationGridView.DataBind();

            this.LocationMultiView.ActiveViewIndex = 0;
            this.ButtonMultiView.ActiveViewIndex = 0;
            this.TourMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

        }

        //submit update
        protected void SubmitButton_Click(object sender, EventArgs e)
        {

        }

        //cancel update
        protected void CancelButton_Click(object sender, EventArgs e)
        {

        }

        //confirm insert
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            
        }

        //cancel insert
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void numberInputValidate(object sender, ServerValidateEventArgs e)
        {
        }

        protected void ViewLinkButton_Click(object sender, EventArgs e)
        {
            this.ViewLinkButton.BackColor = Color.LightGray;
            this.AddNewLinkButton.BackColor = Color.Gray;
            this.LocationMultiView.ActiveViewIndex = 0;
        }

        protected void AddNewLinkButton_Click(object sender, EventArgs e)
        {
            this.AddressTextBox.Text = "";
            this.PostcodeTextBox.Text = "";
            this.LocatinNameTextBox.Text = "";
            this.SequenceTextBox.Text = "";

            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            this.LocationButtonMultiView.ActiveViewIndex = 1;
            this.LocationMultiView.ActiveViewIndex = 1;
        }

        protected void SequenceTextBox_CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Int32 num;
            if (Int32.TryParse(this.SequenceTextBox.Text, out num))
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void LocatinNameTextBox_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.LocatinNameTextBox.Text.Length > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void PostcodeTextBox_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Int32 num;
            if (Int32.TryParse(this.PostcodeTextBox.Text, out num) && (this.PostcodeTextBox.Text.Length==4))
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
            if(Decimal.TryParse(this.CostTextBox.Text, out num))
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void UpdateLocationButton_Click(object sender, EventArgs e)
        {

        }

        protected void CancelUpdateLocationButton_Click(object sender, EventArgs e)
        {
            this.LocationMultiView.ActiveViewIndex = 0;
        }

        protected void InsertLocationButton_Click(object sender, EventArgs e)
        {
            if(this.AddressTextBox_CustomValidator.IsValid && this.CostTextBox_CustomValidator.IsValid 
                && this.LocatinNameTextBox_CustomValidator.IsValid && this.PostcodeTextBox_CustomValidator.IsValid
                && this.SequenceTextBox_CustomValidator3.IsValid)
            {
                int tourID = Convert.ToInt32(this.TourIDHiddenField.Value);
                Decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                short sequence = Convert.ToInt16(this.SequenceTextBox.Text);
                short postcode = Convert.ToInt16(this.PostcodeTextBox.Text);
                double lat = Convert.ToDouble(this.LatitudeHiddenField.Value);
                double lon = Convert.ToDouble(this.LongitudeHiddenField.Value);
                String suburb = this.AddressTextBox.Text.Remove(0, this.AddressTextBox.Text.IndexOf(",")+2);
                suburb = this.AddressTextBox.Text.Substring(0, suburb.IndexOf(","));

                dataAccess.insertTourLocation(tourID, sequence, this.LocatinNameTextBox.Text, lat, lon, this.AddressTextBox.Text,
                    suburb, postcode);
                this.EditLocationGridView.DataBind();

                this.ViewLinkButton.BackColor = Color.LightGray;
                this.AddNewLinkButton.BackColor = Color.Gray;
                this.LocationMultiView.ActiveViewIndex = 0;
            }
        }

        protected void CancelInsertLocationButton_Click(object sender, EventArgs e)
        {
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

    }
}