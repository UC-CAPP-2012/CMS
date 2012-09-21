using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Security;
namespace CMS.AdminPages
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            //Get the creating user
            MembershipUser newUser = Membership.GetUser(RegisterUser.UserName);
            Guid newUserId = (Guid)newUser.ProviderUserKey;

            BLL.ClassManageUser addRole = new BLL.ClassManageUser();

            //Get the Id for role 'General'
            Guid studentRoleID = addRole.getRoleID("General");

            //Assign role 'General' to the new user
            addRole.AddUserRole(newUserId, studentRoleID);
        }

        protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
        {
            //Set status text to show nothing
            status_msg.Text = "";

            //Check if email is in correct form
            if (!Regex.IsMatch(RegisterUser.Email,
              @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {
                //Alert user what the error is
                status_msg.Text = "The email is invalid.";

                // Cancel the create user workflow
                e.Cancel = true;
            }
            //Check if user with the entered email already exists
            else if (Membership.GetUserNameByEmail(RegisterUser.Email) == RegisterUser.Email)
            {
                //Alert user what the error is
                duplicateUserMsg.Text = "A user with this email already exists. Please try again with a different email.";

                // Cancel the create user workflow
                e.Cancel = true;
            }
            else
            {
                //If everything is good, set email to be the creating username
                RegisterUser.UserName = RegisterUser.Email;
            }
        }
    }
}