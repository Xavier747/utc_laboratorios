<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="NuevaReserva.aspx.cs" Inherits="academic_private_reservalab_NuevaReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
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
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblAsignatura" runat="server" Text="ASIGNATURA:"></asp:Label>
                    <asp:DropDownList ID="ddlAsignatura" runat="server" class="form-control" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblHoraInicio" runat="server" Text="HORA DE INICIO:"></asp:Label>
                    <asp:DropDownList ID="ddlHoraInicio" runat="server"class="form-control" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblHoraFin" runat="server" Text="HORA DE FINALIZACION:"></asp:Label>
                    <asp:DropDownList ID="ddlHoraFin" runat="server" class="form-control" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCiclo" runat="server" Text="CICLO:"></asp:Label>
                    <asp:TextBox ID="txtCiclo" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblParalelo" runat="server" Text="PARALELO:"></asp:Label>
                    <asp:TextBox ID="txtParalelo" runat="server" class="form-control" Enabled="false" ></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblCarrera" runat="server" Text="CARRERA:"></asp:Label>
                    <asp:TextBox ID="txtCarrera" runat="server" class="form-control" ></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblNumeroAsistentes" runat="server" Text="TOTAL DE ASISTENTES:"></asp:Label>
                    <asp:TextBox ID="txtNumeroAsistentes" runat="server" class="form-control" ></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <br />   
                    <asp:Button ID="btnVerificar" runat="server" class="btn btn-primary" Text="Verificar" />
                </div>
            </div>
        </fieldset>
        <div id="det_reservacion" runat="server" visible="false">
            <fieldset>
                <legend>DETALLES DE LA RESERVACIÓN</legend>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblTipoMotivo" runat="server" Text="TIPO/MOTIVO:"></asp:Label>
                        <asp:DropDownList ID="selectTipoMotivo" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_selectTipoMotivo" runat="server" ControlToValidate="selectTipoMotivo" CssClass="alert alert-danger form-control" ValidationGroup="formulario_detalle_reservar" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblUnidad" runat="server" Text="Unidad"></asp:Label>
                        <asp:DropDownList ID="ddlUnidad" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_ddlUnidad" runat="server" ControlToValidate="ddlUnidad" CssClass="alert alert-danger form-control"  ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblDescripcionComentario" runat="server" Text="DESCRIPCION / COMENTARIO" class="form-label"></asp:Label>
                        <asp:TextBox ID="txtDescripcionComentario" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblMaterialesEquipos" runat="server" Text="MATERIALES / EQUIPOS:" class="form-label"></asp:Label>
                        <asp:TextBox ID="txtMaterialesEquipos" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </fieldset>
        </div>                        
        <asp:Button ID="btnCancelar" runat="server" class="btn btn-danger" Text="Cancelar" />
        <asp:Button ID="btnGuardar" runat="server" class="btn btn-primary" Text="Guardar" />
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

