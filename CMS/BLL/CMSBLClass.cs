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

        DAL.CMSDBDataSetTableAdapters.TourTableAdapter tourTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.TourTableAdapter();

        DAL.CMSDBDataSetTableAdapters.TourLocationTableAdapter tourLocationTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.TourLocationTableAdapter();

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

        public int UpdatePOI(String ItemName, String Details, decimal Cost, int Rating, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb, 
            int? SubtypeID, int CategoryID, int? Original_SubtypeID, int Original_ItemID, int Original_CategoryID)
        {
            if (poiTableAdapter.Update(CategoryID, Original_ItemID, Original_CategoryID) > 0)
            {
                return itemTableAdapter.Update(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, 
                    Address, Latitude, Longitude, Postcode, Suburb, SubtypeID, Original_ItemID, Original_SubtypeID);
            }
            return 0;
        }

        public int DeletePOI(int Original_ItemID, int Original_SubtypeID)
        {
            if (poiTableAdapter.Delete(Original_ItemID) > 0)
            {
                return itemTableAdapter.Delete(Original_ItemID, Original_SubtypeID);
            }
            return 0;
        }

        public int InsertPOI(String ItemName, String Details, decimal Cost, int Rating, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID,int? CategoryID)
        {
            
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID)>0)
            {
                int ItemID = (int)itemTableAdapter.getNewlyAddedItemID();
                poiTableAdapter.InsertNewPOI(ItemID, CategoryID);
                return Convert.ToInt32(ItemID);
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

        public int UpdateEvent(String ItemName, String Details, decimal Cost, int Rating, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate, int? Original_SubtypeID, int Original_ItemID)
        {
            if (eventTableAdapter.Update(EventStartDate, EventEndDate, Original_ItemID) > 0)
            {
                return itemTableAdapter.Update(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, Original_ItemID, Original_SubtypeID);
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

        public int InsertEvent(String ItemName, String Details, decimal Cost, int Rating, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate)
        {
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Rating, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID) > 0)
            {
                int? ItemID = itemTableAdapter.getNewlyAddedItemID();
                eventTableAdapter.InsertNewItem(ItemID,EventStartDate, EventEndDate);
                return Convert.ToInt32(ItemID);
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

        public int UpdateMedia(int? ItemID, String MediaURL, String MediaType, int? TourID, int Original_MediaID)
        {
            return mediaTableAdapter.Update(ItemID, MediaURL, MediaType, TourID, Original_MediaID);
        }

        public int DeleteMediaByItemID(int ItemID)
        {
            return mediaTableAdapter.DeleteByItemID(ItemID);
        }

        public int DeleteMediaByMediaURL(int ItemID, string MediaURL)
        {
            return mediaTableAdapter.DeleteByMediaURL( MediaURL, ItemID);
        }

        public int DeleteMedia(int Original_MediaID)
        {
            return mediaTableAdapter.Delete(Original_MediaID);
        }

        public int CountImagesMediaByItemId(int ItemID)
        {
            return Convert.ToInt32(mediaTableAdapter.CountImagesMediaByItemId(ItemID));
        }

        public int DeleteVideoMediaByItemId(int ItemID)
        {
            return mediaTableAdapter.DeleteVideoMedia(ItemID);
        }

        public int InsertMedia(int? ItemID, String MediaURL, String MediaType, int? TourID)
        {
            return mediaTableAdapter.Insert(ItemID, MediaURL, MediaType, TourID);
        }


        /*********************************************************************
        * News data access
        *********************************************************************/
        public DAL.CMSDBDataSet.NewsDataTable getAllNews()
        {
            return newsTableAdapter.GetData();
        }

        public int UpdateNews(String newsHeading, DateTime newsDateTime, String newsBody, String newsMediaURL, String newsPublisher, String newsAuthor, int original_NewsID)
        {
            return newsTableAdapter.Update(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, newsAuthor, original_NewsID);
        }

        public int DeleteNews(int original_NewsID)
        {
            return newsTableAdapter.Delete(original_NewsID);
        }

        public int InsertNews(String newsHeading, DateTime newsDateTime, String newsBody, String newsMediaURL, String newsPublisher, String newsAuthor)
        {
            return newsTableAdapter.Insert(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, newsAuthor);
        }

        public DAL.CMSDBDataSet.NewsRow getNewsById(int Id)
        {
            return (DAL.CMSDBDataSet.NewsRow)newsTableAdapter.GetNewsByID(Id).Rows[0];
        }

        /*********************************************************************
        * Tour data access
        *********************************************************************/

        public DAL.CMSDBDataSet.TourDataTable getAllTour()
        {
            return tourTableAdapter.GetData();        
        }

        public DAL.CMSDBDataSet.TourRow getTourByID(int TourID)
        {
            return (DAL.CMSDBDataSet.TourRow)tourTableAdapter.GetDataByID(TourID).Rows[0];
        }


        public int updateTour(String TourName, String TourDetail, Decimal TourCost, String TourPhone, String TourWebsite, 
            String TourEmail, int TourID)
        {
            return tourTableAdapter.UpdateByTourID(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourID);
        }

        public int insertTour(String TourName, String TourDetail, Decimal TourCost, String TourPhone, String TourWebsite, String TourEmail)
        {
            return tourTableAdapter.Insert(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail);        
        }

        public int getNewlyInsertedTourID()
        {
            return (int)tourTableAdapter.getNewlyAddedTourID();
        }

        public int deleteTour(int TourID)
        {
            tourLocationTableAdapter.DeleteByTourID(TourID);
            return tourTableAdapter.Delete(TourID);
        }

        /*********************************************************************
        * Tour Location data access
        *********************************************************************/

        public DAL.CMSDBDataSet.TourLocationDataTable getTourLocationByTourID(int TourID)
        {
            return tourLocationTableAdapter.GetDataByTourID(TourID);
        }

        public int deleteTourLocationByLocationID(int TourLocationID)
        {
            return tourLocationTableAdapter.Delete(TourLocationID);
        }

        public int insertTourLocation(int TourID, short TourSeqNum, String LocationName, double Latitude, double Longitude, 
            String Address, String Suburb, short Postcode)
        {
            return tourLocationTableAdapter.Insert(TourID, TourSeqNum, LocationName, Latitude, Longitude, Address, Suburb, Postcode);        
        }

        public int updateTourLocation(int TourID, short TourSeqNum, String LocationName, double Latitude, double Longitude, String Address, 
                String Suburb, short Postcode, int Original_TourLocationID)
        {
            return tourLocationTableAdapter.UpdateByTourLocationID(TourID, TourSeqNum, LocationName, Latitude, Longitude, Address, 
                Suburb, Postcode, Original_TourLocationID);
        }

        

    }
}