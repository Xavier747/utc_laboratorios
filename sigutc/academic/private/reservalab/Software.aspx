<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="Software.aspx.cs" Inherits="academic_private_reservaLab_Software" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../../Styles/Nuevo/assets/css/laboratorio-style.css" rel="stylesheet" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado de softwares
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <!--Boton para agregar un nuevo laboratorio-->
        <div class="col-md-12 text-right">
            <button type="button" class="btn btn-primary btn-nuevo" data-toggle="modal" data-target="#form_registrar">
                <i class="bi bi-plus-lg"></i>Nuevo software
            </button>
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
            <div class="table-responsive">
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
                                    <asp:ImageButton ID="imgbtnSelect" runat="server" CausesValidation="False" CommandName="Select" CssClass="btn btn-warning" ImageUrl="~/images/static/edit.svg" CommandArgument ='<%# Eval("strCod_sof") %>' />&nbsp;&nbsp;
                                    <!--Boton para eliminar el laboratorio-->
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CausesValidation="False" CommandName="Eliminar" CssClass="btn btn-danger" ImageUrl="~/images/static/delete.svg" CommandArgument ='<%# Eval("strCod_sof") %>' OnClientClick="return confirm('¿Seguro que desea eliminar?');" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
<asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
    

    <!-- Ventana Modal -->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog  modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="modalNuevoSoftware">Nuevo Software</h4>
                </div>
                <div class="modal-body">
                    <div class="row" id="SedeFacultad" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="col-md-6">
                                    <asp:Label ID="lblSede" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlSede" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSede_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlListSedes" runat="server" ControlToValidate="ddlSede" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblFacultad" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_DropDownListFacultades" runat="server" ControlToValidate="ddlFacultad" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" InitialValue="" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSede" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtNombre" runat="server" ControlToValidate="txtNombre" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-6">
                            <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control custom-input" placeholder="0" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvf_txtCantidad" runat="server" ControlToValidate="txtCantidad" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rv_Cantidad" runat="server" ControlToValidate="txtCantidad" MinimumValue="1" MaximumValue="50" Type="Integer" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="El número debe estar entre 1 y 50"></asp:RangeValidator>
                        </div>
                    </div>
                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-6">
                                    <asp:Label ID="lblTipo" runat="server" Text="Tipo de licencia" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control custom-input" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_selectTipo" runat="server" ControlToValidate="ddlTipo" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>  
                                </div>
                                <div class="col-md-6">
                                    <div id="content_NombreLicencia" runat="server" visible="false">
                                        <asp:Label ID="lblNombreLicencia" runat="server" Text="Nombre de la licencia" CssClass="control-label required"></asp:Label>
                                        <asp:TextBox ID="txtNombreLicencia" runat="server" CssClass="form-control custom-input text-multiple" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvf_txtNombreLicencia" runat="server" ControlToValidate="txtNombreLicencia" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                                    </div>                      
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCosto" runat="server" Text="Costo" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control custom-input" placeholder="0.0"></asp:TextBox>
                            <asp:RangeValidator ID="rv_txtCosto" runat="server" ControlToValidate="txtCosto" MinimumValue="0" MaximumValue="999999" Type="Double" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="El número debe ser mayor a 0" ></asp:RangeValidator>

                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control custom-input text-multiple" placeholder="Descripcion" TextMode="MultiLine" Rows="6"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtDescripcion" runat="server" ControlToValidate="txtDescripcion" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>

                            <asp:Label ID="lblLink" runat="server" Text="Link de descarga" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtLink" runat="server" CssClass="form-control custom-input" placeholder="www.google.com" TextMode="Url"></asp:TextBox>
                        </div>                                
                        <div class="col-md-6">
                            <asp:Label ID="lblImg1" runat="server" Text="Fotografía 1" CssClass="control-label required"></asp:Label>
                            <asp:FileUpload ID="fulImg1" runat="server" CausesValidation="true" CssClass="file" accept="image/*" data-show-upload="false"/>
                            <asp:RequiredFieldValidator ID="rfv_img1" runat="server" ControlToValidate="fulImg1" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default border-radius" data-dismiss="modal" onclick="cerrar()">Cerrar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Enviar" ValidationGroup="formNuevoSoft" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!--Formulario para actualizar software-->
    <!-- Ventana Modal -->
    <div class="modal fade" id="form_actualizar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog  modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="modalActSoftware">Actualizar Software</h4>
                    <asp:Label ID="lblIdSoftAct" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="modal-body">
                    <div class="row" id="Div1" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="col-md-6">
                                    <asp:Label ID="lblSedeAct" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlSedeAct" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSedeAct_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlSedeAct" runat="server" ControlToValidate="ddlSedeAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblFacultadAct" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlFacultadAct" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlFacultadAct" runat="server" ControlToValidate="ddlFacultadAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" InitialValue="" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSede" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNombreAct" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNombreAct" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" ControlToValidate="txtNombreAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCantidadAct" runat="server" Text="Cantidad" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtCantidadAct" runat="server" CssClass="form-control custom-input" placeholder="0" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtCantidadAct" runat="server" ControlToValidate="txtCantidadAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rv_txtCantidadAct" runat="server" ControlToValidate="txtCantidadAct" MinimumValue="1" MaximumValue="50" Type="Integer" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="El número debe estar entre 1 y 50"></asp:RangeValidator>
                        </div>
                    </div>
                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="col-md-6">
                                    <asp:Label ID="lblTipoAct" runat="server" Text="Tipo de licencia" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlTipoAct" runat="server" CssClass="form-control custom-input" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlTipoAct" runat="server" ControlToValidate="ddlTipoAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>  
                                </div>
                                <div class="col-md-6">
                                    <div id="content_NombreLicenciaAct" runat="server" visible="false">
                                        <asp:Label ID="lblNombreLicenciaAct" runat="server" Text="Nombre de la licencia" CssClass="control-label required"></asp:Label>
                                        <asp:TextBox ID="txtNombreLicenciaAct" runat="server" CssClass="form-control custom-input text-multiple" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_txtNombreLicenciaAct" runat="server" ControlToValidate="txtNombreLicenciaAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                                    </div>                      
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCostoAct" runat="server" Text="Costo" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtCostoAct" runat="server" CssClass="form-control custom-input" placeholder="0.0"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtCostoAct" MinimumValue="0" MaximumValue="999999" Type="Double" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="El número debe ser mayor a 0" ></asp:RangeValidator>

                            <asp:Label ID="lblDescripcionAct" runat="server" Text="Descripcion" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtDescripcionAct" runat="server" CssClass="form-control custom-input text-multiple" placeholder="Descripcion" TextMode="MultiLine" Rows="6"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtDescripcionAct" runat="server" ControlToValidate="txtDescripcionAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>

                            <asp:Label ID="lblLinkAct" runat="server" Text="Link de descarga" CssClass="control-label"></asp:Label>
                            <asp:TextBox ID="txtLinkAct" runat="server" CssClass="form-control custom-input" placeholder="www.google.com" TextMode="Url"></asp:TextBox>
                        </div>                                
                        <div class="col-md-6">
                            <asp:Label ID="lblImg1Act" runat="server" Text="Fotografía 1" CssClass="control-label"></asp:Label>
                            <asp:FileUpload ID="fulImg1Act" runat="server" CausesValidation="true" CssClass="file" accept="image/*" data-show-upload="false"/>
                            <asp:Label ID="lblImg1NameAct" runat="server" Text="Label" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default border-radius" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" ValidationGroup="formulario_Act" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
                </div>
            </div>
        </div>
    </div>

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
    <script type="text/javascript">
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
                // Recargar la página una vez que el toast desaparezca
                window.location.href = window.location.pathname;
            });
        }
    </script>

</asp:Content>

