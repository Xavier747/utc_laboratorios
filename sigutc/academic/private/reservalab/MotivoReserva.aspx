<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="MotivoReserva.aspx.cs" Inherits="academic_private_reservalab_MotivoReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado Motivo de Reservas
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12 text-right">
            <button type="button" class="btn btn-primary btn-nuevo" data-toggle="modal" data-target="#form_registrar">
                <i class="bi bi-plus-lg"></i> Nuevo motivo
            </button>
        </div>  
    </div>  
    <br />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    <div class="table-responsive">
        <asp:GridView ID="gvMotivoReserva" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvMotivoReserva_PageIndexChanging" CssClass="table table-striped table-bordered" OnRowCommand="gvMotivoReserva_RowCommand">
            <Columns>
                <asp:BoundField DataField="strNombre_motRes" HeaderText="Nombre"></asp:BoundField>
                <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                    <ItemTemplate>
                        <div style="display:flex;">
                            <asp:Button ID="btnSelect" runat="server" Text="Editar" CssClass="btn btn-warning" CommandName="Select" CommandArgument ='<%# Eval("strCod_motRes") %>' />&nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="Eliminar" CommandArgument ='<%# Eval("strCod_motRes") %>' OnClientClick="return confirm('¿Seguro que desea eliminar?');" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal para agregar nuevo motivo reserva -->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="modalNuevoMotivoLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalNuevoMotivoLabel">Nuevo Motivo de Reserva</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_tbxNombre" runat="server" ControlToValidate="txtNombre" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="control-label required"></asp:Label>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Activo" Value="1" />
                                <asp:ListItem Text="Inactivo" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlEstado" runat="server" ControlToValidate="ddlEstado" CssClass="alert alert-danger form-control" ValidationGroup="formulario" InitialValue="" ErrorMessage="Seleccione un estado"></asp:RequiredFieldValidator>
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

    <!-- Modal para actualizar motivo reserva -->
    <div class="modal fade" id="form_actualizar" tabindex="-1" role="dialog" aria-labelledby="modalActualizarMotivoLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalActualizarMotivoLabel">Actualizar Motivo de Reserva</h4>
                </div>
                <div class="modal-body" data-bs-smooth-scroll="true" tabindex="0">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblCodeMotivoAct" runat="server" Text="" Visible="False"></asp:Label>
                            <asp:Label ID="lblNombreAct" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNombreAct" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" ControlToValidate="txtNombreAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblEstadoAct" runat="server" Text="Estado" CssClass="control-label required"></asp:Label>
                            <asp:DropDownList ID="ddlEstadoAct" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Activo" Value="1" />
                                <asp:ListItem Text="Inactivo" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlEstadoAct" runat="server" ControlToValidate="ddlEstadoAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" InitialValue="" ErrorMessage="Seleccione un estado"></asp:RequiredFieldValidator>
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
    </script>
</asp:Content>
