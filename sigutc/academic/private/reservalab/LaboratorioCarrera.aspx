<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="LaboratorioCarrera.aspx.cs" Inherits="academic_private_reservalab_LaboratorioCarrera" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
    Laboratorio exclusivo
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h5 id="nombreLboratorio" runat="server" class="text-center"></h5>
    <hr />
    <div class="container">
        <asp:Label ID="lblCarrera" runat="server" 
            Text="SELECCIONE UNA CARRERA" 
            CssClass="font-weight-bold" />
        <br />
        <br />

        <!-- Lista desplegable de carreras-->
        <asp:DropDownList ID="ddlCarreras" runat="server" 
            CssClass="form-control custom-input" />
        <br />

        <!-- Boton para agregar nuevos laboratorios-->
        <div class="text-center">
            <asp:Button ID="btnGuardar" runat="server" 
                Text="Agregar" 
                CssClass="btn btn-primary" 
                OnClick="btnGuardar_Click" />
        </div>  
    </div>
    <br />

    <!-- Muestra mensajes de la base de datos -->
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    
    <!-- Obtiene los codigod primarios de Sede y Facultad -->
    <asp:Label ID="lblFacultadId" runat="server" 
        Text="" 
        Visible="false" />
    <asp:Label ID="lblSedeId" runat="server" 
        Text="" 
        Visible="false" />

    <!-- Muestra una tabla con todos los registros -->
    <div class="table-responsive">
        <asp:GridView ID="gvCarreras" runat="server" 
            AutoGenerateColumns="False" 
            OnRowCommand="gvCarreras_RowCommand" 
            CssClass="table table-cielo table-hover tbl-buscador dataTable no-footer">
            <Columns>
                <asp:BoundField DataField="strNombre_Car" 
                    HeaderText="Nombre" />
                <asp:TemplateField ShowHeader="False" 
                    HeaderText="Accion" 
                    ItemStyle-CssClass="text-center" 
                    HeaderStyle-CssClass="text-center">
                    <ItemTemplate>
                        <!--Boton para eliminar el laboratorio-->
                        <asp:Button ID="btnDelete" runat="server" 
                            Text="Eliminar" 
                            CssClass="btn btn-danger" 
                            OnClientClick="return showAlertDelete(this);"
                            CommandName="Eliminar" 
                            CommandArgument ='<%# Eval("strCod_Car") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
    <!-- Importa archivos js -->
    <script src="../../../Scripts/reservalab/reservas_utilidades.js"></script>
</asp:Content>