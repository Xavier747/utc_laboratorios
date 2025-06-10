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
            <!--ddl para mostrar y seleccionar SEDES -->
            <asp:Label ID="lblSedeSoft" runat="server" 
                Text="Sede" 
                CssClass="control-label required" />
            <asp:DropDownList ID="ddlSedeSoft" runat="server" 
                CssClass="form-control custom-input" 
                AutoPostBack="True" 
                OnSelectedIndexChanged="ddlSedeSoft_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfv_ddlSedeSoft" runat="server" 
                ControlToValidate="ddlSedeSoft" 
                CssClass="alert alert-danger form-control" 
                ValidationGroup="formulario" 
                ErrorMessage="Seleccione una opción" />
        </div>
        <div class="col-md-6">
            <!--ddl para mostrar y seleccionar FACULTADES -->
            <asp:Label ID="lblFacultadSoft" runat="server" 
                Text="Facultades" 
                CssClass="control-label required" />
            <asp:DropDownList ID="ddlFacultadSoft" runat="server" 
                CssClass="form-control custom-input"  
                AutoPostBack="True" 
                OnSelectedIndexChanged="ddlFacultadSoft_SelectedIndexChanged" />
            <asp:RequiredFieldValidator ID="rfv_ddlFacultadSoft" runat="server" 
                ControlToValidate="ddlFacultadSoft" 
                CssClass="alert alert-danger form-control" 
                ValidationGroup="formulario" 
                ErrorMessage="Seleccione una opción" />
        </div>
        <br />
        <div class="col-md-12">
            <div class="table-responsive">
                <!--Tabla donde se muestran los registros-->
                <asp:GridView ID="gvSoftware" runat="server" 
                    AutoGenerateColumns="False" 
                    AllowPaging="True" 
                    PageSize="10" 
                    OnPageIndexChanging="gvSoftware_PageIndexChanging" 
                    CssClass="table table-striped table-bordered" 
                    OnRowCommand="gvSoftware_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="strNombre_sof" HeaderText="Nombre"></asp:BoundField>
                        <asp:BoundField DataField="intCantidad_sof" HeaderText="Cantidad"></asp:BoundField>
                        <asp:BoundField DataField="strDescripcion_sof" HeaderText="Descripciom"></asp:BoundField>
                        <asp:TemplateField HeaderText="Imagen">
                            <ItemTemplate>
                                <div class="content">
                                    <div class="content-img">
                                        <!--Ruta de la imagen envia desde el codigo para mostrar la imagen -->
                                        <asp:Image ID="imgSoftware" runat="server" 
                                            ImageUrl='<%# "ImageHandlerSoftware.ashx?image=" + System.IO.Path.GetFileName(Eval("strImagen_sof").ToString()) %>' />
                                    </div>
                                    <br />

                                    <!--Boton para mostrar la imagen en grande atraves de una ventana modal -->
                                    <asp:Button ID="btnVistaCompleta" runat="server" 
                                        CssClass="btn btn-info" 
                                        OnClick="btnVistaCompleta_Click" 
                                        CommandArgument='<%# "ImageHandlerSoftware.ashx?image=" + System.IO.Path.GetFileName(Eval("strImagen_sof").ToString()) %>' 
                                        Text="Ver Imagen" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderText="Accion">
                            <ItemTemplate>
                                <div style="display:flex;">
                                    <!--Boton que refleja formulario para actualizar el laboratorio-->
                                    <asp:Button ID="btnSelect" runat="server" 
                                        Text="Editar" 
                                        CommandName="Select" 
                                        CssClass="btn btn-warning" 
                                        CommandArgument ='<%# Eval("strCod_sof") %>' /> &nbsp; &nbsp;
                                
                                    <!--Boton para eliminar el laboratorio-->
                                    <asp:Button ID="btnDelete" runat="server" 
                                        Text="Eliminar" 
                                        CssClass="btn btn-danger" 
                                        OnClientClick="return showAlertDelete(this);"      
                                        CommandName="Eliminar" 
                                        CommandArgument ='<%# Eval("strCod_sof") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    

    <!-- Ventana Modal -->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog  modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="modalNuevoSoftware">Nuevo Software</h4>
                </div>
                <div class="modal-body">
                    <div id="SedeFacultad" runat="server" class="row">
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="upSede" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblSede" runat="server" 
                                        Text="Sedes" 
                                        CssClass="control-label required" />
                                    <asp:DropDownList ID="ddlSede" runat="server" 
                                        CssClass="form-control custom-input"
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddlSede_SelectedIndexChanged" />
                                    <asp:RequiredFieldValidator ID="rfv_ddlSede" runat="server"
                                        ControlToValidate="ddlSede"
                                        CssClass="alert alert-danger form-control"
                                        ValidationGroup="formNuevoSoft"
                                        InitialValue=""
                                        ErrorMessage="Seleccione una sede." />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-md-6">
                            <asp:UpdatePanel ID="upFacultad" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblFacultad" runat="server" 
                                        Text="Facultades" 
                                        CssClass="control-label required" />
                                    <asp:DropDownList ID="ddlFacultad" runat="server" 
                                        CssClass="form-control custom-input" />
                                    <asp:RequiredFieldValidator ID="rfv_ddlFacultad" runat="server"
                                        ControlToValidate="ddlFacultad"
                                        CssClass="alert alert-danger form-control"
                                        ValidationGroup="formNuevoSoft"
                                        InitialValue=""
                                        ErrorMessage="Seleccione una facultad." />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSede" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre" 
                                CssClass="control-label required" />
                            <asp:TextBox ID="txtNombre" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="Nombre" />
                            <asp:RequiredFieldValidator ID="rfv_txtNombre" runat="server" 
                                ControlToValidate="txtNombre" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formNuevoSoft" 
                                ErrorMessage="Campo requerido" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblCantidad" runat="server" 
                                Text="Cantidad" 
                                CssClass="control-label required" />
                            <asp:TextBox ID="txtCantidad" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="0" 
                                TextMode="Number" />
                            <asp:RequiredFieldValidator ID="rvf_txtCantidad" runat="server" 
                                ControlToValidate="txtCantidad" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formNuevoSoft" 
                                ErrorMessage="Campo requerido" />
                            <asp:RangeValidator ID="rv_Cantidad" runat="server" 
                                ControlToValidate="txtCantidad" 
                                MinimumValue="1" 
                                MaximumValue="50" 
                                Type="Integer" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formNuevoSoft" 
                                ErrorMessage="El número debe estar entre 1 y 50" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblTipo" runat="server" 
                                        Text="Tipo de licencia" 
                                        CssClass="control-label required" />
                                    <asp:DropDownList ID="ddlTipo" runat="server" 
                                        CssClass="form-control custom-input" 
                                        AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" />
                                    <asp:RequiredFieldValidator ID="rfv_selectTipo" runat="server" 
                                        ControlToValidate="ddlTipo" 
                                        CssClass="alert alert-danger form-control" 
                                        ValidationGroup="formNuevoSoft" 
                                        ErrorMessage="Seleccione una opción" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>          
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="content_NombreLicencia" runat="server">
                                        <asp:Label ID="lblNombreLicencia" runat="server" 
                                            Text="Nombre de la licencia" 
                                            CssClass="control-label required" />
                                        <asp:TextBox ID="txtNombreLicencia" runat="server" 
                                            CssClass="form-control custom-input text-multiple" 
                                            TextMode="MultiLine" Rows="2" />
                                        <asp:RequiredFieldValidator ID="rvf_txtNombreLicencia" runat="server" 
                                            ControlToValidate="txtNombreLicencia" 
                                            CssClass="alert alert-danger form-control" 
                                            ValidationGroup="formNuevoSoft" 
                                            ErrorMessage="Campo requerido" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCosto" runat="server" 
                                Text="Costo" 
                                CssClass="control-label" />
                            <asp:TextBox ID="txtCosto" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="0,0" />
                            <asp:RangeValidator ID="rv_txtCosto" runat="server" 
                                ControlToValidate="txtCosto" 
                                MinimumValue="0" 
                                MaximumValue="999999" 
                                Type="Double" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formNuevoSoft" 
                                ErrorMessage="El número debe ser mayor a 0" />
                        </div>                                
                        <div class="col-md-6">
                            <asp:Label ID="lblImg1" runat="server" 
                                Text="Fotografía 1" 
                                CssClass="control-label required" />
                            <asp:FileUpload ID="fulImg1" runat="server" 
                                CausesValidation="true" 
                                CssClass="form-control" 
                                accept="image/*" 
                                data-show-upload="false" />
                            <asp:RequiredFieldValidator ID="rfv_img1" runat="server" 
                                ControlToValidate="fulImg1" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formNuevoSoft" 
                                ErrorMessage="Campo requerido" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblDescripcion" runat="server" 
                                Text="Descripcion" 
                                CssClass="control-label required" />
                            <asp:TextBox ID="txtDescripcion" runat="server" 
                                CssClass="form-control custom-input text-multiple" 
                                placeholder="Descripcion" 
                                TextMode="MultiLine" 
                                Rows="6" />
                            <asp:RequiredFieldValidator ID="rfv_txtDescripcion" runat="server" 
                                ControlToValidate="txtDescripcion" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formNuevoSoft" 
                                ErrorMessage="Campo requerido" />
                        </div>                                
                        <div class="col-md-6">
                            <asp:Label ID="lblLink" runat="server"
                                Text="Link de descarga" 
                                CssClass="control-label" />
                            <asp:TextBox ID="txtLink" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="www.google.com" 
                                TextMode="Url" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default border-radius" data-dismiss="modal" onclick="cerrar()">Cerrar</button>
                    <asp:Button ID="btnGuardar" runat="server"
                        Text="Guardar" 
                        CausesValidation="true" 
                        CssClass="btn btn-primary"
                        OnClientClick="return validarArchivo();"
                        ValidationGroup="formNuevoSoft" 
                        OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!--Formulario para actualizar software-->
    <!-- Ventana Modal -->
    <div class="modal fade" id="form_actualizar_software" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="modalActSoftware">Actualizar Software</h4>
                    <asp:Label ID="lblIdSoftAct" runat="server" Text="" Visible="false"></asp:Label>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6">
                             <asp:Label ID="lblNombreAct" runat="server" 
                                 Text="Nombre" 
                                 CssClass="control-label required" />
                            <asp:TextBox ID="txtNombreAct" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="Nombre" />
                            <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" 
                                ControlToValidate="txtNombreAct" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formulario_Act" 
                                ErrorMessage="Campo requerido" />                 
                        </div>

                        <div class="col-md-6">
                            <asp:Label ID="lblCantidadAct" runat="server" 
                                Text="Cantidad" 
                                CssClass="control-label required" />
                            <asp:TextBox ID="txtCantidadAct" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="0" 
                                TextMode="Number" />
                            <asp:RequiredFieldValidator ID="rfv_txtCantidadAct" runat="server" 
                                ControlToValidate="txtCantidadAct" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formulario_Act" 
                                ErrorMessage="Campo requerido" />
                            <asp:RangeValidator ID="rv_txtCantidadAct" runat="server" 
                                ControlToValidate="txtCantidadAct" 
                                MinimumValue="1" 
                                MaximumValue="50" 
                                Type="Integer" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formulario_Act" 
                                ErrorMessage="El número debe estar entre 1 y 50" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <asp:UpdatePanel ID="upTipoAct" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblTipoAct" runat="server" 
                                        Text="Tipo de licencia" 
                                        CssClass="control-label required" />
                                    <asp:DropDownList ID="ddlTipoAct" runat="server" 
                                        CssClass="form-control custom-input" 
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddlTipoAct_SelectedIndexChanged" />
                                    <asp:RequiredFieldValidator ID="rfv_ddlTipoAct" runat="server" 
                                        ControlToValidate="ddlTipoAct" 
                                        CssClass="alert alert-danger form-control" 
                                        ValidationGroup="formulario_Act" 
                                        ErrorMessage="Seleccione una opción" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <div class="col-md-6">
                            <asp:UpdatePanel ID="upNombreLicenciaAct" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="content_NombreLicenciaAct" runat="server">
                                        <asp:Label ID="lblNombreLicenciaAct" runat="server" 
                                            Text="Nombre de la licencia" 
                                            CssClass="control-label required" />
                                        <asp:TextBox ID="txtNombreLicenciaAct" runat="server" 
                                            CssClass="form-control custom-input text-multiple" 
                                            TextMode="MultiLine" Rows="2" />
                                        <asp:RequiredFieldValidator ID="rfv_txtNombreLicenciaAct" runat="server" 
                                            ControlToValidate="txtNombreLicenciaAct" 
                                            CssClass="alert alert-danger form-control" 
                                            ValidationGroup="formulario_Act" 
                                            ErrorMessage="Campo requerido" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoAct" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblCostoAct" runat="server" 
                                Text="Costo" 
                                CssClass="control-label" />
                            <asp:TextBox ID="txtCostoAct" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="0,0" />
                            <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                ControlToValidate="txtCostoAct" 
                                MinimumValue="0" 
                                MaximumValue="999999" 
                                Type="Double" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formulario_Act" 
                                ErrorMessage="El número debe ser mayor a 0" />
                        </div>
                        <div class="col-md-6">
                             <asp:Label ID="lblImg1Act" runat="server" 
                                 Text="Fotografía 1" 
                                 CssClass="control-label" />
                            <asp:FileUpload ID="fulImg1Act" runat="server" 
                                CausesValidation="true" 
                                CssClass="form-control" 
                                accept="image/*" 
                                data-show-upload="false"/>
                            <asp:Label ID="lblImg1NameAct" runat="server" 
                                Text="" 
                                Visible="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblLinkAct" runat="server" 
                                Text="Link de descarga" 
                                CssClass="control-label" />
                            <asp:TextBox ID="txtLinkAct" runat="server" 
                                CssClass="form-control custom-input" 
                                placeholder="www.google.com" 
                                TextMode="Url" />
                            <br />
                            <asp:Label ID="lblEstadoAct" runat="server" 
                                Text="Estado" 
                                CssClass="control-label" />
                            <asp:DropDownList ID="ddlEstadoAct" runat="server" CssClass="form-control" >
                                <asp:ListItem Value="1" Text="Activo" ></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactivo" ></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescripcionAct" runat="server" 
                                Text="Descripción" 
                                CssClass="control-label required" />
                            <asp:TextBox ID="txtDescripcionAct" runat="server" 
                                CssClass="form-control custom-input text-multiple" 
                                TextMode="MultiLine" Rows="6" />
                            <asp:RequiredFieldValidator ID="rfv_txtDescripcionAct" runat="server" 
                                ControlToValidate="txtDescripcionAct" 
                                CssClass="alert alert-danger form-control" 
                                ValidationGroup="formulario_Act" 
                                ErrorMessage="Campo requerido" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default border-radius" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnActualizar" runat="server" 
                        Text="Actualizar" 
                        CausesValidation="true" 
                        CssClass="btn btn-primary" 
                        OnClientClick="return validarArchivo();" 
                        ValidationGroup="formulario_Act" 
                        OnClick="btnActualizar_Click" />
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
    <script>
        function validarArchivo() {
            var input1 = $('#<%= fulImg1.ClientID %>')[0].files.length > 0
                ? $('#<%= fulImg1.ClientID %>')[0]
                : $('#<%= fulImg1Act.ClientID %>')[0];
         
            var maxMB = 4; // Límite permitido (ajustable)
            if (input1.files.length > 0 ) {
                var file1 = input1.files[0];
                var sizeMB1 = file1.size / (1024 * 1024);               
               if (sizeMB1 > maxMB) {
                    showAlertImageBig("La imagen 1 excede el tamaño máximo permitido de " + maxMB + " MB.", "error");
                    return false; // evita que se envíe el formulario
               }
            }

            // Detectar qué formulario está visible o activo
            var formularioNuevoVisible = $('#form_registrar').is(':visible');
            var formularioActVisible = $('#form_actualizar_software').is(':visible');

            if (typeof (Page_ClientValidate) === 'function') {
                if (formularioNuevoVisible) {
                    if (!Page_ClientValidate('formNuevoSoft')) {
                        return false;
                    }
                } else if (formularioActVisible) {
                    if (!Page_ClientValidate('formulario_Act')) {
                        console.log("hola mundo");
                        return false;
                    }
                }
            }
            return true; // Todo válido
        }

        //Notificacion imagen muy grande
        function showAlertImageBig(title, icon) {
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
            });
        }

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