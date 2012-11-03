using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMS.BLL;
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
            bool expected = true;
            int actual = targetClass.getAllCategory().Count;
            Assert.AreEqual(expected, actual>0);
        }


        [TestMethod]
        public void TestInsertCategory()
        {
            newID = targetClass.InsertCategory("TestInsertName");
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
        public void TestDeleteCategory()
        {
            int expected = 1;
            int actual = targetClass.DeleteCategory(newID);
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
            bool expected = true;
            int actual = targetClass.getAllSubtype().Count;
            Assert.AreEqual(expected, actual>0);
        }


        [TestMethod]
        public void TestInsertSubtype()
        {
            newID = targetClass.InsertSubtype("TestInsertName");
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
        public void TestGetSubtypeName()
        {
            String expected = "TestUpdateName";
            String actual = targetClass.getSubtypeName(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteSubtype()
        {
            int expected = 1;
            int actual = targetClass.DeleteSubtype(newID);
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
            bool expected = true;
            int actual = targetClass.getAllMajorRegion().Count;
            Assert.AreEqual(expected, actual>0);
        }

        [TestMethod]
        public void TestInsertMajorRegion()
        {
            newID = targetClass.InsertMajorRegion("TestInsertName");
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
        public void TestGetMajorRegionName()
        {
            String expected = "TestUpdateName";
            String actual = targetClass.getMajorRegionName(newID);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeleteMajorRegion()
        {
            int expected = 1;
            int actual = targetClass.DeleteMajorRegion(newID);
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
            bool expected = true;
            int actual = targetClass.getAllPOIList().Count;
            Assert.AreEqual(expected, actual > 0);
        }
        
        [TestMethod]
        public void TestInsertPOI()
        {
            newID = targetClass.InsertPOI("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1, 1, 2);
            Assert.AreNotEqual(0, newID);
        }

        [TestMethod]
        public void TestUpdatePOI()
        {
            int expected = 1;
            int actual = targetClass.UpdatePOI("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", "TestUpdateHours", "TestUpdateAddress", 0, 0, 0, "TestUpdateSuburb", 1, 2, 1, newID, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearchPOI()
        {
            int expected = 1;
            int actual = targetClass.searchPOI("TestUpdateName").Rows.Count;
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
        public void TestDeletePOI()
        {
            int expected = 1;
            int actual = targetClass.DeletePOI(newID);
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
            bool expected = true;
            int actual = targetClass.getAllEventList().Count;
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestInsertEvent()
        {
            newID = targetClass.InsertEvent("TestInsertName", "TestInsertDetail", 0, "TestInsertPhone", "TestInsertWebsite", "TestInsertEmail", "TestInsertHours", "TestInsertAddress", 0, 0, 0, "TestInsertSuburb", 1,
                DateTime.Today, DateTime.Today.AddDays(1), 2);
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
        public void TestGetEventByItemID()
        {
            String expected = "TestUpdateName";
            var actual = targetClass.getEventByItemID(newID);
            Assert.AreEqual(expected, actual.ItemName);
        }
        
        [TestMethod]
        public void TestDeleteEvent()
        {
            int expected = 1;
            int actual = targetClass.DeleteEvent(newID);
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
            bool expected = true;
            var actual = targetClass.getAllUsers();
            Assert.AreEqual(expected, actual.Count > 0);
        }

        [TestMethod]
        public void TestGetAllSubcribedUsers()
        {
            bool expectedCount = true;
            bool expectedSubscribed = true;
            var actual = targetClass.getAllSubcribedUsers();
            Assert.AreEqual(expectedCount, actual.Count > 0);
            foreach (var item in actual)
            {
                Assert.AreEqual(expectedSubscribed, Convert.ToBoolean(item.Subscribe));
            }
        }


        [TestMethod]
        public void TestGetAllUnsubcribedUsers()
        {
            bool expectedCount = true;
            bool expectedSubscribed = false;
            var actual = targetClass.getAllUnsubcribedUsers();
            Assert.AreEqual(expectedCount, actual.Count > 0);
            foreach (var item in actual)
            {
                Assert.AreEqual(expectedSubscribed, Convert.ToBoolean(item.Subscribe));
            }
        }

        #endregion
        
        /*********************************************************************
        * News data access Test
        *********************************************************************/
        #region News Test
        [TestMethod]
        public void TestGetAllNews()
        {
            bool expected = true;
            int actual = targetClass.getAllNews().Count;
            Assert.AreEqual(expected, actual > 0);
        }

        [TestMethod]
        public void TestInsertNews()
        {
            newID = targetClass.InsertNews("TestInsertHeading", DateTime.Today, "TestInsertBody", "TestInsertMediaURL", "TestInsertPublisher", "TestInsertAuthor");
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
        public void TestGetNewsByID()
        {
            String expected = "TestUpdateHeading";
            var actual = targetClass.getNewsById(newID);
            Assert.AreEqual(expected, actual.NewsHeading);
        }

        [TestMethod]
        public void TestDeleteNews()
        {
            int expected = 1;
            int actual = targetClass.DeleteNews(newID);
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
            bool expected = true;
            int actual = targetClass.getAllTours().Count;
            Assert.AreEqual(expected, actual > 0);
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
        public void TestGetNewlyInsertedTourID()
        {
            bool expected = true;
            int actual = targetClass.getNewlyInsertedTourID();
            Assert.AreEqual(expected, actual > 0);
        }
        
        [TestMethod]
        public void TestUpdateTour()
        {
            int expected = 1;
            int actual = targetClass.updateTour("TestUpdateName", "TestUpdateDetail", 0, "TestUpdatePhone", "TestUpdateWebsite", "TestUpdateEmail", "TestUpdateAgent", newID);
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
        public void TestDeleteTour()
        {
            int expected = 1;
            int actual = targetClass.deleteTour(newID);
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
        public void TestgetTourPOIListByTourID()
        {
            int expected = 3;
            int actual = targetClass.getTourPOIListByTourID(newID).Count;
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
        public void TestGetMediaByItemID()
        {
            int expected = 3;
            int actual = targetClass.getMediaByItemID(newID).Count;
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
        public void TestUpdateMediaForItem()
        {
            int expected = 1;
            int actual = targetClass.UpdateMedia(newID, "UpdateAudioTestURL", "Audio", null, newMediaID);
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
        public void TestGetAudioURLByItemID()
        {
            String expected = "UpdateAudioTestURL";
            String actual = targetClass.getAudioURLByItemID(newID);
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
        public void TestDeleteAudioByItemID()
        {
            int expected = 1;
            int actual = targetClass.DeleteAudioByItemID(newID);
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
        public void TestDeleteMediaforItem()
        {
            TestInsertAudioForPOI();
            int expected = 1;
            int actual = targetClass.DeleteMedia(newMediaID);
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
        public void TestGetMediaByTourID()
        {
            int expected = 3;
            int actual = targetClass.getMediaByTourID(newID).Count;
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
        public void TestUpdateMediaForTour()
        {
            int expected = 1;
            int actual = targetClass.UpdateMedia(null, "UpdateAudioTestURL", "Audio", newID, newMediaID);
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
        public void TestGetAudioURLByTourID()
        {
            String expected = "UpdateAudioTestURL";
            String actual = targetClass.getAudioURLByTourID(newID);
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
        public void TestDeleteAudioByTourID()
        {
            int expected = 1;
            int actual = targetClass.DeleteAudioByTourID(newID);
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
        public void TestDeleteMediaForTour()
        {
            TestInsertAudioForTour();
            int expected = 1;
            int actual = targetClass.DeleteMedia(newMediaID);
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
        #endregion
    }
}
