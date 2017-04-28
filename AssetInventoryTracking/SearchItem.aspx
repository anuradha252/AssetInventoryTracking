<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="SearchItem.aspx.cs" Inherits="AssetInventoryTracking.SearchItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
        $(document).ready(function () {
            $('#divQuerySection').hide();

             $('#<% =btngetvalue.ClientID %>').click(function (e) {
                 var str = "";
                 var selectedOption = $("input:radio[name*=optionsRadios]:checked").val();
                var valinvID = "";
                var valstatus = "";
                var valName = "";
                var valLW = "";
                 
                valinvID = $('input[id*=txtInventoryID]').val();
                valmake = $('input[id*=txtMake]').val();
                valModel = $('input[id*=txtModel]').val();
                valDatePurchasedFrom = $('input[id*=txtDatePurchasedFrom]').val();
                valDatePurchasedTo = $('input[id*=txtDatePurchasedTo]').val();
                valCost = $('input[id*=txtCost]').val();
                valName = $('input[id*=txtName]').val();
                valLW = $('input[id*=txtLengthOfWarranty]').val();
                valstatus = selectedOption;
               
                if (valName == '') {
                    valName = "N";
                }
                if (valLW == '') {
                    valLW = "LW";
                }
                if (valinvID == '') {
                    valinvID = "I";
                }
                if (valmake == '') {
                    valmake = "M";
                }
                if (valModel == '') {
                    valModel = "Mo";
                }
                if (valCost == '') {
                    valCost = "C";
                }
                if ($('input[id*=chkToReplace]').is(":checked")) {
                    str = "getanswer(" + valinvID + "," + valCost + ",DP," + valmake + "," + valModel + "," + valstatus + "," + valName + "," + valLW + ",DAL,NewCost) :- ";

                }
                else {
                    str = "getanswer(" + valinvID + "," + valCost + ",DP," + valmake + "," + valModel + "," + valstatus + "," + valName + "," + valLW + ",DAL) :- ";

                }
                //need to get queyr for this
                if ($('input[id*=chkwarrantyexpired]').is(":checked")) {
                    str += "warranty_expired(" + valinvID + "),";
                }
                if ($('input[id*=chkToReplace]').is(":checked")) {
                    str += "replace(" + valinvID + "),";
                    str += "cost_to_replace(" + valinvID + ",NewCost),";
                }
                //date purchased
                str += "date_purchased_of_item(" + valinvID + ",DP,DTP)";
                if (valDatePurchasedFrom != "") {
                    var from = valDatePurchasedFrom.split("-");
                    str += ",DP > date(" + from[0] + "," + from[1] + "," + from[2] + ")";
                }
                if (valDatePurchasedTo != "")
                {
                    var to = valDatePurchasedTo.split("-");
                    str += ",DP < date(" + to[0] + "," + to[1] + "," + to[2] + ")";
                }
                str += ",get_date_purchased_of_item(" + valinvID + ",DTP),";
               
                //str += "warranty_expired(" + valinvID + ");";
                str += "name_of_item(" + valinvID + "," + valName + ",DTN),get_name_of_item(" + valinvID + ", DTN),";
                str += "length_of_warranty_of_item(" + valinvID + "," + valLW + ",DLW),get_length_of_warranty_of_item(" + valinvID + ", DLW),";
                str += "make_of_item(" + valinvID + "," + valmake + ",DTM),get_make_of_item(" + valinvID + ", DTM),";

                str += "model_of_item(" + valinvID + "," + valModel + ",DTMo),get_model_of_item(" + valinvID + ", DTMo),";

                str += "cost_of_item(" + valinvID + "," + valCost + ",DTC),get_cost_of_item(" + valinvID + ", DTC),";

                str += "status_of_item(" + valinvID + "," + valstatus + ",DTS),get_status_of_item(" + valinvID + ", DTS),date_item_added_to_inv(" + valinvID + ",DAL).";

                $('#lblQueries').text(str);
                $('#divQuerySection').show();
                      $('#<% =HiddenField.ClientID %>').attr('value', str);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="col-md-6">
                <h1>Asset Inventory Search</h1>
                <div class="form-group">
                    <label for="txtInventoryID">InventoryID</label>
                    <input type="text" class="form-control" id="txtInventoryID"  runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtName">Name of Inventory Item</label>
                    <input type="text" class="form-control" id="txtName"  runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtCost">Cost</label>
                    <input type="text" class="form-control" id="txtCost"  runat="server"/>
                </div>
                <div class="form-group">
                    <div>
                        <label for="txtDatePurchased">Date Purchased</label>
                    </div>
                    <div class="col-md-6" style="padding-left: 0px; padding-right: 0px">
                        <label>From</label><input type="date" class="form-control" id="txtDatePurchasedFrom"  runat="server"/>
                    </div>
                    <div class="col-md-6" style="padding-left: 0px; padding-right: 0px">
                        <label>To</label>
                        <input type="date" class="form-control" id="txtDatePurchasedTo"  runat="server"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtMake">Make</label>
                    <input type="text" class="form-control" id="txtMake"  runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtModel">Model</label>
                    <input type="text" class="form-control" id="txtModel"  runat="server"/>
                </div>
                 <div class="form-group">
                    <label for="txtLengthOfWarranty">Length Of Warranty</label>
                    <input type="text" class="form-control" id="txtLengthOfWarranty"  runat="server"/>
                </div>
                <fieldset class="form-group">
                    <legend>Status</legend>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="optionsRadios" id="upradio" value="up" checked runat="server"/>
                            Up
                        </label>
                    </div>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="optionsRadios" id="downradio" value="down" runat="server" />
                            Down
                        </label>
                    </div>

                </fieldset>
                <div class="form-check">
                    <label class="form-check-label">
                        <input type="checkbox" class="form-check-input" id="chkwarrantyexpired"  runat="server"/>
                        Warranty Expired?
                    </label>
                </div>
               <div class="form-check">
                    <label class="form-check-label">
                        <input type="checkbox" class="form-check-input" id="chkToReplace"  runat="server"/>
                        To replace?
                    </label>
                </div>
               <asp:Button ID="btngetvalue" runat="server" Text="Search using ASP" OnClick="AddClicked" class="btn btn-primary"/>
               
                  <button type="button" class="btn btn-primary" id="btnsearchnet" runat="server" onserverclick="search_clicked">Search using .NET</button>
              <input id="HiddenField" type="hidden" runat="server" value="" />
        </div>
            <div class="col-md-6">
                <div id="divQuerySection">
                    <h1>Query Section</h1>
                    <div id="lblQueries"></div>
                </div>
            </div>
            <div class="col-md-12">
                 <asp:GridView ID="gvAssetInventory" runat="server" class="table table-striped">

    </asp:GridView>
                  <div id="ItemdivQuerySection" runat="server"></div>
            </div>
</asp:Content>
