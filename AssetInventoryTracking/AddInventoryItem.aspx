<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="AddInventoryItem.aspx.cs" Inherits="AssetInventoryTracking.AddInventoryItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        $(document).ready(function () {
            $.extend({
                getUrlVars: function () {
                    var vars = [], hash;
                    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                    for (var i = 0; i < hashes.length; i++) {
                        hash = hashes[i].split('=');
                        vars.push(hash[0]);
                        vars[hash[0]] = hash[1];
                    }
                    return vars;
                },
                getUrlVar: function (name) {
                    return $.getUrlVars()[name];
                }
            });

            $('#<% =btngetvalue.ClientID %>').click(function (e) {
                var str = "";
                var currentdate = new Date();
             
                var curr_datetime = "datetime(" + currentdate.getFullYear() + ","
                            + (currentdate.getMonth() + 1) + ","
                            + currentdate.getDate() + ","
                            + currentdate.getHours() + ","
                            + currentdate.getMinutes() + ","
                            + currentdate.getSeconds() + ")";
                var selectedstatus = $("input:radio[name=AddoptionsRadios]:checked").val();
                if ($('input[id*=txtAddCost]').val() != "")
                {
                    str += "cost_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + $('input[id*=txtAddCost]').val() + "," + curr_datetime + ").;";
                }
                if ($('input[id*=txtAddName]').val() != "") {
                    str += "name_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + $('input[id*=txtAddName]').val().toLowerCase().replace(/ /g, "_") + "," + curr_datetime + ").;";

                }
                if ($('input[id*=txtAddDatePurchased]').val() != "") {
                    var datepurchased = $('input[id*=txtAddDatePurchased]').val().split("-");
                    str += "date_purchased_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + "date(" + datepurchased[0] + "," + datepurchased[1] + "," + datepurchased[2] + ")" + "," + curr_datetime + ").;";

                }
                if ($('input[id*=txtAddMake]').val() != "") {
                    str += "make_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + $('input[id*=txtAddMake]').val().toLowerCase().replace(/ /g, "_") + "," + curr_datetime + ").;";
                }
                if ($('input[id*=txtAddModel]').val() != "")
                {
                    str += "model_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + $('input[id*=txtAddModel]').val().toLowerCase().replace(/ /g, "_") + "," + curr_datetime + ").;";
                }
                if ($('input[id*=txtAddInventoryID]').val() != "")
                {
                    str += "status_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + selectedstatus + "," + curr_datetime + ").;";
                }
                if ($('#txtLengthOfWarranty').val() != "")
                {
                    str += "length_of_warranty_of_item(" + $('input[id*=txtAddInventoryID]').val() + "," + $('input[id*=txtLengthOfWarranty]').val() + "," + curr_datetime + ").;";
                }
                var byID = $.getUrlVar('ID');
                alert(byID);
                if (byID.length > 1) {

                }
                else {
                    str += "date_item_added_to_inv(" + $('input[id*=txtAddInventoryID]').val() + "," + curr_datetime + ").;";
                }
               

                $('#<% =HiddenField.ClientID %>').attr('value', str);
             

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1> Inventory Item</h1>
    <label style="color:#099125" runat="server" id="valmessage"></label>
                <div class="form-group">
                    <label for="txtInventoryID">InventoryID</label>
                    <input type="text" class="form-control" id="txtAddInventoryID" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtName">Name of Inventory Item</label>
                    <input type="text" class="form-control" id="txtAddName" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtCost">Cost</label>
                    <input type="text" class="form-control" id="txtAddCost" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtDatePurchased">Date Purchased</label>
                    <input  class="form-control" id="txtAddDatePurchased" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtMake">Make</label>
                    <input type="text" class="form-control" id="txtAddMake" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtModel">Model</label>
                    <input type="text" class="form-control" id="txtAddModel" runat="server"/>
                </div>
                <fieldset class="form-group">
                    <legend>Status</legend>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio1" value="up" checked runat="server"/>
                            Up
                        </label>
                    </div>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio2" value="down" runat="server"/>
                            Down
                        </label>
                    </div>

                </fieldset>
                <div class="form-group">
                    <label for="txtLengthOfWarranty">Length of Warranty</label>
                    <input type="text" class="form-control" id="txtLengthOfWarranty" runat="server"/>
                </div>
               
      <asp:Button ID="btngetvalue" runat="server" Text="Save using ASP" OnClick="AddClicked" class="btn btn-primary"/>
          <asp:Button ID="btnsaveNET" runat="server" Text="Save using .NET" OnClick="NETAddClicked" class="btn btn-primary"/>
          <input id="HiddenField" type="hidden" runat="server" value="" />
   
</asp:Content>
