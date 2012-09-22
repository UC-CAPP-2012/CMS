using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.CMSPages
{
    public partial class News : System.Web.UI.Page
    {
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridViewNews.Sort(GridViewNews.Columns[2].SortExpression, SortDirection.Descending);
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 1;
            this.NameTextBox.Text = this.GridViewNews.SelectedRow.Cells[2].Text;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.GridViewNews.SelectedDataKey.Value;
            dataAccess.DeleteNews(id);
            this.GridViewNews.DataBind();
            this.NewsMultiView.ActiveViewIndex = -1;

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {

        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {

        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {

        }

        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {

        }

        protected void GridViewNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GridViewNews, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void GridViewNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 0;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            this.newsTitle.InnerText = this.GridViewNews.SelectedRow.Cells[1].Text;
            this.newsLabel.InnerHtml = "<span>"+Convert.ToDateTime(this.GridViewNews.SelectedRow.Cells[2].Text).ToShortDateString()+"</span> | <span>"+this.GridViewNews.SelectedRow.Cells[3].Text+"</span>";
            this.newsBody.InnerHtml = News.NewsBody;
            this.newsImage.InnerHtml ="<img alt='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' title='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' src='"+News.NewsMediaURL.Split('#')[1]+"'>";
        }

    }
}