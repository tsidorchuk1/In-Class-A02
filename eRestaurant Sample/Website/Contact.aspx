<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Terry Sidorchuk</h3>
    <address>
        <br />
        <br />
        <abbr title="Phone">P:</abbr>
        780.940.4176
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">tsidorchuk1@studentmai.nait.ca</a><br />
       <%-- <strong>Marketing:</strong> <a href="mailto:Marketing@example.com"></a>--%>
    </address>
</asp:Content>
