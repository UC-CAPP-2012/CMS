using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.CMSPages
{
    public partial class POI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void POIGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#f8c9c7'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(POIGridView, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void POIGridView_SelectedIndexChanged(object sender, EventArgs e)
        {            
            
        }

        protected void InsertLinkButton_Click(object sender, EventArgs e)
        {
            this.POIMultiView.ActiveViewIndex = 2;
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {

        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            this.POIMultiView.ActiveViewIndex = -1;
        }
    }
}