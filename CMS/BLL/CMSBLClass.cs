using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.BLL
{
    public class CMSBLClass
    {
        DAL.CMSDBDataSetTableAdapters.EventTableAdapter eventTableAdapter
            = new DAL.CMSDBDataSetTableAdapters.EventTableAdapter();

        public DAL.CMSDBDataSet.EventDataTable getAllEvent()
        {
            return eventTableAdapter.GetDataByAll();
        
        }
    }
}