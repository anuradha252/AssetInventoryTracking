<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AssetInventoryTracking.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div>
        <h1> Actions</h1>
          <asp:Button ID="getassetsusingNET" runat="server" Text="Get All Assets NET"  OnClick="getassetsusingNET_click"/>
          <asp:Button ID="getassetsusingASP" runat="server" Text="Get All Assets Datalog" OnClick="getassetsusingASP_click" />
          <asp:Button ID="getwosusingNET" runat="server" Text="Get All WO NET" OnClick="getwosusingNET_click" />
          <asp:Button ID="getwosusingASP" runat="server" Text="Get All WO Datalog" OnClick="getwosusingASP_click"/>
        <h1>Assets</h1>
      
    <asp:GridView ID="gvAssetInventory" runat="server" class="table table-striped">
        
    </asp:GridView>
        <h1>Work orders</h1>
         <asp:GridView ID="gvworkorder" runat="server" class="table table-striped">

    </asp:GridView>
           <div id="divQuerySection" runat="server"></div>
    </div>
</asp:Content>
