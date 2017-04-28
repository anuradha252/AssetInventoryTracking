<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="AddWorkOrder.aspx.cs" Inherits="AssetInventoryTracking.AddWorkOrder" %>
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
                var data = '<%=this.Request.QueryString["workorderID"]%>';
                //how to get the data from datalong if we are editing a WO?
                var str = "";
                var currentdate = new Date();

                
                var completeddate = new Date($('input[id*=txtAddDateCompleted]').val());
                var curr_datetime = "datetime(" + currentdate.getFullYear() + ","
                            + (currentdate.getMonth() + 1) + ","
                            + currentdate.getDate() + ","
                            + currentdate.getHours() + ","
                            + currentdate.getMinutes() + ","
                            + currentdate.getSeconds() + ")";

                var comp_datetime = "datetime(" + completeddate.getFullYear() + ","
                            + (completeddate.getMonth() + 1) + ","
                            + completeddate.getDate() + ","
                            + completeddate.getHours() + ","
                            + completeddate.getMinutes() + ","
                            + completeddate.getSeconds() + ")";
            
                var selectedstatus = $("input[name*=AddoptionsRadios]:checked").val();

               
               

                //get status of item up or down based on status of work order
                if (selectedstatus == "closed")
                {
                    if ($('#txtWorkOrderID').val() != "") {
                        str += "status_of_workorder(" + $('input[id*=txtWorkOrderID]').val() + "," + selectedstatus + "," + curr_datetime + ").;";
                    }
                   // str += "status_of_item(" + $('input[id*=txtAddInventoryID]').val() + ",up," + curr_datetime + ").;";
                    str += "date_completed_of_workorder(" + $('input[id*=txtWorkOrderID]').val() + "," + comp_datetime + "," + curr_datetime + ").;";
                }
                else {
                    if ($('input[id*=txtAddInventoryID]').val() != "") {
                        str += "workorder_of_item(" + $('input[id*=txtWorkOrderID]').val() + "," + $('input[id*=txtAddInventoryID]').val() + "," + curr_datetime + ").;";
                    }
                    if ($('#txtWorkOrderID').val() != "") {
                        str += "status_of_workorder(" + $('input[id*=txtWorkOrderID]').val() + "," + selectedstatus + "," + curr_datetime + ").;";
                    }
                   // str += "status_of_item(" + $('input[id*=txtAddInventoryID]').val() + ",down," + curr_datetime + ").;";
                    str += "date_completed_of_workorder(" + $('input[id*=txtWorkOrderID]').val() + ",null," + curr_datetime + ").;";
                }


                $('#<% =HiddenField.ClientID %>').attr('value', str);
            });
        });

       
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1>Open/Close Work Order</h1>
    <label style="color:#099125" runat="server" id="valmessage"></label>
        <div class="form-group">
                    <label for="txtWorkOrderID">WorkOrderID</label>
                    <input type="text" class="form-control" id="txtWorkOrderID" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtInventoryID">InventoryID</label>
                    <input type="text" class="form-control" id="txtAddInventoryID" runat="server"/>
                </div>
                <div class="form-group">
                    <label for="txtAddDateCompleted">Date Time Completed</label>
                    <input type="datetime" class="form-control" id="txtAddDateCompleted" runat="server"/>
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
                            <input type="radio" class="form-check-input" name="AddoptionsRadios" id="Radio2" value="closed" runat="server"/>
                            Close
                        </label>
                    </div>

                </fieldset>
          <asp:Button ID="btngetvalue" runat="server" Text="Save using ASP" OnClick="AddClicked" class="btn btn-primary"/>
          <asp:Button ID="btnsaveNET" runat="server" Text="Save using .NET" OnClick="NETAddClicked" class="btn btn-primary"/>
       
          <input id="HiddenField" type="hidden" runat="server" value="" />
  
    <div id="datalogsection" runat="server">

    </div>
</asp:Content>
