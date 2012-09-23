using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.CMSPages
{
    public partial class Users : System.Web.UI.Page
    {
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
    }
}