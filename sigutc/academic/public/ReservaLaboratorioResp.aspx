﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ReservaLaboratorioResp.aspx.cs" Inherits="academic_public_reservalab_ReservaLaboratorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        .fc-daygrid-body tr:nth-child(6) {
            display: none;
        }

        .fc-button {
            background-color: #f5f5f5 !important;
            color: #000 !important;
            border: 1px solid #b8b8b8 !important;
        }

        .fc-button:hover {
            background-color: #bebebe !important;
            border-color: #f5f5f5;
            color: #fff !important;
        }

        .modal .modal-lg{
            width: 90% !important;
            max-width: 100%;
        }

        fieldset{
            border: 1px solid #000; 
            padding: 0 20px 20px 20px;
        }

        legend{
            font-weight:bold; 
            font-size:17px; 
            width:250px;
        }

        /* Estilo contenedor */
        .switch {
          position: relative;
          display: inline-block;
          width: 50px;
          height: 24px;
        }

        /* Oculta el checkbox */
        .switch input {
          opacity: 0;
          width: 0;
          height: 0;
        }

        /* Estilo del interruptor */
        .slider {
          position: absolute;
          cursor: pointer;
          top: 0;
          left: 0;
          right: 0;
          bottom: 0;
          background-color: #ccc;
          transition: .4s;
          border-radius: 24px;
        }

        .slider:before {
          position: absolute;
          content: "";
          height: 18px;
          width: 18px;
          left: 3px;
          bottom: 3px;
          background-color: white;
          transition: .4s;
          border-radius: 50%;
        }

        /* Estado activo */
        input:checked + .slider {
          background-color: #337ab7; /* color de Bootstrap 3 (btn-primary) */
        }

        input:checked + .slider:before {
          transform: translateX(26px);
        }

        .fc-daygrid-event-dot{
            display: none;
        }

        .fc-event-title{
            font-weight: normal !important;
        }

        .fc-event-time{
            font-weight: bold;
        }

        .evento-personalizado {
          background-color: #a8e6a3 !important;
          color: black !important;
          border: 1px solid #90d68b;
          overflow: hidden;
          white-space: nowrap;
          text-overflow: ellipsis;
          padding: 0 2px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Reservar Laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <h3 runat="server" id="titulo" class="text-center"></h3>
        <br />
        <div id="calendarLab"></div>
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
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
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
                                <asp:Label ID="lblNombres" runat="server" Text="NOMBRES:"></asp:Label>
                                <asp:DropDownList ID="ddlDocentes" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Enabled="false"/>
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
                                <button type="button" class="btn btn-primary" onclick="validarReservacion()">Verificar</button>  
                                <span id="tooltipError" class="alert alert-danger form-control" style="display:none;">Esta hora ya está ocupada</span>          
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
                                            <div class="col-md-4">
                                                <label>¿ENCONTRÓ EL SOFTWARE?:</label>
                                                <br />
                                                <label class="switch">
                                                    <input type="checkbox" id="switchEncontrado" checked/>
                                                    <span class="slider round"></span>
                                                </label>
                                                <label id="lblSoftwareVal">SI</label>
                                            </div>
                                            <div class="col-md-4">
                                                <div id="content_nombre" style="display:none;">
                                                    <label>NOMBRE DEL SOFTWARE:</label>
                                                    <input type="text" id="txtSoftware" class="form-control"/>
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
                    <button type="button" class="btn btn-default" data-dismiss="modal">CANCELAR</button>
                </div>
            </div>
        </div>
    </div>

    <!--Formulario detalle-->
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
                            <div class="col-md-2">
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
                                    <div id="content-softwareAct" style="display:none; width: 100%;">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div id="list-softwareAct">
                                                    <label>SOFTWARE:</label>
                                                    <select id="countries" multiple name="softwares[]"></select>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <label>¿ENCONTRÓ EL SOFTWARE?:</label>
                                                <br />
                                                <label class="switch">
                                                    <input type="checkbox" id="switchEncontradoAct" checked/>
                                                    <span class="slider round"></span>
                                                </label>
                                                <label id="lblSoftwareValAct">SI</label>
                                            </div>
                                            <div class="col-md-4">
                                                <div id="content_nombreAct" style="display:none;">
                                                    <label>NOMBRE DEL SOFTWARE:</label>
                                                    <input type="text" id="txtSoftwareAct" class="form-control"/>
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
                                    <div id="content_ddlTema" style="display:none;">
                                        <asp:Label ID="lblTemaAct" runat="server" Text="TEMA:"></asp:Label>
                                        <select id="selectTemaAct" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div id="content_txtTema" style="display:none;">
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
                    </div>
                </div>               
                <div class="modal-footer">
                    <button type="button" id="btnActualizar" class="btn btn-success">ACTUALIZAR</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">CANCELAR</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <script>
        var codLab = '<%= Session["laboratorioId"] %>';
        var cedula = '<%= Context.User.Identity.Name%>'
        var txtFecha = '<%= txtFecha.ClientID %>'
    </script>
    <script src="../../Scripts/reservalab/reservas_utilidades.js"></script>
    <script src="../../Scripts/reservalab/reservas_carga.js"></script>
    <script src="../../Scripts/reservalab/reservas_crud.js"></script>
    <script src="../../Scripts/reservalab/calendario_laboratoristas.js"></script>
</asp:Content>

