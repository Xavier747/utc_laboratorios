﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="TipoLaboratorio.aspx.cs" Inherits="academic_private_reservalab_TipoLaboratorio" %>

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
    <br />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <div class="table-responsive">
        <asp:GridView ID="gvTipoLaboratorio" runat="server" 
            AutoGenerateColumns="False" 
            AllowPaging="True" PageSize="10" 
            OnPageIndexChanging="gvTipoLaboratorio_PageIndexChanging" 
            CssClass="table table-striped table-bordered" 
            OnRowCommand="gvTipoLaboratorio_RowCommand">
            <Columns>
                <asp:BoundField DataField="strNombre_tipoLab" HeaderText="Nombre"></asp:BoundField>
                <asp:TemplateField ShowHeader="False" HeaderText="Accion">
                    <ItemTemplate>
                        <div style="display:flex;">
                            <!--Boton que refleja formulario para actualizar el laboratorio-->
                            <asp:Button ID="btnSelect" runat="server" 
                                Text="Editar" 
                                CssClass="btn btn-warning" 
                                CommandName="Select"  
                                CommandArgument ='<%# Eval("strCod_tipoLab") %>' />&nbsp;&nbsp;
                            <!--Boton para eliminar el laboratorio-->
                            <asp:Button ID="btnDelete" runat="server" 
                                Text="Eliminar" 
                                CssClass="btn btn-danger" 
                                OnClientClick="return showAlertDelete(this);"
                                CommandName="Eliminar" 
                                CommandArgument ='<%# Eval("strCod_tipoLab") %>'/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <!--Formulario para agregar nuevo tipo laboratorio-->
    <!-- Ventana Modal -->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalNuevoLaboratorio">Nuevo Laboratorio</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblNombre" runat="server" 
                        Text="Nombre:" 
                        CssClass="control-label" />
                    <asp:TextBox ID="txtNombre" runat="server" 
                        CssClass="form-control custom-input" 
                        placeholder="Nombre" />
                    <asp:RequiredFieldValidator ID="rfv_tbxNombre" runat="server" 
                        ControlToValidate="txtNombre" 
                        CssClass="alert alert-danger form-control" 
                        ValidationGroup="formulario" 
                        ErrorMessage="Campo requerido" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default border-radius" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnSubmit" runat="server" 
                        Text="Enviar" 
                        ValidationGroup="formulario" 
                        CssClass="btn btn-primary" 
                        OnClick="btnSubmit_Click"/>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Ventana Modal -->
    <div class="modal fade" id="form_actualizar_Tipo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalActLaboratorio">Actualizar Laboratorio</h4>
                </div>
                <div class="modal-body" data-bs-smooth-scroll="true" tabindex="0">
                    <asp:Label ID="lblCodeTipoLabAct" runat="server" 
                        Text="" 
                        Visible="False" />
                    <asp:Label ID="lblNombreAct" runat="server" 
                        Text="Nombre" 
                        CssClass="control-label required" />
                    <asp:TextBox ID="txtNombreAct" runat="server" 
                        CssClass="form-control custom-input" 
                        placeholder="Nombre" />
                    <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" 
                        ControlToValidate="txtNombreAct" 
                        CssClass="alert alert-danger form-control" 
                        ValidationGroup="formularioActualizar" 
                        ErrorMessage="Campo requerido" />
                    <asp:Label ID="lblEstadoAct" runat="server" Text="Estado:" CssClass="control-label" />
                    <asp:DropDownList ID="ddlEstadoAct" runat="server" CssClass="form-control">
                        <asp:ListItem Value="1" Text="Activo" />
                        <asp:ListItem Value="0" Text="Inactivo" />
                    </asp:DropDownList>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btn_Actualizar" runat="server" 
                        Text="Actualizar" 
                        ValidationGroup="formularioActualizar" 
                        CssClass="btn btn-success" 
                        OnClick="btn_Actualizar_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script>
        function showAlertAndReload(title, icon) {
            const Toast = Swal.mixin({
                toast: true,
                position: "top-end",
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer);
                    toast.addEventListener('mouseleave', Swal.resumeTimer);
                }
            });

            Toast.fire({
                icon: icon,
                title: title
            }).then(() => {
                window.location.href = window.location.href;
            });
        }

        function showAlertDelete(btn) {
            event.preventDefault(); // Detiene el postback

            Swal.fire({
                title: "¿Estás seguro?",
                text: "¡No podrás revertir esta acción!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar"
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack(btn.name, '');
                }
            });

            return false; // Siempre evitar el postback automático
        }
    </script>
</asp:Content>

