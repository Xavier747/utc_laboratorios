<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="GestionLaboratorios.aspx.cs" Inherits="academic_private_reservalab_GestionLaborarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../../../Styles/Nuevo/assets/css/laboratorio-style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Listado de laboratorios
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <!--Titulo de la pagina-->
    <div class="title-content">
        <h5 class="title">Listado de los Laboratorios</h5>
    </div>
    <div class="row">
        <!--Boton para agregar un nuevo laboratorio-->
        <div class="col-md-12 text-right">
            <button type="button" class="btn btn-primary btn-nuevo" data-toggle="modal" data-target="#form_registrar">
                <i class="bi bi-plus-lg"></i>Nuevo laboratorio
            </button>
        </div>  
    </div>  
    <div class="alert alert-info alert-dismissible text-center" id="lblMsgLstRegistros" runat="server" visible="false">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
        <span class="mensaje">No existen registros</span>
    </div>

    <!-- GridView para listar laboratorios -->
    <div class="table-responsive">
        <asp:GridView ID="gvLaboratorios" runat="server" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnPageIndexChanging="gvLaboratorios_PageIndexChanging" CssClass="table table-striped table-bordered" OnRowCommand="gvLaboratorios_RowCommand">
            <Columns>
                <asp:BoundField DataField="strNombre_lab" HeaderText="Nombre" />
                <asp:BoundField DataField="intNumeroEquipos_lab" HeaderText="Capacidad" />
                <asp:BoundField DataField="strUbicacion_lab" HeaderText="Ubicación" />
                <asp:TemplateField HeaderText="Imagen 1">
                    <ItemTemplate>
                        <div class="content">
                            <div class="content-img">
                                <!--Ruta de la imagen envia desde el codigo para mostrar la imagen -->
                                <img src='<%= ResolveUrl("~/images/Laboratorio/") %><%# Eval("strFotografia1_lab") %>'/>
                            </div>
                            <br />
                            <!--Boton para mostrar la imagen en grande atraves de una ventana modal -->
                            <asp:ImageButton ID="imgbtnViewImage1" runat="server" OnClick="btnViewImage1_Click" CssClass="btn btn-info" ImageUrl="~/images/static/display.svg" CommandArgument='<%# ResolveUrl("~/images/Laboratorio/") + Eval("strFotografia1_lab") %>' data-toggle="tooltip" data-placement="bottom" title="Ver imagen"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Imagen 2">
                    <ItemTemplate>
                        <div class="content">
                            <div class="content-img">
                                <img src='<%= ResolveUrl("~/images/Laboratorio/") %><%# Eval("strFotografia2_lab") %>' />
                            </div>
                            <br />
                            <asp:ImageButton ID="imgbtnViewImage2" runat="server" OnClick="btnViewImage2_Click" CssClass="btn btn-info btn-img" ImageUrl="~/images/static/display.svg" CommandArgument='<%# ResolveUrl("~/images/Laboratorio/") + Eval("strFotografia2_lab") %>' data-toggle="tooltip" data-placement="bottom" title="Ver imagen"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="Carrera">
                    <ItemTemplate>
                        <div style="display:flex;">
                            <asp:Button ID="btnCarrera" runat="server"  Text="Relacionar" CausesValidation="False" CommandName="Carrera" CssClass="btn btn-default" CommandArgument ='<%# Eval("strCod_lab") %>'/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="Accion">
                    <ItemTemplate>
                        <div style="display:flex;">
                            <!--Boton que refleja formulario para actualizar el laboratorio-->
                            <asp:ImageButton ID="imgbtnEditar" runat="server" CausesValidation="False" CommandName="Select" CssClass="btn btn-warning" ImageUrl="~/images/static/edit.svg" CommandArgument ='<%# Eval("strCod_lab") %>' data-toggle="tooltip" data-placement="left" title="Editar"/>&nbsp;&nbsp;
                            <!--Boton para eliminar el laboratorio-->
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CausesValidation="False" CommandName="Eliminar" CssClass="btn btn-danger" ImageUrl="~/images/static/delete.svg" CommandArgument ='<%# Eval("strCod_lab") %>' OnClientClick="return confirm('¿Seguro que desea eliminar?');" data-toggle="tooltip" data-placement="bottom" title="Eliminar"/>&nbsp;&nbsp;
                            <!--Boton que refleja formulario para asignar responsable el laboratorio-->
                            <asp:ImageButton ID="imgbtnLaboratoristas" runat="server"  CausesValidation="False" CommandName="Laboratoristas" CssClass="btn btn-primary" ImageUrl="~/images/static/add-person.svg" CommandArgument='<%# Eval("strCod_lab") %>' data-toggle="tooltip" data-placement="bottom" title="Asignar laboratorista" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
    </div>    

    <!--Formulario para agregar nuevo laboratorio-->
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
                            <div class="col-md-6">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_tbxNombre" runat="server" ControlToValidate="txtNombre" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">                                    
                                <asp:Label ID="lblNumeroEquipos" runat="server" Text="Numero de equipos" CssClass="control-label required"></asp:Label>
                                <asp:TextBox ID="txtNumeroEquipos" runat="server" CssClass="form-control custom-input" placeholder="50" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_txtNumeroEquipos" runat="server" ControlToValidate="txtNumeroEquipos" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rv_txtNumeroEquipos" runat="server" ControlToValidate="txtNumeroEquipos" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="El número debe estar entre 1 y 100" CssClass="alert alert-danger form-control" ValidationGroup="formulario"></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblTipo" runat="server" Text="Tipo de laboratorio" CssClass="control-label required"></asp:Label>
                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_ddlTipo" runat="server" ControlToValidate="ddlTipo" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblCampoAmplio" runat="server" Text="Campo amplio" CssClass="control-label required"></asp:Label>
                                <asp:DropDownList ID="ddlCampoAmplio" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_ddlCampoAmplio" runat="server" ControlToValidate="ddlCampoAmplio" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblUbicacion" runat="server" Text="Ubicación" CssClass="control-label required"></asp:Label>
                                <asp:TextBox ID="txtUbicacion" runat="server" CssClass="form-control custom-input text-multiple" placeholder="Ubicación" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_txtUbicacion" runat="server" ControlToValidate="txtUbicacion" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblSede" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                                        <asp:DropDownList ID="ddlSede" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_ddlListSedes" runat="server" ControlToValidate="ddlSede" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblFacultad" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                                        <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-control custom-input"  AutoPostBack="True" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_DropDownListFacultades" runat="server" ControlToValidate="ddlFacultad" CssClass="alert alert-danger form-control" ValidationGroup="formulario" InitialValue="" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <div id="listSoftware" runat="server">
                                            <asp:Label ID="lblSoftware" runat="server" Text="Software" CssClass="control-label"></asp:Label>
                                            <div class="softwareContainer">
                                                <asp:UpdatePanel ID="upRepeaterSoftware" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Repeater ID="rptSoftware" runat="server">
                                                            <ItemTemplate>
                                                                <div class="form-control item">
                                                                    <asp:CheckBox ID="chkSoftware" runat="server" ToolTip='<%# Eval("strCod_sof") %>' />
                                                                    <label><%# Eval("strNombre_sof") %></label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlFacultad" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSede" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblImg1" runat="server" Text="Fotografía 1" CssClass="control-label required"></asp:Label>
                                <asp:FileUpload ID="fulImg1" runat="server" CausesValidation="true" CssClass="file" accept="image/*" data-show-upload="false"/>
                                <asp:RequiredFieldValidator ID="rfv_fulImg1" runat="server" ControlToValidate="fulImg1" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblImg2" runat="server" Text="Fotografía 2" CssClass="control-label required"></asp:Label>
                                <asp:FileUpload ID="fulImg2" runat="server" CausesValidation="true" CssClass="file" accept="image/*" data-show-upload="false"/>
                                <asp:RequiredFieldValidator ID="rfv_fulImg2" runat="server" ControlToValidate="fulImg2" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
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
                        <div class="col-md-6">
                            <asp:Label ID="lblCodeLabAct" runat="server" Text="" Visible="False"></asp:Label>
                            <asp:Label ID="lblNombreAct" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNombreAct" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" ControlToValidate="txtNombreAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblNumeroEquiposAct" runat="server" Text="Numero de equipos" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtNumeroEquiposAct" runat="server" CssClass="form-control custom-input" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtNumeroEquiposAct" runat="server" ControlToValidate="txtNumeroEquiposAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rv_txtNumeroEquiposAct" runat="server" ControlToValidate="txtNumeroEquiposAct" MinimumValue="1" MaximumValue="100" Type="Integer" ErrorMessage="El número debe estar entre 1 y 100" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar"></asp:RangeValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblTipoAct" runat="server" Text="Tipo" CssClass="control-label required"></asp:Label>
                            <asp:DropDownList ID="ddlTipoAct" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlTipoAct" runat="server" ControlToValidate="ddlTipoAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                        </div>         
                        <div class="col-md-6">
                            <asp:Label ID="lblCampoAmplioAct" runat="server" Text="Campo amplio" CssClass="control-label required"></asp:Label>
                            <asp:DropDownList ID="ddlCampoAmplioAct" runat="server" CssClass="form-control custom-input" ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlCampoAmplioAct" runat="server" ControlToValidate="ddlCampoAmplioAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblUbicacionAct" runat="server" Text="Ubicación" CssClass="control-label required"></asp:Label>
                            <asp:TextBox ID="txtUbicacionAct" runat="server" CssClass="form-control custom-input text-multiple" placeholder="Ubicación" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtUbicacionAct" runat="server" ControlToValidate="txtUbicacionAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>                            
                                <div class="col-md-6">
                                    <asp:Label ID="lblSedeAct" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlSedeAct" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSedeAct_SelectedIndexChanged" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlSedeAct" runat="server" ControlToValidate="ddlSedeAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblFacultadAct" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                                    <asp:DropDownList ID="ddlFacultadAct" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlFacultadAct_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddlFacultadAct" runat="server" ControlToValidate="ddlFacultadAct" CssClass="alert alert-danger form-control" ValidationGroup="formularioActualizar" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <div id="listSoftwareAct" runat="server">
                                        <asp:Label ID="lblSoftwareAct" runat="server" Text="Software" CssClass="control-label"></asp:Label>
                                        <div class="softwareContainer">
                                            <asp:UpdatePanel ID="upRepeaterSoftwareAct" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="rptSoftwareAct" runat="server" >
                                                        <ItemTemplate>
                                                            <div style="width:100%; border:none; box-shadow:none;" class="form-control">
                                                                <asp:CheckBox ID="chkSoftwareAct" runat="server" ToolTip='<%# Eval("strCod_sof") %>'  />
                                                                <label for="chkSoftwareAct"><%# Eval("strNombre_sof") %></label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlFacultadAct" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlSedeAct" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblImg1Act" runat="server" Text="Fotografia 1" CssClass="control-label"></asp:Label>
                                <asp:FileUpload ID="fulImg1Act" runat="server" CausesValidation="true"  CssClass="file"  accept="image/*"  data-show-upload="false"/>
                                <asp:Label ID="lblImg1InfAct" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblImg2Act" runat="server" Text="Fotografia 2" CssClass="control-label"></asp:Label>
                                <asp:FileUpload ID="fulImg2Act" runat="server" CausesValidation="true"  CssClass="file"  accept="image/*"  data-show-upload="false"/>
                                <asp:Label ID="lblImg2InfAct" runat="server" Text="" Visible="false" ></asp:Label>
                            </div>
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
    
    <!-- Ventana Modal Detalle del personal de laboratorio-->
    <div class="modal fade" id="Lab_Detalle" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel1">Visualizar laboratorio</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblCodLab" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblSedeNombre" runat="server" Text="Sede:"></asp:Label>
                            <asp:TextBox ID="txtSedeNombre" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFacultadNombre" runat="server" Text="Facultad:"></asp:Label>
                            <asp:TextBox ID="txtFacultadNombre" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLaboratorioNombre" runat="server" Text="Laboratorio:"></asp:Label>
                            <asp:TextBox ID="txtLaboratorioNombre" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblRespAdmin" runat="server" Text="Responsable Administrativo:"></asp:Label>
                            <asp:TextBox ID="txtRespAdmin" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRespAcad" runat="server" Text="Responsable Academico:"></asp:Label>
                            <asp:TextBox ID="txtRespAcad" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnAsignarResponsable" runat="server" Text="Nuevo" CssClass="btn btn-primary" CommandArgument='<%# Eval("strCod_lab") %>'  OnClick="btnAsignarResponsable_Click"/>
                    <asp:Button ID="btnActulizarResponsable" runat="server" Text="Actualizar" CssClass="btn btn-warning" CommandArgument='<%# Eval("strCod_lab") %>' OnClick="btnActulizarResponsable_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Ventana Modal Asignacion del personal de laboratorio-->
    <div class="modal fade" id="Form_NuevoResponsable" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalNuevoLaboratorista">Agregar nuevo responsable</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblSedeNuevo" runat="server" Text="Sede:"></asp:Label>
                            <asp:TextBox ID="txtSedeNuevo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFacNuevo" runat="server" Text="Facultad:"></asp:Label>
                            <asp:TextBox ID="txtFacNuevo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLabNuevo" runat="server" Text="Laboratorio:"></asp:Label>
                            <asp:TextBox ID="txtLabNuevo" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblRespAdminNuevo" runat="server" Text="Responsable Administrativo:"></asp:Label>
                            <asp:Label ID="lblRespAdminTipoNuevo" runat="server" Text="Responsable Administrativo" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlRespAdminNuevo" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlRespAdminNuevo" runat="server" ControlToValidate="ddlRespAdminNuevo" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoResponsable" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRespAcadNuevo" runat="server" Text="Responsable Academico:"></asp:Label>
                            <asp:Label ID="lblRespAcadTipoNuevo" runat="server" Text="Responsable Academico" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlRespAcadNuevo" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlRespAcadNuevo" runat="server" ControlToValidate="ddlRespAcadNuevo" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoResponsable" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGaurdar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGaurdar_Click" ValidationGroup="formNuevoResponsable"/>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Ventana Modal Actulizacion del personal de laboratorio-->
    <div class="modal fade" id="Form_ActualizarResponsable" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document" style="margin: 30px auto !important; left: 0% !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="modalLaboratorista">Visualizar laboratorista</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblSedeActualizar" runat="server" Text="Sede:"></asp:Label>
                            <asp:TextBox ID="txtSedeActualizar" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFacActualizar" runat="server" Text="Facultad:"></asp:Label>
                            <asp:TextBox ID="txtFacActualizar" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLabActualizar" runat="server" Text="Laboratorio:"></asp:Label>
                            <asp:TextBox ID="txtLabActualizar" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="lblRespAdminActualizar" runat="server" Text="Responsable Administrativo:"></asp:Label>
                            <asp:Label ID="lblCedulaRespAdmin" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblIdRespAdmin" runat="server" Text="Responsable Administrativo" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlRespAdminActualizar" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddlRespAdminActualizar" runat="server" ControlToValidate="ddlRespAdminActualizar" CssClass="alert alert-danger form-control" ValidationGroup="formActulizarResponsable" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblRespAcadActualizar" runat="server" Text="Responsable Academico:"></asp:Label>
                            <asp:Label ID="lblCedulaRespAcad" runat="server" Text="" Visible="false"></asp:Label>
                            <asp:Label ID="lblIdRespAcad" runat="server" Text="Responsable Academico" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlRespAcadActualizar" runat="server" CssClass="form-control custom-input"></asp:DropDownList> 
                            <asp:RequiredFieldValidator ID="rfv_ddlRespAcadActualizar" runat="server" ControlToValidate="ddlRespAcadActualizar" CssClass="alert alert-danger form-control" ValidationGroup="formActulizarResponsable" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="btnActualizar_Click" ValidationGroup="formActulizarResponsable"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script>
        //Metodo para mostrar mensajes de exito o error
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

