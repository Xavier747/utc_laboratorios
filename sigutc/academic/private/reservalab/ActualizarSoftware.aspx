<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ActualizarSoftware.aspx.cs" Inherits="academic_private_reservalab_ActualizarSoftware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        .required::after{
            content: '*';
            margin-left:2px;
            color:red;
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Actualizar Software
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblSedeAct" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                <asp:DropDownList ID="ddlSedeAct" runat="server" CssClass="form-control custom-input" AutoPostBack="True" OnSelectedIndexChanged="ddlSedeAct_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_ddlSedeAct" runat="server" ControlToValidate="ddlSedeAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblFacultadAct" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                <asp:DropDownList ID="ddlFacultadAct" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_ddlFacultadAct" runat="server" ControlToValidate="ddlFacultadAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" InitialValue="" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblNombreAct" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                <asp:TextBox ID="txtNombreAct" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_txtNombreAct" runat="server" ControlToValidate="txtNombreAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblCantidadAct" runat="server" Text="Cantidad" CssClass="control-label required"></asp:Label>
                <asp:TextBox ID="txtCantidadAct" runat="server" CssClass="form-control custom-input" placeholder="0" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_txtCantidadAct" runat="server" ControlToValidate="txtCantidadAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rv_txtCantidadAct" runat="server" ControlToValidate="txtCantidadAct" MinimumValue="1" MaximumValue="50" Type="Integer" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="El número debe estar entre 1 y 50"></asp:RangeValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblTipoAct" runat="server" Text="Tipo de licencia" CssClass="control-label required"></asp:Label>
                <asp:DropDownList ID="ddlTipoAct" runat="server" CssClass="form-control custom-input" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoAct_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_ddlTipoAct" runat="server" ControlToValidate="ddlTipoAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>  
            </div>
            <div class="col-md-6">
                <div id="content_NombreLicenciaAct" runat="server">
                    <asp:Label ID="lblNombreLicenciaAct" runat="server" Text="Nombre de la licencia" CssClass="control-label required"></asp:Label>
                    <asp:TextBox ID="txtNombreLicenciaAct" runat="server" CssClass="form-control custom-input text-multiple" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_txtNombreLicenciaAct" runat="server" ControlToValidate="txtNombreLicenciaAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                </div>                      
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblCostoAct" runat="server" Text="Costo" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtCostoAct" runat="server" CssClass="form-control custom-input" placeholder="0.0"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtCostoAct" MinimumValue="0" MaximumValue="999999" Type="Double" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="El número debe ser mayor a 0" ></asp:RangeValidator>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblImg1Act" runat="server" Text="Fotografía 1" CssClass="control-label"></asp:Label>
                <asp:FileUpload ID="fulImg1Act" runat="server" CausesValidation="true" CssClass="form-control" accept="image/*" data-show-upload="false"/>
                <asp:Label ID="lblImg1NameAct" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblDescripcionAct" runat="server" Text="Descripcion" CssClass="control-label required"></asp:Label>
                <asp:TextBox ID="txtDescripcionAct" runat="server" CssClass="form-control custom-input text-multiple" placeholder="Descripcion" TextMode="MultiLine" Rows="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_txtDescripcionAct" runat="server" ControlToValidate="txtDescripcionAct" CssClass="alert alert-danger form-control" ValidationGroup="formulario_Act" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblLinkAct" runat="server" Text="Link de descarga" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtLinkAct" runat="server" CssClass="form-control custom-input" placeholder="www.google.com" TextMode="Url"></asp:TextBox>
            </div>
        </div>
        <div class="text-center">
            <asp:Button ID="btnCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp; &nbsp;            
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" ValidationGroup="formulario_Act" CssClass="btn btn-success" OnClick="btnActualizar_Click" />
        </div>
    </div>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

