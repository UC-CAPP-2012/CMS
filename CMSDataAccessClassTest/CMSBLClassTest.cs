using CMS.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using CMS.DAL;

namespace CMSDataAccessClassTest
{
    
    
    /// <summary>
    ///This is a test class for CMSBLClassTest and is intended
    ///to contain all CMSBLClassTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CMSBLClassTest
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


        /// <summary>
        ///A test for CMSBLClass Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void CMSBLClassConstructorTest()
        {
            CMSBLClass target = new CMSBLClass();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CountImagesMediaByItemId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void CountImagesMediaByItemIdTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.CountImagesMediaByItemId(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CountImagesMediaByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void CountImagesMediaByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.CountImagesMediaByTourID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteAudioByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteAudioByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteAudioByItemID(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteAudioByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteAudioByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteAudioByTourID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteCategory
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteCategoryTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int original_CategoryID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteCategory(original_CategoryID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteEvent
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteEventTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int Original_ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteEvent(Original_ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteMajorRegion
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteMajorRegionTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int Original_MajorRegionID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMajorRegion(Original_MajorRegionID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteMedia
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteMediaTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int Original_MediaID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMedia(Original_MediaID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteMediaByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteMediaByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMediaByItemID(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteMediaByMediaURL
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteMediaByMediaURLTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            string MediaURL = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMediaByMediaURL(ItemID, MediaURL);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteMediaByMediaURLAndTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteMediaByMediaURLAndTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            string MediaURL = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMediaByMediaURLAndTourID(TourID, MediaURL);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteMediaByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteMediaByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMediaByTourID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteNews
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteNewsTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int original_NewsID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteNews(original_NewsID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeletePOI
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeletePOITest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int Original_ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeletePOI(Original_ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteSubtype
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteSubtypeTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int original_SubtypeID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteSubtype(original_SubtypeID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteVideoMediaByItemId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteVideoMediaByItemIdTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteVideoMediaByItemId(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteVideoMediaByTourId
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void DeleteVideoMediaByTourIdTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteVideoMediaByTourId(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertCategory
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertCategoryTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string categoryName = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertCategory(categoryName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertEvent
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertEventTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string ItemName = string.Empty; // TODO: Initialize to an appropriate value
            string Details = string.Empty; // TODO: Initialize to an appropriate value
            int Cost = 0; // TODO: Initialize to an appropriate value
            string Phone = string.Empty; // TODO: Initialize to an appropriate value
            string Website = string.Empty; // TODO: Initialize to an appropriate value
            string Email = string.Empty; // TODO: Initialize to an appropriate value
            string OpeningHours = string.Empty; // TODO: Initialize to an appropriate value
            string Address = string.Empty; // TODO: Initialize to an appropriate value
            double Latitude = 0F; // TODO: Initialize to an appropriate value
            double Longitude = 0F; // TODO: Initialize to an appropriate value
            int Postcode = 0; // TODO: Initialize to an appropriate value
            string Suburb = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> SubtypeID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            DateTime EventStartDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime EventEndDate = new DateTime(); // TODO: Initialize to an appropriate value
            Nullable<int> MajorRegionID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertEvent(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address, Latitude, Longitude, Postcode, Suburb, SubtypeID, EventStartDate, EventEndDate, MajorRegionID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertMajorRegion
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertMajorRegionTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string MajorRegionName = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertMajorRegion(MajorRegionName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertMedia
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertMediaTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            Nullable<int> ItemID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string MediaURL = string.Empty; // TODO: Initialize to an appropriate value
            string MediaType = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> TourID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertMedia(ItemID, MediaURL, MediaType, TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertNews
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertNewsTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string newsHeading = string.Empty; // TODO: Initialize to an appropriate value
            DateTime newsDateTime = new DateTime(); // TODO: Initialize to an appropriate value
            string newsBody = string.Empty; // TODO: Initialize to an appropriate value
            string newsMediaURL = string.Empty; // TODO: Initialize to an appropriate value
            string newsPublisher = string.Empty; // TODO: Initialize to an appropriate value
            string newsAuthor = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertNews(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, newsAuthor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertPOI
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertPOITest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string ItemName = string.Empty; // TODO: Initialize to an appropriate value
            string Details = string.Empty; // TODO: Initialize to an appropriate value
            int Cost = 0; // TODO: Initialize to an appropriate value
            string Phone = string.Empty; // TODO: Initialize to an appropriate value
            string Website = string.Empty; // TODO: Initialize to an appropriate value
            string Email = string.Empty; // TODO: Initialize to an appropriate value
            string OpeningHours = string.Empty; // TODO: Initialize to an appropriate value
            string Address = string.Empty; // TODO: Initialize to an appropriate value
            double Latitude = 0F; // TODO: Initialize to an appropriate value
            double Longitude = 0F; // TODO: Initialize to an appropriate value
            int Postcode = 0; // TODO: Initialize to an appropriate value
            string Suburb = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> SubtypeID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int CategoryID = 0; // TODO: Initialize to an appropriate value
            Nullable<int> MajorRegionID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertPOI(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address, Latitude, Longitude, Postcode, Suburb, SubtypeID, CategoryID, MajorRegionID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for InsertSubtype
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void InsertSubtypeTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string subtypeName = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.InsertSubtype(subtypeName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateCategory
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdateCategoryTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string categoryName = string.Empty; // TODO: Initialize to an appropriate value
            int original_CategoryID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateCategory(categoryName, original_CategoryID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateEvent
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdateEventTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string ItemName = string.Empty; // TODO: Initialize to an appropriate value
            string Details = string.Empty; // TODO: Initialize to an appropriate value
            int Cost = 0; // TODO: Initialize to an appropriate value
            string Phone = string.Empty; // TODO: Initialize to an appropriate value
            string Website = string.Empty; // TODO: Initialize to an appropriate value
            string Email = string.Empty; // TODO: Initialize to an appropriate value
            string OpeningHours = string.Empty; // TODO: Initialize to an appropriate value
            string Address = string.Empty; // TODO: Initialize to an appropriate value
            double Latitude = 0F; // TODO: Initialize to an appropriate value
            double Longitude = 0F; // TODO: Initialize to an appropriate value
            int Postcode = 0; // TODO: Initialize to an appropriate value
            string Suburb = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> SubtypeID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            DateTime EventStartDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime EventEndDate = new DateTime(); // TODO: Initialize to an appropriate value
            Nullable<int> MajorRegionID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int Original_ItemID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateEvent(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address, Latitude, Longitude, Postcode, Suburb, SubtypeID, EventStartDate, EventEndDate, MajorRegionID, Original_ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateMajorRegion
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdateMajorRegionTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string MajorRegionName = string.Empty; // TODO: Initialize to an appropriate value
            int Original_MajorRegionID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateMajorRegion(MajorRegionName, Original_MajorRegionID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateMedia
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdateMediaTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            Nullable<int> ItemID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            string MediaURL = string.Empty; // TODO: Initialize to an appropriate value
            string MediaType = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> TourID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int Original_MediaID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateMedia(ItemID, MediaURL, MediaType, TourID, Original_MediaID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateNews
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdateNewsTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string newsHeading = string.Empty; // TODO: Initialize to an appropriate value
            DateTime newsDateTime = new DateTime(); // TODO: Initialize to an appropriate value
            string newsBody = string.Empty; // TODO: Initialize to an appropriate value
            string newsMediaURL = string.Empty; // TODO: Initialize to an appropriate value
            string newsPublisher = string.Empty; // TODO: Initialize to an appropriate value
            string newsAuthor = string.Empty; // TODO: Initialize to an appropriate value
            int original_NewsID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateNews(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, newsAuthor, original_NewsID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdatePOI
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdatePOITest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string ItemName = string.Empty; // TODO: Initialize to an appropriate value
            string Details = string.Empty; // TODO: Initialize to an appropriate value
            int Cost = 0; // TODO: Initialize to an appropriate value
            string Phone = string.Empty; // TODO: Initialize to an appropriate value
            string Website = string.Empty; // TODO: Initialize to an appropriate value
            string Email = string.Empty; // TODO: Initialize to an appropriate value
            string OpeningHours = string.Empty; // TODO: Initialize to an appropriate value
            string Address = string.Empty; // TODO: Initialize to an appropriate value
            double Latitude = 0F; // TODO: Initialize to an appropriate value
            double Longitude = 0F; // TODO: Initialize to an appropriate value
            int Postcode = 0; // TODO: Initialize to an appropriate value
            string Suburb = string.Empty; // TODO: Initialize to an appropriate value
            Nullable<int> SubtypeID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> MajorRegionID = new Nullable<int>(); // TODO: Initialize to an appropriate value
            int CategoryID = 0; // TODO: Initialize to an appropriate value
            int Original_ItemID = 0; // TODO: Initialize to an appropriate value
            int Original_CategoryID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdatePOI(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address, Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID, CategoryID, Original_ItemID, Original_CategoryID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateSubtype
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void UpdateSubtypeTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string subtypeName = string.Empty; // TODO: Initialize to an appropriate value
            int original_SubtypeID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.UpdateSubtype(subtypeName, original_SubtypeID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for deleteTour
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void deleteTourTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.deleteTour(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for deleteTourPOIListByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void deleteTourPOIListByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int tourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.deleteTourPOIListByTourID(tourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllCategory
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllCategoryTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.CategoryDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.CategoryDataTable actual;
            actual = target.getAllCategory();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllEventList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllEventListTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.EventListDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.EventListDataTable actual;
            actual = target.getAllEventList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllMajorRegion
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllMajorRegionTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.MajorRegionDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MajorRegionDataTable actual;
            actual = target.getAllMajorRegion();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllNews
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllNewsTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.NewsDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.NewsDataTable actual;
            actual = target.getAllNews();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllPOIList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllPOIListTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.POIListDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.POIListDataTable actual;
            actual = target.getAllPOIList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllSubcribedUsers
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllSubcribedUsersTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.UserDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.UserDataTable actual;
            actual = target.getAllSubcribedUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllSubtype
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllSubtypeTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.SubTypeDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.SubTypeDataTable actual;
            actual = target.getAllSubtype();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllTours
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllToursTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.TourDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.TourDataTable actual;
            actual = target.getAllTours();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllUnsubcribedUsers
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllUnsubcribedUsersTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.UserDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.UserDataTable actual;
            actual = target.getAllUnsubcribedUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAllUsers
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAllUsersTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            CMSDBDataSet.UserDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.UserDataTable actual;
            actual = target.getAllUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAudioURLByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAudioURLByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int Item = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getAudioURLByItemID(Item);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getAudioURLByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getAudioURLByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getAudioURLByTourID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getEventByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getEventByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.EventItemRow expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.EventItemRow actual;
            actual = target.getEventByItemID(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getMajorRegionName
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getMajorRegionNameTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int MajorRegionID = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getMajorRegionName(MajorRegionID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getMediaByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getMediaByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable actual;
            actual = target.getMediaByItemID(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getMediaByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getMediaByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable actual;
            actual = target.getMediaByTourID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getMediaURLByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getMediaURLByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable actual;
            actual = target.getMediaURLByItemID(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getMediaURLByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getMediaURLByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.MediaDataTable actual;
            actual = target.getMediaURLByTourID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getNewlyInsertedTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getNewlyInsertedTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.getNewlyInsertedTourID();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getNewsById
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getNewsByIdTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int Id = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.NewsRow expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.NewsRow actual;
            actual = target.getNewsById(Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getPOIByItemID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getPOIByItemIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int ItemID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.POIItemRow expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.POIItemRow actual;
            actual = target.getPOIByItemID(ItemID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getSubtypeName
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getSubtypeNameTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int subtypeID = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.getSubtypeName(subtypeID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getTourByID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getTourByIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.TourRow expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.TourRow actual;
            actual = target.getTourByID(TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getTourPOIListByTourID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void getTourPOIListByTourIDTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int tourID = 0; // TODO: Initialize to an appropriate value
            CMSDBDataSet.TourPOIListDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.TourPOIListDataTable actual;
            actual = target.getTourPOIListByTourID(tourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for insertTour
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void insertTourTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string TourName = string.Empty; // TODO: Initialize to an appropriate value
            string TourDetail = string.Empty; // TODO: Initialize to an appropriate value
            int TourCost = 0; // TODO: Initialize to an appropriate value
            string TourPhone = string.Empty; // TODO: Initialize to an appropriate value
            string TourWebsite = string.Empty; // TODO: Initialize to an appropriate value
            string TourEmail = string.Empty; // TODO: Initialize to an appropriate value
            string TourAgent = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.insertTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourAgent);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for insertTourPOIList
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void insertTourPOIListTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            int POIID = 0; // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int seqNum = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.insertTourPOIList(POIID, TourID, seqNum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for searchPOI
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void searchPOITest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string searchString = string.Empty; // TODO: Initialize to an appropriate value
            CMSDBDataSet.POIListDataTable expected = null; // TODO: Initialize to an appropriate value
            CMSDBDataSet.POIListDataTable actual;
            actual = target.searchPOI(searchString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for updateTour
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\CMS\\CMS", "/")]
        [UrlToTest("http://localhost:58832/")]
        public void updateTourTest()
        {
            CMSBLClass target = new CMSBLClass(); // TODO: Initialize to an appropriate value
            string TourName = string.Empty; // TODO: Initialize to an appropriate value
            string TourDetail = string.Empty; // TODO: Initialize to an appropriate value
            int TourCost = 0; // TODO: Initialize to an appropriate value
            string TourPhone = string.Empty; // TODO: Initialize to an appropriate value
            string TourWebsite = string.Empty; // TODO: Initialize to an appropriate value
            string TourEmail = string.Empty; // TODO: Initialize to an appropriate value
            string TourAgent = string.Empty; // TODO: Initialize to an appropriate value
            int TourID = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.updateTour(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourAgent, TourID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
