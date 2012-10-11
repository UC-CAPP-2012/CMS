using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.GeneralPages
{
    public partial class MajorRegion : System.Web.UI.Page
    {
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GridViewRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GridViewRegion, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void GridViewRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = 0;
            this.NameDataLabel.Text = this.GridViewRegion.SelectedRow.Cells[2].Text;
        }


        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = 1;
            this.NameTextBox.Text = this.GridViewRegion.SelectedRow.Cells[2].Text;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.GridViewRegion.SelectedDataKey.Value;
            dataAccess.DeleteMajorRegion(id);
            this.GridViewRegion.DataBind();
            this.RegionMultiView.ActiveViewIndex = -1;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.NameTextBox.Text.Length > 0)
            {
                dataAccess.UpdateMajorRegion(this.NameTextBox.Text, (Int32)this.GridViewRegion.SelectedDataKey.Value);
                this.GridViewRegion.DataBind();
                this.NameDataLabel.Text = this.GridViewRegion.SelectedRow.Cells[2].Text;
                this.RegionMultiView.ActiveViewIndex = 0;
            }

        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = 0;
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.InsertNameTextBox.Text = "";
            this.RegionMultiView.ActiveViewIndex = 2;
        }

        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {
            if (this.InsertNameTextBox.Text.Length > 0)
            {
                dataAccess.InsertMajorRegion(this.InsertNameTextBox.Text);
                this.GridViewRegion.DataBind();
                this.RegionMultiView.ActiveViewIndex = -1;
            }
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = -1;
        }

    }
}