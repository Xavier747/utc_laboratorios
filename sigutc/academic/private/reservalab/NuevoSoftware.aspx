<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="NuevoSoftware.aspx.cs" Inherits="academic_private_reservalab_NuevoSoftware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Nuevo software
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container-fluid">
        <div class="row" id="SedeFacultad" runat="server">
            <div class="col-md-6">
                <asp:Label ID="lblSede" runat="server" Text="Sedes" CssClass="control-label required"></asp:Label>
                <asp:DropDownList ID="ddlSede" runat="server" CssClass="form-control custom-input" AutoPostBack="true" OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_ddlListSedes" runat="server" ControlToValidate="ddlSede" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6">
                <asp:Label ID="lblFacultad" runat="server" Text="Facultades" CssClass="control-label required"></asp:Label>
                <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-control custom-input"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_DropDownListFacultades" runat="server" ControlToValidate="ddlFacultad" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" InitialValue="" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="control-label required"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control custom-input" placeholder="Nombre"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_txtNombre" runat="server" ControlToValidate="txtNombre" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6">
                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad" CssClass="control-label required"></asp:Label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control custom-input" placeholder="0" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvf_txtCantidad" runat="server" ControlToValidate="txtCantidad" CssClass="alert alert-danger form-control" ValidationGroup="formulario" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rv_Cantidad" runat="server" ControlToValidate="txtCantidad" MinimumValue="1" MaximumValue="50" Type="Integer" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="El número debe estar entre 1 y 50"></asp:RangeValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblTipo" runat="server" Text="Tipo de licencia" CssClass="control-label required"></asp:Label>
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control custom-input" AutoPostBack="true" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_selectTipo" runat="server" ControlToValidate="ddlTipo" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Seleccione una opción"></asp:RequiredFieldValidator>  
            </div>
            <div class="col-md-6">
                <div id="content_NombreLicencia" runat="server">
                    <asp:Label ID="lblNombreLicencia" runat="server" Text="Nombre de la licencia" CssClass="control-label required"></asp:Label>
                    <asp:TextBox ID="txtNombreLicencia" runat="server" CssClass="form-control custom-input text-multiple" TextMode="MultiLine" Rows="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvf_txtNombreLicencia" runat="server" ControlToValidate="txtNombreLicencia" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
                </div>                      
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblCosto" runat="server" Text="Costo" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control custom-input" placeholder="0.0"></asp:TextBox>
                <asp:RangeValidator ID="rv_txtCosto" runat="server" ControlToValidate="txtCosto" MinimumValue="0" MaximumValue="999999" Type="Double" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="El número debe ser mayor a 0" ></asp:RangeValidator>
            </div>                                
            <div class="col-md-6">
                <asp:Label ID="lblImg1" runat="server" Text="Fotografía 1" CssClass="control-label required"></asp:Label>
                <asp:FileUpload ID="fulImg1" runat="server" CausesValidation="true" CssClass="form-control" accept="image/*" data-show-upload="false"/>
                <asp:RequiredFieldValidator ID="rfv_img1" runat="server" ControlToValidate="fulImg1" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion" CssClass="control-label required"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control custom-input text-multiple" placeholder="Descripcion" TextMode="MultiLine" Rows="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_txtDescripcion" runat="server" ControlToValidate="txtDescripcion" CssClass="alert alert-danger form-control" ValidationGroup="formNuevoSoft" ErrorMessage="Campo requerido"></asp:RequiredFieldValidator>
            </div> 
            <div class="col-md-6">
                <asp:Label ID="lblLink" runat="server" Text="Link de descarga" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtLink" runat="server" CssClass="form-control custom-input" placeholder="www.google.com" TextMode="Url"></asp:TextBox>
            </div>                               
        </div>
        <div class="text-center">
            <asp:Button ID="btnCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />&nbsp; &nbsp;
            <asp:Button ID="btnGuardar" runat="server" Text="Enviar" ValidationGroup="formNuevoSoft" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        </div>
    </div>

    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

