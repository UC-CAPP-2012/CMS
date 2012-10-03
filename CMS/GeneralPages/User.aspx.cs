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


namespace CMS.CMSPages
{
    public partial class Users : System.Web.UI.Page
    {
        BLL.CMSBLClass dataAccess = new BLL.CMSBLClass();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AllUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersMultiView.ActiveViewIndex = 0;
        }

        protected void SubcribedUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersMultiView.ActiveViewIndex = 1;
        }

        protected void UnsubcribedUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersMultiView.ActiveViewIndex = 2;
        }

        protected void btnAllUsersXML_Click(object sender, EventArgs e)
        {
            var allUsers = dataAccess.getAllUsers();
            DataSet ds = new DataSet();
            ds.Tables.Add(allUsers);
            this.ConnectionXML(ds);
        }

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

        protected void btnUnsubcribedUsersXML_Click(object sender, EventArgs e)
        {
            var allUsers = dataAccess.getAllUnsubcribedUsers();
            DataSet ds = new DataSet();
            ds.Tables.Add(allUsers);
            this.ConnectionXML(ds);
        }

        protected void btnSubcribedUsersXML_Click(object sender, EventArgs e)
        {
            var allUsers = dataAccess.getAllSubcribedUsers();
            DataSet ds = new DataSet();
            ds.Tables.Add(allUsers);
            this.ConnectionXML(ds);
        }

        public void DeleteAllTempFiles()
        {
            foreach (var f in System.IO.Directory.GetFiles(Server.MapPath("../XMLTempFiles")))
                System.IO.File.Delete(f);
        }
    }
}