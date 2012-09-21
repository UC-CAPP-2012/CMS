using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Web.Security;

namespace CMS
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Redirect to login page when logged out
            if (!Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Index.aspx");
            }

            if(Page.User.IsInRole("Admin")){
                Admin_link.Visible = true;
            }
            else{
                Admin_link.Visible = false;
            }

            Settings_link.HRef = "/GeneralPages/ChangePassword.aspx";
            Admin_link.HRef = "/AdminPages/AddUser.aspx";
            //Set current menu button colour
            string[] file = Request.CurrentExecutionFilePath.Split('/');
            string fileName = file[file.Length - 1];
            switch (fileName)
            {
                case "Default.aspx":
                    this.LinkButtonHome.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");     
                    break;
                case "Category.aspx":
                    this.LinkButtonCategory.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                case "POI.aspx":
                    this.LinkButtonPOI.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                case "Event.aspx":
                    this.LinkButtonEvent.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                case "Tour.aspx":
                    this.LinkButtonTour.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                case "News.aspx":
                    this.LinkButtonNews.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                case "User.aspx":
                    this.LinkButtonUser.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                default:
                    break;
            }
             


        }


        //Redirect when menu button is clicked.
        protected void MenuButton_Clicked(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;

            switch (clickedButton.ID)
            {
                case "LinkButtonHome":
                    Response.Redirect("~/Default.aspx");
                    break;
                case "LinkButtonCategory":
                    Response.Redirect("~/GeneralPages/Category.aspx");
                    break;
                case "LinkButtonPOI":
                    Response.Redirect("~/GeneralPages/POI.aspx");
                    break;
                case "LinkButtonEvent":
                    Response.Redirect("~/GeneralPages/Event.aspx");
                    break;
                case "LinkButtonTour":
                    Response.Redirect("~/GeneralPages/Tour.aspx");
                    break;
                case "LinkButtonNews":
                    Response.Redirect("~/GeneralPages/News.aspx");
                    break;
                case "LinkButtonUser":
                    Response.Redirect("~/GeneralPages/User.aspx");
                    break;
                case "LinkButtonAdmin":
                    Response.Redirect("~/AdminPages/Admin.aspx");
                    break;
                case "LinkButtonSubType":
                    Response.Redirect("~/GeneralPages/SubType.aspx");
                    break;
                default:
                    Response.Redirect("~/Default.aspx");
                    break;
            }
        }

    }
}
