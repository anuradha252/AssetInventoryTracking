using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssetInventoryTracking
{
    public partial class AddWorkOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                BO.AssetInventoryTracking.workorder wo = (new BO.AssetInventoryTracking.workorder()).GetByIDworkorder(Convert.ToInt32(Request.QueryString[0]));
                txtAddInventoryID.Value = wo.inventoryID.ToString();
                txtWorkOrderID.Value = wo.workorderID.ToString();
                txtAddDateCompleted.Value =  wo.date_completed.ToString();
                if (wo.status == "open")
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

            if (Request.QueryString.Count > 0 && Request.QueryString[0] != "")
            {
                BO.AssetInventoryTracking.workorder wo = (new BO.AssetInventoryTracking.workorder()).GetByIDworkorder(Convert.ToInt32(Request.QueryString[0]));
                wo.inventoryID = Convert.ToInt32(txtAddInventoryID.Value);
                BO.AssetInventoryTracking.inventory_item xitem = (new BO.AssetInventoryTracking.inventory_item()).GetByIDinventory_item(wo.inventoryID);
                string status = "";
                if (Radio1.Checked)
                {
                    status = "open";
                    xitem.status_of_item = "down";
                    wo.date_created = DateTime.Now;
                }
                else
                {
                    xitem.status_of_item = "up";
                    status = "closed";
                    wo.date_completed = Convert.ToDateTime(txtAddDateCompleted.Value);
                }
                wo.status = status;
                xitem.date_modified = DateTime.Now;
                wo.date_modified = DateTime.Now;
                xitem.Save();
                wo.Save();
            }
            else
            {
                BO.AssetInventoryTracking.workorder wo = (new BO.AssetInventoryTracking.workorder()).GetByIDworkorder(Convert.ToInt32(txtWorkOrderID.Value));
                if(wo.workorderID == -1)
                {
                    wo = new BO.AssetInventoryTracking.workorder();
                }
               // BO.AssetInventoryTracking.workorder wo = new BO.AssetInventoryTracking.workorder();
                wo.inventoryID = Convert.ToInt32(txtAddInventoryID.Value);

                BO.AssetInventoryTracking.inventory_item xitem = (new BO.AssetInventoryTracking.inventory_item()).GetByIDinventory_item(wo.inventoryID);
                string status = "";
                if (Radio1.Checked)
                {
                    status = "open";
                    xitem.status_of_item = "down";
                    wo.date_created = DateTime.Now;
                }
                else
                {
                    xitem.status_of_item = "up";
                    status = "closed";
                    wo.date_completed = Convert.ToDateTime(txtAddDateCompleted.Value);
                }
                wo.status = status;
                xitem.date_modified = DateTime.Now;
                wo.date_modified = DateTime.Now;
                xitem.Save();
                wo.Save();
                valmessage.InnerText = "Workorder saved successfully!";
            }

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

            valmessage.InnerText = "Workorder saved successfully!";
            //txtAddInventoryID.Value = "";
            //txtWorkOrderID.Value = "";
        }
    }
}