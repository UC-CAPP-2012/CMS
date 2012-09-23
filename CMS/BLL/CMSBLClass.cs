using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class CMSBLClass
    {

        DAL.CMSDBDataSetTableAdapters.CategoryTableAdapter categoryTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.CategoryTableAdapter();

        DAL.CMSDBDataSetTableAdapters.EventItemTableAdapter eventItemTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.EventItemTableAdapter();

        DAL.CMSDBDataSetTableAdapters.EventTableAdapter eventTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.EventTableAdapter();

        DAL.CMSDBDataSetTableAdapters.EventListTableAdapter eventListTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.EventListTableAdapter();

        DAL.CMSDBDataSetTableAdapters.ItemTableAdapter itemTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.ItemTableAdapter();

        DAL.CMSDBDataSetTableAdapters.MediaTableAdapter mediaTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.MediaTableAdapter();

        DAL.CMSDBDataSetTableAdapters.POIItemTableAdapter poiItemTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.POIItemTableAdapter();

        DAL.CMSDBDataSetTableAdapters.POITableAdapter poiTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.POITableAdapter();

        DAL.CMSDBDataSetTableAdapters.POIListTableAdapter poiListTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.POIListTableAdapter();

        DAL.CMSDBDataSetTableAdapters.SubtypeTableAdapter subtypeTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.SubtypeTableAdapter();

        DAL.CMSDBDataSetTableAdapters.UserTableAdapter userTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.UserTableAdapter();

        DAL.CMSDBDataSetTableAdapters.NewsTableAdapter newsTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.NewsTableAdapter();

        /*********************************************************************
         * Category data access
         *********************************************************************/

        public DAL.CMSDBDataSet.CategoryDataTable getAllCategory()
        {
            return categoryTableAdapter.GetData();        
        }

        public int UpdateCategory(String categoryName, int original_CategoryID)
        {
            return categoryTableAdapter.Update(categoryName, original_CategoryID);
        }

        public int DeleteCategory(int original_CategoryID)
        {
            return categoryTableAdapter.Delete(original_CategoryID);
        }

        public int InsertCategory(String categoryName)
        {
            return categoryTableAdapter.Insert(categoryName);
        }

        /*********************************************************************
         * Sub Type data access
         *********************************************************************/

        public DAL.CMSDBDataSet.SubtypeDataTable getAllSubtype()
        {
            return subtypeTableAdapter.GetData();
        }

        public int UpdateSubtype(String subtypeName, int original_SubtypeID)
        {
            return subtypeTableAdapter.Update(subtypeName, original_SubtypeID);
        }

        public int DeleteSubtype(int original_SubtypeID)
        {
            return subtypeTableAdapter.Delete(original_SubtypeID);
        }

        public int InsertSubtype(String subtypeName)
        {
            return subtypeTableAdapter.Insert(subtypeName);
        }


        /*********************************************************************
         * POI data access
         *********************************************************************/

        public DAL.CMSDBDataSet.POIListDataTable getAllPOIList()
        {
            return poiListTableAdapter.GetDataAll();
        }

        public DAL.CMSDBDataSet.POIItemRow getPOIByItemID(int ItemID)
        {
            return (DAL.CMSDBDataSet.POIItemRow)poiItemTableAdapter.GetDataByItemID(ItemID).Rows[0];
        }

        public int UpdatePOI(String ItemName, String Details, decimal Cost, int Rating, int? Phone, String Website, String Email,
            String OpeningHours, String StreetNo, String StreetName, double Latitude, double Longitude, int Postcode, String Suburb, 
            int? SubtypeID, int CategoryID, int? Original_SubtypeID, int Original_ItemID, int Original_CategoryID)
        {
            if (poiTableAdapter.Update(CategoryID, Original_ItemID, Original_CategoryID) > 0)
            {
                return itemTableAdapter.Update(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, StreetNo,
                    StreetName, Latitude, Longitude, Postcode, Suburb, SubtypeID, Original_ItemID, Original_SubtypeID);
            }
            return 0;
        }

        public int DeletePOI(int Original_ItemID, int Original_CategoryID, int Original_SubtypeID)
        {
            if (poiTableAdapter.Delete(Original_ItemID, Original_CategoryID) > 0)
            {
                return itemTableAdapter.Delete(Original_ItemID, Original_SubtypeID);
            }
            return 0;
        }

        public int InsertPOI(String ItemName, String Details, decimal Cost, int Rating, int? Phone, String Website, String Email,
            String OpeningHours, String StreetNo, String StreetName, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID,int CategoryID)
        {
            
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, StreetNo,
                    StreetName, Latitude, Longitude, Postcode, Suburb, SubtypeID)>0)
            {
                int? ItemID = itemTableAdapter.getNewlyAddedID();
                return poiTableAdapter.InsertNewPOI(ItemID, CategoryID);
            }
            return 0;
        }


        /*********************************************************************
         * Event data access
         *********************************************************************/

        public DAL.CMSDBDataSet.EventListDataTable getAllEventList()
        {
            return eventListTableAdapter.GetDataAll();
        }

        public DAL.CMSDBDataSet.EventItemRow getEventByItemID(int ItemID)
        {
            return (DAL.CMSDBDataSet.EventItemRow)eventItemTableAdapter.GetDataByItemID(ItemID).Rows[0];
        }

        public int UpdateEvent(String ItemName, String Details, decimal Cost, int Rating, int? Phone, String Website, String Email,
            String OpeningHours, String StreetNo, String StreetName, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate, int? Original_SubtypeID, int Original_ItemID)
        {
            if (eventTableAdapter.Update(EventStartDate, EventEndDate, Original_ItemID) > 0)
            {
                return itemTableAdapter.Update(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, StreetNo,
                    StreetName, Latitude, Longitude, Postcode, Suburb, SubtypeID, Original_ItemID, Original_SubtypeID);
            }
            return 0;
        }

        public int DeleteEvent(int Original_ItemID, int Original_SubtypeID)
        {
            if (eventTableAdapter.Delete(Original_ItemID) > 0)
            {
                return itemTableAdapter.Delete(Original_ItemID, Original_SubtypeID);
            }
            return 0;
        }

        public int InsertEvent(String ItemName, String Details, decimal Cost, int Rating, int? Phone, String Website, String Email,
            String OpeningHours, String StreetNo, String StreetName, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate)
        {
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, StreetNo,
                    StreetName, Latitude, Longitude, Postcode, Suburb, SubtypeID) > 0)
            {
                int? ItemID = itemTableAdapter.getNewlyAddedID();
                return eventTableAdapter.InsertNewItem(ItemID,EventStartDate, EventEndDate);
            }
            return 0;
        }


        /*********************************************************************
         * User data access
         *********************************************************************/

        public DAL.CMSDBDataSet.UserDataTable getAllUsers()
        {
            return userTableAdapter.GetData();
        }

        public DAL.CMSDBDataSet.UserDataTable getAllSubcribedUsers()
        {
            return userTableAdapter.GetAllSubcribedUsers();
        }

        public DAL.CMSDBDataSet.UserDataTable getAllUnsubcribedUsers()
        {
            return userTableAdapter.GetAllUnsubscribedUsers();
        }


        /*********************************************************************
         * Media data access
         *********************************************************************/

        public DAL.CMSDBDataSet.MediaDataTable getMediaByItemID(int ItemID)
        {
            return mediaTableAdapter.GetDataByItemID(ItemID);
        }

        public int UpdateMedia(int ItemID, String MediaURL, String MediaType, int Original_MediaID, int Original_ItemID)
        {
            return mediaTableAdapter.Update(ItemID, MediaURL, MediaType, Original_MediaID, Original_ItemID);
        }

        public int DeleteMediaByItemID(int ItemID)
        {
            return mediaTableAdapter.DeleteByItemID(ItemID);
        }

        public int DeleteMedia(int Original_MediaID, int Original_ItemID)
        {
            return mediaTableAdapter.Delete(Original_MediaID, Original_ItemID);
        }

        public int InsertMedia(int ItemID, String MediaURL, String MediaType)
        {
            return mediaTableAdapter.Insert(ItemID, MediaURL, MediaType);
        }


        /*********************************************************************
        * News data access
        *********************************************************************/
        public DAL.CMSDBDataSet.NewsDataTable getAllNews()
        {
            return newsTableAdapter.GetData();
        }

        public int UpdateNews(String newsHeading, DateTime newsDateTime, String newsBody, String newsMediaURL, String newsPublisher,int original_NewsID)
        {
            return newsTableAdapter.Update(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, original_NewsID);
        }

        public int DeleteNews(int original_NewsID)
        {
            return newsTableAdapter.Delete(original_NewsID);
        }

        public int InsertNews(String newsHeading, DateTime newsDateTime, String newsBody, String newsMediaURL, String newsPublisher)
        {
            return newsTableAdapter.Insert(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher);
        }

        public DAL.CMSDBDataSet.NewsRow getNewsById(int Id)
        {
            return (DAL.CMSDBDataSet.NewsRow)newsTableAdapter.GetNewsByID(Id).Rows[0];
        }
    }
}