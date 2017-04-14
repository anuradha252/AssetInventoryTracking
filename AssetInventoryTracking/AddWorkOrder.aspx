<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="AddWorkOrder.aspx.cs" Inherits="AssetInventoryTracking.AddWorkOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>
        $(document).ready(function () {
           
            $('#<% =btngetvalue.ClientID %>').click(function (e) {
                var data = '<%=this.Request.QueryString["workorderID"]%>';
                //how to get the data from datalong if we are editing a WO?
                var str = "";
                var currentdate = new Date();
                var curr_datetime = "datetime(" + currentdate.getFullYear() + ","
                            + (currentdate.getMonth() + 1) + ","
                            + currentdate.getDate() + ","
                            + currentdate.getHours() + ","
                            + currentdate.getMinutes() + ","
                            + currentdate.getSeconds() + ")";
                var selectedstatus = $("input:radio[name=AddoptionsRadios]:checked").val();

                if ($('#txtAddInventoryID').val() != "") {
                    str += "workorder_for_item(" + $('#txtWorkOrderID').val() + "," + $('#txtAddInventoryID').val() + "," + curr_datetime + ").;";
                }
                if ($('#txtWorkOrderID').val() != "") {
                    str += "status_of_workorder(" + $('#txtWorkOrderID').val() + "," + selectedstatus + "," + curr_datetime + ").;";
                }

                //get status of item up or down based on status of work order
                if (selectedstatus == "close")
                {
                    str += "status_of_item(" + $('#txtAddInventoryID').val() + ",up," + curr_datetime + ").;";
                }
                else {
                    str += "status_of_item(" + $('#txtAddInventoryID').val() + ",down," + curr_datetime + ").;";
                }


                $('#<% =HiddenField.ClientID %>').attr('value', str);
            });
        });

       
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1>Open/Close Work Order</h1>
        <div class="form-group">
                    <label for="txtWorkOrderID">WorkOrderID</label>
                    <input type="text" class="form-control" id="txtWorkOrderID" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtInventoryID">InventoryID</label>
                    <input type="text" class="form-control" id="txtAddInventoryID" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtComment">Comment</label>
                    <input type="text" class="form-control" id="txtComment" runat="server"/>
                </div>
                <fieldset class="form-group">
                    <legend>Status</legend>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio1" value="open" checked runat="server" />
                            Open
                        </label>
                    </div>
                    <div class="form-check">
                        <label class="form-check-label">
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio2" value="close" runat="server"/>
                            Close
                        </label>
                    </div>

                </fieldset>
          <asp:Button ID="btngetvalue" runat="server" Text="Save using ASP" OnClick="AddClicked" class="btn btn-primary"/>
          <asp:Button ID="btnsaveNET" runat="server" Text="Save using .NET" OnClick="NETAddClicked" class="btn btn-primary"/>
        <asp:Button ID="btnDatalog" runat="server" Text="send to DataLog" OnClick="DataLogClicked" class="btn btn-primary"/>
          <input id="HiddenField" type="hidden" runat="server" value="" />
  
    <div id="datalogsection" runat="server">

    </div>
</asp:Content>
