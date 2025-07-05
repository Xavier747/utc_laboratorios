<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ReservaLaboratorioDocen.aspx.cs" Inherits="academic_public_ReservaLaboratorioDocen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href='<%= ResolveUrl("~/Styles/Nuevo/assets/css/Laboratorio/calendario.css") %>' rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Reservar Laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="contenedor-general">
        <div class="cabecera">
            <h4 runat="server" id="titulo" class="text-center"></h4>
        </div>
        <div id="calendar"></div>

        <asp:Label ID="lblCrono" runat="server" 
            style="display: none;"/>
    </div>

    <!--Formulario para una nueva reservacion-->
    <div class="modal fade" id="form_listReserva" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Listado de reservación</h4>
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
                                    <th>Horio</th>
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
                <div id="form_insert">
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
                                    <asp:Label ID="lblNombres" runat="server" Text="NOMBRES:"></asp:Label>
                                    <asp:TextBox ID="txtNombreSolicitante" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblEmail" runat="server" Text="CORREO ELECTRONICO:"></asp:Label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend>DATOS DE RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblFecha" runat="server" Text="FECHA:"></asp:Label>
                                    <input type="text" id="txtFecha" class="form-control" disabled="disabled"/>
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
                                    <input type="text" class="form-control" id="txtCiclo" disabled="disabled"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblParalelo" runat="server" Text="PARALELO:"></asp:Label>
                                    <input type="text" class="form-control" id="txtParalelo" disabled="disabled"/>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblCarrera" runat="server" Text="CARRERA:"></asp:Label>
                                    <input type="text" class="form-control" id="txtCarrera" disabled="disabled"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblNumeroAsistentes" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                    <input type="text" class="form-control" id="txtNumeroAsistentes" disabled="disabled"/>
                                </div>
                                <div class="col-md-2">
                                    <br />
                                    <input type="text" id="id_horario" hidden="hidden"/>
                                    <button type="button" class="btn btn-primary" id="btnValidar">Verificar</button>  
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <span id="tooltipError" class="alert alert-danger form-control" style="display:none;"></span>
                                </div>
                            </div>
                        </fieldset>
                        <div id="det_reservacion" style="display:none;">
                            <fieldset>
                                <legend>DETALLES DE LA RESERVACIÓN</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>¿REQUIERE SOFTWARE?:</label>
                                        <br />
                                        <label class="switch">
                                            <input type="checkbox" id="switchSoftware" />
                                            <span class="slider round"></span>
                                        </label>
                                        <label id="lblSoftwareValidate">NO</label>
                                    </div>
                                    <div class="col-md-9">
                                        <div id="content-software" style="display:none; width: 100%;">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div id="list-software">
                                                        <label>SOFTWARE:</label>
                                                        <select id="countries" multiple name="softwares[]"></select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblTipoMotivo" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                                        <select id="selectTipoMotivo" class="form-control">
                                            <option value="clase práctica">CLASE PRÁCTICA</option>
                                            <option value="titulación">TITULACIÓN</option>
                                            <option value="investigación">INVESTIGACIÓN</option>
                                            <option value="posgrados">POSGRADOS</option>
                                            <option value="examen final">EXAMEN FINAL</option>
                                            <option value="examen de gracia">EXAMEN DE GRACIA</option>
                                            <option value="evento ocasional">EVENTO OCASIONAL</option>
                                        </select>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="content_unidad">
                                            <asp:Label ID="lblUnidad" runat="server" Text="UNIDAD:"></asp:Label>
                                            <select id="selectUnidad" class="form-control"></select>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="content_ddlTema" style="display:none;">
                                            <asp:Label ID="lblTema" runat="server" Text="TEMA:"></asp:Label>
                                            <select id="selectTema" class="form-control"></select>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="content_txtTema" style="display:none;">
                                            <label>TEMA:</label>
                                            <input type="text" id="txtTema" class="form-control"/>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblDescripcionComentario" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                        <textarea class="form-control" id="txtDescripcion"></textarea>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblMaterialesEquipos" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                        <textarea class="form-control" id="txtMaterial"></textarea>
                                    </div>
                                </div>
                            </fieldset>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <label id="msg_registro" class="form-control alert alert-danger" style="display: none;"></label>
                                </div>
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
                                <input type="text" id="txtCorreoDet" class="form-control" disabled />
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblNombresDet" runat="server" Text="NOMBRES:"></asp:Label>
                                <input type="text" id="txtNombresDet" class="form-control" disabled />
                            </div>
                        </div>
                    </fieldset>
                    <br />
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
                            <div class="col-md-2">
                                <asp:Label ID="Label" runat="server" Text="CICLO:"></asp:Label>
                                <input type="text" class="form-control" id="txtCicloDet" disabled/>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label3" runat="server" Text="PARALELO:"></asp:Label>
                                <input type="text" class="form-control" id="txtParaleloDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label4" runat="server" Text="CARRERA:"></asp:Label>
                                <input type="text" class="form-control" id="txtCarreraDet" disabled/>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label5" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                                <input type="text" class="form-control" id="txtAsistentes" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" Text="SOFTWARE:"></asp:Label>
                                <select id="ddlSoftwareDet" class="form-control"></select>
                            </div>                         
                        </div>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>DETALLES DE LA RESERVACIÓN</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblTipoMotivoDet" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                                <input type="text" class="form-control" id="txtTipoMotivoDet" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblUnidadDet" runat="server" Text="UNIDAD:" class="form-label"></asp:Label>
                                <input type="text" id="txtUnidadDet" class="form-control"  disabled />
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblTemaDet" runat="server" Text="TEMA:" class="form-label"></asp:Label>
                                <input type="text" id="txtTemaDet" class="form-control"  disabled />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblDescripcioDet" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                <textarea class="form-control" id="txtDescDet" disabled></textarea>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblMaterialesDet" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                <textarea class="form-control" id="txtMaterialDet" disabled></textarea>
                            </div>
                        </div>
                    </fieldset> 
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Formulario Actualizar-->
    <div class="modal fade" id="form_actualizar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true" onclick="cerrar()">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Actualizar de reservacion</h4>
                    <label id="lblCodReserva" style="display: none;"></label>
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
                                <input type="text" id="txtAsignaturaAct" class="form-control" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraInicioAct" runat="server" Text="HORA DE INICIO:"></asp:Label>
                                <input type="text" id="txtHoraInicioAct" class="form-control" disabled/>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblHoraFinAct" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                                <input type="text" id="txtHoraFinAct" class="form-control" disabled/>
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
                            <div class="col-md-3">
                                <asp:Label ID="Label6" runat="server" Text="SOFTWARE:"></asp:Label>
                                <select id="ddlSoftwareAct" class="form-control"></select>
                            </div>
                    </fieldset>
                    <div id="det_reservacionAct">
                        <fieldset>
                            <legend>DETALLES DE LA RESERVACIÓN</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>¿REQUIERE SOFTWARE?:</label>
                                    <br />
                                    <label class="switch">
                                        <input type="checkbox" id="switchSoftwareAct" />
                                        <span class="slider round"></span>
                                    </label>
                                    <label id="lblSoftwareValidateAct">NO</label>
                                </div>
                                <div class="col-md-9">
                                    <div id="content-softwareAct" style="width: 100%; display:none; ">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div id="list-softwareAct">
                                                    <label>SOFTWARE:</label>
                                                    <select id="softwareSelect" multiple name="softwares[]"></select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoMotivoAct" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                                    <select id="selectTipoMotivoAct" class="form-control">
                                        <option value="clase práctica">CLASE PRÁCTICA</option>
                                        <option value="titulación">TITULACIÓN</option>
                                        <option value="investigación">INVESTIGACIÓN</option>
                                        <option value="posgrados">POSGRADOS</option>
                                        <option value="examen final">EXAMEN FINAL</option>
                                        <option value="examen de gracia">EXAMEN DE GRACIA</option>
                                        <option value="evento ocasional">EVENTO OCASIONAL</option>
                                    </select>
                                </div>
                                <div class="col-md-3">
                                    <div id="content_unidadAct">
                                        <asp:Label ID="lblUnidadAct" runat="server" Text="UNIDAD:"></asp:Label>
                                        <select id="selectUnidadAct" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div id="content_ddlTemaAct" style="display:none;">
                                        <asp:Label ID="lblTemaAct" runat="server" Text="TEMA:"></asp:Label>
                                        <select id="selectTemaAct" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div id="content_txtTemaAct" style="display:none;">
                                        <label>TEMA:</label>
                                        <input type="text" id="txtTemaAct" class="form-control"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblDescripcionAct" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtDescripcionAct"></textarea>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblMaterialesAct" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                                    <textarea class="form-control" id="txtMaterialesAct"></textarea>
                                </div>
                            </div>
                        </fieldset>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <label id="msg_actualizar" class="form-control alert alert-danger" style="display: none;"></label>
                            </div>
                        </div>
                    </div>
                </div>               
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CANCELAR</button>
                    <button type="button" id="btnActualizar" class="btn btn-success">ACTUALIZAR</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script>
        var codLabCli = '<%= lblCrono.ClientID %>';
        var cedula = '<%= Context.User.Identity.Name%>'
    </script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_utilidades.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_carga.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservas_crud.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/reservalab/reservarLaboratorio.js") %>' type="text/javascript"></script>
</asp:Content>

