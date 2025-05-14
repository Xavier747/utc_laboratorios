<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="NuevaReserva.aspx.cs" Inherits="academic_private_reservalab_NuevaReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
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
                                    <asp:ImageButton ID="txtFecha" runat="server" CssClass="form-control" Enabled="false" />
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
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

