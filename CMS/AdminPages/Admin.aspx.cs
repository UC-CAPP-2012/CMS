using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace CMS.AdminPages
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0)
            {
                nullNoUser_msg.Visible = true;
                instruction_msg.Visible = false;
            }
            else
            {
                nullNoUser_msg.Visible = false;
                instruction_msg.Visible = true;
            }
        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Notify user when successfully remove a user
            status_msg.Text = "You have successfully removed the user.";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Reset the status message
            status_msg.Text = "";
        }
    }
}