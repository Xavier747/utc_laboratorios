<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="TipoLaboratorio.aspx.cs" Inherits="academic_private_reservalab_TipoLaboratorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado Tipo de Laboratorios
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <!--Boton para agregar un nuevo laboratorio-->
        <div class="col-md-12 text-right">
            <button type="button" class="btn btn-primary btn-nuevo" data-toggle="modal" data-target="#form_registrar">
                <i class="bi bi-plus-lg"></i>Nuevo tipo
            </button>
        </div>  
    </div>  
    <div class="alert alert-info alert-dismissible text-center" id="lblMsgLstRegistros" runat="server" visible="false">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
        <span class="mensaje">No existen registros</span>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="gvTipoLaboratorio" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvTipoLaboratorio_PageIndexChanging" CssClass="table table-striped table-bordered" OnRowCommand="gvTipoLaboratorio_RowCommand">
            <Columns>
                <asp:BoundField DataField="strNombre_tipoLab" HeaderText="Nombre"></asp:BoundField>
                <asp:TemplateField ShowHeader="False" HeaderText="Accion">
                    <ItemTemplate>
                        <div style="display:flex;">
                            <!--Boton que refleja formulario para actualizar el laboratorio-->
                            <asp:ImageButton ID="imgbtnSelect" runat="server" CausesValidation="False" CommandName="Select" CssClass="btn btn-warning" ImageUrl="~/images/static/edit.svg" CommandArgument ='<%# Eval("strCod_tipoLab") %>' />&nbsp;&nbsp;
                            <!--Boton para eliminar el laboratorio-->
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CausesValidation="False" CommandName="Eliminar" CssClass="btn btn-danger" ImageUrl="~/images/static/delete.svg" CommandArgument ='<%# Eval("strCod_tipoLab") %>' OnClientClick="return confirm('¿Seguro que desea eliminar?');" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <!--Formulario para agregar nuevo tipo laboratorio-->
    <!-- Ventana Modal -->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog  modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalNuevoLaboratorio">Nuevo Laboratorio</h4>
                </div>
                <div class="modal-body">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_tbxNombre" runat="server" ControlToValidate="txtNombre" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default border-radius" data-dismiss="modal" onclick="cerrar()">Cerrar</button>
                    <asp:Button ID="btnSubmit" runat="server" Text="Enviar" ValidationGroup="formulario" CssClass="btn btn-primary" OnClick="btnSubmit_Click"/>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Ventana Modal -->
    <div class="modal fade" id="form_actualizar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalActLaboratorio">Actualizar Laboratorio</h4>
                </div>
                <div class="modal-body" data-bs-smooth-scroll="true" tabindex="0">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblCodeTipoLabAct" runat="server" Text="" Visible="False"></asp:Label>
                            <asp:Label ID="lblNombreAct" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNombreAct" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" ControlToValidate="txtNombreAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">Cerrar</button>
                    <asp:Button ID="btn_Actualizar" runat="server" Text="Actualizar" ValidationGroup="formularioActualizar" CssClass="btn btn-success" OnClick="btn_Actualizar_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

