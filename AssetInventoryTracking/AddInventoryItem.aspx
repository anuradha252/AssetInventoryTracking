<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="AddInventoryItem.aspx.cs" Inherits="AssetInventoryTracking.AddInventoryItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        $(document).ready(function () {
           
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
                if ($('#txtAddCost').val() != "")
                {
                    str += "cost_of_item(" + $('#txtAddInventoryID').val() + "," + $('#txtAddCost').val() + "," + curr_datetime + ").;";
                }
                if ($('#txtAddName').val() != "") {
                    str += "name_of_item(" + $('#txtAddInventoryID').val() + "," + $('#txtAddName').val() + "," + curr_datetime + ").;";

                }
                if ($('#txtAddDatePurchased').val() != "") {
                    var datepurchased = $('#txtAddDatePurchased').val().split("-");
                    str += "date_purchased_of_item(" + $('#txtAddInventoryID').val() + "," + "date(" + datepurchased[0] + "," + datepurchased[1] + "," + datepurchased[2] + ")" + "," + curr_datetime + ").;";

                }
                if ($('#txtAddMake').val() != "") {
                    str += "make_of_item(" + $('#txtAddInventoryID').val() + "," + $('#txtAddMake').val() + "," + curr_datetime + ").;";
                }
                if ($('#txtAddModel').val() != "")
                {
                    str += "model_of_item(" + $('#txtAddInventoryID').val() + "," + $('#txtAddModel').val() + "," + curr_datetime + ").;";
                }
                if ($('#txtAddInventoryID').val() != "")
                {
                    str += "status_of_item(" + $('#txtAddInventoryID').val() + "," + selectedstatus + "," + curr_datetime + ").;";
                }
                if ($('#txtLengthOfWarranty').val() != "")
                {
                    str += "length_of_warranty_of_item(" + $('#txtAddInventoryID').val() + "," + $('#txtLengthOfWarranty').val() + "," + curr_datetime + ").;";
                }
                

                $('#<% =HiddenField.ClientID %>').attr('value', str);
             

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1>Add Inventory Item</h1>
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
                    <input type="date" class="form-control" id="txtAddDatePurchased" runat="server"/>
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
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio1" value="up" checked />
                            Up
                        </label>
                    </div>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio2" value="down" />
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
