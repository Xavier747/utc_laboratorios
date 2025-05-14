<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ListadoLaboratorio.aspx.cs" Inherits="academic_private_reservalab_ListadoLaboratorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../../Styles/Nuevo/assets/css/laboratorio-style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado de laboratorios
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">     
    <!-- GridView para listar laboratorios -->
    <div class="table-responsive">
        <asp:GridView ID="gvLaboratorios" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvLaboratorios_PageIndexChanging" CssClass="table table-striped table-bordered" OnRowCommand="gvLaboratorios_RowCommand">
            <Columns>
                
                 <asp:BoundField DataField="strNombre_lab" HeaderText="Nombre" />
                <asp:BoundField DataField="strUbicacion_lab" HeaderText="Ubicación" />
                <asp:TemplateField HeaderText="Imagen 1">
                    <ItemTemplate>
                        <div class="content">
                            <div class="content-img">
                                <!--Ruta de la imagen envia desde el codigo para mostrar la imagen -->
                                <asp:Image ID="imgLaboratorio1" runat="server" ImageUrl='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia1_lab").ToString()) %>' />
                            </div>
                            <br />
                            <!--Boton para mostrar la imagen en grande atraves de una ventana modal -->
                            <asp:Button ID="btnViewImage1" CssClass="btn btn-info" OnClick="btnViewImage1_Click" CommandArgument='<%# "ImageHandlerLaboratorio.ashx?image=" + System.IO.Path.GetFileName(Eval("strFotografia1_lab").ToString()) %>' runat="server" Text="Ver Imagen" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField ShowHeader="False" HeaderText="Accion">
                    <ItemTemplate>
                        <div style="display:flex;">
                            
                            
                            <!--Boton para eliminar el laboratorio-->
                            <asp:Button ID="btnVerReservaciones" runat="server" CommandName="VerReservaciones" CssClass="btn btn-danger" CommandArgument ='<%# Eval("strCod_lab") %>' OnClientClick="return confirm('¿Seguro que desea eliminar?');" Text="VerReservaciones" />&nbsp;&nbsp;
                           
                          
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>   
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>       
     <!-- Ventana Modal Imagen-->
    <div class="modal fade" id="view-image" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalVisualizarLab">Visualizar laboratorio</h4>
                </div>
                <div class="modal-body">
                    <asp:Image ID="vistaCompletaImagen" runat="server" style="width:100%;" AlternateText="hola"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

