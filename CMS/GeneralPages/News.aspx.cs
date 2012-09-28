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
        public string fileName;
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
            this.AuthorTextBox.Text = this.GridViewNews.SelectedRow.Cells[4].Text;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            this.NewsImageUpdate.ImageUrl = News.NewsMediaURL;
            this.NewsBodyEditor.Content = News.NewsBody; 
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.GridViewNews.SelectedDataKey.Value;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            dataAccess.DeleteNews(id);
            System.IO.File.Delete(Server.MapPath("~/Media/" + News.NewsMediaURL.Split('/')[2]));
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
                string newsPublisher = PublisherTextBox.Text;
                string newsAuthor = AuthorTextBox.Text;
                string newsImageUrl = "../Media/" + NewsImageUpdate.ImageUrl.Split('/')[2];

                var NewsItem = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
                System.IO.File.Delete(Server.MapPath("~/Media/" + NewsItem.NewsMediaURL.Split('/')[2]));
                dataAccess.UpdateNews(title, publishedDate, body, newsImageUrl, newsPublisher, newsAuthor, (Int32)this.GridViewNews.SelectedDataKey.Value);
                System.IO.File.Move(Server.MapPath("~/Temp_Media/" + NewsImageUploadFileName.Value), Server.MapPath("~/Media/" + NewsImageUploadFileName.Value));
                DeleteAllTempFiles();

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
                string newsPublisher = InsertNewsPublisher.Text;
                string newsAuthor = InsertNewsAuthor.Text;
                string newsImageUrl = "../Media/" + InsertNewsImage.ImageUrl.Split('/')[2];
                int result = dataAccess.InsertNews(title, publishedDate, body, newsImageUrl, newsPublisher, newsAuthor);
                System.IO.File.Move(Server.MapPath("~/Temp_Media/" + InsertNewsImageName.Value), Server.MapPath("~/Media/" + InsertNewsImageName.Value));
                DeleteAllTempFiles();
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
                e.Row.Cells[2].Text = Convert.ToDateTime(e.Row.Cells[2].Text).ToShortDateString();

            }
        }

        protected void GridViewNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 0;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            this.newsTitle.InnerText = this.GridViewNews.SelectedRow.Cells[1].Text;
            this.newsLabel.InnerHtml = "<span>"+Convert.ToDateTime(this.GridViewNews.SelectedRow.Cells[2].Text).ToShortDateString()+"</span> | <span>"+this.GridViewNews.SelectedRow.Cells[3].Text+"</span> | <span>"+this.GridViewNews.SelectedRow.Cells[4].Text+"</span>";
            this.newsBody.InnerHtml = News.NewsBody;
            this.newsImage.InnerHtml ="<img alt='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' title='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' src='"+News.NewsMediaURL+"'/>";
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "";
            if (NewsImageUpload.HasFile)
            {
                try
                {
                    if (NewsImageUpload.PostedFile.ContentType == "image/jpeg" ||
                       NewsImageUpload.PostedFile.ContentType == "image/png" ||
                       NewsImageUpload.PostedFile.ContentType == "image/gif")
                    {
                        if (NewsImageUpload.PostedFile.ContentLength < 102400)
                        {
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            long NowInBinary = DateTime.Today.ToBinary();
                            DeleteAllTempFiles();
                            NewsImageUpload.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + NowInBinary.ToString() + NewsImageUpload.FileName);
                            this.NewsImageUpdate.ImageUrl = "../Temp_Media/" + numIterations.ToString() + NowInBinary.ToString() + NewsImageUpload.FileName;
                            NewsImageUploadFileName.Value = numIterations.ToString() + NowInBinary.ToString() + NewsImageUpload.FileName;
                        }
                        else
                        {
                            StatusLabel.Text = "The file has to be less than 100 kb!";
                        }
                    }
                    else
                    {
                        StatusLabel.Text = "Only JPEG, PNG and GIF files are accepted!";
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else{
                StatusLabel.Text = "Please select a file to upload.";
            }
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 2;
            this.InsertNewsImage.ImageUrl = "../Media/default-news-image.png";
        }

        protected void btnInsertNewsUpload_Click(object sender, EventArgs e)
        {
            InsertStatusLabel.Text = "";
            if (InsertNewsImageUpload.HasFile)
            {
                try
                {
                    if (InsertNewsImageUpload.PostedFile.ContentType == "image/jpeg" ||
                       InsertNewsImageUpload.PostedFile.ContentType == "image/png" ||
                       InsertNewsImageUpload.PostedFile.ContentType == "image/gif")
                    {
                        if (InsertNewsImageUpload.PostedFile.ContentLength < 51200)
                        {
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            long NowInBinary = DateTime.Today.ToBinary();
                            DeleteAllTempFiles();
                            InsertNewsImageUpload.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + NowInBinary.ToString() + InsertNewsImageUpload.FileName);
                            this.InsertNewsImage.ImageUrl = "../Temp_Media/" + numIterations.ToString() + NowInBinary.ToString() + InsertNewsImageUpload.FileName;
                            InsertNewsImageName.Value = numIterations.ToString() + NowInBinary.ToString() + InsertNewsImageUpload.FileName;
                        }
                        else
                        {
                            InsertStatusLabel.Text = "The file has to be less than 50 kb!";
                        }
                    }
                    else
                    {
                        InsertStatusLabel.Text = "Only JPEG, PNG and GIF files are accepted!";
                    }

                }
                catch (Exception ex)
                {
                    InsertStatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else
            {
                InsertStatusLabel.Text = "Please select a file to upload.";
            }
        }

        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../Temp_Media")))
            System.IO.File.Delete(f);
        }
    }
}