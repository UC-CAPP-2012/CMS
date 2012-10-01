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
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(Convert.ToInt32(this.TourGridView.SelectedDataKey.Value));
            this.NameDataLabel.Text = tour["TourName"].ToString();
            this.PhoneDataLabel.Text = tour["TourPhone"].ToString();
            this.EmailDataLabel.Text = tour["TourEmail"].ToString();
            this.WebsiteDataLabel.Text = tour["TourWebsite"].ToString();
            this.CostDataLabel.Text = tour["TourCost"].ToString();
            this.DescriptionDataLabel.Text = tour["TourDetail"].ToString();
            this.TourIDHiddenField.Value = tour["TourID"].ToString();
            this.LocationGridView.DataBind();            
            this.TourMultiView.ActiveViewIndex = 0;
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

            this.TourIDHiddenField.Value = "";
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
            locationTable = dataAccess.getTourLocationByTourID(Convert.ToInt32(this.TourIDHiddenField.Value));
            this.EditLocationGridView.DataSource = locationTable;
            this.EditLocationGridView.DataBind();
            DAL.CMSDBDataSet.TourRow tour = dataAccess.getTourByID(Convert.ToInt32(this.TourIDHiddenField.Value));
            this.EditTitleLabel.Text = "Update Tour";
            this.NameTextBox.Text = tour["TourName"].ToString();
            this.PhoneTextBox.Text = tour["TourPhone"].ToString();
            this.EmailTextBox.Text = tour["TourEmail"].ToString();
            this.WebsiteTextBox.Text = tour["TourWebsite"].ToString();
            this.CostTextBox.Text = tour["TourCost"].ToString();
            this.DescriptionTextBox.Text = tour["TourDetail"].ToString();            

            this.LocationMultiView.ActiveViewIndex = 0;
            this.ButtonMultiView.ActiveViewIndex = 0;
            this.TourMultiView.ActiveViewIndex = 1;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int tourID = Convert.ToInt32(this.TourGridView.SelectedDataKey.Value);
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
                int TourID = Convert.ToInt32(this.TourIDHiddenField.Value);

                dataAccess.updateTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourID);

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
                            dataAccess.deleteTourLocationByLocationID(Convert.ToInt32(id));
                        }
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

        }

        protected void numberInputValidate(object sender, ServerValidateEventArgs e)
        {
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

            this.AddressTextBox.Text = "";
            this.PostcodeTextBox.Text = "";
            this.LocationNameTextBox.Text = "";
            this.SeqDropDownList.SelectedIndex = 0;            
            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            this.LocationMultiView.ActiveViewIndex = 1;
            this.LocationButtonMultiView.ActiveViewIndex = 1;
        }

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
                this.AddNewLinkButton.Text = "Add New";
                this.ViewLinkButton.BackColor = Color.LightGray;
                this.AddNewLinkButton.BackColor = Color.Gray;
                this.LocationMultiView.ActiveViewIndex = 0;
            }
        }

        protected void CancelUpdateLocationButton_Click(object sender, EventArgs e)
        {
            this.LocationMultiView.ActiveViewIndex = 0;
        }

        protected void InsertLocationButton_Click(object sender, EventArgs e)
        {
            if(this.AddressTextBox_CustomValidator.IsValid && this.CostTextBox_CustomValidator.IsValid 
                && this.LocatinNameTextBox_CustomValidator.IsValid && this.PostcodeTextBox_CustomValidator.IsValid)
            {
                int tourID;
                if (this.ButtonMultiView.ActiveViewIndex == 0)
                    tourID = Convert.ToInt32(this.TourIDHiddenField.Value);
                else
                {
                    tourID = -1;
                    this.TourIDHiddenField.Value = tourID.ToString();
                }

                Decimal cost = Convert.ToDecimal(this.CostTextBox.Text);
                short sequence = Convert.ToInt16(this.SeqDropDownList.SelectedItem.ToString());
                short postcode = Convert.ToInt16(this.PostcodeTextBox.Text);
                double lat = Convert.ToDouble(this.LatitudeHiddenField.Value);
                double lon = Convert.ToDouble(this.LongitudeHiddenField.Value);
                String suburb = this.AddressTextBox.Text.Remove(0, this.AddressTextBox.Text.IndexOf(",")+2);
                suburb = this.AddressTextBox.Text.Substring(0, suburb.IndexOf(","));

                if (Session["LocationTable"] == null)
                {
                    if(this.TourIDHiddenField.Value != "")
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
            this.SelectedLocationIDHiddenField.Value = this.EditLocationGridView.SelectedDataKey.Value.ToString();
            this.SelectedLocationIndexHiddenField.Value = this.EditLocationGridView.SelectedIndex.ToString();

            List<ListItem> lst = new List<ListItem>();
            for (int i = 1; i < 11; i++)
            {
                ListItem item = new ListItem(i.ToString(), "true");
                lst.Add(item);
            }
            this.SeqDropDownList.DataSource = lst;
            this.SeqDropDownList.DataBind();

            if (Session["LocationTable"] == null)
                locationTable = dataAccess.getTourLocationByTourID(Convert.ToInt32(this.TourIDHiddenField.Value));
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

            this.AddNewLinkButton.Text = "Update";
            this.ViewLinkButton.BackColor = Color.Gray;
            this.AddNewLinkButton.BackColor = Color.LightGray;
            this.LocationButtonMultiView.ActiveViewIndex = 0;
            this.LocationMultiView.ActiveViewIndex = 1;
        }



    }
}