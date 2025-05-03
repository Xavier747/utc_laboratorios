<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="InformacionLaboratorios.aspx.cs" Inherits="academic_private_reservalab_InformacionLaboratorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="<%= ResolveUrl("~/Content/CSS/informacionLaboratorios.css") %>" rel="stylesheet" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Detalle del laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <div class="section-heading text-center">
            <h2 class="morado titulo-claro text-uppercase" id="nombre" runat="server"></h2>            
        </div>
        <div class="row mb-4">
            <div class="col-md-6 contenedor-imagen">
                <asp:Image ID="imgLaboratorio1" runat="server" AlternateText="Imagen del laboratorio" CssClass="img"/>
            </div>
            <div class="col-md-6 d-flex align-items-center">
                <div class="text-container">
                    <div class="row align-items-center mb-3">
                        <div class="col-md-7">
                            <label class="verde">
                                <b id="tipoLaboratorio" runat="server"></b>
                            </label>
                        </div>
                        <div class="col-md-5">
                            <a href="javascript:history.back()" class="pull-right regresar">
                                <b><i class="fa fa-chevron-left"></i></b> REGRESAR
                            </a>
                        </div>
                    </div>
                    <br />
                    <p>
                        El laboratorio de Alto voltaje cuenta con espacios destinados al aprendizaje técnico de sistemas voltaicos de alta tensión, de sus protecciones, y su correcto aislamiento a través de la Jaula de Faraday.
                    </p>
                    <br />
                    <div class="text-center">
                        <b class=""><i class="glyphicon glyphicon-map-marker"></i> Ubicación: </b> 
                    </div>
                    <br />
                    
                    <p id="txtUbicacion" runat="server" class="text-center"></p>
                    <center>
                        <br />
                        <br />
                        <a href="../Pages/ReservaLaboratorio.aspx" class="btn btn-outline-primary">
                            &nbsp;&nbsp;&nbsp;<i class="fa fa-calendar">&nbsp;</i>&nbsp;Reservar&nbsp;Laboratorio&nbsp;&nbsp;&nbsp;
                        </a>
                    </center>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />    
        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="text-center">
                                    <b style="color: #312783;">RESPONSABLE ACADÉMICO</b>
                                </div>
                                <br />
                                <div class="content">
                                    <div class="resp-image text-center">
                                        <asp:Image ID="imgRespAcad" runat="server" AlternateText="Foto Responsable Académico" CssClass="img-responsable"/>
                                    </div>
                                </div>
                                <br />
                                <div class="text-center">
                                    <span id="lblRespAcadInf" runat="server"></span>
                                </div>
                            </div>
                            <div class="col-md-6 bar-left">
                                <div class="text-center">
                                    <b style="color: #312783;">RESPONSABLE ADMINISTRATIVO</b>
                                </div>
                                <br />
                                <div class="content">
                                    <div class="resp-image text-center">
                                        <asp:Image ID="imgRespAdmin" runat="server" AlternateText="Foto Responsable Administrativo" CssClass="img-responsable"/>
                                    </div>
                                </div>
                                <br />
                                <div class="text-center">
                                    <span id="lblRespAdminInf" runat="server"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 contenedor-imagen">
                <asp:Image ID="imgLaboratorio2" runat="server" AlternateText="Imagen del laboratorio" CssClass="img"/>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script src="<%= ResolveUrl("~/Scripts/Custom/listarLaboratorios.js") %>" type="text/javascript"></script>
</asp:Content>

