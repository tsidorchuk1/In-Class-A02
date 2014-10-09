<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ItemsByMenuCategory.aspx.cs" Inherits="Admin_ItemsByMenuCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="row col-md-12">
        <h1>Items by Menu Category</h1>
     MenuCategory:
     <asp:DropDownList ID="MenuCategoryDropDown" runat="server" AppendDataBoundItems="True" DataSourceID="ObjectDataSource1" DataTextField="Description" DataValueField="MenuCategoryID">
     <asp:ListItem Value="-">[Select a Menu Category]</asp:ListItem>
 <asp:ListItem Value="-">[No Menu Catgory]</asp:ListItem>

</asp:DropDownList>
     <asp:LinkButton ID="ShowItems" runat="server">Show Items</asp:LinkButton>
    
     
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
         <Columns>
             <asp:BoundField DataField="MenuCategoryID" HeaderText="MenuCategoryID" SortExpression="MenuCategoryID"></asp:BoundField>
             <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
         </Columns>
     </asp:GridView>
     <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" OldValuesParameterFormatString="original_{0}" SelectMethod="MenuCategoriesbyItem" TypeName="eRestaurant.BLL.RestaurantAdminController">
         <SelectParameters>
             <asp:Parameter Name="itemId" Type="String"></asp:Parameter>
         </SelectParameters>
     </asp:ObjectDataSource>
 </div>
</asp:Content>

