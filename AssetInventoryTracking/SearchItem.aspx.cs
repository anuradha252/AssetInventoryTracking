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
        public string getanswers(string output, string[] passcaptions)
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
            for (int captionindex = 0; captionindex < passcaptions.Length; captionindex++)
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
                            if (stringinsplit.Contains("date("))
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
                                    if (getdateyearmonth.Length > 3)
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
                                                finaldate += getdateyearmonth[value];
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
            else if (output.Contains("answer"))
            {
                //regex to make the change in teh month directory name
                Regex regexcaption = new Regex("answer(.*)");
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


                    string[] splitnew = Regex.Split(matchedcaption, "answer(.*)");

                    foreach (string stringinsplit_loopVariable in splitnew)
                    {
                        string stringinsplit = stringinsplit_loopVariable;
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
                            if (stringinsplit.Contains("date("))
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
                                    if (getdateyearmonth.Length > 3)
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
                                                finaldate += getdateyearmonth[value];
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
        protected void AddClicked(object sender, EventArgs e)
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
            // sw.WriteLine("getanswer(W, I, S,DT, DT1) :- workorder_of_item(W, I, DT),status_of_workorder(W, S, DT2),date_completed_of_workorder(W, DT1, DT2).");
            sw.WriteLine(HiddenField.Value);
            sw.Close();
            if (chkToReplace.Checked)
            {
                string[] passcaptions = { "Inventory_ID", "Cost", "Date Purchased", "Make", "Model", "Status", "Name", "Length of warranty", "Date time added", "Cost To Replace" };
                string output = startprocess.StandardOutput.ReadToEnd();
                string outputtoHTMLNew = getanswers(output, passcaptions);
                ItemdivQuerySection.InnerHtml = outputtoHTMLNew;
            }
            else
            {
                string[] passcaptions = { "Inventory_ID", "Cost", "Date Purchased", "Make", "Model", "Status", "Name", "Length of warranty", "Date time added" };
                string output = startprocess.StandardOutput.ReadToEnd();
                string outputtoHTMLNew = getanswers(output, passcaptions);
                ItemdivQuerySection.InnerHtml = outputtoHTMLNew;
           }
            

            string err = startprocess.StandardError.ReadToEnd();
            Console.WriteLine(err);
            startprocess.WaitForExit();
        }


        }

}