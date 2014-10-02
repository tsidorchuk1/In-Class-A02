﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageSpecialEvents.aspx.cs" Inherits="Admin_ManageSpecialEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
       <div class="row col-md-12">
        <h1>Manage Special Events <span class="glyphicon glyphicon-glass"></span></h1>
    </div>
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SpecialEventDataSource" InsertItemPosition="LastItem" DataKeyNames="EventCode">
      
        <EditItemTemplate>
            <div style="">
                <asp:LinkButton runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                 &nbsp;&nbsp;
                <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                
                &nbsp;&nbsp;&nbsp;
                EventCode:
                <asp:TextBox Text='<%# Bind("EventCode") %>' runat="server" ID="EventCodeTextBox" />
                Description:
                <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" />
                <asp:CheckBox Checked='<%# Bind("Active") %>' runat="server" ID="ActiveCheckBox" Text="Active" />
               <%-- Reservations:
                <asp:TextBox Text='<%# Bind("Reservations") %>' runat="server" ID="ReservationsTextBox" />--%>
                
                
                
              
            </div>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <span>No data was returned.</span>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <span style="">
                 <asp:LinkButton runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" >Insert <span class="glphicon glypyicon-plus"></span></asp:LinkButton>
                &nbsp;&nbsp;
                <asp:LinkButton runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" >Clear <span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>

                 &nbsp;&nbsp; &nbsp;&nbsp;
                <asp:CheckBox Checked='<%# Bind("Active") %>' runat="server" ID="ActiveCheckBox" Text="Active" />
                &mdash;
              <asp:Label ID="Label3" runat="server" AssociatedControlID="EventCodeTextBox" CssClass="control-label">Event Code</asp:Label>
              
                  <asp:TextBox Text='<%# Bind("EventCode") %>' runat="server" ID="EventCodeTextBox" />
                &mdash;
                Description:
                <asp:Label ID="Label4" runat="server" AssociatedControlID="DescriptionTextBox" CssClass="control-label">Description</asp:Label>

                <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" />
                
               <%-- Reservations:
                <asp:TextBox Text='<%# Bind("Reservations") %>' runat="server" ID="ReservationsTextBox" />--%>
               
            </span>
        </InsertItemTemplate>
        <ItemTemplate>
            <div style="">
                 <asp:LinkButton runat="server" CommandName="Edit"  ID="EditButton" >Edit<span class ="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                &nbsp; &nbsp;
                <asp:LinkButton runat="server" CommandName="Delete"  ID="DeleteButton" >Delete<span class ="glyphicon glyphicon-trash"></span></asp:LinkButton>
                 &nbsp; &nbsp; &nbsp;
                 <asp:CheckBox Checked='<%# Eval("Active") %>' runat="server" ID="ActiveCheckBox" Enabled="false" Text="Active" />
                &mdash;
                <asp:Label ID="Label1" runat="server" AssociatedControlID="EventCodeLabel" CssClass="control-label">Event Code</asp:Label>
                <asp:Label Text='<%# Eval("EventCode") %>' runat="server" ID="EventCodeLabel" />
                &mdash;
                <asp:Label ID="Label2" runat="server" AssociatedControlID="DescriptionLabel" CssClass="control-label">Description</asp:Label>
                <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" />
               
               <%-- Reservations:
                <asp:Label Text='<%# Eval("Reservations") %>' runat="server" ID="ReservationsLabel" /><br />--%>
                
                
            </div>
        </ItemTemplate>
        <LayoutTemplate>

            <fieldset runat="server" id="itemPlaceholderContainer"><div runat="server" id="itemPlaceholder" />

            </fieldset>
           
        </LayoutTemplate>
       <%-- <SelectedItemTemplate>
            <span style="">EventCode:
                <asp:Label Text='<%# Eval("EventCode") %>' runat="server" ID="EventCodeLabel" /><br />
                Description:
                <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /><br />
                <asp:CheckBox Checked='<%# Eval("Active") %>' runat="server" ID="ActiveCheckBox" Enabled="false" Text="Active" /><br />
                Reservations:
                <asp:Label Text='<%# Eval("Reservations") %>' runat="server" ID="ReservationsLabel" /><br />
                <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                <br />
                <br />
            </span>
        </SelectedItemTemplate>--%>
    </asp:ListView>


    <asp:ObjectDataSource runat="server" ID="SpecialEventDataSource" DataObjectTypeName="eRestaurant.Entities.SpecialEvent" DeleteMethod="DeleteSpecialEvent" InsertMethod="AddSpecialEvent" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllSpecialEvents" TypeName="eRestaurant.BLL.RestaurantAdminController" UpdateMethod="UpdateSpecialEvent"></asp:ObjectDataSource>
</asp:Content>

