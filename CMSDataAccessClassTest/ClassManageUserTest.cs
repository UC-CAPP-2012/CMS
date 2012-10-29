using CMS.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using CMS.DAL;
using System.Web.Security;

namespace CMSDataAccessClassTest
{
    
    
    /// <summary>
    ///This is a test class for ClassManageUserTest and is intended
    ///to contain all ClassManageUserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClassManageUserTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        public MembershipUser CreateUser()
        {
            Membership.CreateUser("TestUser", "123123");
            return Membership.GetUser("TestUser");
        }

        public void RemoveUser()
        {
            Membership.DeleteUser("TestUser", true);
        }

        /// <summary>
        ///A test for ClassManageUser Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void ClassManageUserConstructorTest()
        {
            ClassManageUser target = new ClassManageUser();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AddUserRole
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AddUserRoleTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            MembershipUser user = CreateUser();

            Guid userID = (Guid) user.ProviderUserKey; // TODO: Initialize to an appropriate value
            Guid roleID = Guid.Parse("165c76d0-9cd6-4055-9ce0-dcfd1b3af6ad"); // TODO: Initialize to an appropriate value
            int expected = 1; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.AddUserRole(userID, roleID);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAllRoles
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void GetAllRolesTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            DataSetManageUser.aspnet_RolesDataTable expected = null; // TODO: Initialize to an appropriate value
            DataSetManageUser.aspnet_RolesDataTable actual;
            actual = target.GetAllRoles();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAllUsers
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void GetAllUsersTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            DataSetManageUser.aspnet_UsersDataTable expected = null; // TODO: Initialize to an appropriate value
            DataSetManageUser.aspnet_UsersDataTable actual;
            actual = target.GetAllUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void GetUserIDTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            Guid expected = new Guid(); // TODO: Initialize to an appropriate value
            Guid actual;
            actual = target.GetUserID(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUserRoleID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void GetUserRoleIDTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            Guid userID = new Guid(); // TODO: Initialize to an appropriate value
            Guid expected = new Guid(); // TODO: Initialize to an appropriate value
            Guid actual;
            actual = target.GetUserRoleID(userID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getRoleID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getRoleIDTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            string role = string.Empty; // TODO: Initialize to an appropriate value
            Guid expected = new Guid(); // TODO: Initialize to an appropriate value
            Guid actual;
            actual = target.getRoleID(role);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getRoleName
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getRoleNameTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            Guid roleID = new Guid(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getRoleName(roleID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getUserRole
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getUserRoleTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getUserRole(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for isUserExist
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void isUserExistTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.isUserExist(username);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for removeUser
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void removeUserTest()
        {
            ClassManageUser target = new ClassManageUser(); // TODO: Initialize to an appropriate value
            Guid ApplicationId = new Guid(); // TODO: Initialize to an appropriate value
            string LoweredUserName = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.removeUser(ApplicationId, LoweredUserName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
