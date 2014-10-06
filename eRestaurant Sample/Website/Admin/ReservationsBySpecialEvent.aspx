<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReservationsBySpecialEvent.aspx.cs" Inherits="Admin_ReservationsBySpecialEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   
     <div class="row col-md-12">
        <h1>Reservations by Special Events </h1>
    </div>
    <div data-style="bootstrap">
        <fieldset data-style="inline">
            <asp:Label ID="Label1" runat="server" Text="Special Event" AssociatedControlID="SpecialEventsDropDown" />
            <asp:DropDownList ID="SpecialEventsDropDown" runat="server" OnSelectedIndexChanged="SpecialEventsDropDown_SelectedIndexChanged"></asp:DropDownList>
            <asp:LinkButton ID="ShowReservations" runat="server" Text="Show Reservations"/>
        </fieldset>
        <blockquote>
            <asp:Label ID="MessageLabel" runat="server" CssClass="label label-default" />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
        </blockquote>



        </div>

    



</asp:Content>

