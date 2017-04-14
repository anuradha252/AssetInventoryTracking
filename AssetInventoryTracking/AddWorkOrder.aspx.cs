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
        protected void DataLogClicked(object sender, EventArgs e)
        {
            Process startprocess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = "C:\\Users\\Anuradha\\Downloads\\DES Software-20170323T222711Z-001\\DES Software\\des\\des.exe";
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;

            startprocess.StartInfo = startInfo;
            startprocess.Start();

            StreamWriter sw = startprocess.StandardInput;
            sw.WriteLine("/consult C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\InventoryTracking\\InventoryTracking\\DataLog Files\\Inventory_Tracking.dl");
            sw.WriteLine("/listing C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\InventoryTracking\\InventoryTracking\\DataLog Files\\Inventory_Tracking.dl");
            sw.WriteLine("cost_of_item(X,Y,DT).");
            sw.Close();


            string output = startprocess.StandardOutput.ReadToEnd();
            string outputtoHTML = "";

            string whole = output;
            string[] split = Regex.Split(whole, "DES>");
            foreach (string part in split)
            {
                if (!(part.Contains("*********")))
                {
                    outputtoHTML = outputtoHTML + part;
                }
            }

            datalogsection.InnerHtml = outputtoHTML;
            string err = startprocess.StandardError.ReadToEnd();
            Console.WriteLine(err);

            startprocess.WaitForExit();



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
                }
                else
                {
                    xitem.status_of_item = "up";
                    status = "close";
                }
                wo.status = status;
                if (status == "close")
                {
                    wo.date_completed = DateTime.Now;
                }

                xitem.Save();
                wo.Save();
            }
            else
            {
                BO.AssetInventoryTracking.workorder wo = new BO.AssetInventoryTracking.workorder();
                wo.inventoryID = Convert.ToInt32(txtAddInventoryID.Value);
                BO.AssetInventoryTracking.inventory_item xitem = (new BO.AssetInventoryTracking.inventory_item()).GetByIDinventory_item(wo.inventoryID);
                string status = "";
                if (Radio1.Checked)
                {
                    status = "open";
                    xitem.status_of_item = "down";
                }
                else
                {
                    xitem.status_of_item = "up";
                    status = "close";
                }
                wo.status = status;
                wo.date_created = DateTime.Now;
                xitem.Save();
                wo.Save();
            }

        }
        protected void AddClicked(object sender, EventArgs e)
        {
            // String test = Request.Form["HiddenInput"];
            String test = HiddenField.Value;
            String[] testlist = test.Split(';');

            //Response.Write(test);
            StreamWriter sw = default(StreamWriter);
            string strFile = "C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\InventoryTracking\\InventoryTracking\\DataLog Files\\Inventory_Tracking.dl";
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