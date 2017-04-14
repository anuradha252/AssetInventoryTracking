using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetInventoryTracking
{
    public partial class SearchItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void search_clicked(object sender, EventArgs e)
        {
            string status = "";
            if (upradio.Checked)
            {
                status = "up";
            }
            else
            {
                status = "down";
            }
            // BO.AssetInventoryTracking.inventory_item itemx =  new BO.AssetInventoryTracking.inventory_item();

            List<BO.AssetInventoryTracking.inventory_item> itemxlist =
 (new BO.AssetInventoryTracking.inventory_item()).Searchinventory_item(txtName.Value, txtInventoryID.Value, txtMake.Value, txtModel.Value, txtLengthOfWarranty.Value, txtCost.Value, status, txtDatePurchasedFrom.Value, txtDatePurchasedTo.Value);
            gvAssetInventory.DataSource = itemxlist;
            gvAssetInventory.DataBind();
        }
    }

}