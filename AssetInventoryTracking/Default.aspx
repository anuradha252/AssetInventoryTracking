<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AssetInventoryTracking.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div>
        <h2> Actions</h2>
          <asp:Button ID="getassetsusingNET" runat="server" Text="Get All Assets NET"  OnClick="getassetsusingNET_click"/>
          <asp:Button ID="getassetsusingASP" runat="server" Text="Get All Assets Datalog" OnClick="getassetsusingASP_click" />
          <asp:Button ID="getwosusingNET" runat="server" Text="Get All WO NET" OnClick="getwosusingNET_click" />
          <asp:Button ID="getwosusingASP" runat="server" Text="Get All WO Datalog" OnClick="getwosusingASP_click"/>
        <h2>Assets</h2>
      
    <asp:GridView ID="gvAssetInventory" runat="server" class="table table-striped"  OnRowDataBound="grdDataItem_RowDataBound">
        
    </asp:GridView>
            <div id="ItemdivQuerySection" runat="server"></div>
        <h2>Work orders</h2>
         <asp:GridView ID="gvworkorder" runat="server" class="table table-striped"  OnRowDataBound="grdData_RowDataBound">
            
    </asp:GridView>
           <div id="WOdivQuerySection" runat="server"></div>
    </div>
</asp:Content>
