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


        /// <summary>
        /// Retrieves all Roles from the database.
        /// </summary>
        /// <returns> Datatable containing all Roles. </returns>
        public DAL.DataSetManageUser.aspnet_RolesDataTable GetAllRoles()
        {
            return roleAdapter.GetData();
        }

        /// <summary>
        /// Assign a specific role to a specific user.
        /// </summary>
        /// <param name="userID">The ID of the User which the role will be assigned to.</param>
        /// <param name="roleID">The ID of the Role which will be assigned to the user.</param>
        /// <returns> 1 for success, 0 for fail </returns>
        public int AddUserRole(Guid userID, Guid roleID)
        {
            return userRoleAdapter.Insert(userID, roleID);
        }

        /// <summary>
        /// Get Role ID using string type Role name.
        /// </summary>
        /// <param name="role">The name of the Role to find ID for.</param>
        /// <returns>Role ID</returns>
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

        /// <summary>
        /// Get Role name using Role ID.
        /// </summary>
        /// <param name="roleID">The ID of the Role to find name for.</param>
        /// <returns>Role name</returns>
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


        /// <summary>
        /// Get the name of the Role which is assigned to a specific User.
        /// </summary>
        /// <param name="userName">The name of the User to find the Role name for.</param>
        /// <returns>Role name</returns>
        public string getUserRole(string userName)
        {
            DataView dataView = new DataView();
            dataView.Table = userAdapter.GetData();
            Guid userID = this.GetUserID(userName);
            Guid userRoleID = this.GetUserRoleID(userID);
            return this.getRoleName(userRoleID);
        }

        /// <summary>
        /// Retrieves all Users from the database.
        /// </summary>
        /// <returns> Datatable containing all Users. </returns>
        public DAL.DataSetManageUser.aspnet_UsersDataTable GetAllUsers()
        {
            return userAdapter.GetAllUsers();
        }


        /// <summary>
        /// Remove User from the database.
        /// </summary>
        /// <param name="ApplicationId">The Application ID of the User.</param>
        /// <param name="LoweredUserName">The ID of the User in lower case letters.</param>
        /// <returns> 1 for success, 0 for fail </returns>
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


        /// <summary>
        /// Get the ID of the User.
        /// </summary>
        /// <param name="userName"> The name of the User to find ID for. </param>
        /// <returns> User ID </returns>
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


        /// <summary>
        /// Check whether the User Name already exists.
        /// </summary>
        /// <param name="username"> The name of the User. </param>
        /// <returns> true for exist, false for not exist </returns>
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


        /// <summary>
        /// Get the ID of the Role which is assigned to a specific User.
        /// </summary>
        /// <param name="userName">The name of the User to find the Role ID for.</param>
        /// <returns>Role ID</returns>
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