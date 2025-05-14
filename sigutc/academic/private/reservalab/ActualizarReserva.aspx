<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ActualizarReserva.aspx.cs" Inherits="academic_private_reservalab_ActualizarReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Actualizar reserva
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
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
        <div class="row">
            <div class="col-md-12">
                    <button type="button" class="btn btn-success" data-dismiss="modal">ACTUALIZAR</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="cerrar()">CANCELAR</button>
            </div>
        </div>
    </div>               
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

