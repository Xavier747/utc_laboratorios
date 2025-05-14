<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="DetalleReservacion.aspx.cs" Inherits="academic_private_reservalab_DetalleReservacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

