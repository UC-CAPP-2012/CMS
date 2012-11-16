using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Serialization;
using System.Xml;
using System.IO;


namespace CMS.GeneralPages
{
    public partial class Users : System.Web.UI.Page
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
        /// Display view with the grid view containing all users.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void AllUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersMultiView.ActiveViewIndex = 0;
        }

        /// <summary>
        /// Display view with the grid view containing subscribed users.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void SubcribedUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersMultiView.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Display view with the grid view containing unsubscribed users.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void UnsubcribedUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersMultiView.ActiveViewIndex = 2;
        }

        /// <summary>
        /// Pass all users to ConnectionXML to export it to xml.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnAllUsersXML_Click(object sender, EventArgs e)
        {
            var allUsers = dataAccess.getAllUsers();
            DataSet ds = new DataSet();
            ds.Tables.Add(allUsers);
            this.ConnectionXML(ds);
        }

        /// <summary>
        /// Export the users data to a xml file.
        /// </summary>
        /// <param name="ds">Dataset containing users to export to xml</param>
        public void ConnectionXML(DataSet ds)
        {
            DeleteAllTempFiles();
            // Get a FileStream object
            Guid id = new Guid();
            id = Guid.NewGuid();
            StreamWriter xmlDoc = new StreamWriter(Server.MapPath("~/XMLTempFiles/"+id.ToString()+".xml"), false);
            // Apply the WriteXml method to write an XML document
            ds.WriteXml(xmlDoc);
            xmlDoc.Close();
            Response.AppendHeader("content-disposition",
            "attachment; filename=" + id+".xml");
            Response.ContentType = "text/xml";
            Response.WriteFile(Server.MapPath("~/XMLTempFiles/"+id.ToString()+".xml"));
            Response.End();
        }

        /// <summary>
        /// Pass only unsubscribed users to ConnectionXML to export it to xml.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnUnsubcribedUsersXML_Click(object sender, EventArgs e)
        {
            var allUsers = dataAccess.getAllUnsubcribedUsers();
            DataSet ds = new DataSet();
            ds.Tables.Add(allUsers);
            this.ConnectionXML(ds);
        }

        /// <summary>
        /// Pass only subscribed users to ConnectionXML to export it to xml.
        /// </summary>
        /// <param name="sender">The object that raised this event.</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        protected void btnSubcribedUsersXML_Click(object sender, EventArgs e)
        {
            var allUsers = dataAccess.getAllSubcribedUsers();
            DataSet ds = new DataSet();
            ds.Tables.Add(allUsers);
            this.ConnectionXML(ds);
        }

        /// <summary>
        /// Delete all temporarily saved images 
        /// </summary>
        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../XMLTempFiles")))
                System.IO.File.Delete(f);
        }

        /// <summary>
        /// Convert number type data into string(yes, no) for Subscribed data field  
        /// </summary>
        /// <param name="obj">Number type data indicating whether the user is subscribed or not</param>
        /// <returns>'Yes' for 1 'No' for the others.</returns>
        protected string AutoConvert(object obj)
        {
            if (Convert.ToByte(obj.ToString()) == 1)
            {
                return "Yes";
            }
            return "No";
        }
    }
}