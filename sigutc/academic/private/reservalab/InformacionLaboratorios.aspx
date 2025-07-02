<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="InformacionLaboratorios.aspx.cs" Inherits="academic_private_reservalab_InformacionLaboratorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href='<%= ResolveUrl("~/Styles/Nuevo/assets/css/Laboratorio/grid-laboratorio.css") %>'  rel="stylesheet" />
    <style>
        .target{
            height: 380px !important;
        }

        .img {
            height: 350px;
            width: 100%;
            object-fit: cover;
            object-position: center;
            display: block;
        }

        .img-responsable {
            object-fit: cover;
            height: 100%;
        }

        .resp-image {
            margin: auto;
            border-radius: 50%;
            border: 3px solid #007BFF;
            width: 100px;
            height: 100px;
            overflow:hidden;
        }

        .verde {
            font-size: 13px;
            margin: 5px 0;
            padding: 0 10px;
            background-color: #A4E4AE;
            border-radius: 100px;
            color: #3A647E;
        }

        .dlLab {
            max-width: 100%;             /* o max-width: 100% si está dentro de otro contenedor */
            display: flex;
            flex-wrap: wrap;
            gap: 16px;
            box-sizing: border-box;
        }   
                 
        .correo{
            margin-top: 10px;
            display: block;
        }

        .correo .icon{
            font-size: 16px;
            margin-right: 10px;
        }


        @media screen and (max-width: 768px){
            .target{
                height: auto !important;
            }       
             
            span{
                font-size: 11px;
            }
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Detalle del laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblCrono" runat="server" 
        Visible="false"/>
    <div class="container-fluid">
        <asp:Repeater ID="rptLaboratorio" runat="server">
            <ItemTemplate>
                <div class="section-heading text-center">
                    <h4 class="morado titulo-claro text-uppercase"><%# Eval("strNombre_lab") %></h4>   
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <asp:Image ID="imgLaboratorio1" runat="server" 
                            ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia1_lab").ToString()) %>' 
                            AlternateText="Imagen del laboratorio" 
                            CssClass="img" />
                    </div>
                    <div class="col-md-6 ">
                        <div class="card target">
                            <div class="card-body">
                                <div class="text-container">
                                    <div class="row">
                                        <div class="col-md-7">
                                            <label class="verde">
                                                <b id="tipoLaboratorio"><%# Eval("TipoLaboratorio.nombre") %></b>
                                            </label>
                                        </div>
                                        <div class="col-md-5">
                                            <a href="javascript:history.back()" class="pull-right regresar">
                                                <b><i class="fa fa-chevron-left"></i></b> REGRESARS
                                            </a>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="text-center">
                                        <b><i class="glyphicon glyphicon-map-marker"></i> Ubicación: </b> 
                                    </div>
                                    <br />
                    
                                    <p id="txtUbicacion" runat="server" class="text-center"><%# Eval("strUbicacion_lab") %></p>
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
                <div class="row">
                    <div class="col-md-6">
                        <div class="card target">
                            <div class="card-body">
                                <div class="row columna">
                                    <div class="col-md-6 fila">
                                        <div class="text-center">
                                            <b style="color: #312783;">RESPONSABLE ACADÉMICO</b>
                                        </div>
                                        <br />
                                        <div class="content">
                                            <div class="resp-image text-center">
                                                <asp:Image ID="imgRespAcad" runat="server" 
                                                    AlternateText="Foto Responsable Académico" 
                                                    ImageUrl='<%# "ImageHandlerUsuario.ashx?image=" + System.IO.Path.GetFileName(Eval("ResponsableAcademico.FotoAcademico").ToString()) %>'
                                                    CssClass="img-responsable"/>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="text-center">
                                            <span><%# Eval("ResponsableAcademico.nombre") %></span>
                                            <br />
                                            <span class="correo"><i class="fa fa-envelope-o icon" aria-hidden="true"></i><%# Eval("ResponsableAcademico.correo") %></span>
                                        </div>
                                    </div>
                                    <div class="col-md-6 fila bar-left">
                                        <div class="text-center">
                                            <b style="color: #312783;">RESPONSABLE ADMINISTRATIVO</b>
                                        </div>
                                        <br />
                                        <div class="content">
                                            <div class="resp-image text-center">
                                                <asp:Image ID="imgRespAdmin" runat="server" 
                                                    AlternateText="Foto Responsable Administrativo" 
                                                    ImageUrl='<%# "ImageHandlerUsuario.ashx?image=" + System.IO.Path.GetFileName(Eval("ResponsableAdministrativo.FotoAdministrativo").ToString()) %>'
                                                    CssClass="img-responsable"/>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="text-center">
                                            <span><%# Eval("ResponsableAdministrativo.nombre") %></span>
                                            <br />
                                            <span class="correo"><i class="fa fa-envelope-o icon" aria-hidden="true"></i><%# Eval("ResponsableAdministrativo.correo") %></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="contenedor-imagen">
                            <asp:Image ID="imgLaboratorio2" runat="server" 
                                AlternateText="Imagen del laboratorio" 
                                ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia2_lab").ToString()) %>' 
                                CssClass="img"/>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

