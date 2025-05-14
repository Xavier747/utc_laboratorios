<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="InformacionLaboratorios.aspx.cs" Inherits="academic_private_reservalab_InformacionLaboratorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        .target{
            height: 350px !important;      /* o cualquier alto que desees */
        }

        .contenedor-imagen{
            width: 100%;        /* o el ancho que necesites */
            overflow: hidden;   /* corta la imagen que se sale */
            position: relative;
        }

        .contenedor-imagen .img {
            height: 350px;
            width: auto;
            object-fit: cover;        /* hace que la imagen se escale y recorte sin deformarse */
            object-position: center;  /* centra la imagen dentro del contenedor */
            display: block;
        }

        .img-responsable {
            object-fit: cover; /* Evita deformaciones */
            height: 100%;
        }

        .resp-image {
            margin: auto;
            border-radius: 50%; /* Hace la imagen redonda */
            border: 3px solid #007BFF; /* Borde azul */
            width: 100px; /* Ajusta el tamaño de la imagen */
            height: 100px; /* Mantén el tamaño uniforme */
            overflow:hidden;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Detalle del laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <div class="section-heading text-center">
            <h3 class="morado titulo-claro text-uppercase" id="nombre" runat="server"></h3>            
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="contenedor-imagen">
                    <asp:DataList ID="DataList1" runat="server">
                        <ItemTemplate>
                            <asp:Image ID="imgLaboratorio1" runat="server" ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia1_lab").ToString()) %>' AlternateText="Imagen del laboratorio" CssClass="img"/>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="col-md-6 ">
                <div class="card target">
                    <div class="card-body">
                        <div class="text-container">
                            <div class="row">
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
                                <b><i class="glyphicon glyphicon-map-marker"></i> Ubicación: </b> 
                            </div>
                            <br />
                    
                            <p id="txtUbicacion" runat="server" class="text-center"></p>
                            <div class="container-fluid text-center">
                                <br />
                                <br />
                                <a href="ReservaLaboratorio.aspx" class="btn btn-outline-primary">
                                    &nbsp;&nbsp;&nbsp;<i class="fa fa-calendar">&nbsp;</i>&nbsp;Reservar&nbsp;Laboratorio&nbsp;&nbsp;&nbsp;
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />    
        <div class="row">
            <div class="col-md-6">
                <div class="card target">
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
            <div class="col-md-6">
                <div class="contenedor-imagen">
                    <asp:DataList ID="DataList2" runat="server">
                        <ItemTemplate>
                            <asp:Image ID="imgLaboratorio2" runat="server" AlternateText="Imagen del laboratorio" ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia2_lab").ToString()) %>' CssClass="img"/>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script src="<%= ResolveUrl("~/Scripts/Custom/listarLaboratorios.js") %>" type="text/javascript"></script>
</asp:Content>

