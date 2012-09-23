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
            this.NameTextBox.Text = this.GridViewNews.SelectedRow.Cells[1].Text;
            this.PublisherTextBox.Text = this.GridViewNews.SelectedRow.Cells[3].Text;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            this.NewsImageUpdate.ImageUrl = News.NewsMediaURL;
            this.NewsBodyEditor.Content = News.NewsBody; 
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
            if (this.NewsBodyEditor.Content.Length > 0)
            {
                string title = NameTextBox.Text;
                DateTime publishedDate = DateTime.Today;
                string body = NewsBodyEditor.Content;
                string newsImageUrl = NewsImageUpdate.ImageUrl;
                string newsPublisher = PublisherTextBox.Text;

                dataAccess.UpdateNews(title, publishedDate, body, newsImageUrl, newsPublisher, (Int32)this.GridViewNews.SelectedDataKey.Value);
                this.GridViewNews.DataBind();
                var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
                this.newsTitle.InnerText = this.GridViewNews.SelectedRow.Cells[1].Text;
                this.newsLabel.InnerHtml = "<span>" + Convert.ToDateTime(this.GridViewNews.SelectedRow.Cells[2].Text).ToShortDateString() + "</span> | <span>" + this.GridViewNews.SelectedRow.Cells[3].Text + "</span>";
                this.newsBody.InnerHtml = News.NewsBody;
                this.newsImage.InnerHtml = "<img alt='" + this.GridViewNews.SelectedRow.Cells[1].Text + "' title='" + this.GridViewNews.SelectedRow.Cells[1].Text + "' src='" + News.NewsMediaURL + "'/>";
                this.NewsMultiView.ActiveViewIndex = 0;
            }
            else
            {
                UpdateNewsNoContentStatus.Text = "News body is required.";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 0;
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = -1;
        }

        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {
            if (this.InsertNewsEditor.Content.Length > 0)
            {
                string title = InsertNewsTitle.Text;
                DateTime publishedDate = DateTime.Today;
                string body = InsertNewsEditor.Content;
                string newsImageUrl = InsertNewsImage.ImageUrl;
                string newsPublisher = InsertNewsPublisher.Text;

                int result = dataAccess.InsertNews(title, publishedDate, body, newsImageUrl, newsPublisher);

                this.GridViewNews.DataBind();
                this.NewsMultiView.ActiveViewIndex = -1;
            }
            else
            {
                InsertNewsNoContentStatus.Text = "News body is required.";
            }
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
            this.newsImage.InnerHtml ="<img alt='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' title='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' src='"+News.NewsMediaURL+"'/>";
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (NewsImageUpload.HasFile)
            {
                try
                {
                        if (NewsImageUpload.PostedFile.ContentLength < 102400)
                        {
                            string filename = NewsImageUpload.FileName;
                            NewsImageUpload.SaveAs(Server.MapPath("~/Media/") +filename);
                            this.NewsImageUpdate.ImageUrl = "../Media/" + filename;
                        }
                        else
                            StatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
                   
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 2;
            this.InsertNewsImage.ImageUrl = "../Media/default-news-image.png";
        }

        protected void btnInsertNewsUpload_Click(object sender, EventArgs e)
        {
            if (InsertNewsImageUpload.HasFile)
            {
                try
                {
                    if (InsertNewsImageUpload.PostedFile.ContentLength < 102400)
                    {
                        string filename = InsertNewsImageUpload.FileName;
                        InsertNewsImageUpload.SaveAs(Server.MapPath("~/Media/") + filename);
                        this.InsertNewsImage.ImageUrl = "../Media/" + filename;
                    }
                    else
                        InsertStatusLabel.Text = "Upload status: The file has to be less than 100 kb!";

                }
                catch (Exception ex)
                {
                    InsertStatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

    }
}