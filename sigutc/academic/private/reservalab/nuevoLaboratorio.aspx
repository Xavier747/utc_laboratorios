<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="nuevoLaboratorio.aspx.cs" Inherits="academic_private_reservalab_nuevoLaboratorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <link href="<%= ResolveUrl("~/Content/CSS/gestionLaboratorio.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
   Nuevo Laboratorio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
       
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
                <div class="col-md-6">
                    <asp:Label ID="lblSede" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                    <%--<asp:DropDownList ID="ddlSede" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_ddlListSedes" runat="server" ControlToValidate="ddlSede" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblFacultad" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                    <%-- <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlFacultad_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_DropDownListFacultades" runat="server" ControlToValidate="ddlFacultad" CssClass="alert alert-danger form-control" ValidationGroup="formulario" InitialValue="" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>--%>
                </div>

                <div class="col-md-6">
                    <div id="listSoftware" runat="server">
                        <asp:Label ID="lblSoftware" runat="server" Text="Software" CssClass="control-label"></asp:Label>
                        <div class="softwareContainer">
                            <asp:Repeater ID="rptSoftware" runat="server">
                                <ItemTemplate>
                                    <div class="form-control item">
                                        <asp:CheckBox ID="chkSoftware" runat="server" ToolTip='<%# Eval("strCod_sof") %>' />
                                        <label><%# Eval("strNombre_sof") %></label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>



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

            <div class="text-right mt-3">
                <%--<button type="button" class="btn btn-default border-radius" data-dismiss="modal" onclick="cerrar()">Cerrar</button>--%>
                <%--<asp:Button ID="btnSubmit" runat="server" Text="Enviar" ValidationGroup="formulario" CssClass="btn btn-primary" OnClick="btnSubmit_Click"/>--%>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

