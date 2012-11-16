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
        //MySQL Data Access Class
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        /// <summary>
        /// The method that runs everytime when the page loads.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Add mouse event attributes to each row to change the background color when moving the mouse over it.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void GridViewRegion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GridViewRegion, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Display detail view for the selected Major Region.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void GridViewRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = 0;
            this.NameDataLabel.Text = this.GridViewRegion.SelectedRow.Cells[2].Text;
        }

        /// <summary>
        /// Display update form view with input controls filled with the original data of the selected Major Region.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = 1;
            this.NameTextBox.Text = this.GridViewRegion.SelectedRow.Cells[2].Text;
        }

        /// <summary>
        /// Delete selected Major Region then refresh the Major Region gridview and display empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.GridViewRegion.SelectedDataKey.Value;
            dataAccess.DeleteMajorRegion(id);
            this.GridViewRegion.DataBind();
            this.RegionMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Update the selected Major Region using the user inputs then refresh the Major Region gridview and display the detail view for the updated Major Region. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
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

        /// <summary>
        /// To cancel update, go back to the detail view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Display insert form view with empty input controls.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.InsertNameTextBox.Text = "";
            this.RegionMultiView.ActiveViewIndex = 2;
        }

        /// <summary>
        /// Insert new Major Region using the user inputs then refresh the Major Region gridview and display the empty view. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {
            if (this.InsertNameTextBox.Text.Length > 0)
            {
                dataAccess.InsertMajorRegion(this.InsertNameTextBox.Text);
                this.GridViewRegion.DataBind();
                this.RegionMultiView.ActiveViewIndex = -1;
            }
        }

        /// <summary>
        /// To cancel insert, display the empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.RegionMultiView.ActiveViewIndex = -1;
        }

    }
}