using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.GeneralPages
{
    public partial class News : System.Web.UI.Page
    {
        //MySQL Data Access Class
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        /// <summary>
        /// Sort the news gridview when the first time loading the page.
        /// The method that runs everytime when the page loads.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridViewNews.Sort(GridViewNews.Columns[2].SortExpression, SortDirection.Descending);
            }
        }

        /// <summary>
        /// Display update form view with input controls filled with the original data of the selected News.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 1;
            this.NameTextBox.Text = this.GridViewNews.SelectedRow.Cells[1].Text;
            this.PublisherTextBox.Text = this.GridViewNews.SelectedRow.Cells[3].Text;
            this.AuthorTextBox.Text = this.GridViewNews.SelectedRow.Cells[4].Text;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            this.NewsImageUpdate.ImageUrl = News.NewsMediaURL;
            this.NewsBodyTextBox.Text = News.NewsBody;
            if (!News.NewsMediaURL.StartsWith("../Media/default-news-image.png"))
            {
                btnRemove.Visible = true;
            }
        }

        /// <summary>
        /// Delete selected News then refresh the News gridview and display empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            int id = (Int32)this.GridViewNews.SelectedDataKey.Value;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            dataAccess.DeleteNews(id);
            if (!News.NewsMediaURL.StartsWith("../Media/default-news-image.png"))
            {
                System.IO.File.Delete(Server.MapPath("~/Media/" + News.NewsMediaURL.Split('/')[2]));
            }
            this.GridViewNews.DataBind();
            this.NewsMultiView.ActiveViewIndex = -1;

        }

        /// <summary>
        /// Update the selected News using the user inputs then refresh 
        /// the News gridview and display the detail view for the updated News. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.NewsBodyTextBox.Text.Length > 0)
            {
                string title = NameTextBox.Text;
                DateTime publishedDate = DateTime.Today;
                string body = NewsBodyTextBox.Text;
                string newsPublisher = PublisherTextBox.Text;
                string newsAuthor = AuthorTextBox.Text;
                string newsImageUrl = "../Media/" + NewsImageUpdate.ImageUrl.Split('/')[2];

                var NewsItem = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
                if (!newsImageUrl.StartsWith("../Media/default-news-image.png"))
                {
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + NewsImageUploadFileName.Value), Server.MapPath("~/Media/" + NewsImageUploadFileName.Value));
                    System.IO.File.Delete(Server.MapPath("~/Media/" + NewsItem.NewsMediaURL.Split('/')[2]));
                }
                
                dataAccess.UpdateNews(title, publishedDate, body, newsImageUrl, newsPublisher, newsAuthor, (Int32)this.GridViewNews.SelectedDataKey.Value);
                
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

        /// <summary>
        /// To cancel update, go back to the detail view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// To cancel insert, display the empty view.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = -1;
        }

        /// <summary>
        /// Insert new News using the user inputs then refresh the News gridview and display the empty view. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubmitNewButton_Click(object sender, EventArgs e)
        {
            if (this.InsertNewsBodyTextBox.Text.Length > 0)
            {
                string title = InsertNewsTitle.Text;
                DateTime publishedDate = DateTime.Today;
                string body = InsertNewsBodyTextBox.Text;
                string newsPublisher = InsertNewsPublisher.Text;
                string newsAuthor = InsertNewsAuthor.Text;
                string newsImageUrl = "../Media/" + InsertNewsImage.ImageUrl.Split('/')[2];
                int result = dataAccess.InsertNews(title, publishedDate, body, newsImageUrl, newsPublisher, newsAuthor);
                if(!newsImageUrl.StartsWith("../Media/default-news-image.png")){
                    System.IO.File.Move(Server.MapPath("~/Temp_Media/" + InsertNewsImageName.Value), Server.MapPath("~/Media/" + InsertNewsImageName.Value));
                }
                DeleteAllTempFiles();
                this.GridViewNews.DataBind();
                this.NewsMultiView.ActiveViewIndex = -1;
            }
            else
            {
                InsertNewsNoContentStatus.Text = "News body is required.";
            }
        }

        /// <summary>
        /// Add mouse event attributes to each row to change the background color when moving the mouse over it.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
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

        /// <summary>
        /// Display detail view for the selected News.  
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void GridViewNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NewsMultiView.ActiveViewIndex = 0;
            btnRemove.Visible = false;
            btnInsertNewsRemove.Visible = false;
            var News = dataAccess.getNewsById((Int32)this.GridViewNews.SelectedDataKey.Value);
            this.newsTitle.InnerText = this.GridViewNews.SelectedRow.Cells[1].Text;
            this.newsLabel.InnerHtml = "<span>"+Convert.ToDateTime(this.GridViewNews.SelectedRow.Cells[2].Text).ToShortDateString()+"</span> | <span>"+this.GridViewNews.SelectedRow.Cells[3].Text+"</span> | <span>"+this.GridViewNews.SelectedRow.Cells[4].Text+"</span>";
            this.newsBody.InnerHtml = News.NewsBody;
            this.newsImage.InnerHtml ="<img alt='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' title='"+this.GridViewNews.SelectedRow.Cells[1].Text+"' src='"+News.NewsMediaURL+"'/>";
        }

        /// <summary>
        /// Upload images that user selected using file uploader.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "";
            //Check if image file is selected
            if (NewsImageUpload.HasFile)
            {
                try
                {
                    //Check the image file types
                    if (NewsImageUpload.PostedFile.ContentType == "image/jpeg" ||
                       NewsImageUpload.PostedFile.ContentType == "image/png" ||
                       NewsImageUpload.PostedFile.ContentType == "image/gif")
                    {
                        //Check the image file size
                        if (NewsImageUpload.PostedFile.ContentLength < 102400)
                        {
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            Guid id = new Guid();

                            // Create new GUID and echo to the console
                            id = Guid.NewGuid();
                            DeleteAllTempFiles();
                            NewsImageUpload.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + id.ToString() + NewsImageUpload.FileName);
                            this.NewsImageUpdate.ImageUrl = "../Temp_Media/" + numIterations.ToString() + id.ToString() + NewsImageUpload.FileName;
                            NewsImageUploadFileName.Value = numIterations.ToString() + id.ToString() + NewsImageUpload.FileName;
                            btnRemove.Visible = true;
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

        /// <summary>
        /// Display insert form view with empty input controls.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.AuthorTextBox.Text = "";
            this.NameTextBox.Text = "";
            this.PublisherTextBox.Text = "";
            this.NewsBodyTextBox.Text = "";
            this.InsertNewsBodyTextBox.Text = "";
            this.NewsMultiView.ActiveViewIndex = 2;
            this.InsertNewsImage.ImageUrl = "../Media/default-news-image.png";
            btnInsertNewsRemove.Visible = false;
        }

        /// <summary>
        /// Upload images for the new News using file uploader.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnInsertNewsUpload_Click(object sender, EventArgs e)
        {
            InsertStatusLabel.Text = "";
            //Check if image file is selected
            if (InsertNewsImageUpload.HasFile)
            {
                try
                {
                    //Check the image file types
                    if (InsertNewsImageUpload.PostedFile.ContentType == "image/jpeg" ||
                       InsertNewsImageUpload.PostedFile.ContentType == "image/png" ||
                       InsertNewsImageUpload.PostedFile.ContentType == "image/gif")
                    {
                        //Check the image file size
                        if (InsertNewsImageUpload.PostedFile.ContentLength < 51200)
                        {
                            Random rand = new Random((int)DateTime.Now.Ticks);
                            int numIterations = 0;
                            numIterations = rand.Next(1000000000, 2147483647);
                            Guid id = new Guid();

                            //Create new GUID and echo to the console
                            id = Guid.NewGuid();
                            DeleteAllTempFiles();
                            InsertNewsImageUpload.SaveAs(Server.MapPath("~/Temp_Media/") + numIterations.ToString() + id.ToString() + InsertNewsImageUpload.FileName);
                            this.InsertNewsImage.ImageUrl = "../Temp_Media/" + numIterations.ToString() + id.ToString() + InsertNewsImageUpload.FileName;
                            InsertNewsImageName.Value = numIterations.ToString() + id.ToString() + InsertNewsImageUpload.FileName;
                            btnInsertNewsRemove.Visible = true;
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

        /// <summary>
        /// Delete all temporarily saved images 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../Temp_Media")))
            System.IO.File.Delete(f);
        }

        /// <summary>
        /// When canceling insert, set the news image to the default image and clear all temp image files. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnInsertNewsRemove_Click(object sender, EventArgs e)
        {
            this.InsertNewsImage.ImageUrl = "../Media/default-news-image.png";
            InsertNewsImageName.Value = "../Media/default-news-image.png";
            DeleteAllTempFiles();
        }

        /// <summary>
        /// When canceling update, set the news image to the default image and clear all temp image files. 
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            this.NewsImageUpdate.ImageUrl = "../Media/default-news-image.png";
            NewsImageUploadFileName.Value = "../Media/default-news-image.png";
            DeleteAllTempFiles();
        }
    }
}