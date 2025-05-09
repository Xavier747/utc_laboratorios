using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibraryTesis;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ClassLibraryLaboratorios;
using System.Web.Services;

public partial class academic_private_reservaLab_Software : System.Web.UI.Page
{
    //Llamado al metodo constructor
    LAB_SOFTWARE software1 = new LAB_SOFTWARE();
    UB_SEDES sede = new UB_SEDES();
    UB_FACULTADES facultad = new UB_FACULTADES();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Comprobar si el usuario se autentifico a treves del login
        if (Context.User.Identity.Name == null) Response.Redirect("~/academic/private/Login.aspx");

        if (!IsPostBack)
        {
            //Llamado a los metodos para que realicen la respectiva accion
            cargarSede();
            cargarFacultad();
            cargarSoftware();
        }
    }

    protected void ddlSedeSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiamos el ddl antes de agregar sus respectivos items
        ddlFacultadSoft.Items.Clear();
        cargarFacultad();
        cargarSoftware();
    }

    protected void ddlFacultadSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiamos la tabla de datos antes de enviar a cargar nuevamente
        gvSoftware.DataSource = null;
        cargarSoftware();
    }

    protected void gvSoftware_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Establecemos paginacion
        gvSoftware.PageIndex = e.NewPageIndex;
    }

    public void cargarSede()
    {
        //Conectamos con la libreria de clases
        //Realiza una consulta a la base de datos
        var listSede = sede.LoadUB_SEDES("ALL", "", "", "", "");

        //Comprueba si devolvieron registros
        if (listSede.Count != 0)
        {
            //ddlSede de listar Software
            ddlSedeSoft.DataSource = listSede;
            ddlSedeSoft.DataTextField = "strNombre_Sede";
            ddlSedeSoft.DataValueField = "strCod_Sede";
            ddlSedeSoft.DataBind();

            lblMsg.Text = sede.msg;
        }
        else
        {
            lblMsg.Text = sede.msg;
        }
    }

    public void cargarFacultad()
    {
        //almacenamos la clave primaria de Sede
        string strCod_Sede = ddlSedeSoft.SelectedValue;

        //Realizamos una consulta a la base de datos por las facultaqdes
        var listFacultad = facultad.LoadUB_FACULTADES("xPKSede", strCod_Sede, "", "", "");

        if (listFacultad.Count != 0)
        {
            ddlFacultadSoft.DataSource = listFacultad;
            ddlFacultadSoft.DataTextField = "strNombre_fac";
            ddlFacultadSoft.DataValueField = "strCod_fac";
            ddlFacultadSoft.DataBind();

            //muestra mensajes devuelto en la consulta
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }

    public void cargarSoftware()
    {
        string strCod_Fac = ddlFacultadSoft.SelectedValue;
        string strCod_Sede = ddlSedeSoft.SelectedValue;

        var listSoftware = software1.LoadLAB_SOFTWARE("xSedeFacultad", strCod_Fac, strCod_Sede, "", "");

        if (listSoftware.Count != 0)
        {
            gvSoftware.DataSource = listSoftware;
            gvSoftware.DataBind();
            lblMsg.Text = software1.msg;
        }
        else
        {
            lblMsg.Text = software1.msg;
        }
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        //Redirecciona a otro formulario
        Response.Redirect("~/academic/private/reservalab/NuevoSoftware.aspx");
    }

    protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Definimos dos condicion con una opciones de acuerdo al nombre del Comando
        if (e.CommandName == "Select")
        {
            //capturamos el id para transportar a otro formulario
            Session["codSoft"] = e.CommandArgument.ToString();
            Response.Redirect("~/academic/private/reservalab/ActualizarSoftware.aspx");
        }
        if (e.CommandName == "Eliminar")
        {
            string codSftware = e.CommandArgument.ToString();

            // Carga los detalles del laboratorio según el ID seleccionado 
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string responsable = Context.User.Identity.Name;
            software1.DelLAB_SOFTWARE("xCodSof", codSftware, fecha, responsable, "");

            //Muestra mensajes de acuerdo a la respuesta del servidor
            string title = software1.resultado ? software1.msg : software1.msg;
            string icon = software1.resultado ? "success" : "error";

            string script = $"showAlertAndReload('{title}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }

    protected void btnVistaCompleta_Click(object sender, EventArgs e)
    {
        // Se obtiene el botón que disparó el evento.
        Button btn = (Button)sender;

        // Se extrae la ruta de la imagen desde el atributo CommandArgument del botón.
        string rutaImagen = btn.CommandArgument;

        // Se asigna la ruta obtenida como la URL de la imagen que se mostrará en el visor (Image control).
        vistaCompletaImagen.ImageUrl = rutaImagen;

        // Se utiliza ScriptManager para ejecutar un script de JavaScript que abre un modal (ventana emergente) con jQuery.
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "$('#view-image').modal('show');", true);
    }
}
