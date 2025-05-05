<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="academic_private_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
    <%--css--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    DEFAULT
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

    <asp:GridView ID="gvwPrueba" runat="server"></asp:GridView>

    <asp:DropDownList ID="ddlFacultad" runat="server"></asp:DropDownList>

    <asp:GridView ID="GridCarrera" runat="server"></asp:GridView>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">

    <script>
        document.cookie.split(';').forEach(function (cookie) {
            if (cookie.trim().startsWith('.ASPXAUTH=')) {  // Asumiendo que la cookie se llama .ASPXAUTH
                console.log('Cookie de sesión:', cookie);
            }
            else {
                console.log('Error:');
            }
        });

    </script>

</asp:Content>

