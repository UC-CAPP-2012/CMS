using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.GeneralPages
{
    public partial class SubType : System.Web.UI.Page
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
        protected void SubtypeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(SubtypeGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        /// <summary>
        /// Display detail view for the selected Subtype.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubtypeGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 0;
            this.NameDataLabel.Text = this.SubtypeGridView.SelectedRow.Cells[2].Text;
        }

        /// <summary>
        /// Display update form view with input controls filled with the original data of the selected Subtype.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 1;
            this.NameTextBox.Text = this.SubtypeGridView.SelectedRow.Cells[2].Text;
        }

        /// <summary>
        /// Delete selected Subtype then refresh the Subtype gridview and display empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.SubtypeGridView.SelectedDataKey.Value;
            dataAccess.DeleteSubtype(id);
            this.SubtypeGridView.DataBind();
            this.SubtypeMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Update the selected Subtype using the user inputs then refresh 
        /// the Subtype gridview and display the detail view for the updated Subtype. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.NameTextBox.Text.Length > 0)
            {
                dataAccess.UpdateSubtype(this.NameTextBox.Text, (Int32)this.SubtypeGridView.SelectedDataKey.Value);
                this.SubtypeGridView.DataBind();
                this.NameDataLabel.Text = this.SubtypeGridView.SelectedRow.Cells[2].Text;
                this.SubtypeMultiView.ActiveViewIndex = 0;
            }
        }

        /// <summary>
        /// To cancel update, go back to the detail view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Insert new Subtype using the user inputs then refresh the Subtype gridview and display the empty view. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {
            if (this.InsertNameTextBox.Text.Length > 0)
            {
                dataAccess.InsertSubtype(this.InsertNameTextBox.Text);
                this.SubtypeGridView.DataBind();
                this.SubtypeMultiView.ActiveViewIndex = -1;
            }
        }

        /// <summary>
        /// To cancel insert, display the empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Display insert form view with empty input controls.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.InsertNameTextBox.Text = "";
            this.SubtypeMultiView.ActiveViewIndex = 2;
        }
    }
}