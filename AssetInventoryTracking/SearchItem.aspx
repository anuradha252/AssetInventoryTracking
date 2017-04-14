<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="SearchItem.aspx.cs" Inherits="AssetInventoryTracking.SearchItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
        $(document).ready(function () {
            $('#divQuerySection').hide();

            $('#btnsubmit').click(function (e) {
                var str = "";
                var selectedOption = $("input:radio[name=optionsRadios]:checked").val();
                var valinvID = "";
                valinvID = $('#txtInventoryID').val();
                valmake = $('#txtMake').val();
                valModel = $('#txtModel').val();
                valDatePurchasedFrom = $('#txtDatePurchasedFrom').val();
                valDatePurchasedTo = $('#txtDatePurchasedTo').val();
                valCost = $('#txtCost').val();

                if (valinvID == '') {
                    valinvID = "X";
                }
                if (valmake == '') {
                    valmake = "Y";
                }
                if (valModel == '') {
                    valModel = "Z";
                }
                if (valCost == '') {
                    valCost = "C";
                }
                if ($('#chkwarrantyexpired').is(":checked")) {
                    str += "has_warranty_expired_of_item(" + valinvID + ");";
                }
                //date purchased
                str += "date_purchased(" + valinvID + ",DP)";
                if (valDatePurchasedFrom != "") {
                    var from = valDatePurchasedFrom.split("-");
                    str += ",DP > date(" + from[2] + "," + from[1] + "," + from[0] + ")";
                }
                if (valDatePurchasedTo != "")
                {
                    var to = valDatePurchasedTo.split("-");
                    str += ",DP < date(" + to[2] + "," + to[1] + "," + to[0] + ")";
                }
                str += ";";
               
                str += "has_warranty_expired_of_item(" + valinvID + ");";

                str += "make_of_item(" + valinvID + "," + valmake + ");";

                str += "model_of_item(" + valinvID + "," + valModel + ");";

                str += "cost_of_item(" + valinvID + "," + valCost + "),max_date_cost_of_item(DT);";

                $('#lblQueries').text(str);
                $('#divQuerySection').show();

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
                <button type="button" class="btn btn-primary" id="btnsubmit" runat="server">Submit</button>
                  <button type="button" class="btn btn-primary" id="btnsearchnet" runat="server" onserverclick="search_clicked">Search using .NET</button>
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
            </div>
</asp:Content>
