using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMS.BLL;
using MySql.Data.MySqlClient;

namespace TestCMS
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestCMSBLClass
    {
        CMSBLClass targetClass =  new CMSBLClass();
        private static int newID;
        private static int newMediaID;
        public TestCMSBLClass()
        {
            
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /*********************************************************************
            Category data access test
         *********************************************************************/
        #region Category Test
        [TestMethod]
        public void TestGetAllCategory()
        {
            int expected = 8;
            int actual = targetClass.getAllCategory().Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInsertCategory()
        {
            newID = targetClass.InsertCategory("TestInsertName");
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertCategoryWithNullName()
        {
            newID = targetClass.InsertCategory(null);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        public void TestUpdateCategory()
        {
            int expected = 1;
            int actual = targetClass.UpdateCategory("TestUpdateName", newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateCategoryWithNullName()
        {
            int expected = 1;
            int actual = targetClass.UpdateCategory(null, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateCategoryWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateCategory("TestUpdateName", 1000000000);
            Assert.AreEqual(expected, actual);
        }
        
        
        [TestMethod]
        public void TestDeleteCategory()
        {
            int expected = 1;
            int actual = targetClass.DeleteCategory(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteCategoryWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteCategory(1000000000);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        /*********************************************************************
            SubType data access test
        *********************************************************************/
        #region SubType Test
        [TestMethod]
        public void TestGetAllSubtype()
        {
            int expected = 44;
            int actual = targetClass.getAllSubtype().Count;
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestInsertSubtype()
        {
            newID = targetClass.InsertSubtype("TestInsertName");
            Assert.AreNotEqual(0, newID);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertSubtypeWithNullName()
        {
            newID = targetClass.InsertSubtype(null);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        public void TestUpdateSubtype()
        {
            int expected = 1;
            int actual = targetClass.UpdateSubtype("TestUpdateName", newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateSubtypeWithNullName()
        {
            int expected = 1;
            int actual = targetClass.UpdateSubtype(null, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateSubtypeWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateSubtype("TestUpdateName", 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetSubtypeName()
        {
            String expected = "TestUpdateName";
            String actual = targetClass.getSubtypeName(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetSubtypeNameWithInvalidID()
        {
            String expected = null;
            String actual = targetClass.getSubtypeName(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteSubtype()
        {
            int expected = 1;
            int actual = targetClass.DeleteSubtype(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteSubtypeWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteSubtype(1000000000);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        /*********************************************************************
            MajorRegion data access test
        *********************************************************************/
        #region MajorRegion Test
        [TestMethod]
        public void TestGetAllMajorRegion()
        {
            int expected = 12;
            int actual = targetClass.getAllMajorRegion().Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInsertMajorRegion()
        {
            newID = targetClass.InsertMajorRegion("TestInsertName");
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertMajorRegionWithNullName()
        {
            newID = targetClass.InsertSubtype(null);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        public void TestUpdateMajorRegion()
        {
            int expected = 1;
            int actual = targetClass.UpdateMajorRegion("TestUpdateName", newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMajorRegionWithNullName()
        {
            int expected = 1;
            int actual = targetClass.UpdateMajorRegion(null, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateMajorRegionWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateMajorRegion("TestUpdateName", 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMajorRegionName()
        {
            String expected = "TestUpdateName";
            String actual = targetClass.getMajorRegionName(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMajorRegionNameWithInvalidID()
        {
            String expected = null;
            String actual = targetClass.getMajorRegionName(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMajorRegion()
        {
            int expected = 1;
            int actual = targetClass.DeleteMajorRegion(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMajorRegionWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMajorRegion(1000000000);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        /*********************************************************************
            POI data access test
        *********************************************************************/
        #region POI Test
        [TestMethod]
        public void TestGetAllPOIList()
        {
            int expected = 398;
            int actual = targetClass.getAllPOIList().Count;
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestInsertPOI()
        {
            newID = targetClass.InsertPOI("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, 1, 2);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertPOIWithNullStrings()
        {
            int poiID = targetClass.InsertPOI(null, null, 0, null, null, null, null, null, 0, 0, 0, null, 1, 1, 2);
            Assert.AreNotEqual(0, poiID);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertPOIWithInvalidCategoryID()
        {
            int poiID = targetClass.InsertPOI("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", 
                "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, 1000000000, 2);
            Assert.AreNotEqual(0, poiID);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertPOIWithInvalidSubtypeID()
        {
            int poiID = targetClass.InsertPOI("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite",
                "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1000000000, 1, 2);
            Assert.AreNotEqual(0, poiID);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertPOIWithInvalidMajorRegionID()
        {
            int poiID = targetClass.InsertPOI("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite",
                "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, 1, 1000000000);
            Assert.AreNotEqual(0, poiID);
        }

        [TestMethod]
        public void TestUpdatePOI()
        {
            int expected = 1;
            int actual = targetClass.UpdatePOI("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, 2, 1, newID, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdatePOIWithNullStrings()
        {
            int expected = 1;
            int actual = targetClass.UpdatePOI(null, null, 0, null, null, null, null, null, 0, 0, 0, null, 1, 2, 1, newID, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdatePOIWithInvaildID()
        {
            int expected = 0;
            int actual = targetClass.UpdatePOI("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", 
                "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, 2, 1, 1000000000, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdatePOIWithInvaildCategoryID()
        {
            int expected = 0;
            int actual = targetClass.UpdatePOI("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite",
                "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, 2, 1000000000, newID, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdatePOIWithInvaildSubtypeID()
        {
            int expected = 0;
            int actual = targetClass.UpdatePOI("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite",
                "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1000000000, 2, 1, newID, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdatePOIWithInvaildMajorRegionID()
        {
            int expected = 0;
            int actual = targetClass.UpdatePOI("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite",
                "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, 1000000000, 1, newID, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearchPOIWithExistingString()
        {
            int expected = 1;
            int actual = targetClass.searchPOI("TestUpdateName").Rows.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearchPOIWithNoneExistingString()
        {
            int expected = 0;
            int actual = targetClass.searchPOI("TestNoneExistingString").Rows.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSearchPOIWithNullString()
        {
            int expected = 1;
            int actual = targetClass.searchPOI(null).Rows.Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetPOIByItemID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getPOIByItemID(newID);
            Assert.AreEqual(expected, actual.ItemName);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetPOIByInvalidID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getPOIByItemID(1000000000);
            Assert.AreEqual(expected, actual.ItemName);
        }

        [TestMethod]
        public void TestDeletePOI()
        {
            int expected = 1;
            int actual = targetClass.DeletePOI(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeletePOIWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeletePOI(1000000000);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        /*********************************************************************
            Event data access test
        *********************************************************************/
        #region Event Test
        [TestMethod]
        public void TestGetAllEventIList()
        {
            int expected = 54;
            int actual = targetClass.getAllEventList().Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInsertEvent()
        {
            newID = targetClass.InsertEvent("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1,
                DateTime.Today, DateTime.Today.AddDays(1), 2);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertEventWithNullStrings()
        {
            newID = targetClass.InsertEvent(null, null, 0, null, null, null, null, null, 0, 0, 0, null, 1, DateTime.Today, DateTime.Today.AddDays(1), 2);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertEventWithInvalidSubtypeID()
        {
            newID = targetClass.InsertEvent("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours", 
                "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1000000000, DateTime.Today, DateTime.Today.AddDays(1), 2);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertEventWithInvalidMajorRegionID()
        {
            newID = targetClass.InsertEvent("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours",
                "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, DateTime.Today, DateTime.Today.AddDays(1), 1000000000);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        public void TestUpdateEvent()
        {
            int expected = 1;
            int actual = targetClass.UpdateEvent("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, DateTime.Today, DateTime.Today.AddDays(1), 2, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateEventWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateEvent("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", 
                "TestUpdateHours",  "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, DateTime.Today, DateTime.Today.AddDays(1), 2, 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateEventWithNullStrings()
        {
            int expected = 1;
            int actual = targetClass.UpdateEvent(null, null, 0, null, null, null, null, null, 0, 0, 0, null, 1, DateTime.Today, DateTime.Today.AddDays(1), 2, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdateEventWithInvalidSubtypeID()
        {
            int expected = 1;
            int actual = targetClass.UpdateEvent("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours",
                "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1000000000, DateTime.Today, DateTime.Today.AddDays(1), 2, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdateEventWithInvalidMajorRegionID()
        {
            int expected = 1;
            int actual = targetClass.UpdateEvent("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours",
                "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, DateTime.Today, DateTime.Today.AddDays(1), 1000000000, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetEventByItemID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getEventByItemID(newID);
            Assert.AreEqual(expected, actual.ItemName);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetEventByInvalidItemID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getEventByItemID(1000000000);
            Assert.AreEqual(expected, actual.ItemName);
        }
        
        [TestMethod]
        public void TestDeleteEvent()
        {
            int expected = 1;
            int actual = targetClass.DeleteEvent(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteEventWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteEvent(1000000000);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        /*********************************************************************
            User data access test
        *********************************************************************/
        #region User Test
        
        [TestMethod]
        public void TestGetAllUsers()
        {
            int expected = 7;
            var actual = targetClass.getAllUsers();
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void TestGetAllSubcribedUsers()
        {
            int expectedCount = 4;
            int actual = targetClass.getAllSubcribedUsers().Count;
            Assert.AreEqual(expectedCount, actual);
        }

        [TestMethod]
        public void TestGetAllUnsubcribedUsers()
        {
            int expectedCount = 3;
            int actual = targetClass.getAllUnsubcribedUsers().Count;
            Assert.AreEqual(expectedCount, actual);
        }

        #endregion
        
        /*********************************************************************
        * News data access Test
        *********************************************************************/
        #region News Test
        [TestMethod]
        public void TestGetAllNews()
        {
            int expected = 1;
            int actual = targetClass.getAllNews().Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInsertNews()
        {
            newID = targetClass.InsertNews("TestInsertHeading", DateTime.Today, "TestInsertBody", "TestInsertMediaURL", "TestInsertPublisher", "TestInsertAuthor");
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertNewsWithNullStrings()
        {
            newID = targetClass.InsertNews(null, DateTime.Today, null, null, null, null);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        public void TestUpdateNews()
        {
            int expected = 1;
            int actual = targetClass.UpdateNews("TestUpdateHeading", DateTime.Today, "TestUpdateBody", "TestUpdateMediaURL", "TestUpdatePublisher", "TestUpdateAuthor", newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateNewsWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateNews("TestUpdateHeading", DateTime.Today, "TestUpdateBody", "TestUpdateMediaURL", "TestUpdatePublisher", 
                "TestUpdateAuthor", 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateNewsWithNullStirngs()
        {
            int expected = 0;
            int actual = targetClass.UpdateNews(null, DateTime.Today, null, null, null, null, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetNewsByID()
        {
            String expected = "TestUpdateHeading";
            var actual = targetClass.getNewsById(newID);
            Assert.AreEqual(expected, actual.NewsHeading);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetNewsByInvalidID()
        {
            String expected = "TestUpdateHeading";
            var actual = targetClass.getNewsById(1000000000);
            Assert.AreEqual(expected, actual.NewsHeading);
        }

        [TestMethod]
        public void TestDeleteNews()
        {
            int expected = 1;
            int actual = targetClass.DeleteNews(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteNewsWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteNews(1000000000);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        /*********************************************************************
        * Tour data access Test
        *********************************************************************/
        #region Tour Test
        [TestMethod]
        public void TestGetAllTour()
        {
            int expected = 1;
            int actual = targetClass.getAllTours().Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInsertTour()
        {
            int expected = 1;
            int actual = targetClass.insertTour("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertAgent");
            newID = targetClass.getNewlyInsertedTourID();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertTourWithNullStrings()
        {
            int expected = 1;
            int actual = targetClass.insertTour(null, null, 0, null, null, null, null);
            newID = targetClass.getNewlyInsertedTourID();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetNewlyInsertedTourID()
        {
            int expected = newID;
            int actual = targetClass.getNewlyInsertedTourID();
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestUpdateTour()
        {
            int expected = 1;
            int actual = targetClass.updateTour("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", "TestUpdateAgent", newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateTourWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.updateTour("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", 
                "TestUpdateAgent", 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateTourWithNullStrings()
        {
            int expected = 0;
            int actual = targetClass.updateTour(null, null, 0, null, null, null, null, newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetTourByID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getTourByID(newID);
            Assert.AreEqual(expected, actual.TourName);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestGetTourByInvalidID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getTourByID(1000000000);
            Assert.AreEqual(expected, actual.TourName);
        }

        [TestMethod]
        public void TestDeleteTour()
        {
            int expected = 1;
            int actual = targetClass.deleteTour(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteTourWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.deleteTour(1000000000);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        /*********************************************************************
        * POITour data access Test
        *********************************************************************/
        #region POITour Test
        [TestMethod]
        public void TestInsertTourPOIList()
        {
            targetClass.insertTour("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertAgent");
            newID = targetClass.getNewlyInsertedTourID();
            int expected = 3;
            int actual = targetClass.insertTourPOIList(100, newID, 1);
            actual += targetClass.insertTourPOIList(101, newID, 2);
            actual += targetClass.insertTourPOIList(102, newID, 3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertTourPOIListWithInvalidPOIID()
        {
            int expected = 1;
            int actual = targetClass.insertTourPOIList(1000000000, newID, 4);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertTourPOIListWithInvalidTourID()
        {
            int expected = 1;
            int actual = targetClass.insertTourPOIList(100, 1000000000, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestgetTourPOIListByTourID()
        {
            int expected = 3;
            int actual = targetClass.getTourPOIListByTourID(newID).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestgetTourPOIListByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.getTourPOIListByTourID(1000000000).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestdeleteTourPOIListByTourID()
        {
            int expected = 3;
            int actual = targetClass.deleteTourPOIListByTourID(newID);
            targetClass.deleteTour(newID);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod]
        public void TestdeleteTourPOIListByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.deleteTourPOIListByTourID(1000000000);
            targetClass.deleteTour(newID);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        /*********************************************************************
            Media data access test
        *********************************************************************/
        #region Media For Item Test
        [TestMethod]
        public void TestInsertImageForPOI()
        {
            newID = targetClass.InsertPOI("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, 1, 2);
            bool expected = true;
            int actual = targetClass.InsertMedia(newID, "InsertImageTestURL", "Images", null);
            Assert.AreEqual(expected, actual>0);
        }

        [TestMethod]
        public void TestInsertVideoForPOI()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(newID, "InsertVideoTestURL", "Video", null);
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestInsertAudioForPOI()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(newID, "InsertAudioTestURL", "Audio", null);
            newMediaID = actual; 
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertMediaForPOIWithInvalidPOIID()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(1000000000, "InsertMediaTestURL", "Media", null);
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertMediaForPOIWithNullStirng()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(newID, null, null, null);
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestGetMediaByItemID()
        {
            int expected = 3;
            int actual = targetClass.getMediaByItemID(newID).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMediaByInvalidItemID()
        {
            int expected = 0;
            int actual = targetClass.getMediaByItemID(1000000000).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCountImagesMediaByItemId()
        {
            int expected = 1;
            int actual = targetClass.CountImagesMediaByItemId(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCountImagesMediaByInvalidItemId()
        {
            int expected = 0;
            int actual = targetClass.CountImagesMediaByItemId(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateMediaForItem()
        {
            int expected = 1;
            int actual = targetClass.UpdateMedia(newID, "UpdateAudioTestURL", "Audio", null, newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateMediaForItemWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateMedia(newID, "UpdateAudioTestURL", "Audio", null, 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdateMediaForItemWithInvalidItemID()
        {
            int expected = 0;
            int actual = targetClass.UpdateMedia(1000000000, "UpdateAudioTestURL", "Audio", null, newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMediaForItemWithNullString()
        {
            int expected = 0;
            int actual = targetClass.UpdateMedia(newID, null, null, null, newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMediaURLByItemID()
        {
            int expected = 2;
            int actual = targetClass.getMediaURLByItemID(newID).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMediaURLByInvalidItemID()
        {
            int expected = 0;
            int actual = targetClass.getMediaURLByItemID(1000000000).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAudioURLByItemID()
        {
            String expected = "UpdateAudioTestURL";
            String actual = targetClass.getAudioURLByItemID(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAudioURLByInvalidItemID()
        {
            String expected = null;
            String actual = targetClass.getAudioURLByItemID(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByMediaURL()
        {
            int expected = 1;
            int actual = targetClass.DeleteMediaByMediaURL(newID, "InsertImageTestURL");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByMediaURLAndInvalidItemID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMediaByMediaURL(1000000000, "InvalidMediaURL");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByInvalidMediaURL()
        {
            int expected = 0;
            int actual = targetClass.DeleteMediaByMediaURL(newID, "InvalidMediaURL");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteAudioByItemID()
        {
            int expected = 1;
            int actual = targetClass.DeleteAudioByItemID(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteAudioByInvalidItemID()
        {
            int expected = 0;
            int actual = targetClass.DeleteAudioByItemID(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteVideoMediaByItemId()
        {
            int expected = 1;
            int actual = targetClass.DeleteVideoMediaByItemId(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteVideoMediaByInvalidItemId()
        {
            int expected = 0;
            int actual = targetClass.DeleteVideoMediaByItemId(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaforItem()
        {
            TestInsertAudioForPOI();
            int expected = 1;
            int actual = targetClass.DeleteMedia(newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaforItemWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMedia(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByItemID()
        {
            targetClass.DeletePOI(newID);
            TestInsertImageForPOI();
            TestInsertVideoForPOI();
            TestInsertAudioForPOI();
            int expected = 3;
            int actual = targetClass.DeleteMediaByItemID(newID);
            targetClass.DeletePOI(newID);
            Assert.AreEqual(expected, actual);           
        }

        [TestMethod]
        public void TestDeleteMediaByInvalidItemID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMediaByItemID(1000000000);
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Media For Tour Test
        [TestMethod]
        public void TestInsertImageForTour()
        {
            targetClass.insertTour("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertAgent");
            newID = targetClass.getNewlyInsertedTourID();
            bool expected = true;
            int actual = targetClass.InsertMedia(null, "InsertImageTestURL", "Images", newID);
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestInsertVideoForTour()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(null, "InsertVideoTestURL", "Video", newID);
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestInsertAudioForTour()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(null, "InsertAudioTestURL", "Audio", newID);
            newMediaID = actual;
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestInsertMediaForTourWithInvalidTourID()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(null, "InsertMediaTestURL", "InsertMediaTestURL", 1000000000);
            newMediaID = actual;
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertMediaForTourWithNullStrings()
        {
            bool expected = true;
            int actual = targetClass.InsertMedia(null, null, null, newID);
            newMediaID = actual;
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestGetMediaByTourID()
        {
            int expected = 3;
            int actual = targetClass.getMediaByTourID(newID).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMediaByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.getMediaByTourID(1000000000).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCountImagesMediaByTourID()
        {
            int expected = 1;
            int actual = targetClass.CountImagesMediaByTourID(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCountImagesMediaByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.CountImagesMediaByTourID(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateMediaForTour()
        {
            int expected = 1;
            int actual = targetClass.UpdateMedia(null, "UpdateAudioTestURL", "Audio", newID, newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateMediaForTourWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.UpdateMedia(null, "UpdateAudioTestURL", "Audio", newID, 1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestUpdateMediaForTourWithInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.UpdateMedia(null, "UpdateAudioTestURL", "Audio", 1000000000, newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateMediaForTourWithNullString()
        {
            int expected = 0;
            int actual = targetClass.UpdateMedia(null, null, null, newID, newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMediaURLByTourID()
        {
            int expected = 2;
            int actual = targetClass.getMediaURLByTourID(newID).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetMediaURLByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.getMediaURLByTourID(1000000000).Count;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAudioURLByTourID()
        {
            String expected = "UpdateAudioTestURL";
            String actual = targetClass.getAudioURLByTourID(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAudioURLByInvalidTourID()
        {
            String expected = null;
            String actual = targetClass.getAudioURLByTourID(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByMediaURLAndTourID()
        {
            int expected = 1;
            int actual = targetClass.DeleteMediaByMediaURLAndTourID(newID, "InsertImageTestURL");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByInvalidMediaURLAndTourID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMediaByMediaURLAndTourID(newID, "InvalidMediaURL");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByMediaURLAndInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMediaByMediaURLAndTourID(1000000000, "InsertImageTestURL");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteAudioByTourID()
        {
            int expected = 1;
            int actual = targetClass.DeleteAudioByTourID(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteAudioByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.DeleteAudioByTourID(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteVideoMediaByTourId()
        {
            int expected = 1;
            int actual = targetClass.DeleteVideoMediaByTourId(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteVideoMediaByInvalidTourId()
        {
            int expected = 0;
            int actual = targetClass.DeleteVideoMediaByTourId(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaForTour()
        {
            TestInsertAudioForTour();
            int expected = 1;
            int actual = targetClass.DeleteMedia(newMediaID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaForTourWithInvalidID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMedia(1000000000);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMediaByTourID()
        {
            targetClass.deleteTour(newID);
            TestInsertImageForTour();
            TestInsertVideoForTour();
            TestInsertAudioForTour();
            int expected = 3;
            int actual = targetClass.DeleteMediaByTourID(newID);
            targetClass.deleteTour(newID);
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod]
        public void TestDeleteMediaByInvalidTourID()
        {
            int expected = 0;
            int actual = targetClass.DeleteMediaByTourID(1000000000);
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
