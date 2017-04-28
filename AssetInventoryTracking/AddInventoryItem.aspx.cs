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
            if (Request.QueryString.Count > 0)
            {
                BO.AssetInventoryTracking.inventory_item itemx = (new BO.AssetInventoryTracking.inventory_item()).GetByIDinventory_item(Convert.ToInt32(Request.QueryString[0]));
                txtAddInventoryID.Value = itemx.inventoryID.ToString();
                txtAddName.Value = itemx.name;
                txtAddDatePurchased.Value = String.Format("{0:MM/dd/yyyy}", itemx.date_purchased);  
                txtAddCost.Value = itemx.cost.ToString();
                txtAddMake.Value = itemx.make.ToString();
                txtAddModel.Value = itemx.model.ToString();
                txtLengthOfWarranty.Value = itemx.length_of_warranty.ToString();
                if (itemx.status_of_item.Trim() == "up")
                {
                    Radio1.Checked = true;
                }
                else
                {
                    Radio2.Checked = true;
                }
            }
        }
        protected void NETAddClicked(object sender, EventArgs e)
        {
            BO.AssetInventoryTracking.inventory_item itemx = (new BO.AssetInventoryTracking.inventory_item()).GetByIDinventory_item(Convert.ToInt32(txtAddInventoryID.Value));
            if(itemx.inventoryID == -1)
            {
                itemx = new BO.AssetInventoryTracking.inventory_item();
                itemx.date_added = DateTime.Now;
            }
            
            itemx.name = txtAddName.Value;
            itemx.date_purchased = Convert.ToDateTime(txtAddDatePurchased.Value);
            itemx.date_modified = DateTime.Now;
            itemx.cost = Convert.ToDecimal(txtAddCost.Value);
            itemx.make = txtAddMake.Value;
            itemx.model = txtAddModel.Value;
            itemx.length_of_warranty = Convert.ToInt32(txtLengthOfWarranty.Value);
            if (Radio1.Checked){
                itemx.status_of_item = "up";
            }
            else
            {
                itemx.status_of_item = "down";
            }

            itemx.Save();
            valmessage.InnerText = "Item saved successfully!";
        }
        protected void AddClicked(object sender, EventArgs e)
        {
            // String test = Request.Form["HiddenInput"];
            String test = HiddenField.Value;
            String[] testlist = test.Split(';');

            //Response.Write(test);
            StreamWriter sw = default(StreamWriter);
            string strFile = "C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\AssetInventoryTracking\\AssetInventoryTracking\\DataLog Files\\Inventory_Tracking.dl";
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
            valmessage.InnerText = "Item saved successfully!";
            //txtAddInventoryID.Value = "";
            //txtAddCost.Value = "";
            //txtAddDatePurchased.Value = "";
            //txtAddMake.Value = "";
            //txtAddModel.Value = "";
            //txtAddName.Value = "";
            //txtLengthOfWarranty.Value = "";
        }
    }
}