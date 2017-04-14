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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void getassetsusingNET_click(object sender, EventArgs e)
        {
            BO.AssetInventoryTracking.inventory_item item = new BO.AssetInventoryTracking.inventory_item();
            List<BO.AssetInventoryTracking.inventory_item> itemlist = new List<BO.AssetInventoryTracking.inventory_item>();
            itemlist = item.GetAllinventory_item();
            gvAssetInventory.DataSource = itemlist;
            gvAssetInventory.DataBind();
        }
        protected void getassetsusingASP_click(object sender, EventArgs e)
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
            sw.WriteLine("/consult C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\AssetInventoryTracking\\AssetInventoryTracking\\DataLog Files\\Inventory_Tracking.dl");
            sw.WriteLine("/listing C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\AssetInventoryTracking\\AssetInventoryTracking\\DataLog Files\\Inventory_Tracking.dl");
            sw.WriteLine("getanswer(I,C,N,D,M,Mo,UD,WL,DT):-cost_of_item(I,C,DT),name_of_item(I,N,DT),date_purchased_of_item(I,D,DT),make_of_item(I,M,DT),model_of_item(I,Mo,DT),status_of_item(I,UD,DT2),length_of_warranty_of_item(I,WL,DT).");
            sw.Close();
            string[] passcaptions = { "Inventory_ID", "Cost", "Name", "Date Purchased", "Make","Model","Status","Length of warranty","Date time added" };
            string output = startprocess.StandardOutput.ReadToEnd();
            string outputtoHTMLNew = getanswers(output, passcaptions);
            divQuerySection.InnerHtml = outputtoHTMLNew;

            string err = startprocess.StandardError.ReadToEnd();
            Console.WriteLine(err);
            startprocess.WaitForExit();
        }
        protected void getwosusingNET_click(object sender, EventArgs e)
        {
            BO.AssetInventoryTracking.workorder item = new BO.AssetInventoryTracking.workorder();
            List<BO.AssetInventoryTracking.workorder> itemlist = new List<BO.AssetInventoryTracking.workorder>();
            itemlist = item.GetAllworkorder();
            gvworkorder.DataSource = itemlist;
            gvworkorder.DataBind();

        }
        protected string getanswers(string output,string[] passcaptions)
        {
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
            string outputtoHTMLNew = "<table class='table table-striped'>";
            outputtoHTMLNew += "<tr>";
            for (int captionindex =0;captionindex < passcaptions.Length; captionindex++)
            {
                outputtoHTMLNew += "<th>";
                outputtoHTMLNew += passcaptions[captionindex];
                outputtoHTMLNew += "</th>";
            }
            outputtoHTMLNew += "</tr>";

          
            if (outputtoHTML.Contains("getanswer"))
            {
                //regex to make the change in teh month directory name
                Regex regexcaption = new Regex("getanswer(.*)");
                string[] matchcaptionregex = regexcaption.Matches(output).Cast<Match>().Select(m => m.Value).ToArray();
                int index = 0;
                List<string> matchanswers = new List<string>();

                foreach (string matchedcaption in matchcaptionregex)
                {
                    if (index > 0)
                    {
                        if (matchedcaption.Contains("),\r"))
                        {
                            matchanswers.Add(matchedcaption.Replace("),\r", ")"));
                        }
                        else
                        {
                            matchanswers.Add(matchedcaption.Replace(")\r", ")"));
                        }


                    }
                    index = index + 1;
                }

                foreach (string matchedcaption in matchanswers)
                {


                    string[] splitnew = Regex.Split(matchedcaption, "getanswer(.*)");

                    foreach (string stringinsplit_loopVariable in splitnew)
                    {
                        string stringinsplit = stringinsplit_loopVariable;
                        //stringinsplit = stringinsplit.Replace("),", ")");
                        if ((stringinsplit.Length > 1))
                        {
                            stringinsplit = stringinsplit.Substring(1, stringinsplit.Length - 2);
                            outputtoHTMLNew += "<tr>";
                            if (stringinsplit.Contains("datetime("))
                            {
                                Regex regexdate = new Regex("datetime\\(\\d{4},\\d{1,2},\\d{1,2},\\d{1,2},\\d{1,2},\\d{1,2}\\)");
                                string[] matchdateregex = regexdate.Matches(stringinsplit).Cast<Match>().Select(m => m.Value).ToArray();
                                string matcheddatenew = "";
                                foreach (string matcheddate in matchdateregex)
                                {
                                    matcheddatenew = matcheddate.Replace(",", "-");
                                    matcheddatenew = matcheddatenew.Replace("datetime(", "");
                                    matcheddatenew = matcheddatenew.Replace(")", "");
                                    stringinsplit = stringinsplit.Replace(matcheddate, matcheddatenew);

                                }
                            }
                            if(stringinsplit.Contains("date("))
                            {
                                Regex regexdate = new Regex("date\\(\\d{4},\\d{1,2},\\d{1,2}\\)");
                                string[] matchdateregex = regexdate.Matches(stringinsplit).Cast<Match>().Select(m => m.Value).ToArray();
                                string matcheddatenew = "";
                                foreach (string matcheddate in matchdateregex)
                                {
                                    matcheddatenew = matcheddate.Replace(",", "-");
                                    matcheddatenew = matcheddatenew.Replace("date(", "");
                                    matcheddatenew = matcheddatenew.Replace(")", "");
                                    stringinsplit = stringinsplit.Replace(matcheddate, matcheddatenew);

                                }
                            }
                            
                            string[] splitvalues = stringinsplit.Split(',');
                            foreach (string splitvalue_loopVariable in splitvalues)
                            {
                                string splitvalue = splitvalue_loopVariable;
                                if ((splitvalue.Contains("-")))
                                {
                                    string finaldate = "";
                                    string[] getdateyearmonth = splitvalue.Split('-');
                                    if(getdateyearmonth.Length > 3)
                                    {
                                        for (int value = 0; value <= 5; value++)
                                        {
                                            if ((value < 2))
                                            {
                                                finaldate += getdateyearmonth[value] + "-";
                                            }
                                            else if (value == 2)
                                            {
                                                finaldate += getdateyearmonth[value] + " ";
                                            }
                                            else if (value == 3 | value == 4)
                                            {
                                                finaldate += getdateyearmonth[value] + ":";
                                            }
                                            else
                                            {
                                                finaldate += getdateyearmonth[value];
                                            }

                                        }
                                        outputtoHTMLNew += "<td> " + DateTime.Parse(finaldate) + "</td>";
                                    }
                                    else
                                    {
                                        for (int value = 0; value < 3; value++)
                                        {
                                            if ((value < 2))
                                            {
                                                finaldate += getdateyearmonth[value] + "-";
                                            }
                                            else 
                                            {
                                                finaldate += getdateyearmonth[value] ;
                                            }
                                        }
                                        outputtoHTMLNew += "<td> " + DateTime.Parse(finaldate).ToShortDateString() + "</td>";
                                    }
                                    
                                    
                                }
                                else
                                {
                                    outputtoHTMLNew += "<td> " + splitvalue + "</td>";
                                }

                            }
                            outputtoHTMLNew += "</tr>";
                        }

                    }
                }

            }

            outputtoHTMLNew += "</table>";
            return outputtoHTMLNew;
        }
        protected void getwosusingASP_click(object sender, EventArgs e)
        {
            //TODO remove first result from answer array
            //ADd comma to last element in answer array
            //do same for items
            //and add datecompleted
            //add title to each column
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
            sw.WriteLine("/consult C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\AssetInventoryTracking\\AssetInventoryTracking\\DataLog Files\\Inventory_Tracking.dl");
            sw.WriteLine("/listing C:\\Users\\Anuradha\\Documents\\visual studio 2015\\Projects\\AssetInventoryTracking\\AssetInventoryTracking\\DataLog Files\\Inventory_Tracking.dl");
            sw.WriteLine("getanswer(W, I, S,DT, DT1) :- workorder_of_item(W, I, DT),status_of_workorder(W, S, DT2),date_completed_of_workorder(W, DT1, DT2).");
            sw.Close();
            string[] passcaptions = { "workorder_ID", "Inventory_ID", "Status of workorder", "Date Created", "Date Completed" };
            string output = startprocess.StandardOutput.ReadToEnd();
            string outputtoHTMLNew = getanswers(output, passcaptions);
            divQuerySection.InnerHtml = outputtoHTMLNew;

            string err = startprocess.StandardError.ReadToEnd();
            Console.WriteLine(err);
            startprocess.WaitForExit();
        }
    }
}