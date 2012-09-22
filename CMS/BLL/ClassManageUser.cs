using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CMS.BLL
{
    public class ClassManageUser
    {
        DAL.DataSetManageUserTableAdapters.aspnet_UsersTableAdapter userAdapter =
                new DAL.DataSetManageUserTableAdapters.aspnet_UsersTableAdapter();

        DAL.DataSetManageUserTableAdapters.aspnet_RolesTableAdapter roleAdapter =
            new DAL.DataSetManageUserTableAdapters.aspnet_RolesTableAdapter();

        DAL.DataSetManageUserTableAdapters.aspnet_UsersInRolesTableAdapter userRoleAdapter =
            new DAL.DataSetManageUserTableAdapters.aspnet_UsersInRolesTableAdapter();

        DAL.DataSetManageUserTableAdapters.aspnet_MembershipTableAdapter membershipAdapter =
            new DAL.DataSetManageUserTableAdapters.aspnet_MembershipTableAdapter();

        public DAL.DataSetManageUser.aspnet_RolesDataTable GetAllRoles()
        {
            return roleAdapter.GetData();
        }

        public int AddUserRole(Guid userID, Guid roleID)
        {
            return userRoleAdapter.Insert(userID, roleID);
        }

        public Guid getRoleID(string role)
        {
            DataView dataView = new DataView();
            dataView.Table = roleAdapter.GetData();
            Guid roleID = new Guid();
            for (int i = 0; i < dataView.Table.Rows.Count; i++)
            {
                if ((string)dataView.Table.Rows[i].ItemArray[2] == role)
                {
                    roleID = (Guid)dataView.Table.Rows[i].ItemArray[1];
                    break;
                }
            }

            return roleID;
        }

        public string getRoleName(Guid roleID)
        {
            DataView dataView = new DataView();
            dataView.Table = roleAdapter.GetData();
            string role = "";
            for (int i = 0; i < dataView.Table.Rows.Count; i++)
            {
                if ((Guid)dataView.Table.Rows[i].ItemArray[1] == roleID)
                {
                    role = dataView.Table.Rows[i].ItemArray[2].ToString();
                    break;
                }
            }

            return role;
        }

        public string getUserRole(string userName)
        {
            DataView dataView = new DataView();
            dataView.Table = userAdapter.GetData();
            Guid userID = this.GetUserID(userName);
            Guid userRoleID = this.GetUserRoleID(userID);
            return this.getRoleName(userRoleID);
        }

        public DAL.DataSetManageUser.aspnet_UsersDataTable GetAllUsers()
        {
            return userAdapter.GetAllUsers();
        }

        public int removeUser(Guid ApplicationId, string LoweredUserName)
        {
            Guid userID = this.GetUserID(LoweredUserName);
            Guid generalRoleID = this.getRoleID("General");

            //remove user from membership table
            membershipAdapter.Delete(userID);

            //remove user from UserRole table
            userRoleAdapter.Delete(userID, generalRoleID);

            return userAdapter.Delete(ApplicationId, LoweredUserName);
        }

        public Guid GetUserID(string userName)
        {
            DataView dataView = new DataView();
            dataView.Table = userAdapter.GetData();
            Guid userID = new Guid();
            for (int i = 0; i < dataView.Table.Rows.Count; i++)
            {
                if ((string)dataView.Table.Rows[i].ItemArray[2] == userName)
                {
                    userID = (Guid)dataView.Table.Rows[i].ItemArray[1];
                    break;
                }
            }

            return userID;
        }

        public bool isUserExist(string username)
        {
            bool result = false;
            DataView dataView = new DataView();
            dataView.Table = userAdapter.GetData();

            for (int i = 0; i < dataView.Table.Rows.Count; i++)
            {
                if ((string)dataView.Table.Rows[i].ItemArray[2] == username)
                {
                    result = true;
                }
            }
            return result;
        }

        public Guid GetUserRoleID(Guid userID)
        {
            DataView dataView = new DataView();
            dataView.Table = userRoleAdapter.GetData();
            Guid userRoleID = new Guid();
            for (int y = 0; y < dataView.Table.Rows.Count; y++)
            {
                if ((Guid)dataView.Table.Rows[y].ItemArray[0] == userID)
                {
                    userRoleID = (Guid)dataView.Table.Rows[y].ItemArray[1];
                    break;
                }
            }
            return userRoleID;
        }
    }
}