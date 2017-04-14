using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetInventoryTracking
{
    public partial class AddInventoryItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void NETAddClicked(object sender, EventArgs e)
        {
            BO.AssetInventoryTracking.inventory_item itemx = new BO.AssetInventoryTracking.inventory_item();
            itemx.date_purchased = Convert.ToDateTime(txtAddDatePurchased.Value);
            itemx.make = txtAddMake.Value;
            itemx.model = txtAddModel.Value;
            itemx.length_of_warranty = Convert.ToInt32(txtLengthOfWarranty.Value);
            itemx.status_of_item = "up";
            itemx.Save();

        }
        protected void AddClicked(object sender, EventArgs e)
        {
            // String test = Request.Form["HiddenInput"];
            String test = HiddenField.Value;
            String[] testlist = test.Split(';');

            //Response.Write(test);
            StreamWriter sw = default(StreamWriter);
            string strFile = "C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\AssetInventoryTracking\\InventoryTracking\\DataLog Files\\Inventory_Tracking.dl";
            if ((!File.Exists(strFile)))
            {
                File.Create(strFile).Close();
                sw = File.CreateText(strFile);
                foreach (string teststr in testlist)
                {
                    sw.WriteLine(teststr);
                }


                sw.Close();

            }
            else
            {
                sw = File.AppendText(strFile);
                foreach (string teststr in testlist)
                {
                    sw.WriteLine(teststr);
                }
                sw.Close();
            }
        }
    }
}