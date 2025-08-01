<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ReservaLaboratorioResp.aspx.cs" Inherits="academic_public_reservalab_ReservaLaboratorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href='<%= ResolveUrl("~/Styles/Nuevo/assets/css/Laboratorio/calendario.css") %>' rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Reservar Laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12 text-right">
            <asp:LinkButton ID="lnkRegresar" runat="server" CssClass="btn btn-default" OnClick="lnkRegresar_Click">
                <i class="fa fa-chevron-left" aria-hidden="true"></i>
                <span>Regresar</span>
            </asp:LinkButton>
        </div>
    </div>
    <br />
    <div class="contenedor-general">
        <div class="cabecera">
            <h4 runat="server" id="titulo" class="text-center"></h4>
        </div>
        <div id="calendarLab"></div>

        <asp:Label ID="lblCrono" runat="server"
            style="display: none;"/>

        <asp:Label ID="lblSede" runat="server"
            style="display: none;"/>

        <asp:Label ID="lblFacultad" runat="server"
            style="display: none;"/>

    </div>

    <!--Formulario para una nueva reservacion-->
    <div class="modal fade" id="form_listReserva" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Nueva reservacion</h4>
                </div>
                <div class="modal-body">
                    <h4 id="fecha" class="text-center"></h4>
                    <br />
                    <div class="text-right">
                        <button type="button" class="btn btn-primary" id="btnNuevaReserv">
                            <span>Nueva reserva</span>
                        </button>
                    </div>
                    <br />
                    <div class="table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Tema</th>
                                    <th>Horario</th>
                                    <th>Curso</th>
                                    <th>Estado</th>
                                    <th>Docente</th>
                                    <th>Accion</th>
                                </tr>
                            </thead>
                            <tbody id="tbl_det_reservacion"></tbody>
                        </table>
                    </div>
                    <br />
                    <p id="txtMsgInfo" class="text-center alert alert-danger" style="display: none;"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Formulario para una nueva reservacion-->
    <div class="modal fade" id="form_registrar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="cerrar()"><span aria-hidden="true">×</span></button>
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
                        <legend>TIPO DE RESERVA</legend>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <label id="lblTipoResInt">Reservación interna</label>                                           
                                <label class="switch" style="margin: 0 30px;">
                                    <input type="checkbox" id="switchReserva"/>
                                    <span class="slider round"></span>
                                </label>
                                <label id="lblTipoResExt">Reservación externa</label>
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>DATOS DE RESERVACIÓN</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblFecha" runat="server" Text="FECHA:"></asp:Label>
                                <input id="txtFecha" class="form-control" disabled="disabled"/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraInicio" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                <select class="form-control" id="selectHoraInicio"></select>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraFin" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                <select class="form-control" id="selectHoraFin"></select>
                            </div>
                            <div class="col-md-3">
                                <br />
                                <input type="text" id="id_horario" hidden="hidden"/>
                                <button type="button" class="btn btn-primary" id="btnValidar">Verificar</button>  
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <span id="tooltipErrorInt" class="alert alert-danger form-control" style="display:none;">Esta hora ya está ocupada</span>
                            </div>
                        </div>
                    </fieldset>
                    <div id="reservaInterna">
                        <fieldset>
                            <legend>DATOS DEL SOLICITANTE</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblNombres" runat="server" Text="NOMBRES:"></asp:Label>
                                    <select id="selectDocentes" class="form-control"></select>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblEmail" runat="server" Text="CORREO ELECTRONICO:"></asp:Label>
                                    <input id="txtEmail" class="form-control" disabled="disabled"/>
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend>DATOS DE RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblAsignatura" runat="server" Text="ASIGNATURA:"></asp:Label>
                                    <select class="form-control" id="selectAsignatura"></select>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblCarrera" runat="server" Text="CARRERA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtCarrera" disabled="disabled"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblCiclo" runat="server" Text="CICLO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtCiclo" disabled="disabled"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblParalelo" runat="server" Text="PARALELO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtParalelo" disabled="disabled"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblNumeroAsistentes" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                    <input type="text" class="form-control" id="txtNumeroAsistentes" disabled="disabled"/>
                                </div>
                            </div>
                            <br />
                        </fieldset>
                        <div id="det_reservacionInt" style="display:none;">
                            <fieldset>
                                <legend>DETALLES DE LA RESERVACIÓN</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>¿REQUIERE SOFTWARE?:</label>
                                        <br />
                                        <label class="switch">
                                            <input type="checkbox" id="switchSoftwareInt" />
                                            <span class="slider round"></span>
                                        </label>
                                        <label id="lblSoftwareValidateInt">NO</label>
                                    </div>
                                    <div id="content-softwareInt" class="col-md-9" style="display:none; width: 100%;">
                                        <div class="row">
                                            <div id="list-softwareInt" class="col-md-4">
                                                <label>SOFTWARE:</label>
                                                <select id="ddlSoftwareInt" multiple name="softwares[]"></select>
                                            </div>
                                            <div class="col-md-4">
                                                <label>¿ENCONTRÓ EL SOFTWARE?:</label>
                                                <br />
                                                <label class="switch">
                                                    <input type="checkbox" id="switchEncontradoInt" checked="checked" />
                                                    <span class="slider round"></span>
                                                </label>
                                                <label id="lblSoftwareValInt">SI</label>
                                            </div>
                                            <div id="content_nombreInt" class="col-md-4" style="display:none;">
                                                <label>NOMBRE DEL SOFTWARE:</label>
                                                <input type="text" id="txtSoftwareInt" class="form-control"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblPropositoInt" runat="server" Text="PROPOSITO:"></asp:Label>
                                        <select id="selectPropositoInt" class="form-control">
                                            <option value="clase práctica">CLASE PRÁCTICA</option>
                                            <option value="titulación">TITULACIÓN</option>
                                            <option value="investigación">INVESTIGACIÓN</option>
                                            <option value="posgrados">POSGRADOS</option>
                                            <option value="examen final">EXAMEN FINAL</option>
                                            <option value="examen de gracia">EXAMEN DE GRACIA</option>
                                            <option value="evento ocasional">EVENTO OCASIONAL</option>
                                        </select>
                                    </div>
                                    <div id="content_unidadInt" class="col-md-3">
                                        <div>
                                            <asp:Label ID="lblUnidadInt" runat="server" Text="UNIDAD:"></asp:Label>
                                            <select id="selectUnidadInt" class="form-control"></select>
                                        </div>
                                    </div>
                                    <div id="content_ddlTemaInt" style="display:none;" class="col-md-3">
                                        <asp:Label ID="lblTemaInt" runat="server" Text="TEMA:"></asp:Label>
                                        <select id="selectTemaInt" class="form-control"></select>
                                    </div>
                                    <div id="content_txtTemaInt" style="display:none;" class="col-md-3">
                                        <label>TEMA:</label>
                                        <input type="text" id="txtTemaInt" class="form-control"/>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblDescripcionComentario" runat="server" Text="DESCRIPCION / COMENTARIO" CssClass="form-label"></asp:Label>
                                        <textarea class="form-control" id="txtDescripcionInt"></textarea>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblMaterialesEquipos" runat="server" Text="MATERIALES / EQUIPOS:" CssClass="form-label"></asp:Label>
                                        <textarea class="form-control" id="txtMaterialInt"></textarea>
                                    </div>
                                </div>
                            </fieldset>
                        </div>                        
                    </div>
                    <div id="reservaExterna" style="display: none;">
                        <div id="det_reservacionExt" style="display: none;">
                            <fieldset>
                                <legend>DETALLES DE LA RESERVACIÓN</legend>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblproposito" runat="server" Text="PROPOSITO:"></asp:Label>
                                        <select id="selectTipoMotivo" class="form-control">
                                            <option value="evento ocasional">EVENTO OCASIONAL</option>
                                        </select>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblTema" runat="server" Text="Tema:"></asp:Label>
                                        <input type="text" id="txtTema" class="form-control" />
                                    </div>
                                     <div class="col-md-6">
                                         <asp:Label ID="lblObservacion" runat="server" Text="OBSERVACIÓN:"></asp:Label>
                                         <textarea id="txtObservacion" class="form-control" ></textarea>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label19" runat="server" Text="DESCRIPCION / COMENTARIO" CssClass="form-label"></asp:Label>
                                        <textarea class="form-control" id="txtDescripcion"></textarea>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label20" runat="server" Text="MATERIALES / EQUIPOS:" CssClass="form-label"></asp:Label>
                                        <textarea class="form-control" id="txtMaterial"></textarea>
                                    </div>
                                </div>
                            </fieldset>
                        </div>                        
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="cerrar()">CANCELAR</button>
                    <button type="button" id="btnEnviar" class="btn btn-primary" >GUARDAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Formulario detalle-->
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
                    <br />
                    <fieldset>
                        <legend>DATOS DEL SOLICITANTE</legend>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblCorreoDet" runat="server" Text="CORREO ELECTRONICO:"></asp:Label>
                                <input type="text" id="txtCorreoDet" class="form-control" disabled="disabled" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblNombresDet" runat="server" Text="NOMBRES:"></asp:Label>
                                <input type="text" id="txtNombresDet" class="form-control" disabled="disabled" />
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <div id="detInterno">
                        <fieldset>
                            <legend>FECHA Y HORA</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFechaDet" runat="server" Text="FECHA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtFechaDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" runat="server" Text="ASIGNATURA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtAsigDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblHoraInicioDet" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtHoraInicioDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblHoraFinDet" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                    <input type="text" class="form-control" id="txtHoraFinDet" disabled="disabled" />
                                </div>                            
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCicloDet" runat="server" Text="CICLO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtCicloDet" disabled="disabled" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblParaleloDet" runat="server" Text="PARALELO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtParaleloDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblCarreraDet" runat="server" Text="CARRERA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtCarreraDet" disabled="disabled" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblTotalDet" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                    <input type="text" class="form-control" id="txtAsistentes" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSoftware" runat="server" Text="SOFTWARE:"></asp:Label>
                                    <select id="ddlSoftwareDet" class="form-control"></select>
                                </div>                         
                            </div>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend>DETALLES DE LA RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoMotivoDet" runat="server" Text="PROPÓSITO DE RESERVA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtTipoMotivoDet" disabled="disabled" />
                                </div>
                                <div id="content_unidadDet" class="col-md-3">
                                    <asp:Label ID="lblUnidadDet" runat="server" Text="UNIDAD:" class="form-label"></asp:Label>
                                    <input type="text" id="txtUnidadDet" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTemaDet" runat="server" Text="TEMA:" class="form-label"></asp:Label>
                                    <input type="text" id="txtTemaDet" class="form-control" disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblDescripcioDet" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtDescDet" disabled="disabled" ></textarea>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblMaterialesDet" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtMaterialDet" disabled="disabled" ></textarea>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div id="detExterno">
                        <fieldset>
                            <legend>FECHA Y HORA</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFechaExtDet" runat="server" Text="FECHA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtFechaExtDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblHoraInicioExtDet" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtHoraInicioExtDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblHoraFinExtDet" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                    <input type="text" class="form-control" id="txtHoraFinExtDet" disabled="disabled" />
                                </div>                            
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>DETALLES DE LA RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblPropositoExtDet" runat="server" Text="PROPÓSITO DE RESERVA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtTipoMotivoExtDet" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTemaExtDet" runat="server" Text="TEMA:" class="form-label"></asp:Label>
                                    <input type="text" id="txtTemaExtDet" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblObExtDet" runat="server" Text="OBSERVACIÓN:" class="form-label"></asp:Label>
                                    <textarea id="txtObExtDet" class="form-control" disabled="disabled" ></textarea>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblDescripcion" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtDescExtDet" disabled="disabled" ></textarea>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblMateriales" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtMaterialExtDet" disabled="disabled" ></textarea>
                                </div>
                            </div>
                        </fieldset> 
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Formulario actualizar-->
    <div class="modal fade" id="form_actualizar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" onclick="cerrar()">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Actualizar de reservacion</h4>
                </div>
                <div class="modal-body">
                    <fieldset>
                        <legend>DATOS DEL LABORATORIO</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <label id="lblCodReserva" style="display:none;"></label>
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
                    <br />
                    
                    <!-- TIPO DE RESERVA -->
                    <fieldset>
                        <legend>TIPO DE RESERVA</legend>
                        <div class="text-center">
                            <label>Reservación interna</label>
                            <label class="switch" style="margin: 0 30px;">
                                <input type="checkbox" id="switchReservaAct" disabled="disabled" />
                                <span class="slider round"></span>
                            </label>
                            <label>Reservación externa</label>
                        </div>
                    </fieldset>
                    <br />
                    <br />

                    <!-- CAMPOS COMUNES -->
                    <fieldset>
                        <legend>DATOS DE RESERVACIÓN</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <label>FECHA:</label>
                                <input type="text" id="txtFechaAct" class="form-control" disabled="disabled" />
                            </div>
                            <div class="col-md-3">
                                <label>HORA INICIO:</label>
                                <input class="form-control" id="txtHoraInicioAct" disabled="disabled" />
                            </div>
                            <div class="col-md-3">
                                <label>HORA FIN:</label>
                                <input class="form-control" id="txtHoraFinAct" disabled="disabled" />
                            </div>
                            <div class="col-md-3">
                                <label>&nbsp;</label>
                                <input type="hidden" id="id_horario_act" />
                            </div>
                        </div>
                    </fieldset>
                    <br />
                    <br />

                    <!-- RESERVA INTERNA -->
                    <div id="reservaInternaAct" style="display:none;">
                        <fieldset>
                            <legend>DATOS DEL SOLICITANTE</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>NOMBRES:</label>
                                    <input type="text" class="form-control" id="txtNombreAct" disabled="disabled" />
                                </div>
                                <div class="col-md-6">
                                    <label>CORREO ELECTRÓNICO:</label>
                                    <input type="text" class="form-control" id="txtEmailAct" disabled="disabled" />
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <br />

                        <fieldset>
                            <legend>INFORMACIÓN ADICIONAL</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>ASIGNATURA:</label>
                                    <input type="text" id="txtAsignaturaAct" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-3">
                                    <label>CARRERA:</label>
                                    <input type="text" id="txtCarreraAct" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-2">
                                    <label>CICLO:</label>
                                    <input type="text" id="txtCicloAct" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-2">
                                    <label>PARALELO:</label>
                                    <input type="text" id="txtParaleloAct" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-2">
                                    <label>ASISTENTES:</label>
                                    <input type="text" id="txtAsistentesAct" class="form-control" disabled="disabled" />
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <br />

                        <fieldset>
                            <legend>DETALLES</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>¿REQUIERE SOFTWARE?</label>
                                    <br />
                                    <label class="switch">
                                        <input type="checkbox" id="switchSoftwareAct" />
                                        <span class="slider round"></span>
                                    </label>
                                    <label id="lblSoftwareValidateAct">NO</label>
                                </div>
                                <div class="col-md-9" id="content-softwareAct" style="display:none;">
                                    <div class="row">
                                        <div id="list-softwareAct" class="col-md-4" >
                                            <label>SOFTWARE:</label>
                                            <select id="ddlSoftwareAct" multiple class="form-control"></select>
                                        </div>
                                        <div class="col-md-4">
                                            <label>¿ENCONTRÓ EL SOFTWARE?</label>
                                            <br />
                                            <label class="switch">
                                                <input type="checkbox" id="switchEncontradoAct" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                            <label id="lblSoftwareValAct">SI</label>
                                        </div>
                                        <div class="col-md-4" id="content_nombreAct" style="display:none;">
                                            <label>NOMBRE DEL SOFTWARE:</label>
                                            <input type="text" id="txtSoftwareAct" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />

                            <div class="row">
                                <div class="col-md-3">
                                    <label>PROPOSITO:</label>
                                    <select id="selectPropositoAct" class="form-control">
                                        <option value="clase práctica">CLASE PRÁCTICA</option>
                                        <option value="titulación">TITULACIÓN</option>
                                        <option value="investigación">INVESTIGACIÓN</option>
                                        <option value="posgrados">POSGRADOS</option>
                                        <option value="examen final">EXAMEN FINAL</option>
                                        <option value="examen de gracia">EXAMEN DE GRACIA</option>
                                        <option value="evento ocasional">EVENTO OCASIONAL</option>
                                    </select>
                                </div>
                                <div class="col-md-3" id="content_unidadAct">
                                    <label>UNIDAD:</label>
                                    <select id="selectUnidadAct" class="form-control"></select>
                                </div>
                                <div class="col-md-3" id="content_ddlTemaAct" style="display:none;">
                                    <label>TEMA:</label>
                                    <select id="selectTemaAct" class="form-control"></select>
                                </div>
                                <div class="col-md-3" id="content_txtTemaAct" style="display:none;">
                                    <label>TEMA:</label>
                                    <input type="text" id="txtTemaAct" class="form-control" />
                                </div>
                            </div>
                            <br />
                            <br />

                            <div class="row">
                                <div class="col-md-6">
                                    <label>DESCRIPCIÓN / COMENTARIO:</label>
                                    <textarea class="form-control" id="txtDescripcionAct"></textarea>
                                </div>
                                <div class="col-md-6">
                                    <label>MATERIALES / EQUIPOS:</label>
                                    <textarea class="form-control" id="txtMaterialesAct"></textarea>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <!-- RESERVA EXTERNA -->
                    <div id="reservaExternaAct" style="display:none;">
                        <fieldset>
                            <legend>DATOS DEL SOLICITANTE</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>NOMBRES:</label>
                                    <input type="text" id="txtNombreActExt" class="form-control" disabled="disabled" />
                                </div>
                                <div class="col-md-6">
                                    <label>CORREO ELECTRÓNICO:</label>
                                    <input type="text" id="txtEmailActExt" class="form-control" disabled="disabled" />
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <br />

                        <fieldset>
                            <legend>DETALLES</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>PROPOSITO:</label>
                                    <select id="selectTipoMotivoExt" class="form-control">
                                        <option value="evento ocasional">EVENTO OCASIONAL</option>
                                    </select>
                                </div>
                                <div class="col-md-3">
                                    <label>TEMA:</label>
                                    <input type="text" id="txtTemaExt" class="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label>OBSERVACIÓN:</label>
                                    <textarea id="txtObservacionExt" class="form-control"></textarea>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <label>DESCRIPCIÓN / COMENTARIO:</label>
                                    <textarea class="form-control" id="txtDescripcionExt"></textarea>
                                </div>
                                <div class="col-md-6">
                                    <label>MATERIALES / EQUIPOS:</label>
                                    <textarea class="form-control" id="txtMaterialExt"></textarea>
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <br />

                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label id="msg_actualizar" class="form-control alert alert-danger" style="display: none;"></label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnActualizar" class="btn btn-success">ACTUALIZAR</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CANCELAR</button>
                </div>
            </div>
        </div>
    </div>

    
    <div id="controlUso" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Registro de asistencia</h4>
                </div>
                <div class="modal-body">
                    <label id="codReserva" style="display: none;"></label>
                    <label id="fechaReg" style="display: none;"></label>
                    <label>Observaciones</label>
                    <textarea id="observacion_general" class="form-control"></textarea>
                    <br />
                    <label>ASISTENCIA:</label>
                    <select id="selectAsistencia" class="form-control">
                        <option value="1">SI</option>
                        <option value="0">NO</option>
                    </select>
                    <br />
                    <div id="contentAsistencias">
                        <label>Horas</label>
                        <div id="listaHoras" class="d-flex flex-column gap-1 overflow-auto">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" id="regUso">Save changes</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script>
        var codLabCli = '<%= lblCrono.Text %>';
        var codSedeCli = '<%= lblSede.Text %>';
        var codFacultadCli = '<%= lblFacultad.Text %>';
        var cedula = '<%= Context.User.Identity.Name%>';
    </script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_utilidades.js") %>'></script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_carga.js") %>'></script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_crud.js") %>'></script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/calendario_laboratoristas.js") %>'></script>
</asp:Content>

