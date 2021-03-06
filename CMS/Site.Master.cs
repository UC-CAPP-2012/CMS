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
        /// <summary>
        /// Enable administration menus if the user has an Amin role and set the tap menu color according to what page the user is viewing. 
        /// If the user is not logged in, Redirect to the login page.
        /// The method that runs everytime when the page loads.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Redirect to login page when logged out
            
            if (!Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Index.aspx");
            }

            if (Page.User.IsInRole("Admin"))
            {
                Admin_link.Visible = true;
            }
            else
            {
                Admin_link.Visible = false;
            }

            Settings_link.HRef = "/GeneralPages/ChangePassword.aspx";
            Admin_link.HRef = "/AdminPages/AddUser.aspx";

            //Set current menu button colour
            string[] file = Request.CurrentExecutionFilePath.Split('/');
            string fileName = file[file.Length - 1];
            switch (fileName)
            {
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
                case "SubType.aspx":
                    this.LinkButtonSubType.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                case "MajorRegion.aspx":
                    this.LinkButtonMajorRegion.BackColor = System.Drawing.ColorTranslator.FromHtml("#acacac");
                    break;
                default:
                    break;
            }
             


        }


        /// <summary>
        /// Detact which page the user requested to browse and set the tap menu color according to it. 
        /// The method that runs everytime when the page loads.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
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
                case "LinkButtonMajorRegion":
                    Response.Redirect("~/GeneralPages/MajorRegion.aspx");
                    break;
                default:
                    Response.Redirect("~/Default.aspx");
                    break;                    
            }
        }

    }
}
