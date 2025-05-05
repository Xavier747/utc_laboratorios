<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="Software.aspx.cs" Inherits="academic_private_reservaLab_Software" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        td{
            word-wrap: break-word;
            white-space: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado de softwares
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <!--Boton para agregar un nuevo laboratorio-->
        <div class="col-md-12 text-right">
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo laboratorio" CssClass="btn btn-primary" OnClick="btnNuevo_Click"/>
        </div>
    </div>   
    <div class=" row">
        <div class="col-md-6">
            <asp:Label ID="lblSedeSoft" runat="server" Text="Sede" CssClass="control-label required"></asp:Label>
            <asp:DropDownList ID="ddlSedeSoft" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSedeSoft_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv_ddlSedeSoft" runat="server" ControlToValidate="ddlSedeSoft" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6">
            <asp:Label ID="lblFacultadSoft" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
            <asp:DropDownList ID="ddlFacultadSoft" runat="server" CssClass="form-control custom-input"  AutoPostBack="True" OnSelectedIndexChanged="ddlFacultadSoft_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv_ddlFacultadSoft" runat="server" ControlToValidate="ddlFacultadSoft" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
        </div>
        <br />
        <div class="col-md-12">
            <div class="table-responsive" style="max-width: 100%; overflow-x: auto;">
                <asp:GridView ID="gvSoftware" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvSoftware_PageIndexChanging" CssClass="table table-striped table-bordered" OnRowCommand="gvSoftware_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="strNombre_sof" HeaderText="Nombre"></asp:BoundField>
                        <asp:BoundField DataField="intCantidad_sof" HeaderText="Cantidad"></asp:BoundField>
                        <asp:BoundField DataField="strDescripcion_sof" HeaderText="Descripciom"></asp:BoundField>
                        <asp:TemplateField HeaderText="Imagen">
                            <ItemTemplate>
                                <div class="content">
                                    <div class="content-img">
                                        <!--Ruta de la imagen envia desde el codigo para mostrar la imagen -->
                                        <img src='<%= ResolveUrl("~/images/Software/") %><%# Eval("strImagen_sof") %>' />
                                    </div>
                                    <br />
                                    <!--Boton para mostrar la imagen en grande atraves de una ventana modal -->
                                    <asp:ImageButton ID="imgbtnVistaCompleta" runat="server" CssClass="btn btn-info" ImageUrl="~/images/static/display.svg" OnClick="btnVistaCompleta_Click" CommandArgument='<%# ResolveUrl("~/images/Software/") + Eval("strImagen_sof") %>' data-toggle="tooltip" data-placement="bottom" title="Ver imagen"/>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="Accion">
                            <ItemTemplate>
                                <div style="display:flex;">
                                    <!--Boton que refleja formulario para actualizar el laboratorio-->
                                    <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Select" CommandArgument ='<%# Eval("strCod_sof") %>' />&nbsp;&nbsp;
                                    <!--Boton para eliminar el laboratorio-->
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Eliminar" CommandName="Eliminar"  ImageUrl="~/images/static/delete.svg" CommandArgument ='<%# Eval("strCod_sof") %>' OnClientClick="return confirm('¿Seguro que desea eliminar?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <asp:Label ID="lblMsg" runat="server"></asp:Label>

    <!-- Ventana Modal Imagen-->
    <div class="modal fade" id="view-image" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel1">Visualizar software</h4>
                </div>
                <div class="modal-body">
                    <asp:Image ID="vistaCompletaImagen" runat="server" style="width:100%;"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

