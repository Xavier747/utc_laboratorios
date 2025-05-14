<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="Laboratorios.aspx.cs" Inherits="academic_private_reservalab_Laboratorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        /*Contenedor de imagenes*/
        .conten-img{
            height:200px;
            overflow:hidden;
        }

        .list_laboratorio{
            position:relative;
            top: 40px;
        }

        /*imagen*/
        .lab-image{
            width:100%;
        }

        /*Contendor de imagenes de responsable*/
        .resp-image{
            height:50px;
            width:50px;
            border-radius: 50%;
            border: 3px solid #000;
            overflow:hidden;
            margin: 0 auto 20px auto;
        }

        /*Imagen de responsable*/
        .imgRespAcad{
            height: 100%;
        }

        .card-title{
            color: #312783;
            font-weight: bold;
            text-align:center;
            font-size: 18px;
        }

        .card-body__{
            text-align:center;
        }

    </style>
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
                    <asp:Label ID="lblCodFacultad" runat="server" Visible="false" Text=''></asp:Label>
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
    <br />
    <div class="container-fluid">
        <asp:UpdatePanel ID="updPanelLaboratorios" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="row">

                        <div class="col-md-12">
                            <asp:Label ID="lblBuscar" runat="server" Text="BUSCAR" CssClass="form-control-bold"></asp:Label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control txtSearch" placeholder="Ingrese el nombre del laboratorio" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="list_laboratorio">
                    <div class="row">
                        <asp:Repeater ID="listarLaboratorios" runat="server" OnItemCommand="listarLaboratorios_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-4 mb-4">
                                    <div class="card">
                                        <div class="conten-img">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia1_lab").ToString()) %>' AlternateText="Imagen del laboratorio" CssClass="card-img-top lab-image"/>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h6 class="card-title" style="height: 45px; margin-bottom: 15px;">
                                                        <asp:Label ID="lblNombreLab" runat="server" Text='<%# Eval("strNombre_lab") %>'></asp:Label>
                                                    </h6>
                                                </div>
                                            </div>
                                            <div class="card-body__">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="resp-image">
                                                            <asp:Image ID="imgRespAcad" runat="server" ImageUrl='<%# "~/images/Usuario/" + Eval("ResponsableAcademico.FotoAcademico") %>' AlternateText="Foto Responsable Académico" CssClass="img-thumbnail" />
                                                        </div>
                                                        <h6>
                                                            <b>
                                                                <asp:Label ID="lblRespAcad" runat="server" Text="RESPONSABLE ACADÉMICO" style ="font-size: 13px;"></asp:Label><br />
                                                            </b>
                                                        </h6>
                                                        <asp:Label ID="lblRespAcadInf" runat="server" Text='<%# Eval("ResponsableAcademico.nombre") %>'></asp:Label><br /><br />
                                                    </div>
                                                    <div class="col-md-6" style="border-left:2px solid #312783">
                                                        <div class="resp-image">
                                                            <asp:Image ID="imgRespAdmin" runat="server" ImageUrl='<%# "~/images/Usuario/" + Eval("ResponsableAcademico.FotoAcademico") %>' AlternateText="Foto Responsable Administrativo" CssClass="img-thumbnail" />
                                                        </div>
                                                        <h6>
                                                            <b>
                                                                <asp:Label ID="lblRespAdmin" runat="server" Text="RESPONSABLE ADMINISTRATIVO" style ="font-size: 13px;"></asp:Label><br />
                                                            </b>
                                                        </h6>
                                                        <asp:Label ID="lblRespAdminInf" runat="server" Text='<%# Eval("ResponsableAdministrativo.nombre") %>'></asp:Label><br /><br />
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

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

