<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="Laboratorios.aspx.cs" Inherits="academic_private_reservalab_Laboratorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href='<%= ResolveUrl("~/Styles/Nuevo/assets/css/Laboratorio/grid-laboratorio.css") %>'  rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado de laboratorios
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12 text-right">
            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="btn btn-default" OnClick="lnkRegresar_Click1">
                <i class="fa fa-chevron-left" aria-hidden="true"></i>
                <span>Regresar</span>
            </asp:LinkButton>
        </div>
    </div>
    <br />
    <nav style="display: flex; justify-content: flex-end;">
        <asp:UpdatePanel ID="updFacultades" runat="server">
            <ContentTemplate>
                <ul class="nav nav-tabs" id="navFacultades">
                    <asp:Label ID="lblCodFacultad" runat="server" 
                        Visible="false" 
                        Text='' />
                    <asp:Label ID="lblCodSede" runat="server" 
                        Visible="false" 
                        Text='' />
                    <asp:DropDownList ID="ddlFacultad" runat="server" 
                        CssClass="form-control" />
                </ul>    
            </ContentTemplate>
        </asp:UpdatePanel>
    </nav>    

    <br />
    <br />
    <div class="container-fluid">
        <asp:UpdatePanel ID="updPanelLaboratorios" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblBuscar" runat="server" 
                                Text="BUSCAR" 
                                CssClass="form-control-bold" />
                            <asp:TextBox ID="txtSearch" runat="server" 
                                CssClass="form-control" 
                                placeholder="Ingrese el nombre del laboratorio" 
                                AutoPostBack="true" 
                                OnTextChanged="txtSearch_TextChanged" />
                        </div>
                    </div>
                </div>

                <div class="list_laboratorio">
                    <div class="row">
                        <asp:Repeater ID="listarLaboratorios" runat="server" 
                            OnItemCommand="listarLaboratorios_ItemCommand">
                            <ItemTemplate>
                                <div class="col-12 col-md-6 col-lg-4">
                                    <div class="card">
                                        <div class="conten-img">
                                            <asp:Image ID="Image1" runat="server" 
                                                ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia1_lab").ToString()) %>' 
                                                AlternateText="Imagen del laboratorio" CssClass="card-img-top lab-image"/>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12" style="height: 6rem;">
                                                    <h6 class="card-title"><%# Eval("strNombre_lab") %></h6>
                                                </div>
                                            </div>
                                            <div class="card-body__">
                                                <div class="row ">
                                                    <div class="col-md-6 fila">
                                                        <div class="resp-image">
                                                            <asp:Image ID="imgRespAcad" runat="server" 
                                                                ImageUrl='<%# Eval("ResponsableAcademico") != null
                                                                    ? "ImageHandlerUsuario.ashx?image=" + System.IO.Path.GetFileName(Eval("ResponsableAcademico.FotoAcademico").ToString()) 
                                                                    : "" %>' 
                                                                AlternateText="Foto Responsable Académico" 
                                                                CssClass="img-thumbnail" />
                                                        </div>
                                                        <h6>RESPONSABLE ACADÉMICO</h6>
                                                        <asp:Label ID="lblNombreAcademico" runat="server" 
                                                            CssClass="lblNombre" 
                                                            Text='<%# Eval("ResponsableAcademico.nombre") ?? "" %>' />
                                                    </div>
                                                    <div class="col-md-6 fila bar-left">
                                                        <div class="resp-image">
                                                            <asp:Image ID="imgRespAdmin" runat="server" 
                                                                ImageUrl='<%# Eval("ResponsableAdministrativo") != null
                                                                    ? "ImageHandlerUsuario.ashx?image="+ System.IO.Path.GetFileName(Eval("ResponsableAdministrativo.FotoAdministrativo").ToString())
                                                                     : ""%>' 
                                                                AlternateText="Foto Responsable Administrativo" 
                                                                CssClass="img-thumbnail" />
                                                        </div>
                                                        <h6>RESPONSABLE ADMINISTRATIVO</h6>
                                                        <asp:Label ID="lblNombreAdministrativo" runat="server" 
                                                            CssClass="lblNombre" 
                                                            Text='<%# Eval("ResponsableAdministrativo.nombre") ?? "" %>' />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnReservar" runat="server" 
                                                            Text="Reservar" 
                                                            CssClass="btn btn-primary btn-block" 
                                                            CommandName="Reservar" 
                                                            CommandArgument='<%# Eval("strCod_lab") %>'/>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnInformacion" runat="server" 
                                                            Text="Informacion" 
                                                            CssClass="btn btn-info btn-block" 
                                                            CommandName="Informacion" 
                                                            CommandArgument='<%# Eval("strCod_lab") %>'/>
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
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
                </Triggers>
        </asp:UpdatePanel>   
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <!-- Importa archivos js -->
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_utilidades.js") %>'></script>
</asp:Content>