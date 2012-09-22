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
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubtypeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(SubtypeGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void SubtypeGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 0;
            this.NameDataLabel.Text = this.SubtypeGridView.SelectedRow.Cells[2].Text;
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 1;
            this.NameTextBox.Text = this.SubtypeGridView.SelectedRow.Cells[2].Text;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.SubtypeGridView.SelectedDataKey.Value;
            dataAccess.DeleteSubtype(id);
            this.SubtypeGridView.DataBind();
            this.SubtypeMultiView.ActiveViewIndex = -1;
        }

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

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 0;
        }

        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {
            if (this.InsertNameTextBox.Text.Length > 0)
            {
                dataAccess.InsertSubtype(this.InsertNameTextBox.Text);
                this.SubtypeGridView.DataBind();
                this.SubtypeMultiView.ActiveViewIndex = -1;
            }
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = -1;
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.SubtypeMultiView.ActiveViewIndex = 2;
        }
    }
}