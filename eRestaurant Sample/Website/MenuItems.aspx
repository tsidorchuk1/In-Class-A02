<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MenuItems.aspx.cs" Inherits="MenuItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row col-md-12">
        <h1>Our Menu</h1>

        <asp:Repeater ID="MenuItemRepeater" runat="server" DataSourceID="MenuItemDataSource">

            <ItemTemplate>
                <div>
                    <h3>
                        <img src='<%# "Images/" + Eval("Description") + "-1.png" %>' />


                        <%# Eval("Description") %></h3>
                    <div class="well">
                        <asp:Repeater ID="InnerRepeater" runat="server"
                            DataSource='<%# Eval("MenuITems") %>'>
                            <ItemTemplate>
                                <div>
                                    <h4>
                                        <%# Eval("Description") %>
                                        <%# Eval("Calories") %>
                                        <%# ((decimal)Eval("Price")).ToString("C") %>
                                        </h4><%# Eval("Comment") %>
                                </div>


                            </ItemTemplate>


                        </asp:Repeater>
                    </div>

                </div>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>


        </asp:Repeater>

        <!-- Todo : add repeater with menu items-->

        <asp:ObjectDataSource runat="server" ID="MenuItemDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCategorizedItems" TypeName="eRestaurant.BLL.MenuController"></asp:ObjectDataSource>
    </div>



</asp:Content>

