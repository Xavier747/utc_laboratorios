<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="Laboratorios.aspx.cs" Inherits="academic_private_reservalab_Laboratorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="<%= ResolveUrl("~/Content/CSS/listadoLaboratorios.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado de laboratorios
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <nav style="display: flex; justify-content: flex-end;">
        <asp:UpdatePanel ID="updFacultades" runat="server">
            <ContentTemplate>
                <ul class="nav nav-tabs" id="navFacultades">
                    <asp:Repeater ID="rptFacultades" runat="server" OnItemCommand="rptFacultades_ItemCommand">
                        <ItemTemplate>
                            <li role="presentation" onclick="setActive(this);">
                                <asp:LinkButton ID="btnFacultad" runat="server" CommandName="CargarLaboratorios" CommandArgument='<%# Eval("strCod_Fac") %>' Text='<%# Eval("strNombre_Fac") %>'></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>    
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rptFacultades" EventName="ItemCommand" />
            </Triggers>
        </asp:UpdatePanel>
    </nav>    

    <br />
    <div class="content_consolidado">
        <asp:HyperLink ID="link_consolidado" class="link_consolidado" runat="server">
            <asp:ImageButton ID="logo" runat="server" CssClass="logo" ImageUrl="~/images/static/calendar3.svg" />
            <asp:Label ID="link" runat="server" Text="CONSOLIDADO" CssClass="button-text" />
        </asp:HyperLink>
    </div>

    <asp:UpdatePanel ID="updPanelLaboratorios" runat="server">
        <ContentTemplate>
            <div class="search">
                <asp:TextBox ID="txtSearch" runat="server" class="form-control txtSearch" placeholder="Ingrese el nombre del laboratorio" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                <div class="btn_search">
                    <b>
                        <i class="bi bi-search"></i>
                        <asp:Label ID="lblBuscar" runat="server" Text="BUSCAR" CssClass="lblBuscar"></asp:Label>
                    </b>
                </div>
            </div>
            <div class="list_laboratorio">
                <div class="row">
                    <asp:Repeater ID="listarLaboratorios" runat="server" OnItemCommand="listarLaboratorios_ItemCommand">
                        <ItemTemplate>
                            <div class="col-md-4 mb-4">
                                <div class="card">
                                    <div class="conten-img">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/images/Laboratorio/" + Eval("strFotografia1_lab") %>' AlternateText="Imagen del laboratorio" CssClass="card-img-top lab-image"/>
                                    </div>
                                    <div class="card-body">
                                        <div class="col-md-12">
                                            <h5 class="card-title" style="height: 45px; margin-bottom: 15px;">
                                                <asp:Label ID="lblNombreLab" runat="server" Text='<%# Eval("strNombre_lab") %>'></asp:Label>
                                            </h5>
                                        </div>
                                        <div class="card-body__">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="resp-image">
                                                        <asp:Image ID="imgRespAcad" runat="server" ImageUrl='<%# "~/images/Usuario/" + Eval("FotoAcademico") %>' AlternateText="Foto Responsable Académico" CssClass="img-thumbnail" />
                                                    </div>
                                                    <h6>
                                                        <b>
                                                            <asp:Label ID="lblRespAcad" runat="server" Text="RESPONSABLE ACADÉMICO" style ="font-size: 13px;"></asp:Label><br />
                                                        </b>
                                                    </h6>
                                                    <asp:Label ID="lblRespAcadInf" runat="server" Text='<%# Eval("ResponsableAcademico") %>'></asp:Label><br /><br />
                                                </div>
                                                <div class="col-md-6" style="border-left:2px solid #312783">
                                                    <div class="resp-image">
                                                        <asp:Image ID="imgRespAdmin" runat="server" ImageUrl='<%# "~/images/Usuario/" + Eval("FotoAdministrativo") %>' AlternateText="Foto Responsable Administrativo" CssClass="img-thumbnail" />
                                                    </div>
                                                    <h6>
                                                        <b>
                                                            <asp:Label ID="lblRespAdmin" runat="server" Text="RESPONSABLE ADMINISTRATIVO" style ="font-size: 13px;"></asp:Label><br />
                                                        </b>
                                                    </h6>
                                                    <asp:Label ID="lblRespAdminInf" runat="server" Text='<%# Eval("ResponsableAdministrativo") %>'></asp:Label><br /><br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnReservar" runat="server" Text="Reservar" CssClass="btn btnReservar btn-block" CommandName="Reservar" CommandArgument='<%# Eval("strCod_lab") %>'/>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnInformacion" runat="server" Text="Informacion" CssClass="btn btnReservar btn-block" CommandName="Informacion" CommandArgument='<%# Eval("strCod_lab") %>'/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>                        
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

