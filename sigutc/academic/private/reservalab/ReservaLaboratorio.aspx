<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ReservaLaboratorio.aspx.cs" Inherits="academic_private_reservalab_ReservaLaboratorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="<%= ResolveUrl("~/Content/CSS/reservaLaboratorio.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Reservar Laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 runat="server" id="titulo" class="nombre_Lab"></h2>
    <div class="container-fluid">
        <div id="calendar"></div>
    </div>

    <!--Nueva reservacoion -->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Nueva reservacion</h4>
                </div>
                <div class="modal-body">
                    <fieldset>
                        <legend>DATOS DEL LABORATORIO</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblNombre" runat="server" Text="NOMBRE:"></asp:Label>
                                <asp:TextBox ID="txtNombreLaboratorio" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblResponsableAcademico" runat="server" Text="RESPONSABLE ACADEMICO:"></asp:Label>
                                <asp:TextBox ID="txtResponsableAcademico" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblResponsableAdministrativo" runat="server" Text="RESPONSABLE ADMINISTRATIVO:"></asp:Label>
                                <asp:TextBox ID="txtResponsableAdministrativo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>DATOS DEL SOLICITANTE</legend>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblEmail" runat="server" Text="CORREO ELECTRONICO:"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblNombres" runat="server" Text="NOMBRES:"></asp:Label>
                                <asp:TextBox ID="txtNombreSolicitante" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>DATOS DE RESERVACIÓN</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblFecha" runat="server" Text="FECHA:"></asp:Label>
                                <input type="text" id="txtFecha" class="form-control" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblAsignatura" runat="server" Text="ASIGNATURA:"></asp:Label>
                                <select class="form-control" id="selectAsignatura"></select>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraInicio" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                <select class="form-control" id="selectHoraInicio"></select>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraFin" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                <select class="form-control" id="selectHoraFin"></select>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCiclo" runat="server" Text="CICLO:"></asp:Label>
                                <input type="text" class="form-control" id="txtCiclo" disabled/>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblParalelo" runat="server" Text="PARALELO:"></asp:Label>
                                <input type="text" class="form-control" id="txtParalelo" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblCarrera" runat="server" Text="CARRERA:"></asp:Label>
                                <input type="text" class="form-control" id="txtCarrera" disabled/>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblNumeroAsistentes" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                <input type="text" class="form-control" id="txtNumeroAsistentes" disabled/>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <input type="text" id="id_horario" hidden/>
                                <button type="button" class="btn btn-primary" onclick="verificar_reservacion()">Verificar</button>            
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <div id="det_reservacion" style="display:none">
                        <fieldset>
                            <legend>DETALLES DE LA RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoMotivo" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                                    <asp:DropDownList ID="selectTipoMotivo" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_selectTipoMotivo" runat="server" ControlToValidate="selectTipoMotivo" CssClass="alert alert-danger form-control" ValidationGroup="formulario_detalle_reservar" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblTema" runat="server" Text="TEMA:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtTema"></textarea>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblDescripcionComentario" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtDescripcionComentario"></textarea>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMaterialesEquipos" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtMaterialesEquipos"></textarea>
                                </div>
                            </div>
                        </fieldset>
                    </div>                        
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CANCELAR</button>
                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="guardar_reservacion()">GUARDAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Detalle de reservacoion -->
    <div class="modal fade" id="form_Detalle" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" onclick="cerrar()">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Detalles de reservacion</h4>
                </div>
                <div class="modal-body">
                    <fieldset>
                        <legend>DATOS DEL LABORATORIO</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblNombreLaboratorioDet" runat="server" Text="NOMBRE:"></asp:Label>
                                <asp:TextBox ID="txtNombreLaboratorioDet" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblRespAcadDet" runat="server" Text="RESPONSABLE ACADEMICO:"></asp:Label>
                                <asp:TextBox ID="txtRespAcadDet" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblRespAdminDet" runat="server" Text="RESPONSABLE ADMINISTRATIVO:"></asp:Label>
                                <asp:TextBox ID="txtRespAdminDet" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>DATOS DEL SOLICITANTE</legend>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblCorreoDet" runat="server" Text="CORREO ELECTRONICO:"></asp:Label>
                                <input type="text" class="form-control" id="txtCorreoDet" disabled/>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblNombresDet" runat="server" Text="NOMBRES:"></asp:Label>
                                <input type="text" class="form-control" id="txtNombresDet" disabled/>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>FECHA Y HORA</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblFechaDet" runat="server" Text="FECHA:"></asp:Label>
                                <input type="text" class="form-control" id="txtFechaDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" Text="ASIGNATURA:"></asp:Label>
                                <input type="text" class="form-control" id="txtAsigDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraInicioDet" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                <input type="text" class="form-control" id="txtHoraInicioDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraFinDet" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                <input type="text" class="form-control" id="txtHoraFinDet" disabled/>
                            </div>                            
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label" runat="server" Text="CICLO:"></asp:Label>
                                <input type="text" class="form-control" id="txtCicloDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label3" runat="server" Text="PARALELO:"></asp:Label>
                                <input type="text" class="form-control" id="txtParaleloDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label4" runat="server" Text="CARRERA:"></asp:Label>
                                <input type="text" class="form-control" id="txtCarreraDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label5" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                <input type="text" class="form-control" id="txtAsistentes" disabled/>
                            </div>                            
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>DETALLES DE LA RESERVACIÓN</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblTipoMotivoDet" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                                <input type="text" class="form-control" id="txtTipoMotivoDet" disabled/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label15" runat="server" Text="TEMA:" class="form-label"></asp:Label>
                                <textarea class="form-control" id="txtTemaDet" disabled></textarea>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label16" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                <textarea class="form-control" id="txtDescDet" disabled></textarea>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="Label17" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                <textarea class="form-control" id="txtMaterial" disabled></textarea>

                            </div>
                        </div>
                    </fieldset> 
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="editarEvento()">EDITAR</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CANCELAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Actualizar reservacoion -->
    <div class="modal fade" id="form_actualizar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Actualizar reservacion</h4>
                </div>
                <div class="modal-body">
                    <fieldset>
                        <legend>DATOS DEL LABORATORIO</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblNombreLabAct" runat="server" Text="NOMBRE:"></asp:Label>
                                <asp:TextBox ID="txtNombreLabAct" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblNombreRespAcdAct" runat="server" Text="RESPONSABLE ACADEMICO:"></asp:Label>
                                <asp:TextBox ID="txtNombreRespAcdAct" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblNombreRespAddAct" runat="server" Text="RESPONSABLE ADMINISTRATIVO:"></asp:Label>
                                <asp:TextBox ID="txtNombreRespAddAct" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>DATOS DEL SOLICITANTE</legend>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblEmailAct" runat="server" Text="CORREO ELECTRONICO:"></asp:Label>
                                <input type="text" class="form-control" id="txtEmailAct" disabled/>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblNombreAct" runat="server" Text="NOMBRES:"></asp:Label>
                                <input type="text" class="form-control" id="txtNombreAct" disabled/>
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>DATOS DE RESERVACIÓN</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblFechaAct" runat="server" Text="FECHA:"></asp:Label>
                                <input type="text" id="txtFechaAct" class="form-control" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblAsignaturaAct" runat="server" Text="ASIGNATURA:"></asp:Label>
                                <select class="form-control" id="selectDistributivoAct"></select>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraInicioAct" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                <select class="form-control" id="selectHoraInicioAct"></select>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraFinAct" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                <select class="form-control" id="selectHoraFinAct"></select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCicloAct" runat="server" Text="CICLO:"></asp:Label>
                                <input type="text" class="form-control" id="txtCicloAct" disabled/>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblParaleloAct" runat="server" Text="PARALELO:"></asp:Label>
                                <input type="text" class="form-control" id="txtParaleloAct" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblCarreraAct" runat="server" Text="CARRERA:"></asp:Label>
                                <input type="text" class="form-control" id="txtCarreraAct" disabled/>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblAsistentesAct" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                <input type="text" class="form-control" id="txtNumeroAsistentesAct" disabled/>
                            </div>
                            <div class="col-md-2">
                                <br />
                                <input type="text" id="id_horarioAct" hidden/>
                                <button type="button" class="btn btn-primary" onclick="verificar_reservacion()">Verificar</button>            
                            </div>
                        </div>
                    </fieldset>
                    <div id="det_reservacionAct">
                        <fieldset>
                            <legend>DETALLES DE LA RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoMotivoAct" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                                    <asp:DropDownList ID="selectTipoMotivoAct" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqvSelectTipoMotivoAct" runat="server" ControlToValidate="selectTipoMotivoAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_detalle_reservar" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblTemaAct" runat="server" Text="TEMA:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtTemaAct"></textarea>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblDescripcionAct" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtDescripcionComentarioAct"></textarea>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMaterialesAct" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtMaterialesEquiposAct"></textarea>
                                </div>
                            </div>
                        </fieldset>
                    </div>                        
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">ACTUALIZAR</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CANCELAR</button>
                </div>
            </div>
        </div>
    </div>
  
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
      <script type="text/javascript">
        var tipo_motivo_cli = '<%= selectTipoMotivo.ClientID %>';
        var tipo_motivo_act_cli = '<%= selectTipoMotivoAct.ClientID %>';

        var eventId = null;
    </script>
</asp:Content>

