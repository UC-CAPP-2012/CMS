using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class CMSBLClass
    {
        #region dataAdapters

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

        DAL.CMSDBDataSetTableAdapters.SubTypeTableAdapter subtypeTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.SubTypeTableAdapter();

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

        #endregion
        /*********************************************************************
         * Category data access
         *********************************************************************/
        #region category

        /// <summary>
        /// Get all Categories from the database.
        /// </summary>
        /// <returns>Datatable containing all Categories.</returns>
        public DAL.CMSDBDataSet.CategoryDataTable getAllCategory()
        {
            return categoryTableAdapter.GetData();        
        }

        /// <summary>
        /// Update one Category.
        /// </summary>
        /// <param name="categoryName">The new name to update Category with.</param>
        /// <param name="original_CategoryID">The original ID of Category to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdateCategory(String categoryName, int original_CategoryID)
        {
            return categoryTableAdapter.Update(categoryName, original_CategoryID);
        }

        /// <summary>
        /// Delete one Category.
        /// </summary>
        /// <param name="original_CategoryID">The original ID of Category to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteCategory(int original_CategoryID)
        {
            return categoryTableAdapter.Delete(original_CategoryID);
        }

        /// <summary>
        /// Insert one Category.
        /// </summary>
        /// <param name="categoryName">The name for the new Category.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertCategory(String categoryName)

        {
            categoryTableAdapter.Insert(categoryName);
            return (int) categoryTableAdapter.getNewID();
        }

        #endregion
        /*********************************************************************
         * Sub Type data access
         *********************************************************************/
        #region Subtype

        /// <summary>
        /// Get all Subtypes from the database.
        /// </summary>
        /// <returns>Datatable containing all Subtypes.</returns>
        public DAL.CMSDBDataSet.SubTypeDataTable getAllSubtype()
        {
            return subtypeTableAdapter.GetData();
        }

        /// <summary>
        /// Update one Subtype.
        /// </summary>
        /// <param name="subtypeName">The new name to update Subtype with.</param>
        /// <param name="original_SubtypeID">The original ID of Subtype to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdateSubtype(String subtypeName, int original_SubtypeID)
        {
            return subtypeTableAdapter.Update(subtypeName, original_SubtypeID);
        }

        /// <summary>
        /// Delete one Subtype.
        /// </summary>
        /// <param name="original_SubtypeID">The original ID of Subtype to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteSubtype(int original_SubtypeID)
        {
            return subtypeTableAdapter.Delete(original_SubtypeID);
        }

        /// <summary>
        /// Insert one Subtype.
        /// </summary>
        /// <param name="subtypeName">The name for the new Subtype.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertSubtype(String subtypeName)
        {
            subtypeTableAdapter.Insert(subtypeName);
            return (int)subtypeTableAdapter.getNewID();
        }

        /// <summary>
        /// Retrieve the name of Subtype
        /// </summary>
        /// <param name="subtypeID">The ID of the Subtype to find name for.</param>
        /// <returns>Subtype name</returns>
        public String getSubtypeName(int subtypeID)
        {
            return subtypeTableAdapter.getNameByID(subtypeID);
        }

        #endregion
        /*********************************************************************
        * MajorRegion data access
        *********************************************************************/
        #region MajorRegion

        /// <summary>
        /// Get all MajorRegions from the database.
        /// </summary>
        /// <returns>Datatable containing all MajorRegions.</returns>
        public DAL.CMSDBDataSet.MajorRegionDataTable getAllMajorRegion()
        {
            return majorRegionTableAdapter.GetData();
        }

        /// <summary>
        /// Update one MajorRegion.
        /// </summary>
        /// <param name="MajorRegionName">The new name to update MajorRegion with.</param>
        /// <param name="Original_MajorRegionID">The original ID of MajorRegion to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdateMajorRegion(String MajorRegionName, int Original_MajorRegionID)
        {
            return majorRegionTableAdapter.Update(MajorRegionName, Original_MajorRegionID);
        }

        /// <summary>
        /// Delete one MajorRegion.
        /// </summary>
        /// <param name="Original_MajorRegionID">The original ID of MajorRegion to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteMajorRegion(int Original_MajorRegionID)
        {
            return majorRegionTableAdapter.Delete(Original_MajorRegionID);
        }

        /// <summary>
        /// Insert one MajorRegion.
        /// </summary>
        /// <param name="MajorRegionName">The name for the new MajorRegion.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertMajorRegion(String MajorRegionName)
        {
            majorRegionTableAdapter.Insert(MajorRegionName);
            return (int) majorRegionTableAdapter.getNewID();
        }

        /// <summary>
        /// Retrieve the name of MajorRegion form the database.
        /// </summary>
        /// <param name="subtypeID">The ID of the MajorRegion to find name for.</param>
        /// <returns>MajorRegion name</returns>
        public String getMajorRegionName(int MajorRegionID)
        {
            return majorRegionTableAdapter.getNameByID(MajorRegionID);
        }

        #endregion
        /*********************************************************************
         * POI data access
         *********************************************************************/
        #region POI

        /// <summary>
        /// Get all POIs from the database.
        /// </summary>
        /// <returns>Datatable containing all POIs.</returns>
        public DAL.CMSDBDataSet.POIListDataTable getAllPOIList()
        {
            return poiListTableAdapter.GetDataAll();
        }

        /// <summary>
        /// Retrieve all POIs with the name that contains the search string form the database.
        /// </summary>
        /// <param name="searchString">The string to search.</param>
        /// <returns>Datatable containing searched POIs </returns>
        public DAL.CMSDBDataSet.POIListDataTable searchPOI(String searchString)
        {
            return poiListTableAdapter.GetDataBySearch(searchString);
        }

        /// <summary>
        /// Retrieve one POI row form the POI table in the database. 
        /// </summary>
        /// <param name="ItemID">The ID of the POI to retrieve.</param>
        /// <returns>Retrieved POI row</returns>
        public DAL.CMSDBDataSet.POIItemRow getPOIByItemID(int ItemID)
        {
            return (DAL.CMSDBDataSet.POIItemRow)poiItemTableAdapter.GetDataByItemID(ItemID).Rows[0];
        }

        /// <summary>
        /// Update one POI.
        /// </summary>
        /// <param name="ItemName">The new name to update POI with.</param>
        /// <param name="Details">The new details to update POI with.</param>
        /// <param name="Cost">The new cost to update POI with.</param>
        /// <param name="Phone">The new phone number to update POI with.</param>
        /// <param name="Website">The new website address to update POI with.</param>
        /// <param name="Email">The new email address to update POI with.</param>
        /// <param name="OpeningHours">The new opening hours to update POI with.</param>
        /// <param name="Address">The new address to update POI with.</param>
        /// <param name="Latitude">The new latitude to update POI with.</param>
        /// <param name="Longitude">The new longitude to update POI with.</param>
        /// <param name="Postcode">The new postcode to update POI with.</param>
        /// <param name="Suburb">The new suburb to update POI with.</param>
        /// <param name="SubtypeID">The new ID of the subtype to update POI with.</param>
        /// <param name="MajorRegionID">The new ID of the major region to update POI with.</param>
        /// <param name="CategoryID">The new ID of the category to update POI with.</param>
        /// <param name="Original_ItemID">The original ItemID of POI to update.</param>
        /// <param name="Original_CategoryID">The original CategoryID of POI to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdatePOI(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb, 
            int? SubtypeID, int? MajorRegionID, int CategoryID, int Original_ItemID, int Original_CategoryID)
        {
            // Update POI table first
            if (poiTableAdapter.UpdatePOI(CategoryID, Original_ItemID) > 0)
            {
                // Update Item table if updating POI table succeeds.
                return itemTableAdapter.Update(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address, Latitude, Longitude, 
                    Postcode, Suburb, SubtypeID, MajorRegionID, Original_ItemID);
            }
            return 0;

        }

        /// <summary>
        /// Delete one POI.
        /// </summary>
        /// <param name="Original_ItemID">The original ID of POI to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeletePOI(int Original_ItemID)
        {
            // Delete POI table first            
            if (poiTableAdapter.Delete(Original_ItemID) > 0)
            {
                // Delete Item table if deleting POI table succeeds.
                return itemTableAdapter.Delete(Original_ItemID);
            }
            return 0;
        }

        /// <summary>
        /// Insert one POI.
        /// </summary>
        /// <param name="ItemName">The name for new POI.</param>
        /// <param name="Details">The details for new POI.</param>
        /// <param name="Cost">The cost for new POI.</param>
        /// <param name="Phone">The phone number for new POI.</param>
        /// <param name="Website">The website address for new POI.</param>
        /// <param name="Email">The email address for new POI.</param>
        /// <param name="OpeningHours">The opening hours for new POI.</param>
        /// <param name="Address">The address for new POI.</param>
        /// <param name="Latitude">The latitude for new POI.</param>
        /// <param name="Longitude">The longitude for new POI.</param>
        /// <param name="Postcode">The postcode for new POI.</param>
        /// <param name="Suburb">The suburb for new POI.</param>
        /// <param name="SubtypeID">The ID of the subtype for new POI.</param>
        /// <param name="MajorRegionID">The ID of the major region for new POI.</param>
        /// <param name="CategoryID">The ID of the category for new POI.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertPOI(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, int CategoryID, int? MajorRegionID)
        {
            // Insert Item table first            
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID)>0)
            {
                // Insert POI table if inserting Item table succeeds.
                int ItemID = (int)itemTableAdapter.getNewlyAddedItemID();
                poiTableAdapter.Insert(ItemID, CategoryID);
                return Convert.ToInt32(ItemID);
            }
            return 0;
        }

        #endregion
        /*********************************************************************
         * Event data access
         *********************************************************************/
        #region Event

        /// <summary>
        /// Get all Events from the database.
        /// </summary>
        /// <returns>Datatable containing all Events.</returns>
        public DAL.CMSDBDataSet.EventListDataTable getAllEventList()
        {
            return eventListTableAdapter.GetDataAll();
        }

        /// <summary>
        /// Retrieve one Event row form the Event table in the database. 
        /// </summary>
        /// <param name="ItemID">The ID of the Event to retrieve.</param>
        /// <returns>Retrieved Event row</returns>
        public DAL.CMSDBDataSet.EventItemRow getEventByItemID(int ItemID)
        {
            return (DAL.CMSDBDataSet.EventItemRow)eventItemTableAdapter.GetDataByItemID(ItemID).Rows[0];
        }

        /// <summary>
        /// Update one Event.
        /// </summary>
        /// <param name="ItemName">The new name for new Event with.</param>
        /// <param name="Details">The new details to update Event with.</param>
        /// <param name="Cost">The new cost to update Event with.</param>
        /// <param name="Phone">The new phone number to update Event with.</param>
        /// <param name="Website">The new website address to update Event with.</param>
        /// <param name="Email">The new email address to update Event with.</param>
        /// <param name="OpeningHours">The new opening hours to update Event with.</param>
        /// <param name="Address">The new address to update Event with.</param>
        /// <param name="Latitude">The new latitude to update Event with.</param>
        /// <param name="Longitude">The new longitude to update Event with.</param>
        /// <param name="Postcode">The new postcode to update Event with.</param>
        /// <param name="Suburb">The new suburb to update Event with.</param>
        /// <param name="SubtypeID">The new ID of the subtype to update Event with.</param>
        /// <param name="EventStartDate">The new start date to update Event with.</param>
        /// <param name="EventEndDate">The new end date to update Event with.</param>        
        /// <param name="MajorRegionID">The new ID of the major region to update Event with.</param>
        /// <param name="Original_ItemID">The original ItemID of Event to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdateEvent(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate, int? MajorRegionID, int Original_ItemID)
        {
            // Update Event table first
            if (eventTableAdapter.Update(EventStartDate, EventEndDate, Original_ItemID) > 0)
            {
                // Update Item table if updating Event table succeeds.
                return itemTableAdapter.Update(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID, Original_ItemID);
            }
            return 0;
        }

        /// <summary>
        /// Delete one POI.
        /// </summary>
        /// <param name="Original_ItemID">The original ID of POI to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteEvent(int Original_ItemID)
        {
            // Delete Event table first
            
            if (eventTableAdapter.Delete(Original_ItemID) > 0)
            {
                // Delete Item table if deleting Event table succeeds.
                return itemTableAdapter.Delete(Original_ItemID);
            }
            return 0;
        }

        /// <summary>
        /// Insert one Event.
        /// </summary>
        /// <param name="ItemName">The name for new Event.</param>
        /// <param name="Details">The details for new Event.</param>
        /// <param name="Cost">The cost for new Event.</param>
        /// <param name="Phone">The phone number for new Event.</param>
        /// <param name="Website">The website address for new Event.</param>
        /// <param name="Email">The email address for new Event.</param>
        /// <param name="OpeningHours">The opening hours for new Event.</param>
        /// <param name="Address">The address for new Event.</param>
        /// <param name="Latitude">The latitude for new Event.</param>
        /// <param name="Longitude">The longitude for new Event.</param>
        /// <param name="Postcode">The postcode for new Event.</param>
        /// <param name="Suburb">The suburb for new Event.</param>
        /// <param name="SubtypeID">The ID of the subtype for new Event.</param>
        /// <param name="EventStartDate">The start date for new Event.</param>
        /// <param name="EventEndDate">The end date for new Event.</param>        
        /// <param name="MajorRegionID">The ID of the major region for new Event.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertEvent(String ItemName, String Details, int Cost, String Phone, String Website, String Email,
            String OpeningHours, String Address, double Latitude, double Longitude, int Postcode, String Suburb,
            int? SubtypeID, DateTime EventStartDate, DateTime EventEndDate, int? MajorRegionID)
        {
            // Insert Item table first            
            if (itemTableAdapter.Insert(ItemName, Details, Cost, Phone, Website, Email, OpeningHours, Address,
                    Latitude, Longitude, Postcode, Suburb, SubtypeID, MajorRegionID) > 0)
            {
                // Insert Event table if inserting Item table succeeds.
                int ItemID = Convert.ToInt32(itemTableAdapter.getNewlyAddedItemID());
                eventTableAdapter.Insert(ItemID,EventStartDate, EventEndDate);
                return Convert.ToInt32(ItemID);
            }
            return 0;
        }

        #endregion
        /*********************************************************************
         * User data access
         *********************************************************************/
        #region User

        /// <summary>
        /// Get all Users from the database.
        /// </summary>
        /// <returns>Datatable containing all Users.</returns>
        public DAL.CMSDBDataSet.UserDataTable getAllUsers()
        {
            return userTableAdapter.GetData();
        }

        /// <summary>
        /// Get all Subscribed Users from the database.
        /// </summary>
        /// <returns>Datatable containing all Subscribed Users.</returns>
        public DAL.CMSDBDataSet.UserDataTable getAllSubcribedUsers()
        {
            return userTableAdapter.GetAllSubcribedUsers();
        }

        /// <summary>
        /// Get all Unsubscribed Users from the database.
        /// </summary>
        /// <returns>Datatable containing all Unsubscribed Users.</returns>
        public DAL.CMSDBDataSet.UserDataTable getAllUnsubcribedUsers()
        {
            return userTableAdapter.GetAllUnsubscribedUsers();
        }

        #endregion
        /*********************************************************************
         * Media data access
         *********************************************************************/
        #region Media

        /// <summary>
        /// Insert one Media for Item or Tour.
        /// </summary>
        /// <param name="ItemID">The Item ID to add new Media for.</param>
        /// <param name="MediaURL">The URL of the media to insert.</param>
        /// <param name="MediaType">The Type of the media to insert.</param>      
        /// <param name="TourID">The Tour ID to add new Media for.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertMedia(int? ItemID, String MediaURL, String MediaType, int? TourID)
        {
            mediaTableAdapter.Insert(ItemID, MediaURL, MediaType, TourID);
            return (int) mediaTableAdapter.getNewID();
        }

        /// <summary>
        /// Retrieve Medias for an Item from the database.
        /// </summary>
        /// <param name="ItemID">The ID of the Item to retrieve Medias for.</param>
        /// <returns>Data Table containing retrieved Medias</returns>
        public DAL.CMSDBDataSet.MediaDataTable getMediaByItemID(int ItemID)
        {
            return mediaTableAdapter.GetDataByItemID(ItemID);
        }

        /// <summary>
        /// Retrieve Medias for a Tour from the database.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to retrieve Medias for.</param>
        /// <returns>Data Table containing retrieved Medias</returns>
        public DAL.CMSDBDataSet.MediaDataTable getMediaByTourID(int TourID)
        {
            return mediaTableAdapter.GetDataByTourID(TourID);
        }

        /// <summary>
        /// Update one Media.
        /// </summary>
        /// <param name="ItemID">The new Item ID to update Media with.</param>
        /// <param name="MediaURL">The new URL of the media to update Media with.</param>
        /// <param name="MediaType">The new type of the media to update Media with.</param>      
        /// <param name="TourID">The new Tour IDto update Media with.</param>
        /// <param name="Original_MediaID">The original ID of Media to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdateMedia(int? ItemID, String MediaURL, String MediaType, int? TourID, int Original_MediaID)
        {
            return mediaTableAdapter.Update(ItemID, MediaURL, MediaType, TourID, Original_MediaID);
        }

        /// <summary>
        /// Delete all Medias for a particular Item.
        /// </summary>
        /// <param name="ItemID">The ID of the Item to delete Medias for.</param>
        /// <returns>Number of Medias deleted</returns>   
        public int DeleteMediaByItemID(int ItemID)
        {
            return mediaTableAdapter.DeleteByItemID(ItemID);
        }

        /// <summary>
        /// Delete all Medias for a particular Tour.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to delete Medias for.</param>
        /// <returns>Number of Medias deleted</returns>  
        public int DeleteMediaByTourID(int TourID)
        {
            return mediaTableAdapter.DeleteByTourID(TourID);
        }

        /// <summary>
        /// Delete one Media of a particular Item with the matching URL.
        /// </summary>
        /// <param name="ItemID">The ID of the Item to delete Media for.</param>
        /// <param name="MediaURL">The URL of the Media to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>  
        public int DeleteMediaByMediaURL(int ItemID, string MediaURL)
        {
            return mediaTableAdapter.DeleteByMediaURL( MediaURL, ItemID);
        }

        /// <summary>
        /// Delete one Media of a particular Tour with the matching URL.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to delete Media for.</param>
        /// <param name="MediaURL">The URL of the Media to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>  
        public int DeleteMediaByMediaURLAndTourID(int TourID, string MediaURL)
        {
            return mediaTableAdapter.DeleteByMediaURLAndTourID(MediaURL, TourID);
        }

        /// <summary>
        /// Delete one Media.
        /// </summary>
        /// <param name="Original_MediaID">The ID of the Media to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>  
        public int DeleteMedia(int Original_MediaID)
        {
            return mediaTableAdapter.Delete(Original_MediaID);
        }

        /// <summary>
        /// Get the number of Images for a particular Item
        /// </summary>
        /// <param name="ItemID">The ID of the Item to count the number of Images.</param>
        /// <returns>The Number of Images</returns>  
        public int CountImagesMediaByItemId(int ItemID)
        {
            return Convert.ToInt32(mediaTableAdapter.CountImagesMediaByItemId(ItemID));
        }

        /// <summary>
        /// Get the number of Images for a particular Tour.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to count the number of Images.</param>
        /// <returns>The Number of Images</returns>  
        public int CountImagesMediaByTourID(int TourID)
        {
            return Convert.ToInt32(mediaTableAdapter.CountImagesMediaByTourID(TourID));
        }

        /// <summary>
        /// Delete video type Media of a particular Item.
        /// </summary>
        /// <param name="ItemID">The ID of the Item to delete video for.</param>
        /// <returns>1 for success, 0 for fail.</returns>  
        public int DeleteVideoMediaByItemId(int ItemID)
        {
            return mediaTableAdapter.DeleteVideoMedia(ItemID);
        }

        /// <summary>
        /// Delete video type Media of a particular Tour.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to delete video for.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteVideoMediaByTourId(int TourID)
        {
            return mediaTableAdapter.DeleteVideoMediaByTourID(TourID);
        }

        /// <summary>
        /// Delete audio type Media of a particular Tour.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to delete audio for.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteAudioByTourID(int TourID)
        {
            return mediaTableAdapter.DeleteAudioByTourID(TourID);
        }

        /// <summary>
        /// Delete audio type Media of a particular Item.
        /// </summary>
        /// <param name="ItemID">The ID of the Item to delete audio for.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteAudioByItemID(int ItemID)
        {
            return mediaTableAdapter.deleteAudioByItemID(ItemID);
        }

        /// <summary>
        /// Get the URL of audio type Media for a particular Tour.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to get the audio URL for.</param>
        /// <returns>The URL of audio type Media</returns>
        public String getAudioURLByTourID(int TourID)
        {
            return mediaTableAdapter.getAudioURLByTourID(TourID);
        }

        /// <summary>
        /// Get the URL of audio type Media for a particular Item.
        /// </summary>
        /// <param name="Item">The ID of the Item to get the audio URL for.</param>
        /// <returns>The URL of audio type Media</returns>
        public String getAudioURLByItemID(int Item)
        {
            return mediaTableAdapter.getAudioURLByItemID(Item);
        }

        /// <summary>
        /// Get all URLs of image and audio type Medias for a particular Tour.
        /// </summary>
        /// <param name="TourID">The ID of the Tour to get the URLs for.</param>
        /// <returns>The datatable containing searched URLs.</returns>
        public DAL.CMSDBDataSet.MediaDataTable getMediaURLByTourID(int TourID)
        {
            return mediaTableAdapter.GetDataByMediaURL(TourID);
        }

        /// <summary>
        /// Get all URLs of image and audio type Medias for a particular Item.
        /// </summary>
        /// <param name="ItemID">The ID of the Item to get the URLs for.</param>
        /// <returns>The datatable containing searched URLs.</returns>
        public DAL.CMSDBDataSet.MediaDataTable getMediaURLByItemID(int ItemID)
        {
            return mediaTableAdapter.GetDataByMediaURLByItemID(ItemID);
        }

        #endregion
        /*********************************************************************
        * News data access
        *********************************************************************/
        #region News

        /// <summary>
        /// Get all News from the database.
        /// </summary>
        /// <returns>Datatable containing all News.</returns>
        public DAL.CMSDBDataSet.NewsDataTable getAllNews()
        {
            return newsTableAdapter.GetData();
        }

        /// <summary>
        /// Update one News.
        /// </summary>
        /// <param name="newsHeading">The new heading to update News with.</param>
        /// <param name="newsDateTime">The new date time to update News with.</param>
        /// <param name="newsBody">The new body to update News with.</param>
        /// <param name="newsMediaURL">The new media url to update News with.</param>
        /// <param name="newsPublisher">The new publiser to update News with.</param>
        /// <param name="newsAuthor">The new author to update News with.</param>
        /// <param name="original_NewsID">The original ID of News to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int UpdateNews(String newsHeading, DateTime newsDateTime, String newsBody, String newsMediaURL, String newsPublisher, String newsAuthor, int original_NewsID)
        {
            return newsTableAdapter.Update(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, newsAuthor, original_NewsID);
        }

        /// <summary>
        /// Delete one News.
        /// </summary>
        /// <param name="original_NewsID">The original ID of News to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int DeleteNews(int original_NewsID)
        {
            return newsTableAdapter.Delete(original_NewsID);
        }

        /// <summary>
        /// Insert one News.
        /// </summary>
        /// <param name="newsHeading">The heading for the new News.</param>
        /// <param name="newsDateTime">The date time for the new News.</param>
        /// <param name="newsBody">The body for the new News.</param>
        /// <param name="newsMediaURL">The media url for the new News.</param>
        /// <param name="newsPublisher">The publiser for the new News.</param>
        /// <param name="newsAuthor">The author for the New news.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int InsertNews(String newsHeading, DateTime newsDateTime, String newsBody, String newsMediaURL, String newsPublisher, String newsAuthor)
        {
            newsTableAdapter.Insert(newsHeading, newsDateTime, newsBody, newsMediaURL, newsPublisher, newsAuthor);
            return (int) newsTableAdapter.getNewID();
        }

        /// <summary>
        /// Retrieve one News row form the News table in the database. 
        /// </summary>
        /// <param name="Id">The ID of the News to retrieve.</param>
        /// <returns>Retrieved News row</returns>
        public DAL.CMSDBDataSet.NewsRow getNewsById(int Id)
        {
            return (DAL.CMSDBDataSet.NewsRow)newsTableAdapter.GetNewsByID(Id).Rows[0];
        }

        #endregion
        /*********************************************************************
        * Tour data access
        *********************************************************************/        
        #region Tour

        /// <summary>
        /// Get all Tours from the database.
        /// </summary>
        /// <returns>Data table containing all Tours.</returns>
        public DAL.CMSDBDataSet.TourDataTable getAllTours()
        {
            return tourTableAdapter.GetData();        
        }

        /// <summary>
        /// Retrieve one Tour row form the Tour table in the database. 
        /// </summary>
        /// <param name="TourID">The ID of the Tour to retrieve.</param>
        /// <returns>Retrieved Tour row</returns>
        public DAL.CMSDBDataSet.TourRow getTourByID(int TourID)
        {
            return (DAL.CMSDBDataSet.TourRow)tourTableAdapter.GetDataByTourID(TourID).Rows[0];
        }

        /// <summary>
        /// Update one Tour.
        /// </summary>
        /// <param name="TourName">The new name to update Tour with.</param>
        /// <param name="TourDetail">The new detail to update Tour with.</param>
        /// <param name="TourCost">The new cost to update Tour with.</param>
        /// <param name="TourPhone">The new phone number to update Tour with.</param>
        /// <param name="TourWebsite">The new website address to update Tour with.</param>
        /// <param name="TourEmail">The new email address to update Tour with.</param>
        /// <param name="TourAgent">The new agent to update Tour with.</param>
        /// <param name="TourID">The original ID of Tour to update.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int updateTour(String TourName, String TourDetail, int TourCost, String TourPhone, String TourWebsite, 
            String TourEmail, String TourAgent, int TourID)
        {
            return tourTableAdapter.Update(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourAgent, TourID);
        }

        /// <summary>
        /// Insert one Tour.
        /// </summary>
        /// <param name="TourName">The name for the new Tour.</param>
        /// <param name="TourDetail">The detail for the new Tour.</param>
        /// <param name="TourCost">The cost for the new Tour.</param>
        /// <param name="TourPhone">The phone number for the new Tour.</param>
        /// <param name="TourWebsite">The website address for the new Tour.</param>
        /// <param name="TourEmail">The email address for the new Tour.</param>
        /// <param name="TourAgent">The agent for the new Tour.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int insertTour(String TourName, String TourDetail, int TourCost, String TourPhone, String TourWebsite, String TourEmail, String TourAgent)
        {
            return tourTableAdapter.Insert(TourName, TourDetail, TourCost, TourPhone, TourWebsite, TourEmail, TourAgent);
        }

        /// <summary>
        /// Get the ID of the newly inserted Tour from the database.
        /// </summary>
        /// <returns>The ID of the newly inserted Tour</returns>
        public int getNewlyInsertedTourID()
        {
            return (int)tourTableAdapter.getNewlyAddedTourID();
        }

        /// <summary>
        /// Delete one Tour.
        /// </summary>
        /// <param name="TourID">The original ID of Tour to delete.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int deleteTour(int TourID)
        {
            return tourTableAdapter.Delete(TourID);
        }

        #endregion
        /*********************************************************************
        * POITour data access
        *********************************************************************/
        #region POITour
        /// <summary>
        /// Get all TourPOIs for a particular Tour from the database.
        /// (TourPOI : Additional table to solve many-to-many relationship between Tour table and POI table.)
        /// </summary>
        /// <param name="tourID">The ID of Tour to retrieve TourPOIs for.</param>
        /// <returns>Data table containing all retrieved TourPOIs.</returns>
        public DAL.CMSDBDataSet.TourPOIListDataTable getTourPOIListByTourID(int tourID)
        {
            return tourPOIListTableAdapter.GetDataTourID(tourID);
        }
        
        /// <summary>
        /// Delete all TourPOIs for a particular Tour.
        /// (TourPOI : Additional table to solve many-to-many relationship between Tour table and POI table.)
        /// </summary>
        /// <param name="tourID">The original ID of Tour to delete.</param>
        /// <returns>Number of TourPOIs deleted</returns>
        public int deleteTourPOIListByTourID(int tourID)
        {
            return poiTourTableAdapter.DeleteByTourID(tourID);
        }

        /// <summary>
        /// Insert one TourPOI which means adding a POI to the itinerary of a particular Tour.
        /// (TourPOI : Additional table to solve many-to-many relationship between Tour table and POI table.)
        /// </summary>
        /// <param name="POIID">The POI ID to add to the Tour.</param>
        /// <param name="TourID">The Tour ID to add the POI to.</param>
        /// <param name="seqNum">The sequence number of the POI in this Tour.</param>
        /// <returns>1 for success, 0 for fail.</returns>
        public int insertTourPOIList(int POIID, int TourID, int seqNum)
        {
            return poiTourTableAdapter.Insert(POIID, TourID, seqNum);
        }
        #endregion
    }
}