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

        DAL.CMSDBDataSetTableAdapters.POITourTableAdapter poiTourTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.POITourTableAdapter();

        DAL.CMSDBDataSetTableAdapters.MajorRegionTableAdapter majorRegionTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.MajorRegionTableAdapter();

        DAL.CMSDBDataSetTableAdapters.TourPOIListTableAdapter tourPOIListTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.TourPOIListTableAdapter();

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

        public String getSubtypeName(int subtypeID)
        {
            return subtypeTableAdapter.getNameByID(subtypeID);
        }


        /*********************************************************************
        * MajorRegion data access
        *********************************************************************/

        public DAL.CMSDBDataSet.MajorRegionDataTable getAllMajorRegion()
        {
            return majorRegionTableAdapter.GetData();
        }

        public int UpdateMajorRegion(String MajorRegionName, int Original_MajorRegionID)
        {
            return majorRegionTableAdapter.Update(MajorRegionName, Original_MajorRegionID);
        }

        public int DeleteMajorRegion(int Original_MajorRegionID)
        {
            return majorRegionTableAdapter.Delete(Original_MajorRegionID);
        }

        public int InsertMajorRegion(String MajorRegionName)
        {
            return majorRegionTableAdapter.Insert(MajorRegionName);
        }

        public String getMajorRegionName(int MajorRegionID)
        {
            return majorRegionTableAdapter.getNameByID(MajorRegionID);
        }

        /*********************************************************************
         * POI data access
         *********************************************************************/

        public DAL.CMSDBDataSet.POIListDataTable getAllPOIList()
        {
            return poiListTableAdapter.GetDataAll();
        }

        public DAL.CMSDBDataSet.POIListDataTable searchPOI(String searchString)
        {
            return poiListTableAdapter.GetDataBySearch(searchString);
        }

        public DAL.CMSDBDataSet.POIItemRow getPOIByItemID(int ItemID)
        {
            return (DAL.CMSDBDataSet.POIItemRow)poiItemTableAdapter.GetDataByItemID(ItemID).Rows[0];
        }

        public int UpdatePOI(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb, 
            int? SubtypeID, int? MajorRegionID, int CategoryID, int Original_ItemID, int Original_CategoryID)
        {
            if (poiTableAdapter.Update(CategoryID, Original_ItemID) > 0)
            {
                return itemTableAdapter.Update(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address, Latitude, Longitude, 
                    Postcode, Suburb, SubtypeID, MajorRegionID, Original_ItemID);
            }
            return 0;
        }

        public int DeletePOI(int Original_ItemID)
        {
            if (poiTableAdapter.Delete(Original_ItemID) > 0)
            {
                return itemTableAdapter.Delete(Original_ItemID);
            }
            return 0;
        }

        public int InsertPOI(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, int? CategoryID, int? MajorRegionID)
        {
            
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID)>0)
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

        public int UpdateEvent(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate, int? MajorRegionID, int Original_ItemID)
        {
            if (eventTableAdapter.Update(EventStartDate, EventEndDate, Original_ItemID) > 0)
            {
                return itemTableAdapter.Update(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID, Original_ItemID);
            }
            return 0;
        }

        public int DeleteEvent(int Original_ItemID)
        {
            if (eventTableAdapter.Delete(Original_ItemID) > 0)
            {
                return itemTableAdapter.Delete(Original_ItemID);
            }
            return 0;
        }

        public int InsertEvent(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate, int? MajorRegionID)
        {
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID) > 0)
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

        public DAL.CMSDBDataSet.MediaDataTable getMediaByTourID(int TourID)
        {
            return mediaTableAdapter.GetDataByTourID(TourID);
        }

        public int UpdateMedia(int? ItemID, String MediaURL, String MediaType, int? TourID, int Original_MediaID)
        {
            return mediaTableAdapter.Update(ItemID, MediaURL, MediaType, TourID, Original_MediaID);
        }

        public int DeleteMediaByItemID(int ItemID)
        {
            return mediaTableAdapter.DeleteByItemID(ItemID);
        }

        public int DeleteMediaByTourID(int TourID)
        {
            return mediaTableAdapter.DeleteByTourID(TourID);
        }

        public int DeleteMediaByMediaURL(int ItemID, string MediaURL)
        {
            return mediaTableAdapter.DeleteByMediaURL( MediaURL, ItemID);
        }

        public int DeleteMediaByMediaURLAndTourID(int TourID, string MediaURL)
        {
            return mediaTableAdapter.DeleteByMediaURLAndTourID(MediaURL, TourID);
        }

        public int DeleteMedia(int Original_MediaID)
        {
            return mediaTableAdapter.Delete(Original_MediaID);
        }

        public int CountImagesMediaByItemId(int ItemID)
        {
            return Convert.ToInt32(mediaTableAdapter.CountImagesMediaByItemId(ItemID));
        }

        public int CountImagesMediaByTourID(int TourID)
        {
            return Convert.ToInt32(mediaTableAdapter.CountImagesMediaByTourID(TourID));
        }

        public int DeleteVideoMediaByItemId(int ItemID)
        {
            return mediaTableAdapter.DeleteVideoMedia(ItemID);
        }

        public int DeleteVideoMediaByTourId(int TourID)
        {
            return mediaTableAdapter.DeleteVideoMediaByTourID(TourID);
        }

        public int DeleteAudioByTourID(int TourID)
        {
            return mediaTableAdapter.DeleteAudioByTourID(TourID);
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

        public DAL.CMSDBDataSet.TourDataTable getAllTours()
        {
            return tourTableAdapter.GetData();        
        }

        public DAL.CMSDBDataSet.TourRow getTourByID(int TourID)
        {
            return (DAL.CMSDBDataSet.TourRow)tourTableAdapter.GetDataByTourID(TourID).Rows[0];
        }


        public int updateTour(String TourName, String TourDetail, int TourCost, String TourPhone, String TourWebsite, 
            String TourEmail, String TourAgent, int TourID)
        {
            return tourTableAdapter.Update(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourAgent, TourID);
        }

        public int insertTour(String TourName, String TourDetail, int TourCost, String TourPhone, String TourWebsite, String TourEmail, String TourAgent)
        {
            return tourTableAdapter.Insert(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourAgent);        
        }

        public int getNewlyInsertedTourID()
        {
            return (int)tourTableAdapter.getNewlyAddedTourID();
        }

        public int deleteTour(int TourID)
        {
            return tourTableAdapter.Delete(TourID);
        }

        /*********************************************************************
        * POITour data access
        *********************************************************************/

        public DAL.CMSDBDataSet.TourPOIListDataTable getTourPOIListByTourID(int tourID)
        {
            return tourPOIListTableAdapter.GetDataTourID(tourID);
        }

        public int deleteTourPOIListByTourID(int tourID)
        {
            return poiTourTableAdapter.DeleteByTourID(tourID);
        }

        public int insertTourPOIList(int POIID, int TourID, int seqNum)
        {
            return poiTourTableAdapter.Insert(POIID, TourID, seqNum);
        }
    }
}